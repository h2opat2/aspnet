using InsuranceCorp.Data;
using InsuranceCorp.Model;
using Microsoft.AspNetCore.Authorization;
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
            var top100 = _context.Persons.Include(person => person.Contracts)
                .OrderByDescending(person => person.Contracts.Count())
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
                ViewData["msg"] = "ID: " + id;
                return View("NotFound");
            }

            return View(person);
        }
        [Authorize]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Add(Person person)
        {
            // ulozit osobu do DB
            _context.Persons.Add(person);
            _context.SaveChanges();

            // navrat GUI
            return Redirect($"/person/detail/{person.Id}");
        }
        [Authorize]
        public IActionResult Edit(int id)
        {
            // najít osobu z DB
            var person = _context.Persons.Find(id);

            // zobrazit editacni formular
            return View(person);
        }

        [HttpPost]
        [Authorize]
        public  IActionResult Edit(Person formPerson)
        {
            if (!ModelState.IsValid) { //nevalidni data
            }
                // najit osobu v db
                var dbPerson = _context.Persons.Find(formPerson.Id);

                // ulozit zmenenou ososbu do db
                //1)
                //dbPerson.FirstName  = formPerson.FirstName;
                //dbPerson.LastName = formPerson.LastName;
                //dbPerson.Email = formPerson.Email;
                //dbPerson.DateOfBirth= formPerson.DateOfBirth;

                //2)
                 _context.Entry(dbPerson).CurrentValues.SetValues(formPerson);

                //3)
                //_context.Entry(formPerson).State = EntityState.Modified;

                _context.SaveChanges();

                // zobrazit zmeny
                ViewData["success_msg"] = "Úspěšně uloženo do databáze";
                return View(dbPerson);
        }

        public IActionResult GetByEmail(string email)
        {
            // ziskat data
            var person = _context.Persons
                         .Where(person => person.Email.ToUpper() == email.ToUpper())
                         .FirstOrDefault();

            if(person == null)
                return NotFound();

            // zobrzit data
            return View("Detail", person);

        }

    }
}
