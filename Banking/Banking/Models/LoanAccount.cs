using Banking.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Banking.Models
{
    public class LoanAccount
    {
        [Key]
        [MaxLength(30)]
        public string LoanId { get; set; } = default!;

        [Required]
        [MaxLength(30)]
        public string ApplicationId { get; set; } = default!;

        [Required]
        [MaxLength(30)]
        public string OfferId { get; set; } = default!;

        [Required]
        [MaxLength(30)]
        public string CustomerId { get; set; } = default!;

        [Required]
        [Column(TypeName = "decimal(12,2)")]
        public decimal Principal { get; set; }

        [Required]
        [Column(TypeName = "decimal(5,2)")]
        public decimal Apr { get; set; }

        [Required]
        public int TenureMonths { get; set; }

        [Required]
        [Column(TypeName = "decimal(12,2)")]
        public decimal Emi { get; set; }

        [Required]
        [Column(TypeName = "decimal(12,2)")]
        public decimal Outstanding { get; set; }

        public DateTime? DisbursedAt { get; set; }

        public DateTime? NextDueDate { get; set; }

        [Required]
        public LoanStatus LoanStatus { get; set; } = LoanStatus.ACTIVE;

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<RepaymentSchedule> RepaymentSchedules { get; set; } = new List<RepaymentSchedule>();
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}