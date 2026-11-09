using System.ComponentModel.DataAnnotations;

namespace GerenciadorLivros.Api.Dtos;

/// <summary>Dados recebidos da página web para criar ou atualizar um livro.</summary>
public class LivroRequest
{
    [Required(ErrorMessage = "O título é obrigatório.")]
    [StringLength(150, MinimumLength = 2, ErrorMessage = "O título deve ter entre 2 e 150 caracteres.")]
    public string Titulo { get; set; } = string.Empty;

    [Required(ErrorMessage = "O autor é obrigatório.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "O autor deve ter entre 2 e 100 caracteres.")]
    public string Autor { get; set; } = string.Empty;

    [Required(ErrorMessage = "O ISBN é obrigatório.")]
    public string Isbn { get; set; } = string.Empty;

    [Range(1000, 2100, ErrorMessage = "O ano de publicação deve estar entre 1000 e 2100.")]
    public int AnoPublicacao { get; set; }

    [Range(1, 10000, ErrorMessage = "O número de páginas deve estar entre 1 e 10000.")]
    public int NumeroPaginas { get; set; }
}

/// <summary>Dados devolvidos pela API, enriquecidos pela DLL GerenciadorLivros.Utils.</summary>
public class LivroResponse
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Autor { get; set; } = string.Empty;
    public string Isbn { get; set; } = string.Empty;
    public string IsbnFormatado { get; set; } = string.Empty;
    public int AnoPublicacao { get; set; }
    public int NumeroPaginas { get; set; }
    public string TempoLeituraEstimado { get; set; } = string.Empty;
    public DateTime DataCadastro { get; set; }
}
