using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParserPlanBastard.Models.Entities;


namespace ParserPlanBastard.Configurations
{
    public class FileEntityTypeConfiguration : IEntityTypeConfiguration<Models.Entities.File>
    {

        public void Configure(EntityTypeBuilder<Models.Entities.File> builder)
        {
            builder.HasKey(file => file.Id)
                .HasName("PK_Files_Id");

            builder.Property(file => file.Hash)
                .HasMaxLength(256)
                .HasColumnType("nvarchar");

            builder.Property(file => file.FilePath)
                .IsRequired()
                .HasMaxLength(256)
                .HasColumnType("nvarchar");

            builder.Property(file => file.FileName)
                .IsRequired()
                .HasMaxLength(256)
                .HasColumnType("nvarchar");

            builder.Property(file => file.FileExtension)
                .IsRequired()
                .HasMaxLength(256)
                .HasColumnType("nvarchar");

            builder.Property(file => file.VolumeFile)
               .IsRequired()
               .HasMaxLength(256)
               .HasColumnType("bigint");

            builder.HasOne(file => file.User)
                .WithMany(user => user.Files)
                .HasForeignKey(file => file.UserId)
                .HasConstraintName("FK_Files_UserId_User_Id")
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
