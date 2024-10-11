using EventRegistrationSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace EventRegistrationSystem.Data
{
    public class EventSystemDbContext : DbContext
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<Registration> Registrations { get; set; }
        public EventSystemDbContext(DbContextOptions<EventSystemDbContext> options) : base(options)
        {
        }
    }
}
