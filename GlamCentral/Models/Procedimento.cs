using GlamCentral.Libraries.Language;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GlamCentral.Models
{
    public class Procedimento
    {
        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E_Obrigatorio")]
        public string Nome { get; set; }
                
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E_Obrigatorio")]
        public String Duracao { get; set; }

        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E_Obrigatorio")]
        public float Valor { get; set; }

        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E_Obrigatorio")]
        public bool Status { get; set; }

        [Display(Name = "Produtos")]
        public int ProdutosId { get; set; }

        [ForeignKey("ProdutosId")]
        public virtual Produto Produtos { get; set; }
    }
}
