using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftITOFlix.Data;
using SoftITOFlix.Models;

namespace SoftITOFlix.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SoftITOFlixUsersController : ControllerBase
    {
        private readonly SoftITOFlixContext _context;

        public SoftITOFlixUsersController(SoftITOFlixContext context)
        {
            _context = context;
        }

        // GET: api/SoftITOFlixUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SoftITOFlixUser>>> GetUsers()
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
            return await _context.Users.ToListAsync();
        }

        // GET: api/SoftITOFlixUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SoftITOFlixUser>> GetSoftITOFlixUser(long id)
        {
          if (_context.Users == null)
          {
              return NotFound();
          }
            var softITOFlixUser = await _context.Users.FindAsync(id);

            if (softITOFlixUser == null)
            {
                return NotFound();
            }

            return softITOFlixUser;
        }

        // PUT: api/SoftITOFlixUsers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSoftITOFlixUser(long id, SoftITOFlixUser softITOFlixUser)
        {
            if (id != softITOFlixUser.Id)
            {
                return BadRequest();
            }

            _context.Entry(softITOFlixUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SoftITOFlixUserExists(id))
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

        // POST: api/SoftITOFlixUsers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SoftITOFlixUser>> PostSoftITOFlixUser(SoftITOFlixUser softITOFlixUser)
        {
          if (_context.Users == null)
          {
              return Problem("Entity set 'SoftITOFlixContext.Users'  is null.");
          }
            _context.Users.Add(softITOFlixUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSoftITOFlixUser", new { id = softITOFlixUser.Id }, softITOFlixUser);
        }

        // DELETE: api/SoftITOFlixUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSoftITOFlixUser(long id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var softITOFlixUser = await _context.Users.FindAsync(id);
            if (softITOFlixUser == null)
            {
                return NotFound();
            }

            _context.Users.Remove(softITOFlixUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SoftITOFlixUserExists(long id)
        {
            return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
