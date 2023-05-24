using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParserPlanBastard.Models.Entities;

namespace ParserPlanBastard.Configurations
{
    public class ElementEntityTypeConfiguration : IEntityTypeConfiguration<Element>
    {
        public void Configure(EntityTypeBuilder<Element> builder)
        {
            builder.HasKey(element => element.Id)
                .HasName("PK_Elements_Id");

            builder.Property(element => element.Name)
                .IsRequired()
                .HasColumnType("nvarchar");

            builder.Property(element => element.Description)
                .IsRequired()
                .HasColumnType("nvarchar");

            builder.HasOne(element => element.File)
                .WithMany(file => file.Elements)
                .HasForeignKey(element => element.FileId)
                .HasConstraintName("FK_Elements_FileId_Files_Id")
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
