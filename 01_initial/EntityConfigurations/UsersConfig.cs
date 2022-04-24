using _01_initial.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace _01_initial.EntityConfigurations
{
    public class UsersConfig : IEntityTypeConfiguration<Users>
    {
        public void Configure(EntityTypeBuilder<Users> builder)
        {
            //modelBuilder.Entity<Users>()
            //    .HasIndex(a=>a.EMail)
            //    .IsUnique(true);
            builder.Property(a=> a.EMail).IsRequired();
            builder.HasIndex(a => a.EMail).IsUnique(true);
            builder.HasKey(x => x.UserId);
        }
        
    }
}
