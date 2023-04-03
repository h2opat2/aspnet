using InsuranceCorp.Data;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceCorp.MVC.Controllers
{
    public class PersonController : Controller
    {
        private readonly InsCorpDbContext _context;
        /// <summary>
        /// konstruktor
        /// </summary>
        public PersonController(InsCorpDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // 1) získání dat / jejich zpracování
            var top100 = _context.Persons
                .OrderBy(person => person.Id)
                .Take(100).ToList();

            // 2) zobrazení dat uživateli

            return View(top100);
        }
    }
}
