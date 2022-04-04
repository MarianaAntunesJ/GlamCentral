using System.Collections.Generic;
using System.Linq;

namespace GlamCentral.Models
{
    public static class TipoPeso
    {
        public static string Kilograma => "Kg";
        public static string Grama => "g";
        public static string Miligrama => "mg";
        public static string Litro => "L";
        public static string Mililitro => "ml";


        public static IEnumerable<string> ObterTiposPesos() => typeof(TipoPeso).GetProperties().Select(_ => _.GetValue(null).ToString()).ToList();
    }
}
