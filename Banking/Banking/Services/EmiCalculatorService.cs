namespace Banking.Services
{
    public class EmiCalculatorService
    {
        public decimal CalculateEmi(decimal principal, decimal annualRate, int months)
        {
            double monthlyRate = (double)annualRate / 12 / 100;
            double p = (double)principal;
            int n = months;

            double emi = p * monthlyRate * Math.Pow(1 + monthlyRate, n)
                       / (Math.Pow(1 + monthlyRate, n) - 1);

            return Math.Round((decimal)emi, 2);
        }
    }
}