using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MVC.Models.Mapping
{
    public class GhepDoiTinhDuyenResultMap : EntityTypeConfiguration<GhepDoiTinhDuyenResult>
    {
        public GhepDoiTinhDuyenResultMap()
        {
            // Primary Key
            this.HasKey(t => t.IdResult);

            // Properties
            this.Property(t => t.IdResult)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.IdFb)
                .HasMaxLength(50);

            this.Property(t => t.Birthday)
                .HasMaxLength(50);

            this.Property(t => t.Image)
                .HasMaxLength(50);

            this.Property(t => t.Name)
                .HasMaxLength(100);

            this.Property(t => t.IdPost)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("GhepDoiTinhDuyenResult");
            this.Property(t => t.IdResult).HasColumnName("IdResult");
            this.Property(t => t.IdFb).HasColumnName("IdFb");
            this.Property(t => t.Birthday).HasColumnName("Birthday");
            this.Property(t => t.Gender).HasColumnName("Gender");
            this.Property(t => t.Image).HasColumnName("Image");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.IdPost).HasColumnName("IdPost");
        }
    }
}
