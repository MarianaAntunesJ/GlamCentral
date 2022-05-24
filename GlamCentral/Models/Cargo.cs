using System.Collections.Generic;

namespace GlamCentral.Models
{
    public class Cargo
    {
        public int Id { get; set; }
        public int Nome { get; set; }
        public List<int> Permissoes { get; set; }
    }
}
