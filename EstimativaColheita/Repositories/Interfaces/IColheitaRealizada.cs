using EstimativaColheita.Models;

namespace EstimativaColheita.Repositories.Interfaces
{
    /// <summary>
    /// Classe interface dos métodos de transações dos dados das colheitas realizadas.
    /// </summary>
    public interface IColheitaRealizada
    {
        /// <summary>
        /// Método buscar todos os registros.
        /// </summary>
        Task<List<ColheitaRealizadaModel>> ConsultarTodasColheitasRealizadasAsync();

        /// <summary>
        /// Método inserir.
        /// </summary>
        /// <param name="request">Classe request das colheitas realizadas.</param>
        Task<int> InserirColheitaRealizadaAsync(ColheitaRealizadaModel request);
    }
}