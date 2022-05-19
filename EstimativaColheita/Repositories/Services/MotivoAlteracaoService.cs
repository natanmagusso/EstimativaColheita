using EstimativaColheita.Repositories.Interfaces;
using EstimativaColheita.Models;
using EstimativaColheita.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EstimativaColheita.Repositories.Services
{
    /// <summary>
    /// Classe de repositório dos métodos de transação de dados dos motivos de alterações.
    /// </summary>
    public class MotivoAlteracaoService : IMotivoAlteracao
    {
        /// <summary>
        /// Declaração de variável referente a classe AppDbContext.
        /// </summary>
        private readonly AppDbContext _appContext;

        /// <summary>
        /// Recebe a instância da classe DataContext.
        /// </summary>
        public MotivoAlteracaoService(AppDbContext appContext)
        {
            _appContext = appContext;
        }

        /// <summary>
        /// Método buscar todos os registros.
        /// </summary>
        public async Task<List<MotivoAlteracaoModel>> ConsultarTodosMotivosAlteracoesAsync()
        {
            return await _appContext
                .MotivosAlteracoes
                .AsNoTracking()
                .OrderBy(mot => mot.Descricao)
                .ToListAsync();
        }

        /// <summary>
        /// Método buscar todos os registros ativos.
        /// </summary>
        public IEnumerable<MotivoAlteracaoModel> ConsultarMotivosAlteracoesAtivosAsync()
        {
            return _appContext
                .MotivosAlteracoes
                .AsNoTracking()
                .Where(mot => mot.Ativo == true)
                .OrderBy(mot => mot.Descricao);
        }

        /// <summary>
        /// Método que retorna o motivo de alteração através do id informado.
        /// </summary>
        /// <param name="id">Id do motivo alteração.</param>
        public async Task<MotivoAlteracaoModel> ConsultarMotivoAlteracaoIdAsync(int id)
        {
            return await _appContext
                .MotivosAlteracoes
                .AsNoTracking()
                .FirstOrDefaultAsync(mot => mot.Id == id);
        }

        /// <summary>
        /// Método inserir.
        /// </summary>
        /// <param name="request">Classe request do motivo de alteração.</param>
        public async Task<int> InserirMotivoAlteracaoAsync(MotivoAlteracaoModel request)
        {
            var inserir = new MotivoAlteracaoModel()
            {
                Descricao = request.Descricao.ToUpper(),
                Ativo = true
            };

            _appContext.MotivosAlteracoes.Add(inserir);
            await _appContext.SaveChangesAsync();
            return inserir.Id;
        }

        /// <summary>
        /// Método alterar.
        /// </summary>
        /// <param name="request">Classe request do motivo de alteração.</param>
        /// <param name="id">Id do motivo de alteração.</param>
        public async Task AlterarMotivoAlteracaoAsync(MotivoAlteracaoModel request, int id)
        {
            var alterar = await _appContext.MotivosAlteracoes.FindAsync(id);
            alterar.Descricao = request.Descricao.ToUpper();

            _appContext.MotivosAlteracoes.Update(alterar);
            await _appContext.SaveChangesAsync();
        }

        /// <summary>
        /// Método para desabilitar o motivo de alteração.
        /// </summary>
        /// <param name="id">Id do motivo de alteração.</param>
        public async Task DesabilitarMotivoAlteracaoAsync(int id)
        {
            var desabilitar = await _appContext.MotivosAlteracoes.FindAsync(id);
            desabilitar.Ativo = false;

            _appContext.MotivosAlteracoes.Update(desabilitar);
            await _appContext.SaveChangesAsync();
        }

        /// <summary>
        /// Método para habilitar o motivo de alteração.
        /// </summary>
        /// <param name="id">Id do motivo de alteração.</param>
        public async Task HabilitarMotivoAlteracaoAsync(int id)
        {
            var habilitar = await _appContext.MotivosAlteracoes.FindAsync(id);
            habilitar.Ativo = true;

            _appContext.MotivosAlteracoes.Update(habilitar);
            await _appContext.SaveChangesAsync();
        }
    }
}