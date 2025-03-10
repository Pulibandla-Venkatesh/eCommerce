using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eCommerce.Models;
using eCommerceDataLayer;
using Microsoft.AspNetCore.Cors;

namespace eCommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyPolicy")]
    public class OrderModelsController : ControllerBase
    {
        private readonly eCommerceContext _context;

        public OrderModelsController(eCommerceContext context)
        {
            _context = context;
        }

        // GET: api/OrderModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderModel>>> GetorderModels()
        {
          if (_context.OrderModel == null)
          {
              return NotFound();
          }
            return await _context.OrderModel.ToListAsync();
        }

        // GET: api/OrderModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderModel>> GetOrderModel(string id)
        {
          if (_context.OrderModel == null)
          {
              return NotFound();
          }
            var orderModel = await _context.OrderModel.FindAsync(id);

            if (orderModel == null)
            {
                return NotFound();
            }

            return orderModel;
        }

        // PUT: api/OrderModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderModel(string id, OrderModel orderModel)
        {
            if (id != orderModel.OrderId)
            {
                return BadRequest();
            }

            _context.Entry(orderModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/OrderModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OrderModel>> PostOrderModel(OrderModel orderModel)
        {
          if (_context.OrderModel == null)
          {
              return Problem("Entity set 'eCommerceContext.orderModels'  is null.");
          }
            _context.OrderModel.Add(orderModel);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (OrderModelExists(orderModel.OrderId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetOrderModel", new { id = orderModel.OrderId }, orderModel);
        }

        // DELETE: api/OrderModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderModel(string id)
        {
            if (_context.OrderModel == null)
            {
                return NotFound();
            }
            var orderModel = await _context.OrderModel.FindAsync(id);
            if (orderModel == null)
            {
                return NotFound();
            }

            _context.OrderModel.Remove(orderModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderModelExists(string id)
        {
            return (_context.OrderModel?.Any(e => e.OrderId == id)).GetValueOrDefault();
        }
    }
}
