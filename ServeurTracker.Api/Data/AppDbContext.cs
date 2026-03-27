using Microsoft.EntityFrameworkCore;
using ServeurTracker.Api.Models; // Pour qu'il connaisse ta classe Device

namespace ServeurTracker.Api.Data
{
    // On hérite de DbContext, qui vient d'Entity Framework Core
    public class AppDbContext : DbContext
    {
        // Le constructeur qui va recevoir la configuration (comme l'URL de la base de données)
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // C'est ici qu'on déclare nos tables !
        public DbSet<Device> Devices { get; set; }
    }
}