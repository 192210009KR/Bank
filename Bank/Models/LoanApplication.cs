using System.ComponentModel.DataAnnotations;
namespace Bank.Models
{
    public class LoanApplication
    {
        [Key]
        public int ApplicationId { get; set; }
        public int CustomerId { get; set; }
        public decimal Amount { get; set; }
        public int TenureMonths { get; set; }
        public decimal MonthlyIncome { get; set; }
        public string EmploymentType { get; set; }
        public int CreditScore { get; set; }

        public string EligibilityStatus { get; set; }
        public string DecisionReason { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public Customer Customer { get; set; }
    }
}
