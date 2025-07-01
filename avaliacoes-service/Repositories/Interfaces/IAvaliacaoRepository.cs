using avaliacoes_service.Data;
using avaliacoes_service.Models.Entities;

namespace avaliacoes_service.Repositories.Interfaces
{
    public interface IAvaliacaoRepository
    {
        Task<Avaliacao> AddAsync(Avaliacao avaliacao, AppDbContext context);
        Task<IEnumerable<Avaliacao>> GetRatingById(int ratingId, AppDbContext context);

        /*Task<IEnumerable<Avaliacao>> GetByEventoIdAsync(int eventoId);
        Task<IEnumerable<Avaliacao>> GetByPalestranteIdAsync(int palestranteId);
        Task<Avaliacao?> UpdateAsync(Avaliacao avaliacao);
        Task<bool> DeleteAsync(int id);*/
    }
}
