using Microsoft.AspNetCore.Mvc;
using Veebipood.Data;
using Veebipood.Models;

namespace Veebipood.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<Category> GetCategory()
        {
            var cat = _context.Categories.ToList();
            return cat;
        }

        [HttpPost]
        public List<Category> PostCategory([FromBody] Category cat)
        {
            _context.Categories.Add(cat);
            _context.SaveChanges();
            return _context.Categories.ToList();
        }

        [HttpDelete("{id}")]
        public List<Category> DeleteCategory(int id)
        {
            var cat = _context.Categories.Find(id); //ищет по id

            if (cat == null) //если нет
            {
                return _context.Categories.ToList();
            }

            _context.Categories.Remove(cat); //udalenie
            _context.SaveChanges();
            return _context.Categories.ToList();
        }
        
        [HttpGet("{id}")]
        public ActionResult<Category> GetCategory(int id) //работа с одной записью
        {
            var cat = _context.Categories.Find(id);

            if (cat == null)
            {
                return NotFound();
            }

            return cat;
        }

        [HttpPut("{id}")]
        public ActionResult<List<Category>> PutCategory(int id, [FromBody] Category updatedCategory) //Изменение записи 
        {
            var cat = _context.Categories.Find(id);

            if (cat == null)
            {
                return NotFound();
            }

            cat.Name = updatedCategory.Name;

            _context.Categories.Update(cat);
            _context.SaveChanges();

            return Ok(_context.Categories);
        }
    }
}
