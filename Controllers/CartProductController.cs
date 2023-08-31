using Microsoft.AspNetCore.Mvc;
using Veebipood.Data;
using Veebipood.Models;

namespace Veebipood.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CartProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CartProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<CartProduct> GetCart()
        {
            var cart = _context.CartProducts.ToList();
            return cart;
        }

        [HttpPost]
        public List<CartProduct> PostCart([FromBody] CartProduct cart)
        {
            _context.CartProducts.Add(cart);
            _context.SaveChanges();
            return _context.CartProducts.ToList();
        }

        [HttpDelete("{id}")]
        public List<CartProduct> DeleteCart(int id)
        {
            var cart = _context.Persons.Find(id); //ищет по id

            if (cart == null) //если нет
            {
                return _context.CartProducts.ToList();
            }

            _context.Persons.Remove(cart); //udalenie
            _context.SaveChanges();
            return _context.CartProducts.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<CartProduct> GetCart(int id) //работа с одной записью
        {
            var cart = _context.CartProducts.Find(id);

            if (cart == null)
            {
                return NotFound();
            }

            return cart;
        }

        [HttpPut("{id}")]
        public ActionResult<List<CartProduct>> PutCart(int id, [FromBody] CartProduct updatedCart) //Изменение записи 
        {
            var cart = _context.CartProducts.Find(id);

            if (cart == null)
            {
                return NotFound();
            }

            cart.ProductId = updatedCart.ProductId;
            cart.Quantity = updatedCart.Quantity;

            _context.CartProducts.Update(cart);
            _context.SaveChanges();

            return Ok(_context.CartProducts);
        }
    }
}
