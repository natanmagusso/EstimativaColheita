using Microsoft.AspNetCore.Mvc;
using EstimativaColheita.Models;
using EstimativaColheita.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EstimativaColheita.Controllers
{
    public class EstimativaColheitaController : Controller
    {
        private readonly IEstimativaColheita _estimativaColheita;
        private readonly IContrato _contrato;
        private readonly ITalhao _talhao;
        private readonly IEncarregado _encarregado;
        private readonly IEstimativaMotivo _estimativaMotivo;
        private readonly ITipoLancamento _tipoLancamento;

        public EstimativaColheitaController(IEstimativaColheita estimativaColheita, IContrato contrato, ITalhao talhao, IEncarregado encarregado, IEstimativaMotivo estimativaMotivo, ITipoLancamento tipoLancamento)
        {
            _estimativaColheita = estimativaColheita;
            _contrato = contrato;
            _talhao = talhao;
            _encarregado = encarregado;
            _estimativaMotivo = estimativaMotivo;
            _tipoLancamento = tipoLancamento;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _estimativaColheita.ConsultarTodasEstimativasColheitaAsync());
        }
        public async Task<IActionResult> Details(int id)
        {
            var estimativaColheita = await _estimativaColheita.ConsultarEstimativaColheitaIdAsync(id);

            return View(estimativaColheita);
        }
        public IActionResult Create()
        {
            ViewData["IdContrato"] = new SelectList(_contrato.ConsultarContratosAtivosAsync(), "Id", "DescricaoCompleta");
            ViewData["IdTalhao"] = new SelectList(_talhao.ConsultarTalhoesAtivosAsync(), "Id", "DescricaoCompleta");
            ViewData["IdEncarregado"] = new SelectList(_encarregado.ConsultarEncarregadosAtivosAsync(), "Id", "DescricaoCompleta");
            ViewData["IdEstimativaMotivo"] = new SelectList(_estimativaMotivo.ConsultarMotivosEstimativasAtivosAsync(), "Id", "Descricao");
            ViewData["IdTipoLancamento"] = new SelectList(_tipoLancamento.ConsultarTiposLancamentoAsync(), "Id", "Descricao");
            
            return View(new EstimativaColheitaModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DataLancamento,Caixas,IdEncarregado,IdContrato,IdTalhao,IdEstimativaMotivo,IdTipoLancamento")] EstimativaColheitaModel estimativaColheita)
        {
            if (ModelState.IsValid)
            {
                estimativaColheita.IdTipoLancamento = 1;
                await _estimativaColheita.InserirEstimativaColheitaAsync(estimativaColheita);

                return RedirectToAction(nameof(Index));
            }
            ViewData["IdContrato"] = new SelectList(_contrato.ConsultarContratosAtivosAsync(), "Id", "DescricaoCompleta", estimativaColheita.IdContrato);
            ViewData["IdTalhao"] = new SelectList(_talhao.ConsultarTalhoesAtivosAsync(), "Id", "DescricaoCompleta", estimativaColheita.IdTalhao);
            ViewData["IdEncarregado"] = new SelectList(_encarregado.ConsultarEncarregadosAtivosAsync(), "Id", "DescricaoCompleta", estimativaColheita.IdEncarregado);
            ViewData["IdEstimativaMotivo"] = new SelectList(_estimativaMotivo.ConsultarMotivosEstimativasAtivosAsync(), "Id", "Descricao", estimativaColheita.IdEstimativaMotivo);
            ViewData["IdTipoLancamento"] = new SelectList(_tipoLancamento.ConsultarTiposLancamentoAsync(), "Id", "Descricao", estimativaColheita.IdTipoLancamento);
            return View(estimativaColheita);
        }
    }
}