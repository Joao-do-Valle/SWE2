using GerenciadorLivros.Api.Controllers;
using GerenciadorLivros.Api.Data;
using GerenciadorLivros.Api.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;

namespace GerenciadorLivros.Tests;

public class LivrosControllerTests : IDisposable
{
    private readonly AppDbContext _contexto;
    private readonly LivrosController _controller;

    public LivrosControllerTests()
    {
        // Banco em memória, único para cada teste
        var opcoes = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _contexto = new AppDbContext(opcoes);
        _controller = new LivrosController(_contexto, NullLogger<LivrosController>.Instance);
    }

    public void Dispose() => _contexto.Dispose();

    private static LivroRequest NovoRequest(string titulo = "clean code", string isbn = "9780132350884") => new()
    {
        Titulo = titulo,
        Autor = "robert c. martin",
        Isbn = isbn,
        AnoPublicacao = 2008,
        NumeroPaginas = 464
    };

    [Fact]
    public async Task Criar_DadosValidos_RetornaCreatedComDadosFormatados()
    {
        var resultado = await _controller.Criar(NovoRequest());

        var created = Assert.IsType<CreatedAtActionResult>(resultado.Result);
        var livro = Assert.IsType<LivroResponse>(created.Value);

        Assert.True(livro.Id > 0);
        Assert.Equal("Clean Code", livro.Titulo);
        Assert.Equal("Robert C. Martin", livro.Autor);
        Assert.Equal("978-0132350884", livro.IsbnFormatado);
        Assert.Equal("Cerca de 16 horas", livro.TempoLeituraEstimado);
    }

    [Fact]
    public async Task Criar_IsbnInvalido_RetornaBadRequest()
    {
        var resultado = await _controller.Criar(NovoRequest(isbn: "1234567890123"));

        Assert.IsType<BadRequestObjectResult>(resultado.Result);
        Assert.Empty(_contexto.Livros);
    }

    [Fact]
    public async Task Criar_IsbnDuplicado_RetornaConflict()
    {
        await _controller.Criar(NovoRequest());

        var resultado = await _controller.Criar(NovoRequest(titulo: "outro titulo"));

        Assert.IsType<ConflictObjectResult>(resultado.Result);
        Assert.Single(_contexto.Livros);
    }

    [Fact]
    public async Task ObterPorId_Inexistente_RetornaNotFound()
    {
        var resultado = await _controller.ObterPorId(999);

        Assert.IsType<NotFoundObjectResult>(resultado.Result);
    }

    [Fact]
    public async Task ObterPorId_Existente_RetornaOk()
    {
        var criado = (CreatedAtActionResult)(await _controller.Criar(NovoRequest())).Result!;
        var id = ((LivroResponse)criado.Value!).Id;

        var resultado = await _controller.ObterPorId(id);

        var ok = Assert.IsType<OkObjectResult>(resultado.Result);
        Assert.Equal(id, ((LivroResponse)ok.Value!).Id);
    }

    [Fact]
    public async Task Listar_ComBusca_FiltraPorTituloOuAutor()
    {
        await _controller.Criar(NovoRequest("clean code", "9780132350884"));
        await _controller.Criar(NovoRequest("effective java", "9780134685991"));

        var resultado = await _controller.Listar("java");

        var ok = Assert.IsType<OkObjectResult>(resultado.Result);
        var lista = Assert.IsAssignableFrom<IEnumerable<LivroResponse>>(ok.Value).ToList();
        Assert.Single(lista);
        Assert.Equal("Effective Java", lista[0].Titulo);
    }

    [Fact]
    public async Task Listar_SemBusca_RetornaTodosOrdenadosPorTitulo()
    {
        await _controller.Criar(NovoRequest("zebra", "9780132350884"));
        await _controller.Criar(NovoRequest("abelha", "9780134685991"));

        var resultado = await _controller.Listar();

        var ok = Assert.IsType<OkObjectResult>(resultado.Result);
        var lista = Assert.IsAssignableFrom<IEnumerable<LivroResponse>>(ok.Value).ToList();
        Assert.Equal(2, lista.Count);
        Assert.Equal("Abelha", lista[0].Titulo);
    }

    [Fact]
    public async Task Atualizar_Existente_AlteraDados()
    {
        var criado = (CreatedAtActionResult)(await _controller.Criar(NovoRequest())).Result!;
        var id = ((LivroResponse)criado.Value!).Id;

        var request = NovoRequest("clean architecture", "9780132350884");
        request.NumeroPaginas = 300;

        var resultado = await _controller.Atualizar(id, request);

        var ok = Assert.IsType<OkObjectResult>(resultado.Result);
        var livro = Assert.IsType<LivroResponse>(ok.Value);
        Assert.Equal("Clean Architecture", livro.Titulo);
        Assert.Equal(300, livro.NumeroPaginas);
    }

    [Fact]
    public async Task Atualizar_Inexistente_RetornaNotFound()
    {
        var resultado = await _controller.Atualizar(999, NovoRequest());

        Assert.IsType<NotFoundObjectResult>(resultado.Result);
    }

    [Fact]
    public async Task Excluir_Existente_RetornaNoContentERemove()
    {
        var criado = (CreatedAtActionResult)(await _controller.Criar(NovoRequest())).Result!;
        var id = ((LivroResponse)criado.Value!).Id;

        var resultado = await _controller.Excluir(id);

        Assert.IsType<NoContentResult>(resultado);
        Assert.Empty(_contexto.Livros);
    }

    [Fact]
    public async Task Excluir_Inexistente_RetornaNotFound()
    {
        var resultado = await _controller.Excluir(999);

        Assert.IsType<NotFoundObjectResult>(resultado);
    }
}
