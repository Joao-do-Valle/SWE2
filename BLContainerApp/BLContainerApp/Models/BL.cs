// Trabalho Prático 02 - CBTSWE2 (ADS 671)
// Dupla: João do Valle Seixas Paula e André Luiz Nascimento de Andrade

using System.ComponentModel.DataAnnotations;

namespace BLContainerApp.Models
{
    // Entidade BL (Bill of Lading) - lado "1" do relacionamento com Container (0..N)
    public class BL
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe o número do BL.")]
        [StringLength(50)]
        [Display(Name = "Número do BL")]
        public string Numero { get; set; }

        [Required(ErrorMessage = "Informe o Consignee.")]
        [StringLength(100)]
        [Display(Name = "Consignee")]
        public string Consignee { get; set; }

        [Required(ErrorMessage = "Informe o navio.")]
        [StringLength(100)]
        [Display(Name = "Navio")]
        public string Navio { get; set; }

        // Coleção de containers associados a este BL (relação 1 -> 0..N)
        public ICollection<Container> Containers { get; set; } = new List<Container>();
    }
}
