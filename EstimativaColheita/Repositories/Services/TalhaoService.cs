using EstimativaColheita.Repositories.Interfaces;
using EstimativaColheita.Models;
using EstimativaColheita.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EstimativaColheita.Repositories.Services
{
    /// <summary>
    /// Classe de repositório dos métodos de transação de dados dos talhões.
    /// </summary>
    public class TalhaoService : ITalhao
    {
        /// <summary>
        /// Declaração de variável referente a classe AppDbContext.
        /// </summary>
        private readonly AppDbContext _appContext;

        /// <summary>
        /// Recebe a instância da classe DataContext.
        /// </summary>
        public TalhaoService(AppDbContext appContext)
        {
            _appContext = appContext;
        }

        /// <summary>
        /// Método buscar todos os registros.
        /// </summary>
        public async Task<List<TalhaoModel>> ConsultarTodosTalhoesAsync()
        {
            return await _appContext
                .Talhoes
                .AsNoTracking()
                .Include(tal => tal.Contrato)
                .Include(tal => tal.Variedade)
                .OrderBy(tal => tal.CodigoInterno)
                .ToListAsync();
        }

        /// <summary>
        /// Método buscar todos os registros ativos.
        /// </summary>
        public IEnumerable<TalhaoModel> ConsultarTalhoesAtivosAsync()
        {
            return _appContext
                .Talhoes
                .AsNoTracking()
                .Include(tal => tal.Contrato)
                .Include(tal => tal.Variedade)
                .Where(tal => tal.Ativo == true)
                .OrderBy(tal => tal.CodigoInterno);
        }

        /// <summary>
        /// Método que retorna o talhão através do código interno do contrato informado.
        /// </summary>
        /// <param name="codigoContrato">Código interno do contrato.</param>
        public IEnumerable<TalhaoModel> ConsultarTalhoesAtivosContratoAsync(int codigoContrato)
        {
            return _appContext
                .Talhoes
                .AsNoTracking()
                .Include(tal => tal.Contrato)
                .Include(tal => tal.Variedade)
                .Where(tal => tal.Ativo == true && tal.IdContrato == codigoContrato)
                .OrderBy(tal => tal.CodigoInterno);
        }

        /// <summary>
        /// Método que retorna talhão através do id informado.
        /// </summary>
        /// <param name="id">Id do talhão.</param>
        public async Task<TalhaoModel> ConsultarTalhaoIdAsync(int id)
        {
            return await _appContext
                .Talhoes
                .AsNoTracking()
                .Include(tal => tal.Contrato)
                .Include(tal => tal.Variedade)
                .FirstOrDefaultAsync(tal => tal.Id == id);
        }

        /// <summary>
        /// Método que retorna talhão através do código interno informado.
        /// </summary>
        /// <param name="codigoInterno">Código interno do talhão.</param>
        public async Task<TalhaoModel> ConsultarTalhaoCodigoInternoAsync(int codigoInterno)
        {
            return await _appContext
                .Talhoes
                .AsNoTracking()
                .Include(tal => tal.Contrato)
                .Include(tal => tal.Variedade)
                .FirstOrDefaultAsync(tal => tal.CodigoInterno == codigoInterno);
        }

        /// <summary>
        /// Método inserir.
        /// </summary>
        /// <param name="request">Classe request do talhão.</param>
        public async Task<int> InserirTalhaoAsync(TalhaoModel request)
        {
            var inserir = new TalhaoModel()
            {
                IdContrato = request.IdContrato,
                IdVariedade = request.IdVariedade,
                CodigoInterno = request.CodigoInterno,
                AnoPlantio = request.AnoPlantio,
                QuantidadePes = request.QuantidadePes,
                Ativo = true,
            };

            _appContext.Talhoes.Add(inserir);
            await _appContext.SaveChangesAsync();
            return inserir.Id;
        }

        /// <summary>
        /// Método alterar.
        /// </summary>
        /// <param name="request">Classe request do talhão.</param>
        /// <param name="id">Id do talhão.</param>
        public async Task AlterarTalhaoAsync(TalhaoModel request, int id)
        {
            var alterar = await _appContext.Talhoes.FindAsync(id);
            alterar.IdVariedade = request.IdVariedade;
            alterar.AnoPlantio = request.AnoPlantio;
            alterar.QuantidadePes = request.QuantidadePes;

            _appContext.Talhoes.Update(alterar);
            await _appContext.SaveChangesAsync();
        }

        /// <summary>
        /// Método para desabilitar o talhão.
        /// </summary>
        /// <param name="id">Id do talhão.</param>
        public async Task DesabilitarTalhaoAsync(int id)
        {
            var desabilitar = await _appContext.Talhoes.FindAsync(id);
            desabilitar.Ativo = false;

            _appContext.Talhoes.Update(desabilitar);
            await _appContext.SaveChangesAsync();
        }

        /// <summary>
        /// Método para habilitar o talhão.
        /// </summary>
        /// <param name="id">Id do talhão.</param>
        public async Task HabilitarTalhaoAsync(int id)
        {
            var habilitar = await _appContext.Talhoes.FindAsync(id);
            habilitar.Ativo = true;

            _appContext.Talhoes.Update(habilitar);
            await _appContext.SaveChangesAsync();
        }
    }
}