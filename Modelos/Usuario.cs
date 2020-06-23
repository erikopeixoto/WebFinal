using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Modelos
{
    [Table("Usuario")]
    public class Usuario
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Campo Nome é obrigatório")]
        [Column(TypeName = "varchar")]
        [StringLength(40)]
        [Display(Name = "Nome Usuário")]
        public string nmUsuario { get; set; }

        [EmailAddress(ErrorMessage = "Por favor, informe um e-mail válido")]
        [Required(ErrorMessage = "Campo Email é obrigatório")]
        [Display(Name = "Email")]
        [Column(TypeName = "varchar")]
        [StringLength(40)]
        [DataType(DataType.EmailAddress)]
        public string dsEmail { get; set; }

        [MinLength(6, ErrorMessage = "Senha deve conter no mínimo 6 caracteres")]
        [Required(ErrorMessage = "Campo senha é obrigatório")]
        [Display(Name = "Senha")]
        [Column(TypeName = "varchar")]
        [StringLength(50)]
        [DataType(DataType.Password)]
        public string dsSenha { get; set; }
        public ICollection<Produto> Produtos { get; set; }
    }
}