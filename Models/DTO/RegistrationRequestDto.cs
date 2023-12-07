namespace Tennis_Court_Booking_API.Models.DTO
{
    public class RegistrationRequestDto
    {
        public string? UserName { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }

        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }

        public string? Password { get; set; }
    }
    internal class Response
    {
        public string Message { get; set; }
        public string Status { get; set; }
    }
}
