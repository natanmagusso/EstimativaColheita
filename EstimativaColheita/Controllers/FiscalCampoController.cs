using Microsoft.AspNetCore.Mvc;
using EstimativaColheita.Models;
using EstimativaColheita.Repositories.Interfaces;

namespace EstimativaColheita.Controllers
{
    public class FiscalCampoController : Controller
    {
        private readonly IFiscalCampo _fiscalCampo;

        public FiscalCampoController(IFiscalCampo fiscalCampo)
        {
            _fiscalCampo = fiscalCampo;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _fiscalCampo.ConsultarTodosFiscaisCampoAsync());
        }
        public async Task<IActionResult> CreateOrEdit(int id = 0)
        {
            if (id == 0) 
                return View(new FiscalCampoModel());
            else 
                return View(await _fiscalCampo.ConsultarFiscalCampoIdAsync(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOrEdit([Bind("Id,CodigoInterno,Nome,Apelido,Ativo")] FiscalCampoModel fiscalCampo)
        {
            if (ModelState.IsValid)
            {
                if (fiscalCampo.Id == 0)                
                    await _fiscalCampo.InserirFiscalCampoAsync(fiscalCampo);                
                else                
                    await _fiscalCampo.AlterarFiscalCampoAsync(fiscalCampo, fiscalCampo.Id);

                return RedirectToAction(nameof(Index));
            }
            return View(fiscalCampo);
        }
        public async Task<IActionResult> Disable(int id)
        {
            await _fiscalCampo.DesabilitarFiscalCampoAsync(id);

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Enable(int id)
        {
            await _fiscalCampo.HabilitarFiscalCampoAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}