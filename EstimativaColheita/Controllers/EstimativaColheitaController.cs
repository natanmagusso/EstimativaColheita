using Microsoft.AspNetCore.Mvc;
using EstimativaColheita.Models;
using EstimativaColheita.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EstimativaColheita.Controllers
{
    public class EstimativaColheitaController : Controller
    {
        private readonly IEstimativaColheita _estimativaColheita;
        private readonly IEncarregado _encarregado;
        private readonly IContrato _contrato;
        private readonly ITalhao _talhao;        
        private readonly IMotivoAlteracao _motivoAlteracao;
        private readonly ITipoLancamento _tipoLancamento;

        public EstimativaColheitaController(IEstimativaColheita estimativaColheita, IEncarregado encarregado, IContrato contrato, ITalhao talhao, IMotivoAlteracao motivoAlteracao, ITipoLancamento tipoLancamento)
        {
            _estimativaColheita = estimativaColheita;
            _encarregado = encarregado;
            _contrato = contrato;
            _talhao = talhao;
            _motivoAlteracao = motivoAlteracao;
            _tipoLancamento = tipoLancamento;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _estimativaColheita.ConsultarTodasEstimativasColheitaAsync());
        }
        public async Task<IActionResult> CreateOrEdit(int id)
        {
            ViewData["IdContrato"] = new SelectList(_contrato.ConsultarContratosAtivosAsync(), "Id", "DescricaoCompleta");
            ViewData["IdEncarregado"] = new SelectList(_encarregado.ConsultarEncarregadosAtivosAsync(), "Id", "DescricaoCompleta");
            ViewData["IdMotivoAlteracao"] = new SelectList(_motivoAlteracao.ConsultarMotivosAlteracoesAtivosAsync(), "Id", "Descricao");
            ViewData["IdTalhao"] = new SelectList(_talhao.ConsultarTalhoesAtivosAsync(), "Id", "DescricaoCompleta");
            ViewData["IdTipoLancamento"] = new SelectList(_tipoLancamento.ConsultarTiposLancamentoAsync(), "Id", "Descricao");

            if (id == 0)
                return View(new EstimativaColheitaModel());
            else
                return View(await _estimativaColheita.ConsultarEstimativaColheitaIdAsync(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOrEdit([Bind("Id,DataLancamento,Caixas,DataAlteracao,IdEncarregado,IdContrato,IdTalhao,IdMotivoAlteracao,IdTipoLancamento")] EstimativaColheitaModel estimativaColheita)
        {
            if (ModelState.IsValid)
            {
                if (estimativaColheita.Id == 0)
                    await _estimativaColheita.InserirEstimativaColheitaAsync(estimativaColheita);
                else
                    await _estimativaColheita.AlterarEstimativaColheitaAsync(estimativaColheita, estimativaColheita.Id);

                return RedirectToAction(nameof(Index));
            }

            ViewData["IdContrato"] = new SelectList(_contrato.ConsultarContratosAtivosAsync(), "Id", "DescricaoCompleta", estimativaColheita.IdContrato);
            ViewData["IdEncarregado"] = new SelectList(_encarregado.ConsultarEncarregadosAtivosAsync(), "Id", "DescricaoCompleta", estimativaColheita.IdEncarregado);
            ViewData["IdMotivoAlteracao"] = new SelectList(_motivoAlteracao.ConsultarMotivosAlteracoesAtivosAsync(), "Id", "Descricao", estimativaColheita.IdMotivoAlteracao);
            ViewData["IdTalhao"] = new SelectList(_talhao.ConsultarTalhoesAtivosAsync(), "Id", "DescricaoCompleta", estimativaColheita.IdTalhao);
            ViewData["IdTipoLancamento"] = new SelectList(_tipoLancamento.ConsultarTiposLancamentoAsync(), "Id", "Descricao", estimativaColheita.IdTipoLancamento);
            return View(estimativaColheita);
        }
    }
}