using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MVC.Models.Mapping
{
    public class TinhTuMap : EntityTypeConfiguration<TinhTu>
    {
        public TinhTuMap()
        {
            // Primary Key
            this.HasKey(t => t.MaTinhTu);

            // Properties
            this.Property(t => t.MaTinhTu)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.TenTinhTu)
                .HasMaxLength(50);

            this.Property(t => t.LoaiTinhTu)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("TinhTu");
            this.Property(t => t.MaTinhTu).HasColumnName("MaTinhTu");
            this.Property(t => t.TenTinhTu).HasColumnName("TenTinhTu");
            this.Property(t => t.LoaiTinhTu).HasColumnName("LoaiTinhTu");
            this.Property(t => t.GiaiThich).HasColumnName("GiaiThich");
        }
    }
}
