using GerenciadorLivros.Api.Data;
using GerenciadorLivros.Api.Middleware;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Banco de dados local (SQLite) via Entity Framework Core
builder.Services.AddDbContext<AppDbContext>(opcoes =>
    opcoes.UseSqlite(builder.Configuration.GetConnectionString("Padrao")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Cria o arquivo do banco e as tabelas automaticamente na primeira execução
using (var escopo = app.Services.CreateScope())
{
    var contexto = escopo.ServiceProvider.GetRequiredService<AppDbContext>();
    contexto.Database.EnsureCreated();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseSwagger();
app.UseSwaggerUI();

// Serve a página web (wwwroot/index.html) na raiz do site
app.UseDefaultFiles();
app.UseStaticFiles();

app.MapControllers();

app.Run();

// Permite evoluir o projeto com testes de integração
public partial class Program { }
