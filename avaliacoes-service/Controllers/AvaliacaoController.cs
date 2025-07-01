using avaliacoes_service.Data;
using avaliacoes_service.Models.DTOs;
using avaliacoes_service.Models.Entities;
using avaliacoes_service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace avaliacoes_service.Controllers
{
    [ApiController]
    [Route("api/avaliacoes-service")]
    public class AvaliacaoController : ControllerBase
    {
        private readonly AvaliacaoService _service;

        public AvaliacaoController(AvaliacaoService service)
        {
            _service = service;
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Avaliacao>> Post(
            [FromBody] CreateAvaliacaoDTO dto,
            [FromServices] AppDbContext context)
        {
            // Improved user ID extraction
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out int parsedUserId))
            {
                return Unauthorized(new { message = "ID do usuário inválido ou não fornecido" });
            }

            // Validate DTO
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var avaliacao = await _service.CriarAvaliacaoAsync(dto, context, parsedUserId);
                return CreatedAtAction(nameof(Post), new { id = avaliacao.Id }, avaliacao);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("/{ratingId:int}")]
        public async Task<ActionResult<IEnumerable<Avaliacao>>> GetRatingById([FromRoute] int ratingId, [FromServices] AppDbContext context)
        {
            var avaliacoes = await _service.GetRatingByIdAsync(ratingId, context);
            return Ok(avaliacoes);
        }

        [HttpGet("events/names")]
        public async Task<ActionResult<IEnumerable<string>>> GetNamesEvents()
        {
            var names = await _service.ListEventNamesAsync();
            return Ok(names);
        }


    }
}
