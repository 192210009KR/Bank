using Banking.DTOs;
using Banking.Services;
using Microsoft.AspNetCore.Mvc;

namespace Banking.Controllers
{
    [ApiController]
    [Route("api/v1/loans")]
    public class LoansController : ControllerBase
    {
        private readonly LoanService _loanService;

        public LoansController(LoanService loanService)
        {
            _loanService = loanService;
        }

        [HttpPost("offers/{offerId}/accept")]
        public async Task<IActionResult> AcceptOffer(string offerId, [FromBody] AcceptOfferRequestDto request)
        {
            try
            {
                var result = await _loanService.AcceptOfferAsync(offerId, request.AcceptedBy);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{loanId}")]
        public async Task<IActionResult> GetLoanDetails(string loanId)
        {
            var result = await _loanService.GetLoanDetailsAsync(loanId);

            if (result == null)
                return NotFound(new { message = "Loan not found" });

            return Ok(result);
        }

        [HttpGet("{loanId}/schedule")]
        public async Task<IActionResult> GetRepaymentSchedule(string loanId)
        {
            var result = await _loanService.GetRepaymentScheduleAsync(loanId);

            return Ok(new
            {
                loanId,
                schedule = result
            });
        }
    }
}