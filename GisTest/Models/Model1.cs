namespace GisTest.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class GisData : DbContext
    {
        public GisData()
            : base("name=GisData")
        {
        }

        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<ThongTinDoiTuongChinh> ThongTinDoiTuongChinhs { get; set; }
        public virtual DbSet<ThongTinDoiTuongPhu> ThongTinDoiTuongPhus { get; set; }
        public virtual DbSet<ThongTinLatLngDoiTuong> ThongTinLatLngDoiTuongs { get; set; }
        public virtual DbSet<ThongTinVeDoiTuong> ThongTinVeDoiTuongs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>()
                .Property(e => e.NameKhongDau)
                .IsUnicode(false);

            modelBuilder.Entity<ThongTinDoiTuongChinh>()
                .Property(e => e.Id)
                .IsUnicode(false);

            modelBuilder.Entity<ThongTinDoiTuongChinh>()
                .Property(e => e.LopId)
                .IsUnicode(false);

            modelBuilder.Entity<ThongTinDoiTuongChinh>()
                .Property(e => e.CauHinhDoiTuongId)
                .IsUnicode(false);

            modelBuilder.Entity<ThongTinDoiTuongChinh>()
                .Property(e => e.PhanKhuId)
                .IsUnicode(false);

            modelBuilder.Entity<ThongTinDoiTuongChinh>()
                .Property(e => e.Tag)
                .IsUnicode(false);

            modelBuilder.Entity<ThongTinDoiTuongChinh>()
                .HasMany(e => e.ThongTinDoiTuongPhus)
                .WithRequired(e => e.ThongTinDoiTuongChinh)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ThongTinDoiTuongChinh>()
                .HasMany(e => e.ThongTinVeDoiTuongs)
                .WithRequired(e => e.ThongTinDoiTuongChinh)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ThongTinDoiTuongPhu>()
                .Property(e => e.Id)
                .IsUnicode(false);

            modelBuilder.Entity<ThongTinDoiTuongPhu>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<ThongTinDoiTuongPhu>()
                .Property(e => e.ThongTinDoiTuongChinhId)
                .IsUnicode(false);

            modelBuilder.Entity<ThongTinDoiTuongPhu>()
                .Property(e => e.KieuDuLieu)
                .IsUnicode(false);

            modelBuilder.Entity<ThongTinLatLngDoiTuong>()
                .Property(e => e.Id)
                .IsUnicode(false);

            modelBuilder.Entity<ThongTinLatLngDoiTuong>()
                .Property(e => e.MaxLat)
                .IsUnicode(false);

            modelBuilder.Entity<ThongTinLatLngDoiTuong>()
                .Property(e => e.MinLat)
                .IsUnicode(false);

            modelBuilder.Entity<ThongTinLatLngDoiTuong>()
                .Property(e => e.MaxLng)
                .IsUnicode(false);

            modelBuilder.Entity<ThongTinLatLngDoiTuong>()
                .Property(e => e.MinLng)
                .IsUnicode(false);

            modelBuilder.Entity<ThongTinVeDoiTuong>()
                .Property(e => e.Id)
                .IsUnicode(false);

            modelBuilder.Entity<ThongTinVeDoiTuong>()
                .Property(e => e.ThongTinDoiTuongChinhId)
                .IsUnicode(false);
        }
    }
}
