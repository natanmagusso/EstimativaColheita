using Microsoft.AspNetCore.Mvc;
using EstimativaColheita.Models;
using EstimativaColheita.Repositories.Interfaces;

namespace EstimativaColheita.Controllers
{
    public class MotivoAlteracaoController : Controller
    {
        private readonly IMotivoAlteracao _motivoAlteracao;

        public MotivoAlteracaoController(IMotivoAlteracao motivoAlteracao)
        {
            _motivoAlteracao = motivoAlteracao;
        }

        public async Task<IActionResult> Index()
        {
              return View(await _motivoAlteracao.ConsultarTodosMotivosAlteracoesAsync());
        }
        public async Task<IActionResult> CreateOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new MotivoAlteracaoModel());
            else
                return View(await _motivoAlteracao.ConsultarMotivoAlteracaoIdAsync(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOrEdit([Bind("Id,Descricao,Ativo")] MotivoAlteracaoModel motivoAlteracao)
        {
            if (ModelState.IsValid)
            {
                if (motivoAlteracao.Id == 0)
                    await _motivoAlteracao.InserirMotivoAlteracaoAsync(motivoAlteracao);
                else
                    await _motivoAlteracao.AlterarMotivoAlteracaoAsync(motivoAlteracao, motivoAlteracao.Id);

                return RedirectToAction(nameof(Index));
            }
            return View(motivoAlteracao);
        }
        public async Task<IActionResult> Disable(int id)
        {
            await _motivoAlteracao.DesabilitarMotivoAlteracaoAsync(id);

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Enable(int id)
        {
            await _motivoAlteracao.HabilitarMotivoAlteracaoAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}