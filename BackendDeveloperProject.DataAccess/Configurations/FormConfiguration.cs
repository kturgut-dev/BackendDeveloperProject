using BackendDeveloperProject.Core.DataAccess.EntityFramework.Configurations;
using BackendDeveloperProject.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackendDeveloperProject.DataAccess.Configurations
{
    public class FormConfiguration : BaseConfiguration<Form>
    {
        public override void Configure(EntityTypeBuilder<Form> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Name)
                .HasColumnName("Name")
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(x => x.Description)
                .HasColumnName("Description")
                .HasMaxLength(500);

            // fields to json
            builder.Property(x => x.Fields)
                .HasColumnName("Fields")
                .HasConversion(
                    v => Newtonsoft.Json.JsonConvert.SerializeObject(v),
                    v => Newtonsoft.Json.JsonConvert.DeserializeObject<List<FormField>>(v)
                )
                .IsRequired();
        }
    }
}
