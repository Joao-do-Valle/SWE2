namespace GerenciadorLivros.Api.Models;

/// <summary>Entidade persistida no banco de dados.</summary>
public class Livro
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Autor { get; set; } = string.Empty;
    public string Isbn { get; set; } = string.Empty;
    public int AnoPublicacao { get; set; }
    public int NumeroPaginas { get; set; }
    public DateTime DataCadastro { get; set; } = DateTime.UtcNow;
}
