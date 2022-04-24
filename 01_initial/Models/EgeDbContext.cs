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
            //modelBuilder.Entity<Users>()
            //    .HasIndex(a=>a.EMail)
            //    .IsUnique(true);

            //modelBuilder.Entity<Events>().HasKey(a => a.EventId);
            //modelBuilder.Entity<Users>().HasKey(a => a.UserId);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
