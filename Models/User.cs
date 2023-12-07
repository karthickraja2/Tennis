using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Tennis_Court_Booking_API.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? UserName { get; set; }

        [Required]
        [MaxLength(30)]
        public string? Name { get; set; }

        [Required]
        public string? Address { get; set; }

        [Required]
        [Phone]
        public string? PhoneNumber { get; set; }
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [PasswordPropertyText]
        public string? Password { get; set; }

        //public ICollection<Booking>? Bookings { get; set; }

        
    }
}
