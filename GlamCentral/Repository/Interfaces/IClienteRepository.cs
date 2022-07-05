using GlamCentral.Models;
using System.Collections.Generic;
using X.PagedList;

namespace GlamCentral.Repository.Interfaces
{
    public interface IClienteRepository
    {
        void Cadastrar(Cliente cliente);
        void Atualizar(Cliente cliente);
        void Excluir(int id);

        Cliente ObterCliente(int id);
        IEnumerable<string> ObterClientesPorNome(string nomeTresCaracteres);
        IEnumerable<Cliente> ObterTodosClientes();
        List<Cliente> ObterClientePorEmail(string email);
        IPagedList<Cliente> ObterTodosClientes(int? pagina, string pesquisa);
        IPagedList<Cliente> ObterTodosClientes(int? pagina, string pesquisa, string ordenacao);
        IPagedList<Cliente> ObterTodosClientes(int? pagina, string pesquisa, string ordenacao, bool status);
        IPagedList<Cliente> ObterTodosClientes(int? pagina, string ordenacao, bool status);
    }
}
