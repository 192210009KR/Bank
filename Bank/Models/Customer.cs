using System.ComponentModel.DataAnnotations;
namespace Bank.Models;

public class Customer
{
    [Key]
    public int CustomerId { get; set; }
    public string FullName { get; set; }
    public string Mobile { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public DateTime DOB { get; set; }
    public string Address { get; set; }
    public string IdLast4 { get; set; }
    public string Status { get; set; } = "ACTIVE";

    public ICollection<LoanApplication> LoanApplications { get; set; }
}