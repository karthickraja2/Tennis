using System.ComponentModel.DataAnnotations;

namespace Tennis_Court_Booking_API.Models.DTO
{
    public class BookingCreateDto
    {

        [Required]
        public decimal BookingFee { get; set; }
        [Required]
        public bool IsPaid { get; set; }
        public string? BookingNotes { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int TennisCourtId { get; set; }
        [Required]
        public int NoOfStayDays { get; set; }
    }
}
