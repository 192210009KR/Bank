namespace Banking.DTOs
{
    public class PaymentResponseDto
    {
        public string Message { get; set; } = default!;
        public string PaymentId { get; set; } = default!;
        public string LoanId { get; set; } = default!;
        public decimal Amount { get; set; }
        public string PaymentStatus { get; set; } = default!;
        public decimal UpdatedOutstanding { get; set; }
        public int PaidInstallmentNo { get; set; }
        public DateTime? NextDueDate { get; set; }
    }
}