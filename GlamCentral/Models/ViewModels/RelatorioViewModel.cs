using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using X.PagedList;

namespace GlamCentral.Models.ViewModels
{
    public class RelatorioViewModel
    {
        public Empresa Empresa { get; set; }
        public IPagedList<Cliente> Clientes { get; set; }

        public List<SelectListItem> Ordenacao
        {
            get
            {
                return new List<SelectListItem>()
                {
                    new SelectListItem("Alfabetica", "A"),
                    new SelectListItem("PorID", "ID")
                };
            }
            private set { }
        }

        public List<SelectListItem> Status
        {
            get
            {
                return new List<SelectListItem>()
                {
                    new SelectListItem("Ativo", "True"),
                    new SelectListItem("Inativo", "False")
                };
            }
            private set { }
        }
    }
}
