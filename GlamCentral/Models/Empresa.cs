using GlamCentral.Libraries.Language;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GlamCentral.Models
{
    public class Empresa
    {
        #region "Propriedades Públicas"
        // Todo: refazer verificações: colocar as que faltam ou mudar de lugar
        public int Id { get; set; }
        public string RazaoSocial { get; set; }
        public string CNPJ { get; set; }
        public string Telefone { get; set; }     
        #endregion
    }
}
