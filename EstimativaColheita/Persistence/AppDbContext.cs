using EstimativaColheita.Models;
using Microsoft.EntityFrameworkCore;

namespace EstimativaColheita.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ColheitaRealizadaConfiguration());
            modelBuilder.ApplyConfiguration(new ContratoConfiguration());
            modelBuilder.ApplyConfiguration(new EncarregadoConfiguration());
            modelBuilder.ApplyConfiguration(new EstimativaColheitaConfiguration());
            modelBuilder.ApplyConfiguration(new FiscalCampoConfiguration());
            modelBuilder.ApplyConfiguration(new MotivoAlteracaoConfiguration());
            modelBuilder.ApplyConfiguration(new TalhaoConfiguration());
            modelBuilder.ApplyConfiguration(new TipoLancamentoConfiguration());
            modelBuilder.ApplyConfiguration(new VariedadeConfiguration());

            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
                var connectionString = configuration.GetConnectionString("DBConnectionString");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        public DbSet<ColheitaRealizadaModel> ColheitasRealizadas { get; set; }
        public DbSet<ContratoModel> Contratos { get; set; }
        public DbSet<EncarregadoModel> Encarregados { get; set; }
        public DbSet<EstimativaColheitaModel> EstimativasColheita { get; set; }
        public DbSet<FiscalCampoModel> FiscaisCampo { get; set; }
        public DbSet<MotivoAlteracaoModel> MotivosAlteracoes { get; set; }
        public DbSet<TalhaoModel> Talhoes { get; set; }
        public DbSet<TipoLancamentoModel> TiposLancamento { get; set; }
        public DbSet<VariedadeModel> Variedades { get; set; }
    }
}