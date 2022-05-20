using EstimativaColheita.Repositories.Interfaces;
using EstimativaColheita.Models;
using EstimativaColheita.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EstimativaColheita.Repositories.Services
{
    /// <summary>
    /// Classe de repositório dos métodos de transação de dados dos encarregados.
    /// </summary>
    public class EncarregadoService : IEncarregado
    {
        /// <summary>
        /// Declaração de variável referente a classe AppDbContext.
        /// </summary>
        private readonly AppDbContext _appContext;

        /// <summary>
        /// Recebe a instância da classe DataContext.
        /// </summary>
        public EncarregadoService(AppDbContext appContext)
        {
            _appContext = appContext;
        }

        /// <summary>
        /// Método buscar todos os registros.
        /// </summary>
        public async Task<List<EncarregadoModel>> ConsultarTodosEncarregadosAsync()
        {
            return await _appContext
                .Encarregados
                .AsNoTracking()
                .Include(enc => enc.FiscalCampo)
                .OrderBy(enc => enc.CodigoInterno)
                .ToListAsync();
        }

        /// <summary>
        /// Método buscar todos os registros ativos.
        /// </summary>
        public IEnumerable<EncarregadoModel> ConsultarEncarregadosAtivosAsync()
        {
            return _appContext
                .Encarregados
                .AsNoTracking()
                .Include(enc => enc.FiscalCampo)
                .Where(enc => enc.Ativo == true)
                .OrderBy(enc => enc.CodigoInterno);
        }

        /// <summary>
        /// Método que retorna o encarregado através do id informado.
        /// </summary>
        /// <param name="id">Id do encarregado.</param>
        public async Task<EncarregadoModel> ConsultarEncarregadoIdAsync(int id)
        {
            return await _appContext
                .Encarregados
                .AsNoTracking()
                .Include(enc => enc.FiscalCampo)
                .FirstOrDefaultAsync(enc => enc.Id == id);
        }

        /// <summary>
        /// Método que retorna o encarregado através do código interno informado.
        /// </summary>
        /// <param name="codigoInterno">Código interno do encarregado.</param>
        public async Task<EncarregadoModel> ConsultarEncarregadoCodigoInternoAsync(int codigoInterno)
        {
            return await _appContext
                .Encarregados
                .AsNoTracking()
                .Include(enc => enc.FiscalCampo)
                .FirstOrDefaultAsync(enc => enc.CodigoInterno == codigoInterno);
        }

        /// <summary>
        /// Método inserir.
        /// </summary>
        /// <param name="request">Classe request do encarregado.</param>
        public async Task<int> InserirEncarregadoAsync(EncarregadoModel request)
        {
            var inserir = new EncarregadoModel()
            {
                IdFiscalCampo = request.IdFiscalCampo,
                CodigoInterno = request.CodigoInterno,
                Nome = request.Nome.ToUpper(),
                Ativo = true,
            };

            _appContext.Encarregados.Add(inserir);
            await _appContext.SaveChangesAsync();
            return inserir.Id;
        }

        /// <summary>
        /// Método alterar.
        /// </summary>
        /// <param name="request">Classe request do encarregado.</param>
        /// <param name="id">Id do encarregado.</param>
        public async Task AlterarEncarregadoAsync(EncarregadoModel request, int id)
        {
            var alterar = await _appContext.Encarregados.FindAsync(id);
            alterar.IdFiscalCampo = request.IdFiscalCampo;
            alterar.CodigoInterno = request.CodigoInterno;
            alterar.Nome = request.Nome.ToUpper();

            _appContext.Encarregados.Update(alterar);
            await _appContext.SaveChangesAsync();
        }

        /// <summary>
        /// Método para desabilitar o encarregado.
        /// </summary>
        /// <param name="id">Id do encarregado.</param>
        public async Task DesabilitarEncarregadoAsync(int id)
        {
            var desabilitar = await _appContext.Encarregados.FindAsync(id);
            desabilitar.Ativo = false;

            _appContext.Encarregados.Update(desabilitar);
            await _appContext.SaveChangesAsync();
        }

        /// <summary>
        /// Método para habilitar o encarregado.
        /// </summary>
        /// <param name="id">Id do encarregado.</param>
        public async Task HabilitarEncarregadoAsync(int id)
        {
            var habilitar = await _appContext.Encarregados.FindAsync(id);
            habilitar.Ativo = true;

            _appContext.Encarregados.Update(habilitar);
            await _appContext.SaveChangesAsync();
        }
    }
}