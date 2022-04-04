using GlamCentral.Migrations;
using GlamCentral.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace GlamCentral.Repository
{
    public class AgendaRepository : IAgendaRepository
    {
        public void Atualizar(Agenda cliente)
        {
            throw new NotImplementedException();
        }

        public void Cadastrar(Agenda cliente)
        {
            throw new NotImplementedException();
        }

        public void Excluir(int id)
        {
            throw new NotImplementedException();
        }

        public Agenda ObterCliente(int id)
        {
            throw new NotImplementedException();
        }

        public List<Agenda> ObterClientePorEmail(string email)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Agenda> ObterTodosClientes()
        {
            throw new NotImplementedException();
        }

        public IPagedList<Agenda> ObterTodosClientes(int? pagina, string pesquisa)
        {
            throw new NotImplementedException();
        }

        public IPagedList<Agenda> ObterTodosClientes(int? pagina, string pesquisa, string ordenacao)
        {
            throw new NotImplementedException();
        }

        public IPagedList<Agenda> ObterTodosClientes(int? pagina, string pesquisa, string ordenacao, bool status)
        {
            throw new NotImplementedException();
        }
    }
}
