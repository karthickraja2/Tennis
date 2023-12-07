using Tennis_Court_Booking_API.Models;
using Tennis_Court_Booking_API.Models.DTO;

namespace Tennis_Court_Booking_API.Repository.IRepository
{
    public interface IUserRepository
    {
        bool IsUniqueUser(string username);
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
        Task<User> Register(RegistrationRequestDto registrationRequestDto);
    }
}
