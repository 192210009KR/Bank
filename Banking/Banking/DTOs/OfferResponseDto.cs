namespace Banking.DTOs
{
    public class OfferResponseDto
    {
        public string Message { get; set; } = default!;
        public string OfferId { get; set; } = default!;
        public string ApplicationId { get; set; } = default!;
        public string CustomerId { get; set; } = default!;  
        public decimal LoanAmount { get; set; }
        public int TenureMonths { get; set; }
        public decimal Apr { get; set; }
        public decimal Emi { get; set; }
        public decimal TotalPayable { get; set; }
        public string OfferStatus { get; set; } = default!;
        public string ApplicationStatus { get; set; } = default!;
        public DateTime ExpiryDate { get; set; }
    }
}