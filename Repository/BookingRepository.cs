using Tennis_Court_Booking_API.Data;
using Tennis_Court_Booking_API.Models;
using Tennis_Court_Booking_API.Repository.IRepository;

namespace Tennis_Court_Booking_API.Repository
{
    public class BookingRepository : Repository<Booking>, IBookingRepository
    {

        private readonly TennisDbContext _dbContext;
        public BookingRepository(TennisDbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Booking> UpdateAsync(Booking booking)
        {
           
            _dbContext.Bookings.Update(booking);
            await _dbContext.SaveChangesAsync();
            return booking;
        }
    }
}
