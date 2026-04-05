using Banking.Data;
using Banking.DTOs;
using Banking.Enums;
using Banking.Models;
using Microsoft.EntityFrameworkCore;

namespace Banking.Services
{
    public class PaymentService
    {
        private readonly AppDbContext _context;

        public PaymentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<PaymentResponseDto> PostPaymentAsync(string loanId, PaymentRequestDto request)
        {
            var loan = await _context.LoanAccounts
                .FirstOrDefaultAsync(x => x.LoanId == loanId);

            if (loan == null)
                throw new Exception("Loan not found.");

            var pendingInstallment = await _context.RepaymentSchedules
                .Where(x => x.LoanId == loanId && x.InstallmentStatus == InstallmentStatus.PENDING)
                .OrderBy(x => x.InstallmentNo)
                .FirstOrDefaultAsync();

            if (pendingInstallment == null)
                throw new Exception("No pending installment found.");

            if (request.Amount < pendingInstallment.Emi)
                throw new Exception("Invalid payment amount. Amount must be at least EMI.");

            var payment = new Payment
            {
                PaymentId = $"PAY-{Guid.NewGuid().ToString("N")[..8].ToUpper()}",
                LoanId = loanId,
                Amount = request.Amount,
                PaymentMode = request.PaymentMode,
                PaymentRef = request.PaymentRef,
                PaymentDate = DateTime.UtcNow,
                PaymentStatus = Enums.PaymentStatus.SUCCESS,
                Remarks = "Payment posted successfully"
            };

            _context.Payments.Add(payment);

            pendingInstallment.InstallmentStatus = InstallmentStatus.PAID;
            pendingInstallment.PaidAt = DateTime.UtcNow;

            loan.Outstanding = Math.Max(0, loan.Outstanding - pendingInstallment.Emi);

            var nextPendingInstallment = await _context.RepaymentSchedules
                .Where(x => x.LoanId == loanId && x.InstallmentStatus == InstallmentStatus.PENDING)
                .OrderBy(x => x.InstallmentNo)
                .FirstOrDefaultAsync();

            loan.NextDueDate = nextPendingInstallment?.DueDate;

            if (loan.Outstanding <= 0)
            {
                loan.LoanStatus = LoanStatus.CLOSED;
            }

            await _context.SaveChangesAsync();

            return new PaymentResponseDto
            {
                Message = "Payment posted successfully",
                PaymentId = payment.PaymentId,
                LoanId = payment.LoanId,
                Amount = payment.Amount,
                PaymentStatus = payment.PaymentStatus.ToString(),
                UpdatedOutstanding = loan.Outstanding,
                PaidInstallmentNo = pendingInstallment.InstallmentNo,
                NextDueDate = loan.NextDueDate
            };
        }
    }
}