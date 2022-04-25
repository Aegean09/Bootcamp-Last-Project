using _01_initial.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace _01_initial.EntityConfigurations
{
    public class DeletedEventsConfig : IEntityTypeConfiguration<DeletedEvents>
    {
        public void Configure(EntityTypeBuilder<DeletedEvents> builder)
        {
            builder.HasKey(x => x.Del_Id);
        }

    }
}
