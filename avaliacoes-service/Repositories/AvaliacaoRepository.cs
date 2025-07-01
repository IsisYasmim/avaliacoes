using avaliacoes_service.Data;
using avaliacoes_service.Models.Entities;
using avaliacoes_service.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using avaliacoes_service.Models.DTOs;

namespace avaliacoes_service.Repositories
{
    public class AvaliacaoRepository : IAvaliacaoRepository
    {

        public async Task<Avaliacao> AddAsync(Avaliacao avaliacao, AppDbContext context)
        {
            context.Avaliacoes.Add(avaliacao);
            await context.SaveChangesAsync();
            return avaliacao;
        }

        public async Task<IEnumerable<Avaliacao>> GetRatingById(int ratingId, AppDbContext context)
        {
            return await context.Avaliacoes
                .Where(a => a.Id == ratingId)
                .ToListAsync();
        }

    }

}
