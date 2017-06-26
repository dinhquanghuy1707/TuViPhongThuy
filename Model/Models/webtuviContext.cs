using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Model.Models.Mapping;

namespace Model.Models
{
    public partial class webtuviContext : DbContext
    {
        static webtuviContext()
        {
            Database.SetInitializer<webtuviContext>(null);
        }

        public webtuviContext()
            : base("Name=webtuviContext")
        {
        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<BatQuai> BatQuais { get; set; }
        public DbSet<NguHanh> NguHanhs { get; set; }
        public DbSet<TaiLieu> TaiLieus { get; set; }
        public DbSet<TinhTu> TinhTus { get; set; }
        public DbSet<TinhTuTheoBaiQuai> TinhTuTheoBaiQuais { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AdminMap());
            modelBuilder.Configurations.Add(new BatQuaiMap());
            modelBuilder.Configurations.Add(new NguHanhMap());
            modelBuilder.Configurations.Add(new TaiLieuMap());
            modelBuilder.Configurations.Add(new TinhTuMap());
            modelBuilder.Configurations.Add(new TinhTuTheoBaiQuaiMap());
        }
    }
}
