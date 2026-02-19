using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DeStaProduction.Infrastucture.Entities
{
    public class ApplicationDbContext : IdentityDbContext<DeStaUser, IdentityRole<Guid>, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
             : base(options)
        {

        }

        public DbSet<Event> Events { get; set; } 
        public DbSet<EventType> EventTypes { get; set; }
        public DbSet<Location> Locations { get; set; } 
        public DbSet<Performance> Performances { get; set; } 
        public DbSet<Schedule> Schedules { get; set; } 
    }
}
