using events_service.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace events_service.Data
{
    public class AppDbContext : DbContext
    {
        private readonly IConfiguration configuration;
        public DbSet<Event> Events { get; set; }

        public AppDbContext(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
    }

}
