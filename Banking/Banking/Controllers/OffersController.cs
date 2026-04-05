using Banking.Services;
using Microsoft.AspNetCore.Mvc;

namespace Banking.Controllers
{
    [ApiController]
    [Route("api/v1/loans")]
    public class OffersController : ControllerBase
    {
        private readonly OfferService _offerService;

        public OffersController(OfferService offerService)
        {
            _offerService = offerService;
        }

        [HttpPost("applications/{applicationId}/offer")]
        public async Task<IActionResult> GenerateOffer(string applicationId)
        {
            try
            {
                var result = await _offerService.GenerateOfferAsync(applicationId);

                if (result == null)
                    return NotFound(new { message = "Application not found" });

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("applications/{applicationId}/offers")]
        public async Task<IActionResult> GetOffers(string applicationId)
        {
            var offers = await _offerService.GetOffersByApplicationIdAsync(applicationId);

            return Ok(new
            {
                applicationId,
                offers = offers.Select(x => new
                {
                    offerId = x.OfferId,
                    loanAmount = x.LoanAmount,
                    tenureMonths = x.TenureMonths,
                    apr = x.Apr,
                    emi = x.Emi,
                    totalPayable = x.TotalPayable,
                    offerStatus = x.OfferStatus.ToString(),
                    expiryDate = x.ExpiryDate
                })
            });
        }
    }
}