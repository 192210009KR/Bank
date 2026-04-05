using Banking.Services;
using Microsoft.AspNetCore.Mvc;

namespace Banking.Controllers
{
    [ApiController]
    [Route("api/v1/admin")]
    public class AdminController : ControllerBase
    {
        private readonly LoanService _loanService;

        public AdminController(LoanService loanService)
        {
            _loanService = loanService;
        }

        [HttpGet("applications")]
        public async Task<IActionResult> GetApplications([FromQuery] string? status, [FromQuery] string? customerId)
        {
            var result = await _loanService.GetApplicationsAsync(status, customerId);
            return Ok(new { applications = result });
        }
    }
}