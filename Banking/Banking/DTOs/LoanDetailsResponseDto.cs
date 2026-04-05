namespace Banking.DTOs
{
    public class LoanDetailsResponseDto
    {
        public string LoanId { get; set; } = default!;
        public string ApplicationId { get; set; } = default!;
        public string CustomerId { get; set; } = default!;
        public decimal Principal { get; set; }
        public decimal Apr { get; set; }
        public int TenureMonths { get; set; }
        public decimal Emi { get; set; }
        public decimal Outstanding { get; set; }
        public DateTime? DisbursedAt { get; set; }
        public DateTime? NextDueDate { get; set; }
        public string LoanStatus { get; set; } = default!;
    }
}