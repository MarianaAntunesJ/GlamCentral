using GlamCentral.Libraries.Language;
using GlamCentral.Libraries.Validation;
using GlamCentral.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace GlamCentral.Models
{
    public class Cliente
    {
        #region "Propriedades Públicas"
        // Todo: refazer verificações: colocar as que faltam ou mudar de lugar
        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E_Obrigatorio")]
        [MinLength(3, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E_Minimo")]
        [MaxLength(50, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E_Maxima")]
        public string Nome { get; set; }

        public string CPF { get; set; }

        [Display(Name = "Celular")]
        public string Telefone { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Data de Nascimento")]
        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }

        [Display(Name = "Ativo")]
        public bool Status { get; set; }

        [EmailAddress(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E_Email")]
        [EmailUnicoCliente]
        [Display(Name = "E-mail")]
        public string Email { get; set; }
        #endregion 
    }
}
