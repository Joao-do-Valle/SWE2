using GerenciadorLivros.Api.Data;
using GerenciadorLivros.Api.Dtos;
using GerenciadorLivros.Api.Models;
using GerenciadorLivros.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorLivros.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LivrosController : ControllerBase
{
    private readonly AppDbContext _contexto;
    private readonly ILogger<LivrosController> _logger;

    public LivrosController(AppDbContext contexto, ILogger<LivrosController> logger)
    {
        _contexto = contexto;
        _logger = logger;
    }

    /// <summary>Lista os livros. Use ?busca=texto para filtrar por título ou autor.</summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<LivroResponse>>> Listar([FromQuery] string? busca = null)
    {
        var consulta = _contexto.Livros.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(busca))
        {
            var termo = busca.Trim().ToLower();
            consulta = consulta.Where(l =>
                l.Titulo.ToLower().Contains(termo) || l.Autor.ToLower().Contains(termo));
        }

        var livros = await consulta.OrderBy(l => l.Titulo).ToListAsync();

        return Ok(livros.Select(ParaResponse));
    }

    /// <summary>Busca um livro pelo Id.</summary>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<LivroResponse>> ObterPorId(int id)
    {
        var livro = await _contexto.Livros.AsNoTracking().FirstOrDefaultAsync(l => l.Id == id);

        if (livro is null)
            return NotFound(new ProblemDetails { Status = 404, Title = "Livro não encontrado." });

        return Ok(ParaResponse(livro));
    }

    /// <summary>Cadastra um novo livro.</summary>
    [HttpPost]
    public async Task<ActionResult<LivroResponse>> Criar([FromBody] LivroRequest request)
    {
        // Validação usando a DLL GerenciadorLivros.Utils
        if (!IsbnValidador.EhValido(request.Isbn))
            return BadRequest(IsbnInvalido());

        var isbn = IsbnValidador.Normalizar(request.Isbn);

        if (await _contexto.Livros.AnyAsync(l => l.Isbn == isbn))
            return Conflict(new ProblemDetails { Status = 409, Title = "Já existe um livro cadastrado com este ISBN." });

        var livro = new Livro
        {
            Titulo = FormatadorTexto.TituloCapitalizado(request.Titulo),
            Autor = FormatadorTexto.TituloCapitalizado(request.Autor),
            Isbn = isbn,
            AnoPublicacao = request.AnoPublicacao,
            NumeroPaginas = request.NumeroPaginas,
            DataCadastro = DateTime.UtcNow
        };

        _contexto.Livros.Add(livro);
        await _contexto.SaveChangesAsync();

        _logger.LogInformation("Livro {Id} cadastrado.", livro.Id);

        return CreatedAtAction(nameof(ObterPorId), new { id = livro.Id }, ParaResponse(livro));
    }

    /// <summary>Atualiza os dados de um livro existente.</summary>
    [HttpPut("{id:int}")]
    public async Task<ActionResult<LivroResponse>> Atualizar(int id, [FromBody] LivroRequest request)
    {
        var livro = await _contexto.Livros.FirstOrDefaultAsync(l => l.Id == id);

        if (livro is null)
            return NotFound(new ProblemDetails { Status = 404, Title = "Livro não encontrado." });

        if (!IsbnValidador.EhValido(request.Isbn))
            return BadRequest(IsbnInvalido());

        var isbn = IsbnValidador.Normalizar(request.Isbn);

        if (await _contexto.Livros.AnyAsync(l => l.Isbn == isbn && l.Id != id))
            return Conflict(new ProblemDetails { Status = 409, Title = "Já existe outro livro cadastrado com este ISBN." });

        livro.Titulo = FormatadorTexto.TituloCapitalizado(request.Titulo);
        livro.Autor = FormatadorTexto.TituloCapitalizado(request.Autor);
        livro.Isbn = isbn;
        livro.AnoPublicacao = request.AnoPublicacao;
        livro.NumeroPaginas = request.NumeroPaginas;

        await _contexto.SaveChangesAsync();

        _logger.LogInformation("Livro {Id} atualizado.", livro.Id);

        return Ok(ParaResponse(livro));
    }

    /// <summary>Remove um livro.</summary>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Excluir(int id)
    {
        var livro = await _contexto.Livros.FirstOrDefaultAsync(l => l.Id == id);

        if (livro is null)
            return NotFound(new ProblemDetails { Status = 404, Title = "Livro não encontrado." });

        _contexto.Livros.Remove(livro);
        await _contexto.SaveChangesAsync();

        _logger.LogInformation("Livro {Id} excluído.", id);

        return NoContent();
    }

    private static ProblemDetails IsbnInvalido() => new()
    {
        Status = 400,
        Title = "ISBN inválido.",
        Detail = "Informe um ISBN-10 ou ISBN-13 válido."
    };

    // Converte a entidade em resposta, usando a DLL para enriquecer os dados
    private static LivroResponse ParaResponse(Livro livro) => new()
    {
        Id = livro.Id,
        Titulo = livro.Titulo,
        Autor = livro.Autor,
        Isbn = livro.Isbn,
        IsbnFormatado = IsbnValidador.Formatar(livro.Isbn),
        AnoPublicacao = livro.AnoPublicacao,
        NumeroPaginas = livro.NumeroPaginas,
        TempoLeituraEstimado = CalculadoraLeitura.DescricaoTempo(livro.NumeroPaginas),
        DataCadastro = livro.DataCadastro
    };
}
