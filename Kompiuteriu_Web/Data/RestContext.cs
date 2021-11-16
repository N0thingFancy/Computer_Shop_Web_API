using Kompiuteriu_Web.Data.Dtos.Auth;
using Kompiuteriu_Web.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Kompiuteriu_Web.Data
{
    public class RestContext : IdentityDbContext<RestUser>
    {
        public DbSet<Shop> Shops { get; set; }
        public DbSet<Computer> Computers { get; set; }
        public DbSet<Worker> Workers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog=KompiuteriuDB");
        }
    }
}
