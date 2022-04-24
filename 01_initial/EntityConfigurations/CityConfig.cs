using _01_initial.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace _01_initial.EntityConfigurations
{
    public class CityConfig : IEntityTypeConfiguration<Cities>
    {
        public void Configure(EntityTypeBuilder<Cities> builder)
        {
            builder.HasKey(x => x.City_Id);
        }

    }
}
