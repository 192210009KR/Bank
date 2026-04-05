using Banking.Data;
using Banking.DTOs;
using Banking.Enums;
using Banking.Models;
using Microsoft.EntityFrameworkCore;

namespace Banking.Services
{
    public class OfferService
    {
        private readonly AppDbContext _context;
        private readonly EmiCalculatorService _emiCalculatorService;

        public OfferService(AppDbContext context, EmiCalculatorService emiCalculatorService)
        {
            _context = context;
            _emiCalculatorService = emiCalculatorService;
        }

        public async Task<OfferResponseDto?> GenerateOfferAsync(string applicationId)
        {
            var application = await _context.LoanApplications
                .FirstOrDefaultAsync(x => x.ApplicationId == applicationId);

            if (application == null)
                return null;

            if (application.ApplicationStatus != ApplicationStatus.ELIGIBLE)
                throw new Exception("Application is not eligible for offer generation.");

            var existingOffer = await _context.LoanOffers
                .FirstOrDefaultAsync(x => x.ApplicationId == applicationId && x.OfferStatus == OfferStatus.ACTIVE);

            if (existingOffer != null)
            {
                return new OfferResponseDto
                {
                    Message = "Offer already exists",
                    OfferId = existingOffer.OfferId,
                    ApplicationId = existingOffer.ApplicationId,
                    CustomerId = existingOffer.CustomerId,
                    LoanAmount = existingOffer.LoanAmount,
                    TenureMonths = existingOffer.TenureMonths,
                    Apr = existingOffer.Apr,
                    Emi = existingOffer.Emi,
                    TotalPayable = existingOffer.TotalPayable,
                    OfferStatus = existingOffer.OfferStatus.ToString(),
                    ApplicationStatus = application.ApplicationStatus.ToString(),
                    ExpiryDate = existingOffer.ExpiryDate
                };
            }

            decimal apr = application.CreditScore >= 780 ? 12 : 16;
            decimal emi = _emiCalculatorService.CalculateEmi(application.Amount, apr, application.TenureMonths);
            decimal totalPayable = Math.Round(emi * application.TenureMonths, 2);

            var offer = new LoanOffer
            {
                OfferId = $"OFF-{Guid.NewGuid().ToString("N")[..8].ToUpper()}",
                ApplicationId = application.ApplicationId,
                CustomerId = application.CustomerId,
                LoanAmount = application.Amount,
                TenureMonths = application.TenureMonths,
                Apr = apr,
                Emi = emi,
                TotalPayable = totalPayable,
                OfferStatus = OfferStatus.ACTIVE,
                ExpiryDate = DateTime.UtcNow.AddDays(3),
                CreatedAt = DateTime.UtcNow
            };

            _context.LoanOffers.Add(offer);

            application.ApplicationStatus = ApplicationStatus.OFFER_GENERATED;

            await _context.SaveChangesAsync();

            return new OfferResponseDto
            {
                Message = "Offer generated successfully",
                OfferId = offer.OfferId,
                ApplicationId = offer.ApplicationId,
                CustomerId = offer.CustomerId,
                LoanAmount = offer.LoanAmount,
                TenureMonths = offer.TenureMonths,
                Apr = offer.Apr,
                Emi = offer.Emi,
                TotalPayable = offer.TotalPayable,
                OfferStatus = offer.OfferStatus.ToString(),
                ApplicationStatus = application.ApplicationStatus.ToString(),
                ExpiryDate = offer.ExpiryDate
            };
        }

        public async Task<List<LoanOffer>> GetOffersByApplicationIdAsync(string applicationId)
        {
            return await _context.LoanOffers
                .Where(x => x.ApplicationId == applicationId)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }
    }
}