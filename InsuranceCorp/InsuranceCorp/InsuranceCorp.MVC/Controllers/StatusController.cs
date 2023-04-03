using Microsoft.AspNetCore.Mvc;
using InsuranceCorp.MVC.Models;
using System.Diagnostics;
using InsuranceCorp.Data;

namespace InsuranceCorp.MVC.Controllers
{
    public class StatusController : Controller
    {
        private readonly InsCorpDbContext _context;
        /// <summary>
        /// konstruktor
        /// </summary>
        public  StatusController(InsCorpDbContext context) 
        {
            _context= context;
        }


        private readonly ILogger<HomeController> _logger;

        public StatusController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
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
