using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SamuraiModel
{
    // To scaffold entity classes from existing database, use following command in Package Manager Console
    // Scaffold-DbContext -Connection "Server=(localdb)\MSSQLLocalDB;Database=SamuraiAppData;Trusted_Connection=True;MultipleActiveResultSets=true;AttachDBFilename=E:\sato\MSSQLLocalDB\EntityFrameworkCore2GettingStarted\SamuraiAppData.mdf" -Provider Microsoft.EntityFrameworkCore.SqlServer -Project SamuraiModel -StartupProject SomeUI
    public partial class SamuraiAppDataContext : DbContext
    {
        public virtual DbSet<Battle> Battle { get; set; }
        public virtual DbSet<Quote> Quote { get; set; }
        public virtual DbSet<Samurai> Samurai { get; set; }
        public virtual DbSet<SamuraiBattle> SamuraiBattle { get; set; }
        public virtual DbSet<SecretIdentity> SecretIdentity { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=SamuraiAppData;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Quote>(entity =>
            {
                entity.HasIndex(e => e.SamuraiId);

                entity.HasOne(d => d.Samurai)
                    .WithMany(p => p.Quote)
                    .HasForeignKey(d => d.SamuraiId);
            });

            modelBuilder.Entity<SamuraiBattle>(entity =>
            {
                entity.HasKey(e => new { e.SamuraiId, e.BattleId });

                entity.HasIndex(e => e.BattleId);

                entity.HasOne(d => d.Battle)
                    .WithMany(p => p.SamuraiBattle)
                    .HasForeignKey(d => d.BattleId);

                entity.HasOne(d => d.Samurai)
                    .WithMany(p => p.SamuraiBattle)
                    .HasForeignKey(d => d.SamuraiId);
            });

            modelBuilder.Entity<SecretIdentity>(entity =>
            {
                entity.HasIndex(e => e.SamuraiId)
                    .IsUnique();

                entity.HasOne(d => d.Samurai)
                    .WithOne(p => p.SecretIdentity)
                    .HasForeignKey<SecretIdentity>(d => d.SamuraiId);
            });
        }
    }
}
