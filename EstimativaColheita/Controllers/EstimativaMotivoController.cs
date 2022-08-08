using Microsoft.AspNetCore.Mvc;
using EstimativaColheita.Models;
using EstimativaColheita.Repositories.Interfaces;

namespace EstimativaColheita.Controllers
{
    public class EstimativaMotivoController : Controller
    {
        private readonly IEstimativaMotivo _estimativaMotivo;

        public EstimativaMotivoController(IEstimativaMotivo estimativaMotivo)
        {
            _estimativaMotivo = estimativaMotivo;
        }

        public IActionResult Index(int? pagina)
        {
            return View(_estimativaMotivo.ConsultarTodosMotivosEstimativaAsync(pagina));
        }
        public async Task<IActionResult> CreateOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new EstimativaMotivoModel());
            else
                return View(await _estimativaMotivo.ConsultarMotivoEstimativaIdAsync(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOrEdit([Bind("Id,Descricao,Ativo")] EstimativaMotivoModel estimativaMotivo)
        {
            if (ModelState.IsValid)
            {
                if (estimativaMotivo.Id == 0)
                    await _estimativaMotivo.InserirMotivoEstimativaAsync(estimativaMotivo);
                else
                    await _estimativaMotivo.AlterarMotivoEstimativaAsync(estimativaMotivo, estimativaMotivo.Id);

                return RedirectToAction(nameof(Index));
            }
            return View(estimativaMotivo);
        }
        public async Task<IActionResult> Disable(int id)
        {
            await _estimativaMotivo.DesabilitarMotivoEstimativaAsync(id);

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Enable(int id)
        {
            await _estimativaMotivo.HabilitarMotivoEstimativaAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}