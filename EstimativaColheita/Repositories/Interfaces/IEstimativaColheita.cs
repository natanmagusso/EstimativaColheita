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
        /// Método que retorna as estimtivas de colehita através do contrato e talhão informado.
        /// </summary>
        /// <param name="contrato">Id do contrato.</param>
        /// <param name="talhao">Id do talhão.</param>
        Task<List<EstimativaColheitaModel>> ConsultarEstimativaColheitaContratoTalhaoAsync(int contrato, int talhao);

        /// <summary>
        /// Método que retorna a estimativa de colheita através do id informado.
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