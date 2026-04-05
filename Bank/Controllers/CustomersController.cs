using Bank.Data;
using Bank.Models;
using Bank.Services;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace Bank.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomersController:ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly PasswordService _passwordService;

        public CustomersController(AppDbContext context, PasswordService passwordService)
        {
            _context = context;
            _passwordService = passwordService;
        }

        [HttpPost]
        public IActionResult CreateCustomer(Customer customer)
        {
            customer.PasswordHash = _passwordService.HashPassword(customer.PasswordHash);

            _context.Customers.Add(customer);
            _context.SaveChanges();

            return Ok(customer);
        }
        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            var customer = _context.Customers
                .FirstOrDefault(c => c.Email == request.Email);

            if (customer == null)
                return NotFound("User not found");

            var hashed = _passwordService.HashPassword(request.Password);

            if (customer.PasswordHash != hashed)
                return Unauthorized("Invalid password");

            return Ok(customer);
        }
    }
}
