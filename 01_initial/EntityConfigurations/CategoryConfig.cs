using _01_initial.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace _01_initial.EntityConfigurations
{
    public class CategoryConfig: IEntityTypeConfiguration<Categories>
    {
        public void Configure(EntityTypeBuilder<Categories> builder)
        {
            builder.HasKey(a=>a.Category_Name);
            //builder.HasKey(x => x.Category_Id);     
        }

    }
}