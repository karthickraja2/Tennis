using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tennis_Court_Booking_API.Models
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }
        public DateTime BookingDateTime { get; set; } = DateTime.Now;
        [Required]
        public decimal BookingFee { get; set; }
        [Required]
        public bool IsPaid { get; set; }
        public string? BookingNotes { get; set; }
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public User? User { get; set; }

        [ForeignKey(nameof(TennisCourt))]
        public int TennisCourtId { get; set; }
        public TennisCourt? TennisCourt { get; set; }
        public string? Status { get; set; }

        [Required]
        public int NoOfStayDays { get; set; }

    }
}
