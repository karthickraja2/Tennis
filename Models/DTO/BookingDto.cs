namespace Tennis_Court_Booking_API.Models.DTO
{
    public class BookingDto
    {
        public int Id { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime UpdatedTimeTime { get; set; }
        public decimal BookingFee { get; set; }
        public bool IsPaid { get; set; }
        public string? BookingNotes { get; set; }
        public int UserId { get; set; }
        public int TennisCourtId { get; set; }
        public string? Status { get; set; }
        public int NoOfStayDays { get; set; }
    }
}
