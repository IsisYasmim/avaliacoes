namespace avaliacoes_service.Models.DTOs
{
    public class CreateAvaliacaoDTO
    {
        public int EventoId { get; set; }
        public int PalestranteId { get; set; }
        public int Pontuacao { get; set; }
        public string? Comentario { get; set; }
    }
}
