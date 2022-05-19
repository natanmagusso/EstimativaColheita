using EstimativaColheita.Repositories.Interfaces;
using EstimativaColheita.Models;
using EstimativaColheita.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EstimativaColheita.Repositories.Services
{
    /// <summary>
    /// Classe de repositório dos métodos de transação de dados dos contratos.
    /// </summary>
    public class ContratoService : IContrato
    {
        /// <summary>
        /// Declaração de variável referente a classe AppDbContext.
        /// </summary>
        private readonly AppDbContext _appContext;

        /// <summary>
        /// Recebe a instância da classe DataContext.
        /// </summary>
        public ContratoService(AppDbContext appContext)
        {
            _appContext = appContext;
        }

        /// <summary>
        /// Método buscar todos os registros.
        /// </summary>
        public async Task<List<ContratoModel>> ConsultarTodosContratosAsync()
        {
            return await _appContext
                .Contratos
                .AsNoTracking()
                .Include(fis => fis.FiscalCampo)
                .OrderBy(con => con.CodigoInterno)
                .ToListAsync();
        }

        /// <summary>
        /// Método buscar todos os registros ativos.
        /// </summary>
        public IEnumerable<ContratoModel> ConsultarContratosAtivosAsync()
        {
            return _appContext
                .Contratos
                .AsNoTracking()
                .Include(fis => fis.FiscalCampo)
                .Where(con => con.Ativo == true)
                .OrderBy(con => con.CodigoInterno);
        }

        /// <summary>
        /// Método que retorna o contrato através do id informado.
        /// </summary>
        /// <param name="id">Id do contrato.</param>
        public async Task<ContratoModel> ConsultarContratoIdAsync(int id)
        {
            return await _appContext
                .Contratos
                .AsNoTracking()
                .Include(fis => fis.FiscalCampo)
                .FirstOrDefaultAsync(con => con.Id == id);
        }

        /// <summary>
        /// Método que retorna o contrato através do código interno informado.
        /// </summary>
        /// <param name="codigoInterno">Código interno do contrato.</param>
        public async Task<ContratoModel> ConsultarContratoCodigoInternoAsync(int codigoInterno)
        {
            return await _appContext
                .Contratos
                .AsNoTracking()
                .Include(fis => fis.FiscalCampo)
                .FirstOrDefaultAsync(con => con.CodigoInterno == codigoInterno);
        }

        /// <summary>
        /// Método inserir.
        /// </summary>
        /// <param name="request">Classe request do contrato.</param>
        public async Task<int> InserirContratoAsync(ContratoModel request)
        {
            var inserir = new ContratoModel()
            {
                IdFiscalCampo = request.IdFiscalCampo,
                CodigoInterno = request.CodigoInterno,
                Propriedade = request.Propriedade.ToUpper(),
                Titular = request.Titular.ToUpper(),
                Ativo = true,
            };

            _appContext.Contratos.Add(inserir);
            await _appContext.SaveChangesAsync();
            return inserir.Id;
        }

        /// <summary>
        /// Método alterar.
        /// </summary>
        /// <param name="request">Classe request do contrato.</param>
        /// <param name="id">Id do contrato.</param>
        public async Task AlterarContratoAsync(ContratoModel request, int id)
        {
            var alterar = await _appContext.Contratos.FindAsync(id);
            alterar.IdFiscalCampo = request.IdFiscalCampo;
            alterar.CodigoInterno = request.CodigoInterno;
            alterar.Propriedade = request.Propriedade.ToUpper();
            alterar.Titular = request.Titular.ToUpper();

            _appContext.Contratos.Update(alterar);
            await _appContext.SaveChangesAsync();
        }

        /// <summary>
        /// Método para desabilitar o contrato.
        /// </summary>
        /// <param name="id">Id do contrato.</param>
        public async Task DesabilitarContratoAsync(int id)
        {
            var desabilitar = await _appContext.Contratos.FindAsync(id);
            desabilitar.Ativo = false;

            _appContext.Contratos.Update(desabilitar);
            await _appContext.SaveChangesAsync();
        }

        /// <summary>
        /// Método para habilitar o contrato.
        /// </summary>
        /// <param name="id">Id do contrato.</param>
        public async Task HabilitarContratoAsync(int id)
        {
            var habilitar = await _appContext.Contratos.FindAsync(id);
            habilitar.Ativo = true;

            _appContext.Contratos.Update(habilitar);
            await _appContext.SaveChangesAsync();
        }        
    }
}