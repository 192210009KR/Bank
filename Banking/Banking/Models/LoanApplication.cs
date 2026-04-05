using Banking.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Banking.Models
{
    public class LoanApplication
    {
        [Key]
        [MaxLength(30)]
        public string ApplicationId { get; set; } = default!;

        [Required]
        [MaxLength(30)]
        public string CustomerId { get; set; } = default!;

        [Required]
        [Column(TypeName = "decimal(12,2)")]
        public decimal Amount { get; set; }

        [Required]
        public int TenureMonths { get; set; }

        [Required]
        [Column(TypeName = "decimal(12,2)")]
        public decimal MonthlyIncome { get; set; }

        [Required]
        public int CreditScore { get; set; }

        [Required]
        public ApplicationStatus ApplicationStatus { get; set; }

        [MaxLength(255)]
        public string? DecisionReason { get; set; }

        [MaxLength(100)]
        public string? CustomerName { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}