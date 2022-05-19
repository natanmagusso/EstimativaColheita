using EstimativaColheita.Models;

namespace EstimativaColheita.Repositories.Interfaces
{
    /// <summary>
    /// Classe interface dos métodos de transações dos dados dos motivos de alterãções.
    /// </summary>
    public interface IMotivoAlteracao
    {
        /// <summary>
        /// Método buscar todos os registros.
        /// </summary>
        Task<List<MotivoAlteracaoModel>> ConsultarTodosMotivosAlteracoesAsync();

        /// <summary>
        /// Método buscar todos os registros ativos.
        /// </summary>
        IEnumerable<MotivoAlteracaoModel> ConsultarMotivosAlteracoesAtivosAsync();

        /// <summary>
        /// Método que retorna o motivo de alteração através do id informado.
        /// </summary>
        /// <param name="id">Id do motivo de alteração.</param>
        Task<MotivoAlteracaoModel> ConsultarMotivoAlteracaoIdAsync(int id);

        /// <summary>
        /// Método inserir.
        /// </summary>
        /// <param name="request">Classe request do motivo de alteração.</param>
        Task<int> InserirMotivoAlteracaoAsync(MotivoAlteracaoModel request);

        /// <summary>
        /// Método alterar.
        /// </summary>
        /// <param name="request">Classe request do motivo de alteração.</param>
        /// <param name="id">Id do motivo de alteração.</param>
        Task AlterarMotivoAlteracaoAsync(MotivoAlteracaoModel request, int id);

        /// <summary>
        /// Método para desabilitar o motivo de alteração.
        /// </summary>
        /// <param name="id">Id do motivo de alteração.</param>
        Task DesabilitarMotivoAlteracaoAsync(int id);

        /// <summary>
        /// Método para habilitar o motivo de alteração.
        /// </summary>
        /// <param name="id">Id do motivo de alteração.</param>
        Task HabilitarMotivoAlteracaoAsync(int id);
    }
}