using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MVC.Models.Mapping
{
    public class NguHanhMap : EntityTypeConfiguration<NguHanh>
    {
        public NguHanhMap()
        {
            // Primary Key
            this.HasKey(t => t.namDL);

            // Properties
            this.Property(t => t.namDL)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.namAL)
                .HasMaxLength(50);

            this.Property(t => t.CungNam)
                .HasMaxLength(50);

            this.Property(t => t.NienMenhNam)
                .HasMaxLength(50);

            this.Property(t => t.CungNu)
                .HasMaxLength(50);

            this.Property(t => t.NienMenhNu)
                .HasMaxLength(50);

            this.Property(t => t.NguHanhNamSinh)
                .HasMaxLength(50);

            this.Property(t => t.TenNguHanh)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("NguHanh");
            this.Property(t => t.namDL).HasColumnName("namDL");
            this.Property(t => t.namAL).HasColumnName("namAL");
            this.Property(t => t.GiaiNghia).HasColumnName("GiaiNghia");
            this.Property(t => t.CungNam).HasColumnName("CungNam");
            this.Property(t => t.NienMenhNam).HasColumnName("NienMenhNam");
            this.Property(t => t.CungNu).HasColumnName("CungNu");
            this.Property(t => t.NienMenhNu).HasColumnName("NienMenhNu");
            this.Property(t => t.NguHanhNamSinh).HasColumnName("NguHanhNamSinh");
            this.Property(t => t.TenNguHanh).HasColumnName("TenNguHanh");
        }
    }
}
