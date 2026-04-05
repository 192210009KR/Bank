using Banking.Data;
using Banking.DTOs;
using Banking.Enums;
using Banking.Models;
using Microsoft.EntityFrameworkCore;

namespace Banking.Services
{
    public class LoanService
    {
        private readonly AppDbContext _context;

        public LoanService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<object> AcceptOfferAsync(string offerId, string acceptedBy)
        {
            var offer = await _context.LoanOffers
                .FirstOrDefaultAsync(x => x.OfferId == offerId);

            if (offer == null)
                throw new Exception("Offer not found.");

            if (offer.OfferStatus != OfferStatus.ACTIVE)
                throw new Exception("Only active offer can be accepted.");

            var application = await _context.LoanApplications
                .FirstOrDefaultAsync(x => x.ApplicationId == offer.ApplicationId);

            if (application == null)
                throw new Exception("Loan application not found.");

            var existingLoan = await _context.LoanAccounts
                .FirstOrDefaultAsync(x => x.OfferId == offerId);

            if (existingLoan != null)
            {
                return new
                {
                    message = "Loan already created for this offer",
                    offerId = offer.OfferId,
                    loanId = existingLoan.LoanId,
                    applicationId = existingLoan.ApplicationId,
                    offerStatus = offer.OfferStatus.ToString(),
                    applicationStatus = application.ApplicationStatus.ToString(),
                    loanStatus = existingLoan.LoanStatus.ToString(),
                    disbursalStatus = "SUCCESS"
                };
            }

            var loan = new LoanAccount
            {
                LoanId = $"LOAN-{Guid.NewGuid().ToString("N")[..8].ToUpper()}",
                ApplicationId = offer.ApplicationId,
                OfferId = offer.OfferId,
                CustomerId = offer.CustomerId,
                Principal = offer.LoanAmount,
                Apr = offer.Apr,
                TenureMonths = offer.TenureMonths,
                Emi = offer.Emi,
                Outstanding = offer.LoanAmount,
                DisbursedAt = DateTime.UtcNow,
                NextDueDate = DateTime.UtcNow.Date.AddMonths(1),
                LoanStatus = LoanStatus.ACTIVE,
                CreatedAt = DateTime.UtcNow
            };

            _context.LoanAccounts.Add(loan);

            for (int i = 1; i <= offer.TenureMonths; i++)
            {
                _context.RepaymentSchedules.Add(new RepaymentSchedule
                {
                    ScheduleId = $"SCH-{Guid.NewGuid().ToString("N")[..8].ToUpper()}",
                    LoanId = loan.LoanId,
                    InstallmentNo = i,
                    DueDate = DateTime.UtcNow.Date.AddMonths(i),
                    Emi = offer.Emi,
                    InstallmentStatus = InstallmentStatus.PENDING
                });
            }

            offer.OfferStatus = OfferStatus.ACCEPTED;
            application.ApplicationStatus = ApplicationStatus.OFFER_ACCEPTED;

            await _context.SaveChangesAsync();

            application.ApplicationStatus = ApplicationStatus.DISBURSED;
            await _context.SaveChangesAsync();

            return new
            {
                message = "Offer accepted and loan created successfully",
                offerId = offer.OfferId,
                loanId = loan.LoanId,
                applicationId = loan.ApplicationId,
                offerStatus = offer.OfferStatus.ToString(),
                applicationStatus = application.ApplicationStatus.ToString(),
                loanStatus = loan.LoanStatus.ToString(),
                disbursalStatus = "SUCCESS",
                acceptedBy
            };
        }

        public async Task<LoanDetailsResponseDto?> GetLoanDetailsAsync(string loanId)
        {
            var loan = await _context.LoanAccounts.FirstOrDefaultAsync(x => x.LoanId == loanId);

            if (loan == null)
                return null;

            return new LoanDetailsResponseDto
            {
                LoanId = loan.LoanId,
                ApplicationId = loan.ApplicationId,
                CustomerId = loan.CustomerId,
                Principal = loan.Principal,
                Apr = loan.Apr,
                TenureMonths = loan.TenureMonths,
                Emi = loan.Emi,
                Outstanding = loan.Outstanding,
                DisbursedAt = loan.DisbursedAt,
                NextDueDate = loan.NextDueDate,
                LoanStatus = loan.LoanStatus.ToString()
            };
        }

        public async Task<List<object>> GetRepaymentScheduleAsync(string loanId)
        {
            return await _context.RepaymentSchedules
                .Where(x => x.LoanId == loanId)
                .OrderBy(x => x.InstallmentNo)
                .Select(x => new
                {
                    scheduleId = x.ScheduleId,
                    installmentNo = x.InstallmentNo,
                    dueDate = x.DueDate,
                    emi = x.Emi,
                    installmentStatus = x.InstallmentStatus.ToString(),
                    paidAt = x.PaidAt
                })
                .Cast<object>()
                .ToListAsync();
        }

        public async Task<List<AdminApplicationResponseDto>> GetApplicationsAsync(string? status, string? customerId)
        {
            var query = _context.LoanApplications.AsQueryable();

            if (!string.IsNullOrWhiteSpace(customerId))
                query = query.Where(x => x.CustomerId == customerId);

            if (!string.IsNullOrWhiteSpace(status) &&
                Enum.TryParse<ApplicationStatus>(status, true, out var parsedStatus))
            {
                query = query.Where(x => x.ApplicationStatus == parsedStatus);
            }

            var applications = await query
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();

            var result = new List<AdminApplicationResponseDto>();

            foreach (var app in applications)
            {
                var offer = await _context.LoanOffers
                    .Where(x => x.ApplicationId == app.ApplicationId)
                    .OrderByDescending(x => x.CreatedAt)
                    .FirstOrDefaultAsync();

                var loan = await _context.LoanAccounts
                    .Where(x => x.ApplicationId == app.ApplicationId)
                    .OrderByDescending(x => x.CreatedAt)
                    .FirstOrDefaultAsync();

                result.Add(new AdminApplicationResponseDto
                {
                    ApplicationId = app.ApplicationId,
                    CustomerId = app.CustomerId,
                    CustomerName = app.CustomerName,
                    Amount = app.Amount,
                    TenureMonths = app.TenureMonths,
                    ApplicationStatus = app.ApplicationStatus.ToString(),
                    OfferId = offer?.OfferId,
                    LoanId = loan?.LoanId,
                    CreatedAt = app.CreatedAt
                });
            }

            return result;
        }
    }
}