using Microsoft.EntityFrameworkCore;
using Bank.Models;

namespace Bank.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<KycRecord> KycRecords { get; set; }
        public DbSet<LoanApplication> LoanApplications { get; set; }
    }
}
