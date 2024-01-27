using Haver_Niagara.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Haver_Niagara.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<CAR> CARs { get; set; }
        public DbSet<Defect> Defects { get; set; }
        public DbSet<Engineering> Engineering { get; set; }
        public DbSet<FollowUp> FollowUp { get; set; }
        public DbSet<Media> Media { get; set; }
        public DbSet<NCR> NCRs { get; set; }
        public DbSet<NewNCR> NewNCRs { get; set; }
        public DbSet<Product> Products { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Fluent API for relationships (cascade, 1:M, M:M, etc etc)

            //establishes bidirectional one to one with NCR and Engineering
            modelBuilder.Entity<NCR>()
                .HasOne(a => a.Engineering)
                .WithOne(b => b.NCR);

            //bidirectional 1 to 1 with NCR and newNCR
            modelBuilder.Entity<NCR>()
                .HasOne(a => a.NewNCR)
                .WithOne(b => b.NCR)
                .HasForeignKey<NewNCR>(n => n.NCRId);
        }
    }
}
