using Microsoft.AspNetCore.Mvc;
using EstimativaColheita.Models;
using EstimativaColheita.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EstimativaColheita.Controllers
{
    public class ContratoController : Controller
    {
        private readonly IContrato _contrato;
        private readonly IFiscalCampo _fiscalCampo;

        public ContratoController(IContrato contrato, IFiscalCampo fiscalCampo)
        {
            _contrato = contrato;
            _fiscalCampo = fiscalCampo;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _contrato.ConsultarTodosContratosAsync());
        }
        public async Task<IActionResult> CreateOrEdit(int id = 0)
        {
            ViewData["IdFiscalCampo"] = new SelectList(_fiscalCampo.ConsultarFiscaisCampoAtivosAsync(), "Id", "DescricaoCompleta");

            if (id == 0)
                return View(new ContratoModel());
            else
                return View(await _contrato.ConsultarContratoIdAsync(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOrEdit([Bind("Id,CodigoInterno,Propriedade,Titular,Ativo,IdFiscalCampo")] ContratoModel contrato)
        {
            if (ModelState.IsValid)
            {
                if (contrato.Id == 0)
                    await _contrato.InserirContratoAsync(contrato);
                else
                    await _contrato.AlterarContratoAsync(contrato, contrato.Id);

                return RedirectToAction(nameof(Index));
            }

            ViewData["IdFiscalCampo"] = new SelectList(_fiscalCampo.ConsultarFiscaisCampoAtivosAsync(), "Id", "DescricaoCompleta", contrato.IdFiscalCampo);
            return View(contrato);
        }
        public async Task<IActionResult> Disable(int id)
        {
            await _contrato.DesabilitarContratoAsync(id);

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Enable(int id)
        {
            await _contrato.HabilitarContratoAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}