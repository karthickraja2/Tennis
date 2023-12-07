using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Tennis_Court_Booking_API.Data;
using Tennis_Court_Booking_API.Models;
using Tennis_Court_Booking_API.Models.DTO;
using Tennis_Court_Booking_API.Repository.IRepository;

namespace Tennis_Court_Booking_API.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly TennisDbContext _dbContext;
        private string secretKey;
        public UserRepository(TennisDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            secretKey = configuration.GetValue<string>("ApiSettings:Secret");
        }
        public bool IsUniqueUser(string username)
        {
            var user = _dbContext.Users.FirstOrDefault(x => x.UserName == username);
            if (user == null)
            {
                return true;
            }
            return false;
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var user = _dbContext.Users.FirstOrDefault(x => x.UserName.ToLower() == loginRequestDto.UserName.ToLower()
            && x.Password == loginRequestDto.Password);
            if (user == null)
            {
                return new LoginResponseDto()
                {
                    Token = "",
                    User = null
                };
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            LoginResponseDto loginResponseDto = new LoginResponseDto()
            {
                Token = tokenHandler.WriteToken(token), 
                User = user
            };
            return loginResponseDto;
        }

        public async Task<User> Register(RegistrationRequestDto registrationRequestDto)
        {
            User user = new User()
            {
                UserName = registrationRequestDto.UserName,
                Name = registrationRequestDto.Name,
                Address = registrationRequestDto.Address,
                PhoneNumber = registrationRequestDto.PhoneNumber,
                Email = registrationRequestDto.Email,
                Password = registrationRequestDto.Password

            };

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
            user.Password = "";
            return user;
        }
    }
}
