using Microsoft.AspNetCore.Mvc;
using System;
using Veebipood.Data;
using Veebipood.Models;

namespace Veebipood.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<Product> GetProduct()
        {
            var prod = _context.Products.ToList();
            return prod;
        }

        [HttpPost]
        public List<Product> PostProduct([FromBody] Product prod)
        {
            _context.Products.Add(prod);
            _context.SaveChanges();
            return _context.Products.ToList();
        }

        [HttpDelete("{id}")]
        public List<Product> DeleteProduct(int id)
        {
            var prod = _context.Persons.Find(id); //ищет по id

            if (prod == null) //если нет
            {
                return _context.Products.ToList();
            }

            _context.Persons.Remove(prod); //udalenie
            _context.SaveChanges();
            return _context.Products.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Product> GetProduct(int id) //работа с одной записью
        {
            var prod = _context.Products.Find(id);

            if (prod == null)
            {
                return NotFound();
            }

            return prod;
        }

        [HttpPut("{id}")]
        public ActionResult<List<Product>> PutProduct(int id, [FromBody] Product updatedProduct) //Изменение записи 
        {
            var prod = _context.Products.Find(id);

            if (prod == null)
            {
                return NotFound();
            }

            prod.Name = updatedProduct.Name;
            prod.Price = updatedProduct.Price;
            prod.Image = updatedProduct.Image;
            prod.Active = updatedProduct.Active;
            prod.Stock = updatedProduct.Stock;
            prod.CategoryId = updatedProduct.CategoryId;

            _context.Products.Update(prod);
            _context.SaveChanges();

            return Ok(_context.Products);
        }
    }
}
