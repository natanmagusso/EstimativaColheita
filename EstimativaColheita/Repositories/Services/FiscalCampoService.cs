using EstimativaColheita.Repositories.Interfaces;
using EstimativaColheita.Models;
using EstimativaColheita.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EstimativaColheita.Repositories.Services
{
    /// <summary>
    /// Classe de repositório dos métodos de transação de dados dos fiscais de campo.
    /// </summary>
    public class FiscalCampoService : IFiscalCampo
    {
        /// <summary>
        /// Declaração de variável referente a classe AppDbContext.
        /// </summary>
        private readonly AppDbContext _appContext;

        /// <summary>
        /// Recebe a instância da classe DataContext.
        /// </summary>
        public FiscalCampoService(AppDbContext appContext)
        {
            _appContext = appContext;
        }

        /// <summary>
        /// Método buscar todos os registros.
        /// </summary>
        public async Task<List<FiscalCampoModel>> ConsultarTodosFiscaisCampoAsync()
        {
            return await _appContext
                .FiscaisCampo
                .AsNoTracking()
                .OrderBy(fis => fis.Nome)
                .ToListAsync();
        }

        /// <summary>
        /// Método buscar todos os registros ativos.
        /// </summary>
        public IEnumerable<FiscalCampoModel> ConsultarFiscaisCampoAtivosAsync()
        {
            return _appContext
                .FiscaisCampo
                .AsNoTracking()
                .Where(fis => fis.Ativo == true)
                .OrderBy(fis => fis.Nome);
        }

        /// <summary>
        /// Método que retorna o fiscal de campo através do id informado.
        /// </summary>
        /// <param name="id">Id do fiscal de campo.</param>
        public async Task<FiscalCampoModel> ConsultarFiscalCampoIdAsync(int id)
        {
            return await _appContext
                .FiscaisCampo
                .AsNoTracking()
                .FirstOrDefaultAsync(fis => fis.Id == id);
        }

        /// <summary>
        /// Método que retorna o fiscal de campo através do código interno informado.
        /// </summary>
        /// <param name="codigoInterno">Código interno do fiscal de campo.</param>
        public async Task<FiscalCampoModel> ConsultarFiscalCampoCodigoInternoAsync(int codigoInterno)
        {
            return await _appContext
                .FiscaisCampo
                .AsNoTracking()
                .FirstOrDefaultAsync(fis => fis.CodigoInterno == codigoInterno);
        }

        /// <summary>
        /// Método inserir.
        /// </summary>
        /// <param name="request">Classe request do fiscal de campo.</param>
        public async Task<int> InserirFiscalCampoAsync(FiscalCampoModel request)
        {
            var inserir = new FiscalCampoModel()
            {
                CodigoInterno = request.CodigoInterno,
                Nome = request.Nome.ToUpper(),
                Apelido = request.Apelido.ToUpper(),
                Ativo = true,
            };

            _appContext.FiscaisCampo.Add(inserir);
            await _appContext.SaveChangesAsync();
            return inserir.Id;
        }

        /// <summary>
        /// Método alterar.
        /// </summary>
        /// <param name="request">Classe request do fiscal de campo.</param>
        /// <param name="id">Id do fiscal de campo.</param>
        public async Task AlterarFiscalCampoAsync(FiscalCampoModel request, int id)
        {
            var alterar = await _appContext.FiscaisCampo.FindAsync(id);
            alterar.CodigoInterno = request.CodigoInterno;
            alterar.Nome = request.Nome.ToUpper();
            alterar.Apelido = request.Apelido.ToUpper();

            _appContext.FiscaisCampo.Update(alterar);
            await _appContext.SaveChangesAsync();
        }

        /// <summary>
        /// Método para desabilitar o contrato.
        /// </summary>
        /// <param name="id">Id do fiscal de campo.</param>
        public async Task DesabilitarFiscalCampoAsync(int id)
        {
            var desabilitar = await _appContext.FiscaisCampo.FindAsync(id);
            desabilitar.Ativo = false;

            _appContext.FiscaisCampo.Update(desabilitar);
            await _appContext.SaveChangesAsync();
        }

        /// <summary>
        /// Método para habilitar o contrato.
        /// </summary>
        /// <param name="id">Id do fiscal de campo.</param>
        public async Task HabilitarFiscalCampoAsync(int id)
        {
            var habilitar = await _appContext.FiscaisCampo.FindAsync(id);
            habilitar.Ativo = true;

            _appContext.FiscaisCampo.Update(habilitar);
            await _appContext.SaveChangesAsync();
        }
    }
}