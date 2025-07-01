using Microsoft.AspNetCore.Mvc;
using events_service.Data;
using events_service.Models;
using Microsoft.EntityFrameworkCore;

namespace events_service.Controllers
{
    [ApiController]
    [Route("api/events")]
    public class EventosController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event>>> GetEvents([FromServices] AppDbContext context)
        {
            var events = await context.Events.ToListAsync();
            return Ok(events);
        }
    }
}
