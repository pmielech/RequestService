using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RequestService.Data;

namespace RequestService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GymMembershipController : ControllerBase
    {
        private readonly RequestServiceContext _context;

        public GymMembershipController(RequestServiceContext context)
        {
            _context = context;
        }

        // GET: api/GymMembership
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GymMembershipType>>> GetGymMembership()
        {
            return await _context.GymMembershipTypes.ToListAsync();
        }

        // GET: api/GymMembership/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GymMembershipType>> GetGymMembership(uint id)
        {
            if (id == null)
            {
                return NotFound();
            }
            // var gymMembership = await _context.GymMembershipTypes.FindAsync(id);
            var gymMembership = await _context.GymMembershipTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            
            if (gymMembership == null)
            {
                return NotFound();
            }

            return gymMembership;
        }

        // PUT: api/GymMembership/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGymMembership(uint id, GymMembershipType gymMembershipType)
        {
            if (id != gymMembershipType.Id)
            {
                return BadRequest();
            }

            _context.Entry(gymMembershipType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GymMembershipExists(id))
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

        // POST: api/GymMembership
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GymMembershipType>> PostGymMembership(GymMembershipType gymMembershipType)
        {
            // _context.GymMembershipTypes.Add(gymMembershipType);
            if (ModelState.IsValid)
            {
                _context.Add(gymMembershipType);
                await _context.SaveChangesAsync();

                
            }
            
            return CreatedAtAction("GetGymMembership", new { id = gymMembershipType.Id }, gymMembershipType);
        }

        // DELETE: api/GymMembership/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGymMembership(uint id)
        {
            var gymMembership = await _context.GymMembershipTypes.FindAsync(id);
            if (gymMembership == null)
            {
                return NotFound();
            }

            _context.GymMembershipTypes.Remove(gymMembership);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GymMembershipExists(uint id)
        {
            return _context.GymMembershipTypes.Any(e => e.Id == id);
        }
    }
}
