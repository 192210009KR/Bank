using Banking.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Banking.Models
{
    public class LoanOffer
    {
        [Key]
        [MaxLength(30)]
        public string OfferId { get; set; } = default!;

        [Required]
        [MaxLength(30)]
        public string ApplicationId { get; set; } = default!;

        [Required]
        [MaxLength(30)]
        public string CustomerId { get; set; } = default!;

        [Required]
        [Column(TypeName = "decimal(12,2)")]
        public decimal LoanAmount { get; set; }

        [Required]
        public int TenureMonths { get; set; }

        [Required]
        [Column(TypeName = "decimal(5,2)")]
        public decimal Apr { get; set; }

        [Required]
        [Column(TypeName = "decimal(12,2)")]
        public decimal Emi { get; set; }

        [Required]
        [Column(TypeName = "decimal(12,2)")]
        public decimal TotalPayable { get; set; }

        [Required]
        public OfferStatus OfferStatus { get; set; } = OfferStatus.ACTIVE;

        [Required]
        public DateTime ExpiryDate { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public LoanApplication? LoanApplication { get; set; }
    }
}