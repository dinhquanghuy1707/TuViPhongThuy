using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MVC.Models.Mapping
{
    public class BatQuai_TuTrachMap : EntityTypeConfiguration<BatQuai_TuTrach>
    {
        public BatQuai_TuTrachMap()
        {
            // Primary Key
            this.HasKey(t => t.MaBatQuai);

            // Properties
            this.Property(t => t.MaBatQuai)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.TuTrach)
                .HasMaxLength(50);

            this.Property(t => t.Cung)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("BatQuai-TuTrach");
            this.Property(t => t.MaBatQuai).HasColumnName("MaBatQuai");
            this.Property(t => t.TuTrach).HasColumnName("TuTrach");
            this.Property(t => t.Cung).HasColumnName("Cung");
        }
    }
}
