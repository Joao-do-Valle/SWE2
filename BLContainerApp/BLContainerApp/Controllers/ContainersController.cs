// Trabalho Prático 02 - CBTSWE2 (ADS 671)
// Dupla: João do Valle Seixas Paula e André Luiz Nascimento de Andrade

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BLContainerApp.Models;

namespace BLContainerApp.Controllers
{
    public class ContainersController : Controller
    {
        private readonly BLContainerContext _context;

        public ContainersController(BLContainerContext context)
        {
            _context = context;
        }

        // GET: Containers
        public async Task<IActionResult> Index()
        {
            var containers = _context.Containers.Include(c => c.BL).OrderBy(c => c.Numero);
            return View(await containers.ToListAsync());
        }

        // GET: Containers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var container = await _context.Containers
                .Include(c => c.BL)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (container == null) return NotFound();

            return View(container);
        }

        // GET: Containers/Create
        public IActionResult Create()
        {
            ViewData["BLId"] = new SelectList(_context.BLs.OrderBy(b => b.Numero), "Id", "Numero");
            return View();
        }

        // POST: Containers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Numero,Tipo,Tamanho,BLId")] Container container)
        {
            if (ModelState.IsValid)
            {
                _context.Add(container);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BLId"] = new SelectList(_context.BLs.OrderBy(b => b.Numero), "Id", "Numero", container.BLId);
            return View(container);
        }

        // GET: Containers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var container = await _context.Containers.FindAsync(id);
            if (container == null) return NotFound();
            ViewData["BLId"] = new SelectList(_context.BLs.OrderBy(b => b.Numero), "Id", "Numero", container.BLId);
            return View(container);
        }

        // POST: Containers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Numero,Tipo,Tamanho,BLId")] Container container)
        {
            if (id != container.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(container);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContainerExists(container.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BLId"] = new SelectList(_context.BLs.OrderBy(b => b.Numero), "Id", "Numero", container.BLId);
            return View(container);
        }

        // GET: Containers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var container = await _context.Containers
                .Include(c => c.BL)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (container == null) return NotFound();

            return View(container);
        }

        // POST: Containers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var container = await _context.Containers.FindAsync(id);
            if (container != null)
            {
                _context.Containers.Remove(container);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool ContainerExists(int id)
        {
            return _context.Containers.Any(e => e.Id == id);
        }
    }
}
