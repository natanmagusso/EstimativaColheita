using EstimativaColheita.Repositories.Interfaces;
using EstimativaColheita.Models;
using EstimativaColheita.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EstimativaColheita.Repositories.Services
{
    /// <summary>
    /// Classe de repositório dos métodos de transação de dados das variedades.
    /// </summary>
    public class VariedadeService : IVariedade
    {
        /// <summary>
        /// Declaração de variável referente a classe AppDbContext.
        /// </summary>
        private readonly AppDbContext _appContext;

        /// <summary>
        /// Recebe a instância da classe DataContext.
        /// </summary>
        public VariedadeService(AppDbContext appContext)
        {
            _appContext = appContext;
        }

        /// <summary>
        /// Método buscar todos os registros.
        /// </summary>
        public async Task<List<VariedadeModel>> ConsultarTodasVariedadesAsync()
        {
            return await _appContext
                .Variedades
                .AsNoTracking()
                .OrderBy(var => var.CodigoInterno)
                .ToListAsync();
        }

        /// <summary>
        /// Método buscar todos os registros ativos.
        /// </summary>
        public IEnumerable<VariedadeModel> ConsultarVariedadesAtivasAsync()
        {
            return _appContext
                .Variedades
                .AsNoTracking()
                .Where(var => var.Ativo == true)
                .OrderBy(var => var.CodigoInterno);
        }

        /// <summary>
        /// Método que retorna a variedade através do id informado.
        /// </summary>
        /// <param name="id">Id da variedade.</param>
        public async Task<VariedadeModel> ConsultarVariedadeIdAsync(int id)
        {
            return await _appContext
                .Variedades
                .AsNoTracking()
                .FirstOrDefaultAsync(var => var.Id == id);
        }

        /// <summary>
        /// Método que retorna variedade através do código interno informado.
        /// </summary>
        /// <param name="codigoInterno">Código interno da variedade.</param>
        public async Task<VariedadeModel> ConsultarVariedadeCodigoInternoAsync(int codigoInterno)
        {
            return await _appContext
                .Variedades
                .AsNoTracking()
                .FirstOrDefaultAsync(var => var.CodigoInterno == codigoInterno);
        }

        /// <summary>
        /// Método inserir.
        /// </summary>
        /// <param name="request">Classe request da variedade.</param>
        public async Task<int> InserirVariedadeAsync(VariedadeModel request)
        {
            var inserir = new VariedadeModel()
            {
                CodigoInterno = request.CodigoInterno,
                Descricao = request.Descricao.ToUpper(),
                Ativo = true
            };

            _appContext.Variedades.Add(inserir);
            await _appContext.SaveChangesAsync();
            return inserir.Id;
        }

        /// <summary>
        /// Método alterar.
        /// </summary>
        /// <param name="request">Classe request da variedade.</param>
        /// <param name="id">Id da variedade.</param>
        public async Task AlterarVariedadeAsync(VariedadeModel request, int id)
        {
            var alterar = await _appContext.Variedades.FindAsync(id);
            alterar.CodigoInterno = request.CodigoInterno;
            alterar.Descricao = request.Descricao.ToUpper();

            _appContext.Variedades.Update(alterar);
            await _appContext.SaveChangesAsync();
        }

        /// <summary>
        /// Método para desabilitar a variedade
        /// </summary>
        /// <param name="id">Id da variedade.</param>
        public async Task DesabilitarVariedadeAsync(int id)
        {
            var desabilitar = await _appContext.Variedades.FindAsync(id);
            desabilitar.Ativo = false;

            _appContext.Variedades.Update(desabilitar);
            await _appContext.SaveChangesAsync();
        }

        /// <summary>
        /// Método para habilitar a variedade.
        /// </summary>
        /// <param name="id">Id da variedade.</param>
        public async Task HabilitarVariedadeAsync(int id)
        {
            var habilitar = await _appContext.Variedades.FindAsync(id);
            habilitar.Ativo = true;

            _appContext.Variedades.Update(habilitar);
            await _appContext.SaveChangesAsync();
        }
    }
}