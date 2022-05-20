using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EstimativaColheita.Models;
using EstimativaColheita.Persistence;

namespace EstimativaColheita.Controllers
{
    public class EstimativaColheitaController : Controller
    {
        private readonly AppDbContext _context;

        public EstimativaColheitaController(AppDbContext context)
        {
            _context = context;
        }

        // GET: EstimativaColheita
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.EstimativasColheita.Include(e => e.Contrato).Include(e => e.Encarregado).Include(e => e.MotivoAlteracao).Include(e => e.Talhao).Include(e => e.TipoLancamento);
            return View(await appDbContext.ToListAsync());
        }

        // GET: EstimativaColheita/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.EstimativasColheita == null)
            {
                return NotFound();
            }

            var estimativaColheitaModel = await _context.EstimativasColheita
                .Include(e => e.Contrato)
                .Include(e => e.Encarregado)
                .Include(e => e.MotivoAlteracao)
                .Include(e => e.Talhao)
                .Include(e => e.TipoLancamento)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (estimativaColheitaModel == null)
            {
                return NotFound();
            }

            return View(estimativaColheitaModel);
        }

        // GET: EstimativaColheita/Create
        public IActionResult Create()
        {
            ViewData["IdContrato"] = new SelectList(_context.Contratos, "Id", "Propriedade");
            ViewData["IdEncarregado"] = new SelectList(_context.Encarregados, "Id", "Nome");
            ViewData["IdMotivoAlteracao"] = new SelectList(_context.MotivosAlteracoes, "Id", "Descricao");
            ViewData["IdTalhao"] = new SelectList(_context.Talhoes, "Id", "Id");
            ViewData["IdTipoLancamento"] = new SelectList(_context.TiposLancamento, "Id", "Descricao");
            return View();
        }

        // POST: EstimativaColheita/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DataLancamento,Caixas,DataAlteracao,IdEncarregado,IdContrato,IdTalhao,IdMotivoAlteracao,IdTipoLancamento")] EstimativaColheitaModel estimativaColheitaModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(estimativaColheitaModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdContrato"] = new SelectList(_context.Contratos, "Id", "Propriedade", estimativaColheitaModel.IdContrato);
            ViewData["IdEncarregado"] = new SelectList(_context.Encarregados, "Id", "Nome", estimativaColheitaModel.IdEncarregado);
            ViewData["IdMotivoAlteracao"] = new SelectList(_context.MotivosAlteracoes, "Id", "Descricao", estimativaColheitaModel.IdMotivoAlteracao);
            ViewData["IdTalhao"] = new SelectList(_context.Talhoes, "Id", "Id", estimativaColheitaModel.IdTalhao);
            ViewData["IdTipoLancamento"] = new SelectList(_context.TiposLancamento, "Id", "Descricao", estimativaColheitaModel.IdTipoLancamento);
            return View(estimativaColheitaModel);
        }

        // GET: EstimativaColheita/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.EstimativasColheita == null)
            {
                return NotFound();
            }

            var estimativaColheitaModel = await _context.EstimativasColheita.FindAsync(id);
            if (estimativaColheitaModel == null)
            {
                return NotFound();
            }
            ViewData["IdContrato"] = new SelectList(_context.Contratos, "Id", "Propriedade", estimativaColheitaModel.IdContrato);
            ViewData["IdEncarregado"] = new SelectList(_context.Encarregados, "Id", "Nome", estimativaColheitaModel.IdEncarregado);
            ViewData["IdMotivoAlteracao"] = new SelectList(_context.MotivosAlteracoes, "Id", "Descricao", estimativaColheitaModel.IdMotivoAlteracao);
            ViewData["IdTalhao"] = new SelectList(_context.Talhoes, "Id", "Id", estimativaColheitaModel.IdTalhao);
            ViewData["IdTipoLancamento"] = new SelectList(_context.TiposLancamento, "Id", "Descricao", estimativaColheitaModel.IdTipoLancamento);
            return View(estimativaColheitaModel);
        }

        // POST: EstimativaColheita/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DataLancamento,Caixas,DataAlteracao,IdEncarregado,IdContrato,IdTalhao,IdMotivoAlteracao,IdTipoLancamento")] EstimativaColheitaModel estimativaColheitaModel)
        {
            if (id != estimativaColheitaModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(estimativaColheitaModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstimativaColheitaModelExists(estimativaColheitaModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdContrato"] = new SelectList(_context.Contratos, "Id", "Propriedade", estimativaColheitaModel.IdContrato);
            ViewData["IdEncarregado"] = new SelectList(_context.Encarregados, "Id", "Nome", estimativaColheitaModel.IdEncarregado);
            ViewData["IdMotivoAlteracao"] = new SelectList(_context.MotivosAlteracoes, "Id", "Descricao", estimativaColheitaModel.IdMotivoAlteracao);
            ViewData["IdTalhao"] = new SelectList(_context.Talhoes, "Id", "Id", estimativaColheitaModel.IdTalhao);
            ViewData["IdTipoLancamento"] = new SelectList(_context.TiposLancamento, "Id", "Descricao", estimativaColheitaModel.IdTipoLancamento);
            return View(estimativaColheitaModel);
        }

        // GET: EstimativaColheita/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.EstimativasColheita == null)
            {
                return NotFound();
            }

            var estimativaColheitaModel = await _context.EstimativasColheita
                .Include(e => e.Contrato)
                .Include(e => e.Encarregado)
                .Include(e => e.MotivoAlteracao)
                .Include(e => e.Talhao)
                .Include(e => e.TipoLancamento)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (estimativaColheitaModel == null)
            {
                return NotFound();
            }

            return View(estimativaColheitaModel);
        }

        // POST: EstimativaColheita/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.EstimativasColheita == null)
            {
                return Problem("Entity set 'AppDbContext.EstimativasColheita'  is null.");
            }
            var estimativaColheitaModel = await _context.EstimativasColheita.FindAsync(id);
            if (estimativaColheitaModel != null)
            {
                _context.EstimativasColheita.Remove(estimativaColheitaModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EstimativaColheitaModelExists(int id)
        {
          return _context.EstimativasColheita.Any(e => e.Id == id);
        }
    }
}
