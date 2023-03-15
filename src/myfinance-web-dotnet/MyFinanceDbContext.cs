using Microsoft.EntityFrameworkCore;
using myfinance_web_dotnet.Domain.Entities;

namespace myfinance_web_dotnet
{
    public class MyFinanceDbContext: DbContext
    {
        public DbSet<PlanoConta> PlanoConta { get; set; }
        public DbSet<Transacao> Transacao { get; set; }
        public DbSet<TipoPagamento> TipoPagamento { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            var connectionString = @"Server=localhost,1433;Database=myfinance;Trusted_Connection=False;TrustServerCertificate=True;Encrypt=False;User=SA;Password=12345OHdf%e;";
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            
        }
  }

}