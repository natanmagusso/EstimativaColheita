using EstimativaColheita.Repositories.Interfaces;
using EstimativaColheita.Models;
using EstimativaColheita.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EstimativaColheita.Repositories.Services
{
    /// <summary>
    /// Classe de repositório dos métodos de transação de dados das estimativas de colheitas.
    /// </summary>
    public class EstimativaColheitaService : IEstimativaColheita
    {
        /// <summary>
        /// Declaração de variável referente a classe AppDbContext.
        /// </summary>
        private readonly AppDbContext _appContext;

        /// <summary>
        /// Recebe a instância da classe DataContext.
        /// </summary>
        public EstimativaColheitaService(AppDbContext appContext)
        {
            _appContext = appContext;
        }

        /// <summary>
        /// Método buscar todos os registros.
        /// </summary>
        public async Task<List<EstimativaColheitaModel>> ConsultarTodasEstimativasColheitaAsync()
        {
            return await _appContext
                .EstimativasColheita
                .AsNoTracking()
                .Include(est => est.Contrato)
                .Include(est => est.Talhao)
                .Include(est => est.Encarregado)
                .Include(est => est.EstimativaMotivo)
                .Include(est => est.TipoLancamento)
                .OrderBy(est => est.DataLancamento)
                .ToListAsync();
        }

        /// <summary>
        /// Método que retorna a estimativa de colheita através do id informado.
        /// </summary>
        /// <param name="id">Id da estimativa de colheita.</param>
        public async Task<EstimativaColheitaModel> ConsultarEstimativaColheitaIdAsync(int id)
        {
            return await _appContext
                .EstimativasColheita
                .AsNoTracking()
                .Include(est => est.Contrato)
                .Include(est => est.Talhao)
                .Include(est => est.Encarregado)
                .Include(est => est.EstimativaMotivo)
                .Include(est => est.TipoLancamento)
                .FirstOrDefaultAsync(est => est.Id == id);
        }

        /// <summary>
        /// Método inserir.
        /// </summary>
        /// <param name="request">Classe request da estimativa de colheita.</param>
        public async Task<int> InserirEstimativaColheitaAsync(EstimativaColheitaModel request)
        {
            var inserir = new EstimativaColheitaModel()
            {
                IdEncarregado = request.IdEncarregado,
                IdContrato = request.IdContrato,
                IdTalhao = request.IdTalhao,
                IdEstimativaMotivo = request.IdEstimativaMotivo,
                IdTipoLancamento = request.IdTipoLancamento,
                DataLancamento = request.DataLancamento,
                Caixas = request.Caixas
            };

            _appContext.EstimativasColheita.Add(inserir);
            await _appContext.SaveChangesAsync();
            return inserir.Id;
        }
    }
}