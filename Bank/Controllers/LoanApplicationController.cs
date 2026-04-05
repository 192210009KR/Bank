using Bank.Data;
using Bank.Models;
using Bank.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace Bank.Controllers
{
    [ApiController]
    [Route("api/loans/applications")]
    public class LoanApplicationController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly EligibilityService _eligibilityService;

        public LoanApplicationController(AppDbContext context, EligibilityService service)
        {
            _context = context;
            _eligibilityService = service;
        }

        [HttpPost]
        public IActionResult ApplyLoan(LoanApplication app)
        {
            var customer = _context.Customers.Find(app.CustomerId);

            if (customer == null)
                return NotFound("Customer not found");

            var result = _eligibilityService.CheckEligibility(app, customer);

            app.EligibilityStatus = result.Item1 ? "ELIGIBLE" : "REJECTED";
            app.DecisionReason = result.Item2;

            _context.LoanApplications.Add(app);
            _context.SaveChanges();

            return Ok(app);
        }

        [HttpGet("{id}")]
        public IActionResult GetApplication(int id)
        {
            var app = _context.LoanApplications.Find(id);

            if (app == null)
                return NotFound();

            return Ok(app);
        }
    }
}
