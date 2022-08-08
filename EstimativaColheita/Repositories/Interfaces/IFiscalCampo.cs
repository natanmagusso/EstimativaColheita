using EstimativaColheita.Models;
using X.PagedList;

namespace EstimativaColheita.Repositories.Interfaces
{
    /// <summary>
    /// Classe interface dos métodos de transações dos dados dos fiscais de campo.
    /// </summary>
    public interface IFiscalCampo
    {
        /// <summary>
        /// Método buscar todos os registros.
        /// </summary>
        PagedList<FiscalCampoModel> ConsultarTodosFiscaisCampoAsync(int? pagina);

        /// <summary>
        /// Método buscar todos os registros ativos.
        /// </summary>
        IEnumerable<FiscalCampoModel> ConsultarFiscaisCampoAtivosAsync();

        /// <summary>
        /// Método que retorna o fiscal de campo através do id informado.
        /// </summary>
        /// <param name="id">Id do fiscal de campo.</param>
        Task<FiscalCampoModel> ConsultarFiscalCampoIdAsync(int id);

        /// <summary>
        /// Método que retorna o fiscal de campo através do código interno informado.
        /// </summary>
        /// <param name="codigoInterno">Código interno do fiscal de campo.</param>
        Task<FiscalCampoModel> ConsultarFiscalCampoCodigoInternoAsync(int codigoInterno);

        /// <summary>
        /// Método inserir.
        /// </summary>
        /// <param name="request">Classe request do fiscal de campo.</param>
        Task<int> InserirFiscalCampoAsync(FiscalCampoModel request);

        /// <summary>
        /// Método alterar.
        /// </summary>
        /// <param name="request">Classe request do fiscal de campo.</param>
        /// <param name="id">Id do fiscal de campo.</param>
        Task AlterarFiscalCampoAsync(FiscalCampoModel request, int id);

        /// <summary>
        /// Método para desabilitar o fiscal de campo.
        /// </summary>
        /// <param name="id">Id do fiscal de campo.</param>
        Task DesabilitarFiscalCampoAsync(int id);

        /// <summary>
        /// Método para habilitar o fiscal de campo.
        /// </summary>
        /// <param name="id">Id do fiscal de campo.</param>
        Task HabilitarFiscalCampoAsync(int id);
    }
}