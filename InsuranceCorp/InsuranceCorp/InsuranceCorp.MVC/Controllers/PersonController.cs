using InsuranceCorp.Data;
using InsuranceCorp.Model;
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

            //if(person == null)
            //{
            //    return NotFound();
            //}

            if (person == null)
            {
                ViewData["id"] = id;
                return View("NotFound");
            }

            return View(person);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Person person)
        {
            // ulozit osobu do DB
            _context.Persons.Add(person);
            _context.SaveChanges();

            // navrat GUI
            return Redirect($"/person/detail/{person.Id}");
        }

        public IActionResult Edit(int id)
        {
            // najít osobu z DB
            var person = _context.Persons.Find(id);

            // zobrazit editacni formular
            return View(person);
        }

        [HttpPost]
        public  IActionResult Edit(Person formPerson)
        {
            // najit osobu v db
            var dbPerson = _context.Persons.Find(formPerson.Id);

            // ulozit zmenenou ososbu do db
            dbPerson.FirstName  = formPerson.FirstName;
            dbPerson.LastName = formPerson.LastName;
            dbPerson.Email = formPerson.Email;
            dbPerson.DateOfBirth= formPerson.DateOfBirth;
            _context.SaveChanges();

            // zobrazit zmeny
            ViewData["success_msg"] = "Úspěšně uloženo do databáze";
            return View(dbPerson);
        }

    }
}
