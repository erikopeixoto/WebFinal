using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Modelos
{
    [Table("Unidade")]
    public class Unidade
    {
        [Key]
        [Display(Name = "Sigla")]
        [StringLength(6)]
        [Column(TypeName = "varchar")]
        public string SgUnidade { get; set; }

        [Required(ErrorMessage = "Campo nome é requerido")]
        [Display(Name = "Descrição")]
        [Column(TypeName = "varchar")]
        [StringLength(50)]
        public string DsUnidade { get; set; }

    }
}