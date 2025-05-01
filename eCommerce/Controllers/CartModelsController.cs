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
using Microsoft.AspNetCore.Authorization;

namespace eCommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyPolicy")]
    public class CartModelsController : ControllerBase
    {
        private readonly eCommerceContext _context;

        public CartModelsController(eCommerceContext context)
        {
            _context = context;
        }

        // GET: api/CartModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CartModel>>> GetCartModels()
        {
          if (_context.CartModel == null)
          {
              return NotFound();
          }
            return await _context.CartModel.ToListAsync();
        }

        // GET: api/CartModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CartModel>> GetCartModel(string id)
        {
          if (_context.CartModel == null)
          {
              return NotFound();
          }
            var cartModel = await _context.CartModel.FindAsync(id);

            if (cartModel == null)
            {
                return NotFound();
            }

            return cartModel;
        }

        // PUT: api/CartModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCartModel(int id, CartModel cartModel)
        {
            if (id != cartModel.CartItemId)
            {
                return BadRequest();
            }

            _context.Entry(cartModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CartModelExists(id))
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

        // POST: api/CartModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<CartModel>> PostCartModel(CartModel cartModel)
        {
          if (_context.CartModel == null)
          {
              return Problem("Entity set 'eCommerceContext.CartModels'  is null.");
          }
            _context.CartModel.Add(cartModel);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CartModelExists(cartModel.CartItemId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCartModel", new { id = cartModel.CartItemId }, cartModel);
        }

        // DELETE: api/CartModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCartModel(string id)
        {
            if (_context.CartModel == null)
            {
                return NotFound();
            }
            var cartModel = await _context.CartModel.FindAsync(id);
            if (cartModel == null)
            {
                return NotFound();
            }

            _context.CartModel.Remove(cartModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CartModelExists(int id)
        {
            return (_context.CartModel?.Any(e => e.CartItemId == id)).GetValueOrDefault();
        }
    }
}
