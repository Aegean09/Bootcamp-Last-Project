using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace _01_initial.Models
{
    public class EgeDbContext : DbContext
    {
        public DbSet<Events> Events { get; set; }
        public DbSet<Users> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=EgeDB;Trusted_Connection=True");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
