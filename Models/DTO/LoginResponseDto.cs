namespace Tennis_Court_Booking_API.Models.DTO
{
    public class LoginResponseDto
    {
        public User? User { get; set; }
        public string? Token { get; set; }
    }
}
