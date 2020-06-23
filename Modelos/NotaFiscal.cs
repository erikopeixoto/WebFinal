using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Modelos
{
    [Table("NotaFiscal")]
    public class NotaFiscal
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Column("ID", TypeName = "bigint")]
        public long ID { get; set; }

        [Required(ErrorMessage = "A Nota fiscal é obrigatória.")]
        [Range(1, 999999999, ErrorMessage = "O numero da NF deve ser maior que zero")]
        [Display(Name = "Nota Fiscal")]
        public long NuNota { get; set; }

        [Required(ErrorMessage = "A serie é obrigatória.")]
        [Range(1, 999, ErrorMessage = "A série deve estar entre 1 e 999.")]
        [Display(Name = "Serie")]
        public int SeNota { get; set; }

        [Required(ErrorMessage = "O modelo é obrigatório.")]
        [Display(Name = "Modelo")]
        public int MdNota { get; set; }

        [Required(ErrorMessage = "A chave de acesso é obrigatória.")]
        [Display(Name = "Chave Acesso")]
        [StringLength(44)]
        public string NuChaveAcesso { get; set; }

        [Required(ErrorMessage = "O cliente é obrigatorio")]

        [Display(Name = "Cliente")]
        [Range(1, 99999999, ErrorMessage = "O código do cliente deve ser maior que zero")]
        public int ClienteId { get; set; }

        [ForeignKey("ClienteId")]
        public virtual Cliente Cliente { get; set; }

        [UIHint("Data")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Dt.Movimento")]
        public DateTime DtMovimento { get; set; }

        [Required(ErrorMessage = "O valor da venda é obrigatorio.")]
        [Range(1, 99999999999999, ErrorMessage = "O campo Valor da venda obrigatório")]
        [Display(Name = "Valor")]
        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString = "{0:C}", ApplyFormatInEditMode = true)]
        public decimal VlNota { get; set; }

        [Display(Name = "Cancelada")]
        [StringLength(1)]
        public string FlCancelada { get; set; }

        public ICollection<ItensNotaFiscal> ItensNotasFiscais { get; set; }
    }
}