using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Model.Models.Mapping
{
    public class BatQuaiMap : EntityTypeConfiguration<BatQuai>
    {
        public BatQuaiMap()
        {
            // Primary Key
            this.HasKey(t => t.MaBatQuai);

            // Properties
            this.Property(t => t.MaBatQuai)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.TuTrach)
                .HasMaxLength(50);

            this.Property(t => t.TenBatQuai)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("BatQuai");
            this.Property(t => t.MaBatQuai).HasColumnName("MaBatQuai");
            this.Property(t => t.TuTrach).HasColumnName("TuTrach");
            this.Property(t => t.TenBatQuai).HasColumnName("TenBatQuai");
        }
    }
}
