using EstimativaColheita.Models;

namespace EstimativaColheita.Repositories.Interfaces
{
    /// <summary>
    /// Classe interface dos métodos de transações das estimativas de colheita.
    /// </summary>
    public interface IEstimativaColheita
    {
        /// <summary>
        /// Método buscar todos os registros.
        /// </summary>
        Task<List<EstimativaColheitaModel>> ConsultarTodasEstimativasColheitaAsync();

        /// <summary>
        /// Método que retorna ocontrato através do id informado.
        /// </summary>
        /// <param name="id">Id da estimativa de colheita.</param>
        Task<EstimativaColheitaModel> ConsultarEstimativaColheitaIdAsync(int id);

        /// <summary>
        /// Método inserir.
        /// </summary>
        /// <param name="request">Classe request da estimativa de colheita.</param>
        Task<int> InserirEstimativaColheitaAsync(EstimativaColheitaModel request);
    }
}