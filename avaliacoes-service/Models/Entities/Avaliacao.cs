namespace avaliacoes_service.Models.Entities
{
    public class Avaliacao
    {
        public int Id { get; set; }
        public int EventoId { get; set; }
        public int PalestranteId { get; set; }
        public int UsuarioId { get; set; }
        public int Pontuacao { get; set; }
        public string? Comentario { get; set; }
        public DateTime Data { get; set; }
    }
}
