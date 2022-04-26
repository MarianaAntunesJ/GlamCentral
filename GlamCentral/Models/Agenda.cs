using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlamCentral.Models
{
    public class Agenda
    {
        public int Id { get; set; }

        [Display(Name = "Cliente")]
        public int ClienteId { get; set; }

        [ForeignKey("ClienteId")]
        public virtual Cliente Cliente { get; set; }

        [Display(Name = "Funcionario")]
        public int FuncionarioId { get; set; }

        [ForeignKey("FuncionarioId")]
        public virtual Funcionario Funcionario { get; set; }

        [Display(Name = "Procedimento")]
        public int ProcedimentoId { get; set; }

        [ForeignKey("ProcedimentoId")]
        public virtual Procedimento Procedimento { get; set; }

        public string Description { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Início")]
        [DataType(DataType.Date)]
        public DateTime Start { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Final")]
        [DataType(DataType.Date)]
        [Required(AllowEmptyStrings = true)]
        public DateTime Duration { get; set; }
        public string ThemeColor { get; set; }
        public bool IsFullDay { get; set; }

        public Agenda()
        {
        }
    }
}
