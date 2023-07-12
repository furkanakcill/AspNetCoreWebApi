using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventWebApi.Models;

namespace EventWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventUsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EventUsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventUser>>> GetEventUsers()
        {
            return await _context.EventUsers.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EventUser>> GetEventUser(int id)
        {
            var eventUser = await _context.EventUsers.FindAsync(id);

            if (eventUser == null)
            {
                return NotFound();
            }

            return eventUser;
        }

        [HttpPost]
        public async Task<ActionResult<EventUser>> CreateEventUser([FromQuery]EventUser eventUser)
        {
            _context.EventUsers.Add(eventUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEventUser), new { id = eventUser.Id }, eventUser);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEventUser(int id, [FromQuery] EventUser eventUser)
        {
            if (id != eventUser.Id)
            {
                return BadRequest();
            }

            _context.Entry(eventUser).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEventUser(int id)
        {
            var eventUser = await _context.EventUsers.FindAsync(id);

            if (eventUser == null)
            {
                return NotFound();
            }

            _context.EventUsers.Remove(eventUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }

}
