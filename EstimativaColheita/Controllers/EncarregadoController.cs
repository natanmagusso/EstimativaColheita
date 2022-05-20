using Microsoft.AspNetCore.Mvc;
using EstimativaColheita.Models;
using EstimativaColheita.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EstimativaColheita.Controllers
{
    public class EncarregadoController : Controller
    {
        private readonly IEncarregado _encarregado;
        private readonly IFiscalCampo _fiscalCampo;

        public EncarregadoController(IEncarregado encarregado, IFiscalCampo fiscalCampo)
        {
            _encarregado = encarregado;
            _fiscalCampo = fiscalCampo;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _encarregado.ConsultarTodosEncarregadosAsync());
        }
        public async Task<IActionResult> CreateOrEdit(int id)
        {
            ViewData["IdFiscalCampo"] = new SelectList(_fiscalCampo.ConsultarFiscaisCampoAtivosAsync(), "Id", "DescricaoCompleta");

            if (id == 0)
                return View(new EncarregadoModel());
            else
                return View(await _encarregado.ConsultarEncarregadoIdAsync(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOrEdit([Bind("Id,CodigoInterno,Nome,Ativo,IdFiscalCampo")] EncarregadoModel encarregado)
        {
            if (ModelState.IsValid)
            {
                if (encarregado.Id == 0)
                    await _encarregado.InserirEncarregadoAsync(encarregado);
                else
                    await _encarregado.AlterarEncarregadoAsync(encarregado, encarregado.Id);

                return RedirectToAction(nameof(Index));
            }

            ViewData["IdFiscalCampo"] = new SelectList(_fiscalCampo.ConsultarFiscaisCampoAtivosAsync(), "Id", "DescricaoCompleta", encarregado.IdFiscalCampo);
            return View(encarregado);
        }
        public async Task<IActionResult> Disable(int id)
        {
            await _encarregado.DesabilitarEncarregadoAsync(id);

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Enable(int id)
        {
            await _encarregado.HabilitarEncarregadoAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}