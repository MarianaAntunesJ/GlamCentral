using GlamCentral.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace GlamCentral.Repository.Interfaces
{
    public interface IAgendaRepository
    {
        void Cadastrar(Agenda cliente);
        void Atualizar(Agenda cliente);
        void Excluir(int id);

        Agenda ObterCliente(int id);
        IEnumerable<Agenda> ObterTodosClientes();
        List<Agenda> ObterClientePorEmail(string email);
        IPagedList<Agenda> ObterTodosClientes(int? pagina, string pesquisa);
        IPagedList<Agenda> ObterTodosClientes(int? pagina, string pesquisa, string ordenacao);
        IPagedList<Agenda> ObterTodosClientes(int? pagina, string pesquisa, string ordenacao, bool status);

    }
}
