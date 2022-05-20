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
    public class EncarregadoController : Controller
    {
        private readonly AppDbContext _context;

        public EncarregadoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Encarregado
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Encarregados.Include(e => e.FiscalCampo);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Encarregado/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Encarregados == null)
            {
                return NotFound();
            }

            var encarregadoModel = await _context.Encarregados
                .Include(e => e.FiscalCampo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (encarregadoModel == null)
            {
                return NotFound();
            }

            return View(encarregadoModel);
        }

        // GET: Encarregado/Create
        public IActionResult Create()
        {
            ViewData["IdFiscalCampo"] = new SelectList(_context.FiscaisCampo, "Id", "Apelido");
            return View();
        }

        // POST: Encarregado/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CodigoInterno,Nome,Ativo,IdFiscalCampo")] EncarregadoModel encarregadoModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(encarregadoModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdFiscalCampo"] = new SelectList(_context.FiscaisCampo, "Id", "Apelido", encarregadoModel.IdFiscalCampo);
            return View(encarregadoModel);
        }

        // GET: Encarregado/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Encarregados == null)
            {
                return NotFound();
            }

            var encarregadoModel = await _context.Encarregados.FindAsync(id);
            if (encarregadoModel == null)
            {
                return NotFound();
            }
            ViewData["IdFiscalCampo"] = new SelectList(_context.FiscaisCampo, "Id", "Apelido", encarregadoModel.IdFiscalCampo);
            return View(encarregadoModel);
        }

        // POST: Encarregado/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CodigoInterno,Nome,Ativo,IdFiscalCampo")] EncarregadoModel encarregadoModel)
        {
            if (id != encarregadoModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(encarregadoModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EncarregadoModelExists(encarregadoModel.Id))
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
            ViewData["IdFiscalCampo"] = new SelectList(_context.FiscaisCampo, "Id", "Apelido", encarregadoModel.IdFiscalCampo);
            return View(encarregadoModel);
        }

        // GET: Encarregado/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Encarregados == null)
            {
                return NotFound();
            }

            var encarregadoModel = await _context.Encarregados
                .Include(e => e.FiscalCampo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (encarregadoModel == null)
            {
                return NotFound();
            }

            return View(encarregadoModel);
        }

        // POST: Encarregado/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Encarregados == null)
            {
                return Problem("Entity set 'AppDbContext.Encarregados'  is null.");
            }
            var encarregadoModel = await _context.Encarregados.FindAsync(id);
            if (encarregadoModel != null)
            {
                _context.Encarregados.Remove(encarregadoModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EncarregadoModelExists(int id)
        {
          return _context.Encarregados.Any(e => e.Id == id);
        }
    }
}
