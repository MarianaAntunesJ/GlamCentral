using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlamCentral.Models
{
    public class Pagamento
    {
        public int Id { get; set; }
        public int Valor { get; set; }
        public int Procedimento { get; set; }
    }
}
