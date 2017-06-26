using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace MVC.Models.Mapping
{
    public class AdminMap : EntityTypeConfiguration<Admin>
    {
        public AdminMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Password)
                .HasMaxLength(50);

            this.Property(t => t.HoTen)
                .HasMaxLength(100);

            this.Property(t => t.Email)
                .HasMaxLength(150);

            this.Property(t => t.Phone)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("Admin");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Password).HasColumnName("Password");
            this.Property(t => t.HoTen).HasColumnName("HoTen");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.Phone).HasColumnName("Phone");
        }
    }
}
