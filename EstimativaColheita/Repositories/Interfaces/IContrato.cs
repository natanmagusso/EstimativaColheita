using EstimativaColheita.Models;
using X.PagedList;

namespace EstimativaColheita.Repositories.Interfaces
{
    /// <summary>
    /// Classe interface dos métodos de transações dos dados dos contratos.
    /// </summary>
    public interface IContrato
    {
        /// <summary>
        /// Método buscar todos os registros.
        /// </summary>
        PagedList<ContratoModel> ConsultarTodosContratosAsync(int? pagina);

        /// <summary>
        /// Método buscar todos os registros ativos.
        /// </summary>
        IEnumerable<ContratoModel> ConsultarContratosAtivosAsync();

        /// <summary>
        /// Método que retorna o contrato através do id informado.
        /// </summary>
        /// <param name="id">Id do contrato.</param>
        Task<ContratoModel> ConsultarContratoIdAsync(int id);

        /// <summary>
        /// Método que retorna o contrato através do código interno informado.
        /// </summary>
        /// <param name="codigoInterno">Código interno do contrato.</param>
        Task<ContratoModel> ConsultarContratoCodigoInternoAsync(int codigoInterno);

        /// <summary>
        /// Método inserir.
        /// </summary>
        /// <param name="request">Classe request do contrato.</param>
        Task<int> InserirContratoAsync(ContratoModel request);

        /// <summary>
        /// Método alterar.
        /// </summary>
        /// <param name="request">Classe request do contrato.</param>
        /// <param name="id">Id do contrato.</param>
        Task AlterarContratoAsync(ContratoModel request, int id);

        /// <summary>
        /// Método para desabilitar o contrato.
        /// </summary>
        /// <param name="id">Id do contrato.</param>
        Task DesabilitarContratoAsync(int id);

        /// <summary>
        /// Método para habilitar o contrato.
        /// </summary>
        /// <param name="id">Id do contrato.</param>
        Task HabilitarContratoAsync(int id);
    }
}