using Microsoft.AspNetCore.Mvc;
using EstimativaColheita.Models;
using EstimativaColheita.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EstimativaColheita.Controllers
{
    public class ColheitaRealizadaController : Controller
    {
        private readonly IColheitaRealizada _colheitaRealizada;
        private readonly IContrato _contrato;
        private readonly ITalhao _talhao;
        private readonly IEncarregado _encarregado;
        private readonly IEstimativaColheita _estimativaColheita;

        public ColheitaRealizadaController(IColheitaRealizada colheitaRealizada, IContrato contrato, ITalhao talhao, IEncarregado encarregado, IEstimativaColheita estimativaColheita)
        {
            _colheitaRealizada = colheitaRealizada;
            _contrato = contrato;
            _talhao = talhao;
            _encarregado = encarregado;
            _estimativaColheita = estimativaColheita;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _colheitaRealizada.ConsultarTodasColheitasRealizadasAsync());
        }
        public IActionResult Create()
        {
            ViewData["IdContrato"] = new SelectList(_contrato.ConsultarContratosAtivosAsync(), "Id", "DescricaoCompleta");
            ViewData["IdTalhao"] = new SelectList(_talhao.ConsultarTalhoesAtivosAsync(), "Id", "DescricaoCompleta");
            ViewData["IdEncarregado"] = new SelectList(_encarregado.ConsultarEncarregadosAtivosAsync(), "Id", "DescricaoCompleta");

            return View(new ColheitaRealizadaModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DataLancamento,Caixas,IdContrato,IdTalhao")] ColheitaRealizadaModel colheitaRealizada)
        {
            if (ModelState.IsValid)
            {
                var estimativaColheita = new EstimativaColheitaModel()
                {
                    IdContrato = colheitaRealizada.IdContrato,
                    IdTalhao = colheitaRealizada.IdTalhao,
                    IdEncarregado = colheitaRealizada.IdEncarregado,
                    IdEstimativaMotivo = 1,
                    IdTipoLancamento = 2,
                    DataLancamento = DateTime.Now,
                    Caixas = colheitaRealizada.Caixas
                };

                await _colheitaRealizada.InserirColheitaRealizadaAsync(colheitaRealizada);
                await _estimativaColheita.InserirEstimativaColheitaAsync(estimativaColheita);

                return RedirectToAction(nameof(Index));
            }
            ViewData["IdContrato"] = new SelectList(_contrato.ConsultarContratosAtivosAsync(), "Id", "DescricaoCompleta", colheitaRealizada.IdContrato);
            ViewData["IdTalhao"] = new SelectList(_talhao.ConsultarTalhoesAtivosAsync(), "Id", "DescricaoCompleta", colheitaRealizada.IdTalhao);
            ViewData["IdEncarregado"] = new SelectList(_encarregado.ConsultarEncarregadosAtivosAsync(), "Id", "DescricaoCompleta", colheitaRealizada.IdEncarregado);
            return View(colheitaRealizada);
        }
    }
}