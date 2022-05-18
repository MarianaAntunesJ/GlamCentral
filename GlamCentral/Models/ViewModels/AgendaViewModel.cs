using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using X.PagedList;

namespace GlamCentral.Models.ViewModels
{
    public class AgendaViewModel
    {
        public IPagedList<Cliente> Clientes { get; set; }
        public Cliente Cliente { get; set; }
        public Cliente ClienteSelecionado { get; set; }
        public IPagedList<Funcionario> Funcionarios { get; set; }
        public Funcionario Funcionario { get; set; }
        public Funcionario FuncionarioSelecionado { get; set; }
        public IPagedList<Procedimento> Procedimentos { get; set; }
        public Procedimento Procedimento { get; set; }
        public Procedimento ProcedimentoSelecionado { get; set; }
        public IPagedList<Agenda> Agendamentos { get; set; }
        public Agenda Agendamento { get; set; }
        public Agenda AgendamentoSelecionado { get; set; }

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

        public AgendaViewModel()
        {
        }

        public AgendaViewModel(IPagedList<Cliente> clientes)
        {
            Clientes = clientes;
        }

        public AgendaViewModel(IPagedList<Funcionario> funcionarios)
        {
            Funcionarios = funcionarios;
        }

        public AgendaViewModel(IPagedList<Procedimento> procedimentos)
        {
            Procedimentos = procedimentos;
        }

        public AgendaViewModel(IPagedList<Cliente> clientes, IPagedList<Funcionario> funcionarios, IPagedList<Procedimento> procedimentos) : this(clientes)
        {
            Funcionarios = funcionarios;
            Procedimentos = procedimentos;
        }
    }
}
