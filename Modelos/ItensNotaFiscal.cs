using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Modelos
{
    [Table("ItensNotaFiscal")]
    public class ItensNotaFiscal
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Column("ID", TypeName = "bigint")]
        public long ID { get; set; }

        [Required(ErrorMessage = "A Nota fiscal é obrigatória.")]

        [Column("NotaFiscalID", TypeName = "bigint")]
        [Display(Name = "Nota Fiscal")]
        public long NotaFiscalID { get; set; }

        [Required(ErrorMessage = "O produto é obrigatório.")]
        [Display(Name = "Produto")]

        public int ProdutoID { get; set; }

        [Required(ErrorMessage = "A unidade é obrigatória.")]
        [Display(Name = "Unidade")]

        [StringLength(6)]
        public string SgUnidade { get; set; }

        [Required(ErrorMessage = "A quantidade é obrigatória.")]
        [Display(Name = "Quantidade")]
        public decimal QtItem { get; set; }

        [Required(ErrorMessage = "O valor é obrigatorio")]
        [Display(Name = "Valor")]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public decimal VlItem { get; set; }

        [ForeignKey("NotaFiscalID")]
        public virtual NotaFiscal NotaFiscal { get; set; }

        [ForeignKey("SgUnidade")]
        public virtual Unidade Unidade { get; set; }

        [ForeignKey("ProdutoID")]
        public virtual Produto Produto { get; set; }

        public string NomeProduto
        {
            get
            {
                return this.ProdutoID + "-" + this.Produto.nmProduto;
            }
        }

        //[DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        //[DataType(DataType.Currency)]
        public string ValorUnitario
        {
            get
            {
                if (this.QtItem > 0)
                    return String.Format("{0:C}",this.VlItem / this.QtItem);
                else
                    return String.Format("{0:C}", 0);
            }
        }
    }

}