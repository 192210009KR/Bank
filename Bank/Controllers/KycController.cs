using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Bank.Data;
using Bank.Models;
namespace Bank.Controllers
{
    [ApiController]
    [Route("api/customers/{customerId}/kyc")]
    public class KycController: ControllerBase
    {
        private readonly AppDbContext _context;

        public KycController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult SubmitKyc(int customerId, KycRecord kyc)
        {
            var customer = _context.Customers.Find(customerId);

            if (customer == null)
                return NotFound("Customer not found");

            int age = DateTime.Now.Year - kyc.DOB.Year;

            if (age < 21)
            {
                kyc.KycStatus = "REJECTED";
                kyc.Reason = "Age below 21";
            }
            else
            {
                kyc.KycStatus = "VERIFIED";
                kyc.VerifiedAt = DateTime.Now;
            }

            kyc.CustomerId = customerId;

            _context.KycRecords.Add(kyc);
            _context.SaveChanges();

            return Ok(kyc);
        }
    }
}
