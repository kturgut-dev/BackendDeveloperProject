using BackendDeveloperProject.Core.DataAccess.EntityFramework.Configurations;
using BackendDeveloperProject.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using System.Net;

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

            //builder.OwnsMany(c => c.Fields, ownedType =>
            //{
            //    ownedType.Property(d => d.Name)
            //        .HasColumnName("Name")
            //        .HasMaxLength(250)
            //        .IsRequired();

            //    ownedType.Property(d => d.DataType)
            //        .HasColumnName("DataType")
            //        .HasMaxLength(50)
            //        .IsRequired();

            //    ownedType.Property(d => d.Required)
            //        .HasColumnName("Required")
            //        .HasDefaultValue(false);

            //    ownedType.ToJson();
            //}).HasNoKey();

            builder.Property(x => x.Fields)
                .HasJsonPropertyName("Fields")
                .HasConversion(
                    v => JsonConvert.SerializeObject(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }),
                    v => JsonConvert.DeserializeObject<FormField[]>(v, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore })
                );
        }
    }
}
