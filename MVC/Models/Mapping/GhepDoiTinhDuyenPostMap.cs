using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MVC.Models.Mapping
{
    public class GhepDoiTinhDuyenPostMap : EntityTypeConfiguration<GhepDoiTinhDuyenPost>
    {
        public GhepDoiTinhDuyenPostMap()
        {
            // Primary Key
            this.HasKey(t => t.IdPost);

            // Properties
            this.Property(t => t.IdPost)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.IdFB)
                .HasMaxLength(50);

            this.Property(t => t.Birthday)
                .HasMaxLength(50);

            this.Property(t => t.Image)
                .HasMaxLength(100);

            this.Property(t => t.Name)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("GhepDoiTinhDuyenPost");
            this.Property(t => t.IdPost).HasColumnName("IdPost");
            this.Property(t => t.IdFB).HasColumnName("IdFB");
            this.Property(t => t.Birthday).HasColumnName("Birthday");
            this.Property(t => t.Gender).HasColumnName("Gender");
            this.Property(t => t.Image).HasColumnName("Image");
            this.Property(t => t.Name).HasColumnName("Name");
        }
    }
}
