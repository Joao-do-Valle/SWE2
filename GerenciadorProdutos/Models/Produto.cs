// Trabalho Prático 03 - CBTSWE2
// Dupla: André Luiz Nascimento de Andrade e João do Valle Seixas Paula

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GerenciadorProdutos.Models;

/// <summary>
/// Objeto de domínio mapeado pelo Entity Framework para a tabela "Produtos".
/// As Data Annotations também fazem a validação dos dados de entrada.
/// </summary>
public class Produto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Informe o nome do produto.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "O nome deve ter entre 2 e 100 caracteres.")]
    [Display(Name = "Nome")]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe a descrição do produto.")]
    [StringLength(500, ErrorMessage = "A descrição pode ter no máximo 500 caracteres.")]
    [Display(Name = "Descrição")]
    public string Descricao { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe o preço.")]
    [Range(0.01, 999999.99, ErrorMessage = "O preço deve estar entre R$ 0,01 e R$ 999.999,99.")]
    [Column(TypeName = "decimal(18,2)")]
    [DataType(DataType.Currency)]
    [Display(Name = "Preço")]
    public decimal Preco { get; set; }

    [Required(ErrorMessage = "Informe a quantidade em estoque.")]
    [Range(0, 1000000, ErrorMessage = "A quantidade deve ser um número inteiro entre 0 e 1.000.000.")]
    [Display(Name = "Quantidade em estoque")]
    public int QuantidadeEstoque { get; set; }
}
