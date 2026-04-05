using Banking.Models;
using Microsoft.EntityFrameworkCore;

namespace Banking.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<LoanApplication> LoanApplications => Set<LoanApplication>();
        public DbSet<LoanOffer> LoanOffers => Set<LoanOffer>();
        public DbSet<LoanAccount> LoanAccounts => Set<LoanAccount>();
        public DbSet<RepaymentSchedule> RepaymentSchedules => Set<RepaymentSchedule>();
        public DbSet<Payment> Payments => Set<Payment>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<LoanApplication>(entity =>
            {
                entity.HasKey(x => x.ApplicationId);
                entity.Property(x => x.ApplicationStatus).HasConversion<string>();
            });

            modelBuilder.Entity<LoanOffer>(entity =>
            {
                entity.HasKey(x => x.OfferId);
                entity.Property(x => x.OfferStatus).HasConversion<string>();

                entity.HasOne(x => x.LoanApplication)
                      .WithMany()
                      .HasForeignKey(x => x.ApplicationId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<LoanAccount>(entity =>
            {
                entity.HasKey(x => x.LoanId);
                entity.Property(x => x.LoanStatus).HasConversion<string>();
            });

            modelBuilder.Entity<RepaymentSchedule>(entity =>
            {
                entity.HasKey(x => x.ScheduleId);
                entity.Property(x => x.InstallmentStatus).HasConversion<string>();

                entity.HasOne(x => x.LoanAccount)
                      .WithMany(x => x.RepaymentSchedules)
                      .HasForeignKey(x => x.LoanId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasKey(x => x.PaymentId);
                entity.Property(x => x.PaymentStatus).HasConversion<string>();

                entity.HasOne(x => x.LoanAccount)
                      .WithMany(x => x.Payments)
                      .HasForeignKey(x => x.LoanId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}