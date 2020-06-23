using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Modelos
{
    [Table("Cliente")]
    public class Cliente
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        [Key]
        [Display(Name = "Código")]
        public int ID { get; set; }

        [Required]
        [Display(Name = "Nome")]
        [Column(TypeName = "varchar")]
        [StringLength(40)]
        public string NmCliente { get; set; }

        [Required]
        [Display(Name = "Endereço")]
        [Column(TypeName = "varchar")]
        [StringLength(40)]
        public string DsEndereco { get; set; }

        [Required]
        [Display(Name = "CEP")]
        [Column(TypeName = "int")]
        [DisplayFormat(DataFormatString = "{0:#####-###}", ApplyFormatInEditMode = false)]
        [DataType(DataType.PostalCode)]
        public int NuCep { get; set; }

        [Required(ErrorMessage = "Campo tipo é obrigatório")]
        [Display(Name = "Tipo")]
        [Column(TypeName = "varchar")]
        [StringLength(1)]
        public string TpCliente { get; set; }

        [Required]
        [Display(Name = "CNPJ/CPF")]
        [Column(TypeName = "bigint")]
        [DataType(DataType.Text)]
        public long NuCnpjCpf { get; set; }

        [Required]
        [Display(Name = "Telefone")]
        [Column(TypeName = "bigint")]
        [DisplayFormat(DataFormatString = "{0:(##)#########}", ApplyFormatInEditMode = false)]
        [DataType(DataType.PhoneNumber)]
        public long NuTelefone { get; set; }

        [UIHint("Data")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Dt.Criação")]
        public DateTime DtCriacao { get; set; }

        public string CPFCNPJFormatado
        {
            get
            {
                if (this.TpCliente == "F")
                    return  String.Format(@"{0:000\.000\.000\-00}",this.NuCnpjCpf);
                else
                    return String.Format(@"{0:00\.000\.000\/0000\-00}", this.NuCnpjCpf);
            }
        }

        public virtual ICollection<NotaFiscal> NotasFiscais { get; set; }
    }
}