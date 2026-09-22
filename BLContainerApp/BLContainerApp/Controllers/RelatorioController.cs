// Trabalho Prático 02 - CBTSWE2 (ADS 671)
// Dupla: João do Valle Seixas Paula e André Luiz Nascimento de Andrade

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BLContainerApp.Models;

namespace BLContainerApp.Controllers
{
    // Controller responsável por exibir o relatório de BL/Container exigido no trabalho.
    public class RelatorioController : Controller
    {
        private readonly BLContainerContext _context;

        public RelatorioController(BLContainerContext context)
        {
            _context = context;
        }

        // GET: Relatorio
        public async Task<IActionResult> Index()
        {
            var bls = await _context.BLs
                .Include(b => b.Containers)
                .OrderBy(b => b.Numero)
                .ToListAsync();

            return View(bls);
        }
    }
}
