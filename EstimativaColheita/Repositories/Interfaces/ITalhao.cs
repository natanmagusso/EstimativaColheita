using EstimativaColheita.Models;
using X.PagedList;

namespace EstimativaColheita.Repositories.Interfaces
{
    /// <summary>
    /// Classe interface dos métodos de transações dos dados dos talhões.
    /// </summary>
    public interface ITalhao
    {
        /// <summary>
        /// Método buscar todos os registros.
        /// </summary>
        PagedList<TalhaoModel> ConsultarTodosTalhoesAsync(int? pagina);

        /// <summary>
        /// Método buscar todos os registros ativos.
        /// </summary>
        IEnumerable<TalhaoModel> ConsultarTalhoesAtivosAsync();

        /// <summary>
        /// Método que retorna o talhão através do código interno do contrato informado.
        /// </summary>
        /// <param name="codigoContrato">Código interno do contrato.</param>
        IEnumerable<TalhaoModel> ConsultarTalhoesAtivosContratoAsync(int codigoContrato);

        /// <summary>
        /// Método que retorna o talhão através do id informado.
        /// </summary>
        /// <param name="id">Id do talhão.</param>
        Task<TalhaoModel> ConsultarTalhaoIdAsync(int id);

        /// <summary>
        /// Método que retorna o talhão através do código interno informado.
        /// </summary>
        /// <param name="codigoInterno">Código interno do talhão.</param>
        Task<TalhaoModel> ConsultarTalhaoCodigoInternoAsync(int codigoInterno);

        /// <summary>
        /// Método inserir.
        /// </summary>
        /// <param name="request">Classe request do talhão.</param>
        Task<int> InserirTalhaoAsync(TalhaoModel request);

        /// <summary>
        /// Método alterar.
        /// </summary>
        /// <param name="request">Classe request do talhão.</param>
        /// <param name="id">Id do talhão.</param>
        Task AlterarTalhaoAsync(TalhaoModel request, int id);

        /// <summary>
        /// Método para desabilitar o talhão.
        /// </summary>
        /// <param name="id">Id do talhão.</param>
        Task DesabilitarTalhaoAsync(int id);

        /// <summary>
        /// Método para habilitar o talhão.
        /// </summary>
        /// <param name="id">Id do talhão.</param>
        Task HabilitarTalhaoAsync(int id);
    }
}