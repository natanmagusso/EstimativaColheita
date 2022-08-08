using EstimativaColheita.Repositories.Interfaces;
using EstimativaColheita.Models;
using EstimativaColheita.Persistence;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace EstimativaColheita.Repositories.Services
{
    /// <summary>
    /// Classe de repositório dos métodos de transação de dados dos motivos de alterações.
    /// </summary>
    public class EstimativaMotivoService : IEstimativaMotivo
    {
        /// <summary>
        /// Declaração de variável referente a classe AppDbContext.
        /// </summary>
        private readonly AppDbContext _appContext;

        /// <summary>
        /// Recebe a instância da classe DataContext.
        /// </summary>
        public EstimativaMotivoService(AppDbContext appContext)
        {
            _appContext = appContext;
        }

        /// <summary>
        /// Método buscar todos os registros.
        /// </summary>
        public PagedList<EstimativaMotivoModel> ConsultarTodosMotivosEstimativaAsync(int? pagina)
        {
            int tamanhoPagina = 5;
            int numeroPagina = pagina ?? 1;

            return (PagedList<EstimativaMotivoModel>)_appContext
                .EstimativaMotivos
                .AsNoTracking()
                .OrderBy(mot => mot.Descricao)
                .ToPagedList(numeroPagina, tamanhoPagina);
        }

        /// <summary>
        /// Método buscar todos os registros ativos.
        /// </summary>
        public IEnumerable<EstimativaMotivoModel> ConsultarMotivosEstimativasAtivosAsync()
        {
            return _appContext
                .EstimativaMotivos
                .AsNoTracking()
                .Where(mot => mot.Ativo == true && mot.Id != 1)
                .OrderBy(mot => mot.Descricao);
        }

        /// <summary>
        /// Método que retorna o motivo de alteração através do id informado.
        /// </summary>
        /// <param name="id">Id do motivo alteração.</param>
        public async Task<EstimativaMotivoModel> ConsultarMotivoEstimativaIdAsync(int id)
        {
            return await _appContext
                .EstimativaMotivos
                .AsNoTracking()
                .FirstOrDefaultAsync(mot => mot.Id == id);
        }

        /// <summary>
        /// Método inserir.
        /// </summary>
        /// <param name="request">Classe request do motivo de alteração.</param>
        public async Task<int> InserirMotivoEstimativaAsync(EstimativaMotivoModel request)
        {
            var inserir = new EstimativaMotivoModel()
            {
                Descricao = request.Descricao.ToUpper(),
                Ativo = true
            };

            _appContext.EstimativaMotivos.Add(inserir);
            await _appContext.SaveChangesAsync();
            return inserir.Id;
        }

        /// <summary>
        /// Método alterar.
        /// </summary>
        /// <param name="request">Classe request do motivo de alteração.</param>
        /// <param name="id">Id do motivo de alteração.</param>
        public async Task AlterarMotivoEstimativaAsync(EstimativaMotivoModel request, int id)
        {
            var alterar = await _appContext.EstimativaMotivos.FindAsync(id);
            alterar.Descricao = request.Descricao.ToUpper();

            _appContext.EstimativaMotivos.Update(alterar);
            await _appContext.SaveChangesAsync();
        }

        /// <summary>
        /// Método para desabilitar o motivo de alteração.
        /// </summary>
        /// <param name="id">Id do motivo de alteração.</param>
        public async Task DesabilitarMotivoEstimativaAsync(int id)
        {
            var desabilitar = await _appContext.EstimativaMotivos.FindAsync(id);
            desabilitar.Ativo = false;

            _appContext.EstimativaMotivos.Update(desabilitar);
            await _appContext.SaveChangesAsync();
        }

        /// <summary>
        /// Método para habilitar o motivo de alteração.
        /// </summary>
        /// <param name="id">Id do motivo de alteração.</param>
        public async Task HabilitarMotivoEstimativaAsync(int id)
        {
            var habilitar = await _appContext.EstimativaMotivos.FindAsync(id);
            habilitar.Ativo = true;

            _appContext.EstimativaMotivos.Update(habilitar);
            await _appContext.SaveChangesAsync();
        }
    }
}