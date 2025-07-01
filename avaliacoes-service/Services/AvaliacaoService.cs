using avaliacoes_service.Data;
using avaliacoes_service.Models.DTOs;
using avaliacoes_service.Models.Entities;
using avaliacoes_service.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Text.Json;

namespace avaliacoes_service.Services
{
    public class AvaliacaoService
    {
        private readonly IAvaliacaoRepository _repository;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public AvaliacaoService(IAvaliacaoRepository repository, HttpClient httpClient, IConfiguration config)
        {
            _repository = repository;
            _httpClient = httpClient;
            _config = config;
        }



        public async Task<Avaliacao> CriarAvaliacaoAsync(CreateAvaliacaoDTO dto, AppDbContext context, int userId)
        {
            var avaliacao = new Avaliacao
            {
                EventoId = dto.EventoId,
                PalestranteId = dto.PalestranteId,
                UsuarioId = userId,
                Pontuacao = dto.Pontuacao,
                Comentario = dto.Comentario,
                Data = DateTime.UtcNow
            };

            return await _repository.AddAsync(avaliacao, context);
        }

        public async Task<IEnumerable<Avaliacao>> GetRatingByIdAsync(int ratingId, AppDbContext context)
        {
            return await _repository.GetRatingById(ratingId, context);
        }

        public async Task<bool> VerifyEventAsync(int eventId)
        {
            var eventsBaseUrl = _config["Services:Events"];
            var response = await _httpClient.GetAsync($"{eventsBaseUrl}/api/eventos/{eventId}");
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> VerifySpeakerAsync(int speakerId)
        {
            var speakersBaseUrl = _config["Services:Speakers"];
            var response = await _httpClient.GetAsync($"{speakersBaseUrl}/api/palestrantes/{speakerId}");
            return response.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<string>> ListEventNamesAsync()
        {
            var eventsBaseUrl = _config["Services:Events"];
            var response = await _httpClient.GetAsync($"{eventsBaseUrl}/api/events");

            if (!response.IsSuccessStatusCode)
                return Enumerable.Empty<string>();

            var content = await response.Content.ReadAsStringAsync();

            var events = JsonSerializer.Deserialize<List<EventDTO>>(content,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return events?.Select(e => e.Name) ?? Enumerable.Empty<string>();
        }
    }
}
