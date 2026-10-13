// Trabalho Prático 03 - CBTSWE2 (ADS 671) - IFSP Cubatão
// Dupla: André Luiz Nascimento de Andrade e João do Valle Seixas Paula
// Tema: Sistema de Gerenciamento de Produtos (ASP.NET Core MVC + Entity Framework Core)

using System.Globalization;
using GerenciadorProdutos.Data;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// MVC (Controllers + Views)
builder.Services.AddControllersWithViews();

// Entity Framework Core com SQLite (o banco é um arquivo local, sem instalar nada)
builder.Services.AddDbContext<AppDbContext>(opcoes =>
    opcoes.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Cultura pt-BR: preço com vírgula (10,50) e moeda em R$
var ptBr = new CultureInfo("pt-BR");
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture(ptBr),
    SupportedCultures = new[] { ptBr },
    SupportedUICultures = new[] { ptBr }
});

// Cria o banco (e os produtos de exemplo) na primeira execução
using (var escopo = app.Services.CreateScope())
{
    var contexto = escopo.ServiceProvider.GetRequiredService<AppDbContext>();
    contexto.Database.EnsureCreated();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Erro");
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// Roteamento de URL
// 1) Rota personalizada: /produto/3 abre os detalhes do produto 3
app.MapControllerRoute(
    name: "detalhes-produto",
    pattern: "produto/{id:int}",
    defaults: new { controller = "Produtos", action = "Detalhes" });

// 2) Rota padrão: /Controller/Action/Id. A página inicial é a lista de produtos
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Produtos}/{action=Index}/{id?}");

app.Run();
