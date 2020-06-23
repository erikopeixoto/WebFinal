using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Modelos
{
    [Table("Categoria")]
    public class Categoria
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Display(Name = "Código")]
        public int ID { get; set; }

        [Required(ErrorMessage = "Campo nome é requerido")]
        [Display(Name = "Categoria")]
        [Column(TypeName = "varchar")]
        [StringLength(30)]
        public string NmCategoria { get; set; }

        public virtual ICollection<Produto> Produtos { get; set; }
    }
}