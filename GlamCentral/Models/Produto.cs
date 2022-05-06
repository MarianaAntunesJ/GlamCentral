using GlamCentral.Libraries.Language;
using GlamCentral.Models.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlamCentral.Models
{
    public class Produto
    {
        #region "Propriedades Públicas"

        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E_Obrigatorio")]
        [MinLength(3, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E_Minimo")]
        [MaxLength(20, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E_Maxima")]
        public string Nome { get; set; }

        [DisplayFormat(DataFormatString = "{0:F2}")]
        public string Peso { get; set; }

        public string TipoPeso { get; set; }

        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E_Obrigatorio")]
        [Range(0, 100, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E_Quantidade")]
        public int Quantidade { get; set; }

        [Display(Name = "Ativo")]
        public bool Status { get; set; }

        [Display(Name = "Categoria")]
        public int CategoriaId { get; set; }

        [ForeignKey("CategoriaId")]
        public virtual Categoria Categoria { get; set; }

        [Range(0, 3, ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E_Quantidade")]
        public virtual ICollection<Imagem> Imagens { get; set; }

        public virtual List<ProdutosDeProcedimento> Procedimentos { get; set; }
        #endregion

        #region "Construtores"
        public Produto()
        {
            Procedimentos = new List<ProdutosDeProcedimento>();
        }
        #endregion
    }
}
