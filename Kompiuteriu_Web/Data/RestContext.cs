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
            optionsBuilder.UseSqlServer("Server=tcp:kompiuteriu-webdbserver.database.windows.net,1433;Initial Catalog=KompiuteriuDB;Persist Security Info=False;" +
                "User ID=dovjak3;Password=Riesutai4213x;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            //optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog=KompiuteriuDB");
        }
    }
}
