using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace ErpDemoEF.Models
{
    public partial class ErpDemoContext : DbContext
    {
        public ErpDemoContext()
        {
        }

        public ErpDemoContext(DbContextOptions<ErpDemoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Articoli> Articoli { get; set; }
        public virtual DbSet<Clienti> Clienti { get; set; }
        public virtual DbSet<Utenti> Utenti { get; set; }
        public virtual DbSet<vwClienti> vwClienti { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=VINB22;Database=Erp_Demo;User ID=sa;Password=sa");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Articoli>(entity =>
            {
                entity.Property(e => e.Descrizione)
                    .HasMaxLength(256)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Clienti>(entity =>
            {
                entity.Property(e => e.Citta)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Indirizzo)
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.RagioneSociale)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Settore)
                    .HasMaxLength(8)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Utenti>(entity =>
            {
                entity.HasKey(e => e.username);

                entity.Property(e => e.username)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.password)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<vwClienti>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwClienti");

                entity.Property(e => e.RIGA1)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.RIGA2)
                    .HasMaxLength(202)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
