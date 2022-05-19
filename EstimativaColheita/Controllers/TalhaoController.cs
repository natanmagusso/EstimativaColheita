using Microsoft.AspNetCore.Mvc;
using EstimativaColheita.Models;
using EstimativaColheita.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EstimativaColheita.Controllers
{
    public class TalhaoController : Controller
    {
        private readonly ITalhao _talhao;
        private readonly IContrato _contrato;        
        private readonly IVariedade _variedade;

        public TalhaoController(ITalhao talhao, IContrato contrato, IVariedade variedade)
        {
            _talhao = talhao;
            _contrato = contrato;
            _variedade = variedade;
        }

        public async Task<IActionResult> Index()
        {            
            return View(await _talhao.ConsultarTodosTalhoesAsync());
        }
        public async Task<IActionResult> CreateOrEdit(int id = 0)
        {
            ViewData["IdContrato"] = new SelectList(_contrato.ConsultarContratosAtivosAsync(), "Id", "DescricaoCompleta");
            ViewData["IdVariedade"] = new SelectList(_variedade.ConsultarVariedadesAtivasAsync(), "Id", "DescricaoCompleta");

            if (id == 0)
                return View(new TalhaoModel());
            else
                return View(await _talhao.ConsultarTalhaoIdAsync(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOrEdit([Bind("Id,CodigoInterno,AnoPlantio,QuantidadePes,Ativo,IdContrato,IdVariedade")] TalhaoModel talhao)
        {
            if (ModelState.IsValid)
            {
                if (talhao.Id == 0)
                    await _talhao.InserirTalhaoAsync(talhao);
                else
                    await _talhao.AlterarTalhaoAsync(talhao, talhao.Id);

                return RedirectToAction(nameof(Index));
            }

            ViewData["IdContrato"] = new SelectList(_contrato.ConsultarContratosAtivosAsync(), "Id", "DescricaoCompleta", talhao.IdContrato);
            ViewData["IdVariedade"] = new SelectList(_variedade.ConsultarVariedadesAtivasAsync(), "Id", "DescricaoCompleta", talhao.IdVariedade);
            return View(talhao);
        }
        public async Task<IActionResult> Disable(int id)
        {
            await _talhao.DesabilitarTalhaoAsync(id);

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Enable(int id)
        {
            await _talhao.HabilitarTalhaoAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}