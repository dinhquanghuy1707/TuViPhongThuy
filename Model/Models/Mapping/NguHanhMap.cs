using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Model.Models.Mapping
{
    public class NguHanhMap : EntityTypeConfiguration<NguHanh>
    {
        public NguHanhMap()
        {
            // Primary Key
            this.HasKey(t => t.NamDL);

            // Properties
            this.Property(t => t.NamDL)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.NamAL)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.GiaiNghia)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.BatQuaiNam)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.BatQuaiNu)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.NguHanhNam)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.NguHanhNu)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("NguHanhs");
            this.Property(t => t.NamDL).HasColumnName("NamDL");
            this.Property(t => t.NamAL).HasColumnName("NamAL");
            this.Property(t => t.GiaiNghia).HasColumnName("GiaiNghia");
            this.Property(t => t.BatQuaiNam).HasColumnName("BatQuaiNam");
            this.Property(t => t.BatQuaiNu).HasColumnName("BatQuaiNu");
            this.Property(t => t.NguHanhNam).HasColumnName("NguHanhNam");
            this.Property(t => t.NguHanhNu).HasColumnName("NguHanhNu");
        }
    }
}
