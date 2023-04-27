using Microsoft.AspNetCore.Mvc;
using MortgageCalc_MVC.Helpers;
using MortgageCalc_MVC.Models;
using System.Diagnostics;

namespace MortgageCalc_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult MortgageCalcPage()
        {
            Loan loan = new Loan();
            return View(loan);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult MortgageCalcPage(Loan loan)
        {
            loan = LoanHelper.GetPayments(loan);
            return View(loan);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}