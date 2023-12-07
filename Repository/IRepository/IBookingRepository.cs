using Tennis_Court_Booking_API.Models;

namespace Tennis_Court_Booking_API.Repository.IRepository
{
    public interface IBookingRepository : IRepository<Booking>
    {
        Task<Booking> UpdateAsync(Booking booking);
    }
}
