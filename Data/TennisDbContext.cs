using Microsoft.EntityFrameworkCore;
using Tennis_Court_Booking_API.Models;

namespace Tennis_Court_Booking_API.Data
{
    public class TennisDbContext : DbContext
    {
        public TennisDbContext(DbContextOptions<TennisDbContext> options)
            : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<TennisCourt> TennisCourts { get; set; }

    }
}
