using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Model.Models.Mapping
{
    public class TinhTuTheoBaiQuaiMap : EntityTypeConfiguration<TinhTuTheoBaiQuai>
    {
        public TinhTuTheoBaiQuaiMap()
        {
            // Primary Key
            this.HasKey(t => t.MaBatQuai);

            // Properties
            this.Property(t => t.MaBatQuai)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Huong)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("TinhTuTheoBaiQuais");
            this.Property(t => t.MaBatQuai).HasColumnName("MaBatQuai");
            this.Property(t => t.Huong).HasColumnName("Huong");
            this.Property(t => t.MaTinhTu).HasColumnName("MaTinhTu");
        }
    }
}
