using Tennis_Court_Booking_API.Data;
using Tennis_Court_Booking_API.Models;
using Tennis_Court_Booking_API.Repository.IRepository;

namespace Tennis_Court_Booking_API.Repository
{
    public class TennisCourtRepository : Repository<TennisCourt>, ITennisCourtRepository
    {

        private readonly TennisDbContext _dbContext;
        public TennisCourtRepository(TennisDbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }
       
        public async Task<TennisCourt> UpdateAsync(TennisCourt tennisCourt)
        {
            _dbContext.TennisCourts.Update(tennisCourt);
            await _dbContext.SaveChangesAsync();
            return tennisCourt;
        }
    }
}
