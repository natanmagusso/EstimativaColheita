using Microsoft.AspNetCore.Mvc;
using EstimativaColheita.Models;
using EstimativaColheita.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using EstimativaColheita.ViewModel;

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
        public async Task<IActionResult> Index(string contrato, string talhao)
        {
            var viewModel = new EstimativaColheitaViewModel
            {
                Estimativas = (!string.IsNullOrEmpty(contrato) && !string.IsNullOrEmpty(talhao) && talhao != "0")
                ? await _estimativaColheita.ConsultarEstimativaColheitaContratoTalhaoAsync(Convert.ToInt32(contrato), Convert.ToInt32(talhao))
                : await _estimativaColheita.ConsultarTodasEstimativasColheitaAsync()
            };

            ViewData["IdContrato"] = new SelectList(_contrato.ConsultarContratosAtivosAsync(), "Id", "DescricaoCompleta");
            return View(viewModel);
        }
        public IActionResult ListarEstimativasTotais(List<EstimativaColheitaModel> lista)
        {
            return View("List", lista);
        }
        public async Task<IActionResult> ListarEstimativas(string contrato, string talhao)
        {
            var lista = await _estimativaColheita.ConsultarEstimativaColheitaContratoTalhaoAsync(Convert.ToInt32(contrato), Convert.ToInt32(talhao));
            return PartialView("List", lista);
        }
        public async Task<IActionResult> ListarEstimativasContrato(string contrato)
        {
            var lista = await _estimativaColheita.ConsultarEstimativaColheitaContratoAsync(Convert.ToInt32(contrato));
            return PartialView("List", lista);
        }
        public async Task<IActionResult> Details(int id)
        {
            return View(await _estimativaColheita.ConsultarEstimativaColheitaIdAsync(id));
        }
        public IActionResult Create()
        {
            ViewData["IdContrato"] = new SelectList(_contrato.ConsultarContratosAtivosAsync(), "Id", "DescricaoCompleta");
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
            ViewData["IdEncarregado"] = new SelectList(_encarregado.ConsultarEncarregadosAtivosAsync(), "Id", "DescricaoCompleta", estimativaColheita.IdEncarregado);
            ViewData["IdEstimativaMotivo"] = new SelectList(_estimativaMotivo.ConsultarMotivosEstimativasAtivosAsync(), "Id", "Descricao", estimativaColheita.IdEstimativaMotivo);
            ViewData["IdTipoLancamento"] = new SelectList(_tipoLancamento.ConsultarTiposLancamentoAsync(), "Id", "Descricao", estimativaColheita.IdTipoLancamento);
            return View(estimativaColheita);
        }
        [HttpGet]
        public IActionResult ListarTalhoesEstimado(string contrato)
        {
            var listaTalhoes = _talhao.ConsultarTalhoesAtivosContratoAsync(Convert.ToInt32(contrato));
            return new JsonResult(new { resultado = listaTalhoes });
        }
    }
}