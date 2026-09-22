// Trabalho Prático 02 - CBTSWE2 (ADS 671)
// Dupla: João do Valle Seixas Paula e André Luiz Nascimento de Andrade

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BLContainerApp.Models;

namespace BLContainerApp.Controllers
{
    public class BLsController : Controller
    {
        private readonly BLContainerContext _context;

        public BLsController(BLContainerContext context)
        {
            _context = context;
        }

        // GET: BLs
        public async Task<IActionResult> Index()
        {
            return View(await _context.BLs
                .Include(b => b.Containers)
                .OrderBy(b => b.Numero)
                .ToListAsync());
        }

        // GET: BLs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var bl = await _context.BLs
                .Include(b => b.Containers)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bl == null) return NotFound();

            return View(bl);
        }

        // GET: BLs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BLs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Numero,Consignee,Navio")] BL bl)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bl);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bl);
        }

        // GET: BLs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var bl = await _context.BLs.FindAsync(id);
            if (bl == null) return NotFound();
            return View(bl);
        }

        // POST: BLs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Numero,Consignee,Navio")] BL bl)
        {
            if (id != bl.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bl);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BLExists(bl.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(bl);
        }

        // GET: BLs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var bl = await _context.BLs
                .Include(b => b.Containers)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bl == null) return NotFound();

            return View(bl);
        }

        // POST: BLs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bl = await _context.BLs
                .Include(b => b.Containers)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (bl != null)
            {
                if (bl.Containers.Any())
                {
                    ModelState.AddModelError(string.Empty,
                        "Não é possível excluir um BL que possui containers associados. Exclua ou reassocie os containers primeiro.");
                    return View(bl);
                }

                _context.BLs.Remove(bl);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool BLExists(int id)
        {
            return _context.BLs.Any(e => e.Id == id);
        }
    }
}
