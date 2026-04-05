namespace Banking.DTOs
{
    public class AdminApplicationResponseDto
    {
        public string ApplicationId { get; set; } = default!;
        public string CustomerId { get; set; } = default!;
        public string? CustomerName { get; set; }
        public decimal Amount { get; set; }
        public int TenureMonths { get; set; }
        public string ApplicationStatus { get; set; } = default!;
        public string? OfferId { get; set; }
        public string? LoanId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}