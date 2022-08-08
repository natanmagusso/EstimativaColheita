using EstimativaColheita.Models;
using X.PagedList;

namespace EstimativaColheita.Repositories.Interfaces
{
    /// <summary>
    /// Classe interface dos métodos de transações dos dados dos encarregados.
    /// </summary>
    public interface IEncarregado
    {
        /// <summary>
        /// Método buscar todos os registros.
        /// </summary>
        PagedList<EncarregadoModel> ConsultarTodosEncarregadosAsync(int? pagina);

        /// <summary>
        /// Método buscar todos os registros ativos.
        /// </summary>
        IEnumerable<EncarregadoModel> ConsultarEncarregadosAtivosAsync();

        /// <summary>
        /// Método que retorna o encarregado através do id informado.
        /// </summary>
        /// <param name="id">Id do encarregado.</param>
        Task<EncarregadoModel> ConsultarEncarregadoIdAsync(int id);

        /// <summary>
        /// Método que retorna o encarregado através do código interno informado.
        /// </summary>
        /// <param name="codigoInterno">Código interno do encarregado.</param>
        Task<EncarregadoModel> ConsultarEncarregadoCodigoInternoAsync(int codigoInterno);

        /// <summary>
        /// Método inserir.
        /// </summary>
        /// <param name="request">Classe request do encarregado.</param>
        Task<int> InserirEncarregadoAsync(EncarregadoModel request);

        /// <summary>
        /// Método alterar.
        /// </summary>
        /// <param name="request">Classe request do encarregado.</param>
        /// <param name="id">Id do encarregado.</param>
        Task AlterarEncarregadoAsync(EncarregadoModel request, int id);

        /// <summary>
        /// Método para desabilitar o encarregado.
        /// </summary>
        /// <param name="id">Id do encarregado.</param>
        Task DesabilitarEncarregadoAsync(int id);

        /// <summary>
        /// Método para habilitar o encarregado.
        /// </summary>
        /// <param name="id">Id do encarregado.</param>
        Task HabilitarEncarregadoAsync(int id);
    }
}