using Tennis_Court_Booking_API.Models;

namespace Tennis_Court_Booking_API.Repository.IRepository
{
    public interface ITennisCourtRepository : IRepository<TennisCourt>
    {   
        Task<TennisCourt> UpdateAsync(TennisCourt tennisCourt);
    }
}
