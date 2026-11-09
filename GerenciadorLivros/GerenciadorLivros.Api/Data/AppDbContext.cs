using GerenciadorLivros.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorLivros.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> opcoes) : base(opcoes)
    {
    }

    public DbSet<Livro> Livros => Set<Livro>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Livro>(entidade =>
        {
            entidade.HasKey(l => l.Id);
            entidade.Property(l => l.Titulo).IsRequired().HasMaxLength(150);
            entidade.Property(l => l.Autor).IsRequired().HasMaxLength(100);
            entidade.Property(l => l.Isbn).IsRequired().HasMaxLength(13);
            entidade.HasIndex(l => l.Isbn).IsUnique();
        });
    }
}
