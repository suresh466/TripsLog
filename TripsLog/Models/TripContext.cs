using Microsoft.EntityFrameworkCore;

namespace TripsLog.Models
{
    // DbContext class for the FAQs database
    public class TripContext : DbContext
    {
        // Constructor to pass default options to the base class
        public TripContext(DbContextOptions<TripContext> options)
            : base(options)
        { }
        // DbSet properties for the Trip models so CRUD operations can be performed
        public DbSet<Trip> Trips { get; set; }
    }
}
