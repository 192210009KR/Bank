using Banking.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Banking.Models
{
    public class RepaymentSchedule
    {
        [Key]
        [MaxLength(30)]
        public string ScheduleId { get; set; } = default!;

        [Required]
        [MaxLength(30)]
        public string LoanId { get; set; } = default!;

        [Required]
        public int InstallmentNo { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        [Column(TypeName = "decimal(12,2)")]
        public decimal Emi { get; set; }

        [Required]
        public InstallmentStatus InstallmentStatus { get; set; } = InstallmentStatus.PENDING;

        public DateTime? PaidAt { get; set; }

        public LoanAccount? LoanAccount { get; set; }
    }
}