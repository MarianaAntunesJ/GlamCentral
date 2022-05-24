using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using X.PagedList;

namespace GlamCentral.Models.ViewModels
{
    public class ClienteViewModel
    {
        public IPagedList<Cliente> Clientes { get; set; }
        public Cliente Cliente { get; set; }
        public Cliente ClienteSelecionado { get; set; }

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

        public ClienteViewModel()
        {
        }

        public ClienteViewModel(IPagedList<Cliente> clientes)
        {
            Clientes = clientes;
        }
    }
}
