// Trabalho Prático 03 - CBTSWE2
// Dupla: André Luiz Nascimento de Andrade e João do Valle Seixas Paula

using GerenciadorProdutos.Models;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorProdutos.Data;

/// <summary>
/// Contexto do Entity Framework: faz a ponte entre os objetos de domínio e o banco de dados.
/// </summary>
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> opcoes) : base(opcoes)
    {
    }

    public DbSet<Produto> Produtos => Set<Produto>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Produtos de exemplo para a lista não começar vazia
        modelBuilder.Entity<Produto>().HasData(
            new Produto { Id = 1, Nome = "Teclado mecânico", Descricao = "Teclado ABNT2 com switches azuis e iluminação LED.", Preco = 289.90m, QuantidadeEstoque = 25 },
            new Produto { Id = 2, Nome = "Mouse sem fio", Descricao = "Mouse óptico 1600 DPI, conexão USB 2,4 GHz.", Preco = 79.90m, QuantidadeEstoque = 4 },
            new Produto { Id = 3, Nome = "Monitor 24 polegadas", Descricao = "Monitor Full HD IPS com entradas HDMI e VGA.", Preco = 899.00m, QuantidadeEstoque = 0 },
            new Produto { Id = 4, Nome = "Cabo HDMI 2 m", Descricao = "Cabo HDMI 2.0 com suporte a 4K a 60 Hz.", Preco = 24.50m, QuantidadeEstoque = 120 }
        );
    }
}
