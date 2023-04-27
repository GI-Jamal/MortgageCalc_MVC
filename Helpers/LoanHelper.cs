using MortgageCalc_MVC.Models;

namespace MortgageCalc_MVC.Helpers
{
    public static class LoanHelper
    {
        public static Loan GetPayments(Loan loan)
        { 
            loan.Payment = CalcPayment(loan.Amount, loan.Rate, loan.Term);

            decimal balance = loan.Amount;
            decimal totalInterst = 0.0m;
            decimal monthlyRate = CalcMonthlyRate(loan.Rate);

            for (int month = 1; month <= loan.Term; month++)
            {
                decimal monthlyInterest = CalcMonthlyInterest(balance, monthlyRate);
                totalInterst+= monthlyInterest;
                decimal monthlyPrincipal = loan.Payment - monthlyInterest;
                balance -= monthlyPrincipal;

                if (balance < 0)
                {
                    balance = 0;
                }

                LoanPayment loanPayment = new LoanPayment()
                {
                    Month = month,
                    Payment = loan.Payment,
                    MonthlyPrincipal = monthlyPrincipal,
                    MonthlyInterest = monthlyInterest,
                    TotalInterest = totalInterst,
                    Balance = balance
                };
                loan.Payments.Add(loanPayment);
            }
                
            loan.TotalInterest = totalInterst;
            loan.TotalCost = loan.Amount + totalInterst;

            return loan;
        }

        private static decimal CalcPayment(decimal amount, decimal rate, int term)
        {
            decimal monthlyRate = CalcMonthlyRate(rate); 
            double rateD = Convert.ToDouble(monthlyRate);
            double amountD = Convert.ToDouble(amount);

            double paymentD = (amountD * rateD) / ( 1 - Math.Pow(1+ rateD, -term));
                
            return Convert.ToDecimal(paymentD);
        }

        private static decimal CalcMonthlyRate(decimal rate) { return rate / 1200; }

        private static decimal CalcMonthlyInterest(decimal balance, decimal monthlyRate) { return balance * monthlyRate; }
    }
}
