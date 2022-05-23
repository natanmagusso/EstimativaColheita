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
    public class EstimativaMotivoController : Controller
    {
        private readonly AppDbContext _context;

        public EstimativaMotivoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: EstimativaMotivo
        public async Task<IActionResult> Index()
        {
              return View(await _context.EstimativaMotivos.ToListAsync());
        }

        // GET: EstimativaMotivo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.EstimativaMotivos == null)
            {
                return NotFound();
            }

            var estimativaMotivoModel = await _context.EstimativaMotivos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (estimativaMotivoModel == null)
            {
                return NotFound();
            }

            return View(estimativaMotivoModel);
        }

        // GET: EstimativaMotivo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EstimativaMotivo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descricao,Ativo")] EstimativaMotivoModel estimativaMotivoModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(estimativaMotivoModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(estimativaMotivoModel);
        }

        // GET: EstimativaMotivo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.EstimativaMotivos == null)
            {
                return NotFound();
            }

            var estimativaMotivoModel = await _context.EstimativaMotivos.FindAsync(id);
            if (estimativaMotivoModel == null)
            {
                return NotFound();
            }
            return View(estimativaMotivoModel);
        }

        // POST: EstimativaMotivo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descricao,Ativo")] EstimativaMotivoModel estimativaMotivoModel)
        {
            if (id != estimativaMotivoModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(estimativaMotivoModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstimativaMotivoModelExists(estimativaMotivoModel.Id))
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
            return View(estimativaMotivoModel);
        }

        // GET: EstimativaMotivo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.EstimativaMotivos == null)
            {
                return NotFound();
            }

            var estimativaMotivoModel = await _context.EstimativaMotivos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (estimativaMotivoModel == null)
            {
                return NotFound();
            }

            return View(estimativaMotivoModel);
        }

        // POST: EstimativaMotivo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.EstimativaMotivos == null)
            {
                return Problem("Entity set 'AppDbContext.EstimativaMotivos'  is null.");
            }
            var estimativaMotivoModel = await _context.EstimativaMotivos.FindAsync(id);
            if (estimativaMotivoModel != null)
            {
                _context.EstimativaMotivos.Remove(estimativaMotivoModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EstimativaMotivoModelExists(int id)
        {
          return _context.EstimativaMotivos.Any(e => e.Id == id);
        }
    }
}
