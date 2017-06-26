using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MVC.Models.Mapping
{
    public class TinhTuTheoBatQuaiMap : EntityTypeConfiguration<TinhTuTheoBatQuai>
    {
        public TinhTuTheoBatQuaiMap()
        {
            // Primary Key
            this.HasKey(t => new { t.MaBatQuai, t.Huong });

            // Properties
            this.Property(t => t.MaBatQuai)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Huong)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("TinhTuTheoBatQuai");
            this.Property(t => t.MaBatQuai).HasColumnName("MaBatQuai");
            this.Property(t => t.Huong).HasColumnName("Huong");
            this.Property(t => t.MaTinhTu).HasColumnName("MaTinhTu");
        }
    }
}
