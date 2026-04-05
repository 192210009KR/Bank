using Banking.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Banking.Models
{
    public class Payment
    {
        [Key]
        [MaxLength(30)]
        public string PaymentId { get; set; } = default!;

        [Required]
        [MaxLength(30)]
        public string LoanId { get; set; } = default!;

        [Required]
        [Column(TypeName = "decimal(12,2)")]
        public decimal Amount { get; set; }

        [Required]
        [MaxLength(20)]
        public string PaymentMode { get; set; } = default!;

        [Required]
        [MaxLength(50)]
        public string PaymentRef { get; set; } = default!;

        [Required]
        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;

        [Required]
        public PaymentStatus PaymentStatus { get; set; }

        [MaxLength(255)]
        public string? Remarks { get; set; }

        public LoanAccount? LoanAccount { get; set; }
    }
}