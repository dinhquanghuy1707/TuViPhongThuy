using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MVC.Models.Mapping
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

            this.Property(t => t.TenNguoiDang)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("TaiLieu");
            this.Property(t => t.MaTaiLieu).HasColumnName("MaTaiLieu");
            this.Property(t => t.ChuDe).HasColumnName("ChuDe");
            this.Property(t => t.NoiDung).HasColumnName("NoiDung");
            this.Property(t => t.NgayDang).HasColumnName("NgayDang");
            this.Property(t => t.TenNguoiDang).HasColumnName("TenNguoiDang");
            this.Property(t => t.LinkAnh).HasColumnName("LinkAnh");
            this.Property(t => t.TieuDe).HasColumnName("TieuDe");
        }
    }
}
