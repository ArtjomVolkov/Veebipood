using Microsoft.AspNetCore.Mvc;
using Veebipood.Data;
using Veebipood.Models;

namespace Veebipood.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PersonController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PersonController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<Person> GetPerson()
        {
            var person = _context.Persons.ToList();
            return person;
        }

        [HttpPost]
        public List<Person> PostPerson([FromBody] Person person)
        {
            _context.Persons.Add(person);
            _context.SaveChanges();
            return _context.Persons.ToList();
        }

        [HttpDelete("{id}")]
        public List<Person> DeletePerson(int id)
        {
            var person = _context.Persons.Find(id); //ищет по id

            if (person == null) //если нет
            {
                return _context.Persons.ToList();
            }

            _context.Persons.Remove(person); //udalenie
            _context.SaveChanges();
            return _context.Persons.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Person> GetPerson(int id) //работа с одной записью
        {
            var person = _context.Persons.Find(id);

            if (person == null)
            {
                return NotFound();
            }

            return person;
        }

        [HttpPut("{id}")]
        public ActionResult<List<Person>> PutArtikkel(int id, [FromBody] Person updatedPerson) //Изменение записи 
        {
            var person = _context.Persons.Find(id);

            if (person == null)
            {
                return NotFound();
            }

            person.PersonCode = updatedPerson.PersonCode;
            person.FirstName = updatedPerson.FirstName;
            person.LastName = updatedPerson.LastName;
            person.Phone = updatedPerson.Phone;
            person.Address = updatedPerson.Address;
            person.Password = updatedPerson.Password;
            person.Admin = updatedPerson.Admin;

            _context.Persons.Update(person);
            _context.SaveChanges();

            return Ok(_context.Persons);
        }
    }
}
