using Haver_Niagara.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Haver_Niagara.Data
{
    public class HaverNiagaraDbContext : DbContext
    {
        public DbSet<CAR> CARs { get; set; }
        public DbSet<Defect> Defects { get; set; }
        public DbSet<Engineering> Engineerings { get; set; }
        public DbSet<FollowUp> FollowUps { get; set; }
        public DbSet<Media> Medias { get; set; }
        public DbSet<NCR> NCRs { get; set; }
        public DbSet<NewNCR> NewNCRs { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }

        //Defect Lists junfction table
        public DbSet<DefectList> DefectLists { get; set; }

        public DbSet<UploadedFile> UploadedFiles { get; set; }

        public DbSet<QualityInspection> QualityInspections { get; set; }

        public HaverNiagaraDbContext(DbContextOptions<HaverNiagaraDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Fluent API for relationships (cascade, 1:M, M:M, etc etc)

            //establishes bidirectional one to one with NCR and Engineering
            //modelBuilder.Entity<NCR>()
            //    .HasOne(a => a.Engineering)
            //    .WithOne(b => b.NCR)
            //    .HasForeignKey<Engineering>(c => c.NCRId);

            ////bidirectional 1 to 1 with NCR and newNCR
            //modelBuilder.Entity<NCR>()
            //    .HasOne(a => a.NewNCR)
            //   .WithOne(b => b.NCR)

            //modelBuilder.Entity<Engineering>()
            //    .HasOne(a => a.NCR)
            //    .WithOne(a => a.Engineering)
            //    .HasForeignKey<Engineering>(e => e.NCRId)
            //    .IsRequired();

            //modelBuilder.Entity<NewNCR>()
            //    .HasOne(n => n.QualityInspection)
            //    .WithOne(n => n.NewNCR)
            //    .HasForeignKey<QualityInspection>(q => q.NewNCRID);


            //ai gen
            modelBuilder.Entity<Operation>()
                .Property(n => n.ID)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Part>()
                .Property(n => n.ID)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Engineering>()
                .Property(n => n.ID)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<QualityInspection>()
                .Property(n => n.ID)
                .ValueGeneratedOnAdd();

        }
    }
}
