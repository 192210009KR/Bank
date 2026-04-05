namespace Banking.DTOs
{
    public class PaymentRequestDto
    {
        public decimal Amount { get; set; }
        public string PaymentMode { get; set; } = default!;
        public string PaymentRef { get; set; } = default!;
    }
}