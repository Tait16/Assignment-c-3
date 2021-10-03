using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ASSIGNMENT.Models;

#nullable disable

namespace ASSIGNMENT.Context
{
    public partial class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DanhBa> DanhBas { get; set; }
        public virtual DbSet<Nguoi> Nguois { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=LAPTOP-3J3MP96N\\SQLEXPRESS;Initial Catalog=ASSIGNMENT;User ID=dungna29;Password=123");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<DanhBa>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Email).IsUnicode(false);

                entity.Property(e => e.Sdt1).IsUnicode(false);

                entity.Property(e => e.Sdt2).IsUnicode(false);

                entity.HasOne(d => d.IdNguoiNavigation)
                    .WithMany(p => p.DanhBas)
                    .HasForeignKey(d => d.IdNguoi)
                    .HasConstraintName("FK__DanhBa__IdNguoi__38996AB5");
            });

            modelBuilder.Entity<Nguoi>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
