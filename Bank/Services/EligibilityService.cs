using Bank.Models;
namespace Bank.Services
{
    public class EligibilityService
    {
        public (bool, string) CheckEligibility(LoanApplication app, Customer customer)
        {
            int age = DateTime.Now.Year - customer.DOB.Year;

            if (age < 21 || age > 60)
                return (false, "Age not eligible");

            if (app.MonthlyIncome < 30000)
                return (false, "Income too low");

            if (app.Amount > app.MonthlyIncome * 10)
                return (false, "Loan amount too high");

            if (app.CreditScore < 700)
                return (false, "Low credit score");

            if (app.TenureMonths < 6 || app.TenureMonths > 60)
                return (false, "Invalid tenure");

            return (true, "Eligible");
        }
    }
}

