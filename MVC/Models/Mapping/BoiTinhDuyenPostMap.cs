using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MVC.Models.Mapping
{
    public class BoiTinhDuyenPostMap : EntityTypeConfiguration<BoiTinhDuyenPost>
    {
        public BoiTinhDuyenPostMap()
        {
            // Primary Key
            this.HasKey(t => t.IdPost);

            // Properties
            this.Property(t => t.IdPost)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Gender1)
                .HasMaxLength(50);

            this.Property(t => t.Gender2)
                .HasMaxLength(50);

            this.Property(t => t.Dob1)
                .HasMaxLength(50);

            this.Property(t => t.Dob2)
                .HasMaxLength(50);

            this.Property(t => t.Image)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("BoiTinhDuyenPost");
            this.Property(t => t.IdPost).HasColumnName("IdPost");
            this.Property(t => t.Gender1).HasColumnName("Gender1");
            this.Property(t => t.Gender2).HasColumnName("Gender2");
            this.Property(t => t.Dob1).HasColumnName("Dob1");
            this.Property(t => t.Dob2).HasColumnName("Dob2");
            this.Property(t => t.Image).HasColumnName("Image");
        }
    }
}
