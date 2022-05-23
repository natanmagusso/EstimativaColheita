using EstimativaColheita.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EstimativaColheita.Components
{
    public class ListaContratos : ViewComponent
    {
        private readonly IContrato _contrato;

        public ListaContratos(IContrato contrato)
        {
            _contrato = contrato;
        }

        public IViewComponentResult Invoke()
        {
            var contratos = _contrato.ConsultarContratosAtivosAsync();

            return View(contratos);
        }
    }
}
