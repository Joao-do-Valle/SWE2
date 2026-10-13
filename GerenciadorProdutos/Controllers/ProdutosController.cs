// Trabalho Prático 03 - CBTSWE2
// Dupla: André Luiz Nascimento de Andrade e João do Valle Seixas Paula

using GerenciadorProdutos.Data;
using GerenciadorProdutos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorProdutos.Controllers;

/// <summary>
/// Camada de controle: recebe as requisições HTTP, conversa com o banco via
/// Entity Framework e devolve a View correta.
/// </summary>
public class ProdutosController : Controller
{
    private readonly AppDbContext _contexto;

    public ProdutosController(AppDbContext contexto)
    {
        _contexto = contexto;
    }

    // GET: /Produtos  (página inicial: lista de produtos, com busca por nome)
    public async Task<IActionResult> Index(string? busca)
    {
        var consulta = _contexto.Produtos.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(busca))
        {
            consulta = consulta.Where(p => EF.Functions.Like(p.Nome, $"%{busca.Trim()}%"));
        }

        ViewData["Busca"] = busca;
        var produtos = await consulta.OrderBy(p => p.Nome).ToListAsync();
        return View(produtos);
    }

    // GET: /produto/5  ou  /Produtos/Detalhes/5
    public async Task<IActionResult> Detalhes(int? id)
    {
        if (id == null) return NotFound();

        var produto = await _contexto.Produtos.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        if (produto == null) return NotFound();

        return View(produto);
    }

    // GET: /Produtos/Criar
    public IActionResult Criar()
    {
        return View(new Produto());
    }

    // POST: /Produtos/Criar
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Criar([Bind("Nome,Descricao,Preco,QuantidadeEstoque")] Produto produto)
    {
        if (!ModelState.IsValid) return View(produto);

        _contexto.Add(produto);
        await _contexto.SaveChangesAsync();

        TempData["Sucesso"] = $"Produto \"{produto.Nome}\" cadastrado.";
        return RedirectToAction(nameof(Index));
    }

    // GET: /Produtos/Editar/5
    public async Task<IActionResult> Editar(int? id)
    {
        if (id == null) return NotFound();

        var produto = await _contexto.Produtos.FindAsync(id);
        if (produto == null) return NotFound();

        return View(produto);
    }

    // POST: /Produtos/Editar/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Editar(int id, [Bind("Id,Nome,Descricao,Preco,QuantidadeEstoque")] Produto produto)
    {
        if (id != produto.Id) return NotFound();
        if (!ModelState.IsValid) return View(produto);

        try
        {
            _contexto.Update(produto);
            await _contexto.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            // O produto pode ter sido excluído por outra pessoa enquanto era editado
            if (!await _contexto.Produtos.AnyAsync(p => p.Id == id)) return NotFound();
            throw;
        }

        TempData["Sucesso"] = $"Alterações em \"{produto.Nome}\" salvas.";
        return RedirectToAction(nameof(Index));
    }

    // GET: /Produtos/Excluir/5  (tela de confirmação)
    public async Task<IActionResult> Excluir(int? id)
    {
        if (id == null) return NotFound();

        var produto = await _contexto.Produtos.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        if (produto == null) return NotFound();

        return View(produto);
    }

    // POST: /Produtos/Excluir/5
    [HttpPost, ActionName("Excluir")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ExcluirConfirmado(int id)
    {
        var produto = await _contexto.Produtos.FindAsync(id);
        if (produto != null)
        {
            _contexto.Produtos.Remove(produto);
            await _contexto.SaveChangesAsync();
            TempData["Sucesso"] = $"Produto \"{produto.Nome}\" excluído.";
        }

        return RedirectToAction(nameof(Index));
    }
}
