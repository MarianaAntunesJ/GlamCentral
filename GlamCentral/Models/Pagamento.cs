using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlamCentral.Models
{
    public class Pagamento
    {
        public int Id { get; set; }

        public int AgendamentoId { get; set; }

        [ForeignKey("AgendamentoId")]
        public virtual Agenda Agendamento { get; set; }
        public int FormaDePagamento { get; set; }
        public string Desconto { get; set; }

        public string Observacao { get; set; }
    }
}
