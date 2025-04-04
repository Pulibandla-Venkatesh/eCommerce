﻿using System;
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
    public class LoginModelsController : ControllerBase
    {
        private readonly eCommerceContext _context;

        public LoginModelsController(eCommerceContext context)
        {
            _context = context;
        }

        // GET: api/LoginModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LoginModel>>> GetLoginModels()
        {
          if (_context.LoginModel == null)
          {
              return NotFound();
          }
            return await _context.LoginModel.ToListAsync();
        }

        // GET: api/LoginModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LoginModel>> GetLoginModel(string id)
        {
          if (_context.LoginModel == null)
          {
              return NotFound();
          }
            var loginModel = await _context.LoginModel.FindAsync(id);

            if (loginModel == null)
            {
                return NotFound();
            }

            return loginModel;
        }

        // PUT: api/LoginModels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLoginModel(string id, LoginModel loginModel)
        {
            if (id != loginModel.Email)
            {
                return BadRequest();
            }

            _context.Entry(loginModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoginModelExists(id))
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

        // POST: api/LoginModels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LoginModel>> PostLoginModel(LoginModel loginModel)
        {
          if (_context.LoginModel == null)
          {
              return Problem("Entity set 'eCommerceContext.LoginModels'  is null.");
          }
            _context.LoginModel.Add(loginModel);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (LoginModelExists(loginModel.Email))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetLoginModel", new { id = loginModel.Email }, loginModel);
        }

        // DELETE: api/LoginModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLoginModel(string id)
        {
            if (_context.LoginModel == null)
            {
                return NotFound();
            }
            var loginModel = await _context.LoginModel.FindAsync(id);
            if (loginModel == null)
            {
                return NotFound();
            }

            _context.LoginModel.Remove(loginModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LoginModelExists(string id)
        {
            return (_context.LoginModel?.Any(e => e.Email == id)).GetValueOrDefault();
        }
    }
}
