using EstimativaColheita.Models;

namespace EstimativaColheita.Repositories.Interfaces
{
    /// <summary>
    /// Classe interface dos métodos de transações dos dados dos motivos de alterãções.
    /// </summary>
    public interface IEstimativaMotivo
    {
        /// <summary>
        /// Método buscar todos os registros.
        /// </summary>
        Task<List<EstimativaMotivoModel>> ConsultarTodosMotivosEstimativaAsync();

        /// <summary>
        /// Método buscar todos os registros ativos.
        /// </summary>
        IEnumerable<EstimativaMotivoModel> ConsultarMotivosEstimativasAtivosAsync();

        /// <summary>
        /// Método que retorna o motivo de estimativa através do id informado.
        /// </summary>
        /// <param name="id">Id do motivo de estimativa.</param>
        Task<EstimativaMotivoModel> ConsultarMotivoEstimativaIdAsync(int id);

        /// <summary>
        /// Método inserir.
        /// </summary>
        /// <param name="request">Classe request do motivo de estimativa.</param>
        Task<int> InserirMotivoEstimativaAsync(EstimativaMotivoModel request);

        /// <summary>
        /// Método alterar.
        /// </summary>
        /// <param name="request">Classe request do motivo de estimativa.</param>
        /// <param name="id">Id do motivo de estimativa.</param>
        Task AlterarMotivoEstimativaAsync(EstimativaMotivoModel request, int id);

        /// <summary>
        /// Método para desabilitar o motivo de estimativa.
        /// </summary>
        /// <param name="id">Id do motivo de estimativa.</param>
        Task DesabilitarMotivoEstimativaAsync(int id);

        /// <summary>
        /// Método para habilitar o motivo de estimativa.
        /// </summary>
        /// <param name="id">Id do motivo de estimativa.</param>
        Task HabilitarMotivoEstimativaAsync(int id);
    }
}