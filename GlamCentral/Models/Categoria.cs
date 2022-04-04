using GlamCentral.Libraries.Language;
using GlamCentral.Models.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlamCentral.Models
{
    public class Categoria
    {
        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E_Obrigatorio")]
        [MinLength(3, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E_Minimo")]
        [MaxLength(25, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E_Maxima")]
        [Display(Name = "Nome da categoria/subcategoria")]
        public string Nome { get; set; }

        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E_Obrigatorio")]
        [MinLength(3, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E_Minimo")]
        [MaxLength(30, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E_Maxima")]
        public string Slug { get; set; }

        [Display(Name = "Categoria")]
        public int? CategoriaPaiID { get; set; }

        [ForeignKey("CategoriaPaiID")]
        [Display(Name = "Categoria")]
        public virtual Categoria CategoriaPai { get; set; }

        public bool Status { get; set; }
    }
}
