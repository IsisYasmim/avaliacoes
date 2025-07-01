using avaliacoes_service.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace avaliacoes_service.Data
{
    public class AppDbContext : DbContext
    {
        private readonly IConfiguration configuration;
        public DbSet<Avaliacao> Avaliacoes { get; set; }

        public AppDbContext(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
    }

}
