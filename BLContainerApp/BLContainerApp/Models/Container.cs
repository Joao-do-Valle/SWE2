// Trabalho Prático 02 - CBTSWE2 (ADS 671)
// Dupla: João do Valle Seixas Paula e André Luiz Nascimento de Andrade

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BLContainerApp.Models
{
    // Tipo do container, conforme especificado no trabalho: {Dry, Reefer}
    public enum TipoContainer
    {
        Dry,
        Reefer
    }

    // Tamanho do container em pés, conforme especificado no trabalho: {20, 40}
    public enum TamanhoContainer
    {
        [Display(Name = "20 pés")]
        Vinte = 20,

        [Display(Name = "40 pés")]
        Quarenta = 40
    }

    // Entidade Container - lado "0..N" do relacionamento, associada obrigatoriamente a um BL
    public class Container
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe o número do container.")]
        [StringLength(11, ErrorMessage = "O número do container deve ter no máximo 11 caracteres.")]
        [Display(Name = "Número")]
        public string Numero { get; set; }

        [Required(ErrorMessage = "Selecione o tipo do container.")]
        [Display(Name = "Tipo")]
        public TipoContainer Tipo { get; set; }

        [Required(ErrorMessage = "Selecione o tamanho do container.")]
        [Display(Name = "Tamanho")]
        public TamanhoContainer Tamanho { get; set; }

        // Chave estrangeira obrigatória para BL (container deve estar sempre associado a um BL)
        [Required(ErrorMessage = "Selecione o BL relacionado.")]
        [Display(Name = "BL")]
        [ForeignKey(nameof(BL))]
        public int BLId { get; set; }

        public BL BL { get; set; }
    }
}
