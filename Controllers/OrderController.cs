using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Veebipood.Data;
using Veebipood.Models;

namespace Veebipood.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public List<Order> GetOrder()
        {
            var odr = _context.Orders.ToList();
            return odr;
        }
        [HttpPost]
        public ActionResult<List<Order>> PostOrder([FromBody] Order odr) //добавление
        {
            odr.created = DateTime.Now;
            _context.Orders.Add(odr);
            _context.SaveChanges();

            var person = _context.Persons.Include(a => a.Id).SingleOrDefault(a => a.Id == odr.Id);

            if (person == null)
            {
                return NotFound();
            }

            return Ok(person.Id);
        }
        
        [HttpDelete("{id}")]
        public List<Order> DeleteOrder(int id)
        {
            var odr = _context.Orders.Find(id);

            if (odr == null)
            {
                return _context.Orders.ToList();
            }

            _context.Orders.Remove(odr);
            _context.SaveChanges();
            return _context.Orders.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Order> GetOrder(int id)
        {
            var odr = _context.Orders.Find(id);

            if (odr == null)
            {
                return NotFound();
            }

            return odr;
        }

        [HttpPut("{id}")]
        public ActionResult<List<Order>> PutOrder(int id, [FromBody] Order updatedOrder)
        {
            var odr = _context.Orders.Find(id);

            if (odr == null)
            {
                return NotFound();
            }

            odr.TotalSum = updatedOrder.TotalSum;
            odr.Paid = updatedOrder.Paid;

            _context.Orders.Update(odr);
            _context.SaveChanges();

            return Ok(_context.Orders);
        }

    }
}
