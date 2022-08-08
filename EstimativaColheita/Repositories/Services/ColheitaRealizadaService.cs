using EstimativaColheita.Repositories.Interfaces;
using EstimativaColheita.Models;
using EstimativaColheita.Persistence;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace EstimativaColheita.Repositories.Services
{
    /// <summary>
    /// Classe de repositório dos métodos de transação de dados das colheitas realizadas.
    /// </summary>
    public class ColheitaRealizadaService : IColheitaRealizada
    {
        /// <summary>
        /// Declaração de variável referente a classe AppDbContext.
        /// </summary>
        private readonly AppDbContext _appContext;

        /// <summary>
        /// Recebe a instância da classe DataContext.
        /// </summary>
        public ColheitaRealizadaService(AppDbContext appContext)
        {
            _appContext = appContext;
        }

        /// <summary>
        /// Método buscar todos os registros.
        /// </summary>
        public PagedList<ColheitaRealizadaModel> ConsultarTodasColheitasRealizadasAsync(int? pagina)
        {
            int tamanhoPagina = 6;
            int numeroPagina = pagina ?? 1;

            return (PagedList<ColheitaRealizadaModel>)_appContext
                .ColheitasRealizadas
                .AsNoTracking()
                .Include(col => col.Contrato)
                .Include(col => col.Talhao)
                .Include(col => col.Encarregado)
                .OrderBy(col => col.Contrato.CodigoInterno)
                .ThenBy(col => col.Talhao.CodigoInterno)
                .ThenBy(col => col.DataLancamento)
                .ToPagedList(numeroPagina, tamanhoPagina);
        }

        /// <summary>
        /// Método inserir.
        /// </summary>
        /// <param name="request">Classe request das colheitas realizadas.</param>
        public async Task<int> InserirColheitaRealizadaAsync(ColheitaRealizadaModel request)
        {
            var inserir = new ColheitaRealizadaModel()
            {
                IdContrato = request.IdContrato,
                IdTalhao = request.IdTalhao,
                IdEncarregado = request.IdEncarregado,
                DataLancamento = DateTime.Now,
                Caixas = request.Caixas,
            };

            _appContext.ColheitasRealizadas.Add(inserir);
            await _appContext.SaveChangesAsync();
            return inserir.Id;
        }
    }
}