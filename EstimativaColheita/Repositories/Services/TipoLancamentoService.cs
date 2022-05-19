using EstimativaColheita.Repositories.Interfaces;
using EstimativaColheita.Persistence;
using Microsoft.EntityFrameworkCore;
using EstimativaColheita.Models;

namespace EstimativaColheita.Repositories.Services
{
    /// <summary>
    /// Classe de repositório dos métodos de transação de dados dos tipos de lançamento.
    /// </summary>
    public class TipoLancamentoService : ITipoLancamento
    {
        /// <summary>
        /// Declaração de variável referente a classe AppDbContext.
        /// </summary>
        private readonly AppDbContext _appContext;

        /// <summary>
        /// Recebe a instância da classe DataContext.
        /// </summary>
        public TipoLancamentoService(AppDbContext appContext)
        {
            _appContext = appContext;
        }

        /// <summary>
        /// Método buscar todos os registros.
        /// </summary>
        public IEnumerable<TipoLancamentoModel> ConsultarTiposLancamentoAsync()
        {
            return _appContext
                .TiposLancamento
                .AsNoTracking()
                .OrderBy(tip => tip.Descricao);
        }

        /// <summary>
        /// Método que retorna o tipo de lançamento através do id informado.
        /// </summary>
        /// <param name="id">Id do tipo de lançamento.</param>
        public async Task<TipoLancamentoModel> ConsultarTipoLancamentoIdAsync(int id)
        {
            return await _appContext
                .TiposLancamento
                .AsNoTracking()
                .FirstOrDefaultAsync(tip => tip.Id == id);
        }
    }
}