using Microsoft.AspNetCore.Mvc;
using EstimativaColheita.Models;
using EstimativaColheita.Repositories.Interfaces;

namespace EstimativaColheita.Controllers
{
    public class VariedadeController : Controller
    {
        private readonly IVariedade _variedade;

        public VariedadeController(IVariedade variedade)
        {
            _variedade = variedade;
        }

        public IActionResult Index(int? pagina)
        {
              return View(_variedade.ConsultarTodasVariedadesAsync(pagina));
        }
        public async Task<IActionResult> CreateOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new VariedadeModel());
            else
                return View(await _variedade.ConsultarVariedadeIdAsync(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOrEdit([Bind("Id,CodigoInterno,Descricao,Ativo")] VariedadeModel variedade)
        {
            if (ModelState.IsValid)
            {
                if (variedade.Id == 0)
                    await _variedade.InserirVariedadeAsync(variedade);
                else
                    await _variedade.AlterarVariedadeAsync(variedade, variedade.Id);

                return RedirectToAction(nameof(Index));
            }
            return View(variedade);
        }
        public async Task<IActionResult> Disable(int id)
        {
            await _variedade.DesabilitarVariedadeAsync(id);

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Enable(int id)
        {
            await _variedade.HabilitarVariedadeAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}