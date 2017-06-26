using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Model.Models.Mapping
{
    public class TaiLieuMap : EntityTypeConfiguration<TaiLieu>
    {
        public TaiLieuMap()
        {
            // Primary Key
            this.HasKey(t => t.MaTaiLieu);

            // Properties
            this.Property(t => t.MaTaiLieu)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ChuDe)
                .IsRequired()
                .HasMaxLength(250);

            this.Property(t => t.NoiDung)
                .IsRequired();

            this.Property(t => t.TenNguoiDang)
                .HasMaxLength(250);

            // Table & Column Mappings
            this.ToTable("TaiLieus");
            this.Property(t => t.MaTaiLieu).HasColumnName("MaTaiLieu");
            this.Property(t => t.ChuDe).HasColumnName("ChuDe");
            this.Property(t => t.NoiDung).HasColumnName("NoiDung");
            this.Property(t => t.NgayDang).HasColumnName("NgayDang");
            this.Property(t => t.TenNguoiDang).HasColumnName("TenNguoiDang");
        }
    }
}
