using GlamCentral.Libraries.Language;
using GlamCentral.Libraries.Validation;
using GlamCentral.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlamCentral.Models
{
    public class Funcionario
    {
        #region "Propriedades Públicas"
        // Todo: refazer verificações: colocar as que faltam ou mudar de lugar
        [Required(AllowEmptyStrings = false)]
        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E_Obrigatorio")]
        [MinLength(3, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E_Minimo")]
        [MaxLength(50, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E_Maxima")]
        public string Nome { get; set; }

        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E_Obrigatorio")]
        [ValidaCpf]
        public string CPF { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Data de Nascimento")]
        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }

        [Display(Name = "Celular")]
        public string Telefone { get; set; }

        [Display(Name = "Ativo")]
        public bool Status { get; set; }

        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E_Obrigatorio")]
        [EmailAddress(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E_Email")]
        [EmailUnicoFuncionario]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E_Obrigatorio")]
        [MinLength(6, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E_Minimo")]
        public string Senha { get; set; }

        [NotMapped]
        [Display(Name = "Confirmar senha")]
        [Compare("Senha", ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E_Senhas")]
        public string ConfirmarSenha { get; set; }

        public int Cargo { get; set; }

        /*
        [Required(ErrorMessage = "Departamento é obrigatório")]
        //[DisplayFormat(DataFormatString = "{0:F2}")]
        [Display(Name = "Departamento")]
        public virtual Department Department { get; set; }

        
        [Required(ErrorMessage = "Departamento é obrigatório")]
        //[DisplayFormat(DataFormatString = "{0:F2}")]
        [Display(Name = "Departamento")]
        public int DepartmentId { get; set; }*/
        #endregion
    }
}
