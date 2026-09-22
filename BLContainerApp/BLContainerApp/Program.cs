// Trabalho Prático 02 - CBTSWE2 (ADS 671)
// Dupla: João do Valle Seixas Paula e André Luiz Nascimento de Andrade

using BLContainerApp.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Adiciona os serviços de MVC (Controllers + Views)
builder.Services.AddControllersWithViews();

// Configura o Entity Framework Core com SQL Server (LocalDB)
builder.Services.AddDbContext<BLContainerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BLContainerContext")
        ?? throw new InvalidOperationException("Connection string 'BLContainerContext' não encontrada.")));

var app = builder.Build();

// Cria o banco de dados automaticamente (com base nas entidades) caso ele ainda não exista.
// Não é necessário rodar "dotnet ef migrations" manualmente para este trabalho.
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<BLContainerContext>();
    db.Database.EnsureCreated();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
