using Microsoft.AspNetCore.Mvc;
using InsuranceCorp.MVC.Models;
using System.Diagnostics;
using InsuranceCorp.Data;
using Microsoft.Extensions.Logging;

namespace InsuranceCorp.MVC.Controllers
{
    public class StatusController : Controller
    {
        private readonly InsCorpDbContext _context;
        private readonly ILogger<HomeController> _logger;

        /// <summary>
        /// konstruktor
        /// </summary>
        public  StatusController(InsCorpDbContext context, ILogger<HomeController> logger) 
        {
            _context= context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            bool status = _context.Database.CanConnect();

            ViewData["status"] = status;

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
