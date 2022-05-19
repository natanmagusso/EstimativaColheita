using EstimativaColheita.Models;

namespace EstimativaColheita.Repositories.Interfaces
{
    /// <summary>
    /// Classe interface dos métodos de transações dos dados dos tipos de lançamentos.
    /// </summary>
    public interface ITipoLancamento
    {
        /// <summary>
        /// Método buscar todos os registros.
        /// </summary>
        IEnumerable<TipoLancamentoModel> ConsultarTiposLancamentoAsync();

        /// <summary>
        /// Método que retorna o tipo de lançamento através do id informado.
        /// </summary>
        /// <param name="id">Id do tipo de lançamento.</param>
        Task<TipoLancamentoModel> ConsultarTipoLancamentoIdAsync(int id);
    }
}