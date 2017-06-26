using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using MVC.Models.Mapping;

namespace MVC.Models
{
    public partial class TuViPhongThuyContext : DbContext
    {
        static TuViPhongThuyContext()
        {
            Database.SetInitializer<TuViPhongThuyContext>(null);
        }

        public TuViPhongThuyContext()
            : base("Name=TuViPhongThuyContext")
        {
        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<BatQuai_TuTrach> BatQuai_TuTrach { get; set; }
        public DbSet<GhepDoiTinhDuyenPost> GhepDoiTinhDuyenPosts { get; set; }
        public DbSet<GhepDoiTinhDuyenResult> GhepDoiTinhDuyenResults { get; set; }
        public DbSet<NguHanh> NguHanhs { get; set; }
        public DbSet<sysdiagram> sysdiagrams { get; set; }
        public DbSet<TaiLieu> TaiLieux { get; set; }
        public DbSet<TinhTu> TinhTus { get; set; }
        public DbSet<TinhTuTheoBatQuai> TinhTuTheoBatQuais { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AdminMap());
            modelBuilder.Configurations.Add(new BatQuai_TuTrachMap());
            modelBuilder.Configurations.Add(new GhepDoiTinhDuyenPostMap());
            modelBuilder.Configurations.Add(new GhepDoiTinhDuyenResultMap());
            modelBuilder.Configurations.Add(new NguHanhMap());
            modelBuilder.Configurations.Add(new sysdiagramMap());
            modelBuilder.Configurations.Add(new TaiLieuMap());
            modelBuilder.Configurations.Add(new TinhTuMap());
            modelBuilder.Configurations.Add(new TinhTuTheoBatQuaiMap());
        }
    }
}
