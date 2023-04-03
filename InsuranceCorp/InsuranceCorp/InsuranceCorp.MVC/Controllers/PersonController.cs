using InsuranceCorp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            var top100 = _context.Persons.Include(person => person.Constracts)
                .OrderByDescending(person => person.Constracts.Count())
                .Take(100).ToList();

            ViewData["count"] = _context.Persons.Count();
            // 2) zobrazení dat uživateli

            return View(top100);
        }

        public IActionResult Detail(int id)
        {
            // ziskat data
            var person = _context.Persons.Find(id);

            return View(person);
        }
    }
}
