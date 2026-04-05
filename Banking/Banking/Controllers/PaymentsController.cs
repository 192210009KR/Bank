using Banking.DTOs;
using Banking.Services;
using Microsoft.AspNetCore.Mvc;

namespace Banking.Controllers
{
    [ApiController]
    [Route("api/v1/loans")]
    public class PaymentsController : ControllerBase
    {
        private readonly PaymentService _paymentService;

        public PaymentsController(PaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("{loanId}/payments")]
        public async Task<IActionResult> PostPayment(string loanId, [FromBody] PaymentRequestDto request)
        {
            try
            {
                var result = await _paymentService.PostPaymentAsync(loanId, request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}