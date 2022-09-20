using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TaxManWebCore.Models;

namespace TaxManCoreWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _config;
        private ISalaryReturnModel _salaryReturn;

        public HomeController(ILogger<HomeController> logger,
                              IConfiguration config,
                              ISalaryReturnModel salaryReturn)
        {
            _logger = logger;
            _config = config;
            _salaryReturn = salaryReturn;
        }

        public IActionResult Index()
        {
            return View(_salaryReturn);
        }

        [HttpPost]
        public IActionResult Index([FromForm] string inpSalary)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_config["APILocation"]);
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("", inpSalary)
                });
                var responseTask = client.PostAsync("Tax", content);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {

                    var readJob = result.Content.ReadFromJsonAsync<SalaryReturnModel>();
                    readJob.Wait();
                    _salaryReturn = readJob.Result;

                    _salaryReturn.dcMnthTaxPd = _salaryReturn.dcGrossTaxPd / 12;
                    _salaryReturn.dcMnthNetSal = _salaryReturn.dcNetSal / 12;

                    return View(_salaryReturn);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error: DAC004");
                    return View(_salaryReturn);
                }
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}