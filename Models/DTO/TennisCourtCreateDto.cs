using System.ComponentModel.DataAnnotations;

namespace Tennis_Court_Booking_API.Models.DTO
{
    public class TennisCourtCreateDto
    {
        [Required]
        [MaxLength(30)]
        public string? Name { get; set; }
        [Required]
        public string? Location { get; set; }
        [Required]
        public String? Type { get; set; }
        [Required]
        public bool IsIndoor { get; set; }
        [Required]
        public int MaxCapacity { get; set; }
        [Required]
        public decimal HourlyRate { get; set; }
        public string? Description { get; set; }
        [Required]
        public bool IsAvailable { get; set; }
       /* public string? ImageUrl { get; set; }*/
    }
}
