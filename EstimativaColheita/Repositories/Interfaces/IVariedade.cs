using EstimativaColheita.Models;
using X.PagedList;

namespace EstimativaColheita.Repositories.Interfaces
{
    /// <summary>
    /// Classe interface dos métodos de transações dos dados das variedades.
    /// </summary>
    public interface IVariedade
    {
        /// <summary>
        /// Método buscar todos os registros.
        /// </summary>
        PagedList<VariedadeModel> ConsultarTodasVariedadesAsync(int? pagina);

        /// <summary>
        /// Método buscar todos os registros ativos.
        /// </summary>
        IEnumerable<VariedadeModel> ConsultarVariedadesAtivasAsync();

        /// <summary>
        /// Método que retorna a variedade através do id informado.
        /// </summary>
        /// <param name="id">Id da variedade.</param>
        Task<VariedadeModel> ConsultarVariedadeIdAsync(int id);

        /// <summary>
        /// Método que retorna a variedade através do código interno informado.
        /// </summary>
        /// <param name="codigoInterno">Código interno da variedade.</param>
        Task<VariedadeModel> ConsultarVariedadeCodigoInternoAsync(int codigoInterno);

        /// <summary>
        /// Método inserir.
        /// </summary>
        /// <param name="request">Classe request da variedade.</param>
        Task<int> InserirVariedadeAsync(VariedadeModel request);

        /// <summary>
        /// Método alterar.
        /// </summary>
        /// <param name="request">Classe request da variedade.</param>
        /// <param name="id">Id da variedade.</param>
        Task AlterarVariedadeAsync(VariedadeModel request, int id);

        /// <summary>
        /// Método para desabilitar a variedade.
        /// </summary>
        /// <param name="id">Id da variedade.</param>
        Task DesabilitarVariedadeAsync(int id);

        /// <summary>
        /// Método para habilitar a variedade.
        /// </summary>
        /// <param name="id">Id da variedade.</param>
        Task HabilitarVariedadeAsync(int id);
    }
}