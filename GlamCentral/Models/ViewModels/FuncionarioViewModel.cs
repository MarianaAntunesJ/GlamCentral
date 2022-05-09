using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using X.PagedList;

namespace GlamCentral.Models.ViewModels
{
    public class FuncionarioViewModel
    {
        public IPagedList<Funcionario> Funcionarios { get; set; }
        public Funcionario Funcionario { get; set; }
        public Funcionario FuncionarioSelecionado { get; set; }

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

        public bool Status { get; set; }

        public List<SelectListItem> StatusList
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

        public FuncionarioViewModel()
        {
        }
    }
}
