// Trabalho Prático 03 - CBTSWE2
// Dupla: André Luiz Nascimento de Andrade e João do Valle Seixas Paula

using Microsoft.AspNetCore.Mvc;

namespace GerenciadorProdutos.Controllers;

public class HomeController : Controller
{
    // Página de erro genérica (usada fora do ambiente de desenvolvimento)
    public IActionResult Erro()
    {
        return View();
    }
}
