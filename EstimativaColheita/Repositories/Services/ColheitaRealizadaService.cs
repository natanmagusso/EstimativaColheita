using EstimativaColheita.Repositories.Interfaces;
using EstimativaColheita.Models;
using EstimativaColheita.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EstimativaColheita.Repositories.Services
{
    /// <summary>
    /// Classe de repositório dos métodos de transação de dados das colheitas realizadas.
    /// </summary>
    public class ColheitaRealizadaService : IColheitaRealizada
    {
        /// <summary>
        /// Declaração de variável referente a classe AppDbContext.
        /// </summary>
        private readonly AppDbContext _appContext;

        /// <summary>
        /// Recebe a instância da classe DataContext.
        /// </summary>
        public ColheitaRealizadaService(AppDbContext appContext)
        {
            _appContext = appContext;
        }

        /// <summary>
        /// Método buscar todos os registros.
        /// </summary>
        public async Task<List<ColheitaRealizadaModel>> ConsultarTodasColheitasRealizadasAsync()
        {
            return await _appContext
                .ColheitasRealizadas
                .AsNoTracking()
                .Include(col => col.Contrato)
                .Include(col => col.Talhao)
                .Include(col => col.Encarregado)
                .OrderBy(col => col.DataLancamento)
                .ToListAsync();
        }

        /// <summary>
        /// Método inserir.
        /// </summary>
        /// <param name="request">Classe request das colheitas realizadas.</param>
        public async Task<int> InserirColheitaRealizadaAsync(ColheitaRealizadaModel request)
        {
            var inserir = new ColheitaRealizadaModel()
            {
                IdContrato = request.IdContrato,
                IdTalhao = request.IdTalhao,
                IdEncarregado = request.IdEncarregado,
                DataLancamento = DateTime.Now,
                Caixas = request.Caixas,
            };

            _appContext.ColheitasRealizadas.Add(inserir);
            await _appContext.SaveChangesAsync();
            return inserir.Id;
        }
    }
}