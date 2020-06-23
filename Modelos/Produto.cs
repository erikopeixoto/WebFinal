using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Modelos
{
    [Table("Produto")]
    public class Produto
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        [Key]
        [DisplayName("Código")]
        public int ID { get; set; }

        [Required(ErrorMessage = "O nome do produto é obrigatório")]
        [StringLength(30)]
        [DisplayName("Nome")]
        [Column(TypeName = "varchar")]
        public string nmProduto { get; set; }

        [Required]
        [StringLength(300, ErrorMessage = "A descrição do produto deve ter no máximo 300 caracteres")]
        [DataType(DataType.MultilineText)]
        [Column(TypeName = "varchar")]
        [DisplayName("Descrição")]
        public string dsProduto { get; set; }


        [Column(TypeName = "varchar")]
        [StringLength(6)]
        [DisplayName("Unidade")]
        public string SgUnidade { get; set; }

        [ForeignKey("SgUnidade")]
        public virtual Unidade Unidade { get; set; }

        [Required(ErrorMessage = "O preço de venda é obrigatorio.")]
        [Range(1, 99999999999999, ErrorMessage = "O campo preço da venda obrigatório")]
        [DisplayName("Preço")]
        public decimal vlPreco { get; set; }

        [Display(Name = "Qtde Estoque")]
        public int qtEstoque { get; set; }

        [Required]

        [DisplayName("Usuário")]
        public int IdUserCreate { get; set; }

        [ForeignKey("IdUserCreate")]
        public virtual Usuario Usuario { get; set; }

        [Required]

        [DisplayName("Categoria")]
        public int CategoriaId { get; set; }

        [ForeignKey("CategoriaId")]
        public virtual Categoria Categoria { get; set; }

    }
}