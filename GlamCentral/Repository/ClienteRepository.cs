using GlamCentral.Database;
using GlamCentral.Models;
using GlamCentral.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using X.PagedList;

namespace GlamCentral.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private IConfiguration _conf;

        #region "Propriedades Privadas"
        private GCContext _banco;
        #endregion

        #region "Construtor"
        public ClienteRepository(GCContext banco, IConfiguration conf)
        {
            _banco = banco;
            _conf = conf;
        }
        #endregion

        #region "Métodos Públicos"
        public void Cadastrar(Cliente cliente)
        {
            _banco.Add(cliente);
            _banco.SaveChanges();
        }

        public void Atualizar(Cliente cliente)
        {
            _banco.Update(cliente);
            _banco.SaveChanges();
        }
                
        public void Excluir(int id)
        {
            _banco.Remove(ObterCliente(id));
            _banco.SaveChanges();
        }

        public Cliente ObterCliente(int id) 
            => _banco.Clientes.Find(id);

        public IEnumerable<string> ObterClientesPorNome(string nomeTresCaracteres)
            => _banco.Clientes.Where(_ => _.Nome.Substring(0, 3) == nomeTresCaracteres).Select(_ => _.Nome);

        public IEnumerable<Cliente> ObterTodosClientes() 
            => _banco.Clientes.ToList();        

        public List<Cliente> ObterClientePorEmail(string email)
        {
            return _banco.Clientes.Where(_ => _.Email == email).AsNoTracking().ToList();
        }

        public IPagedList<Cliente> ObterTodosClientes(int? pagina, string pesquisa)
        {
            return ObterTodosClientes(pagina, pesquisa, "A");
        }

        public IPagedList<Cliente> ObterTodosClientes(int? pagina, string pesquisa, string ordenacao)
        {
            int registroPorPagina = _conf.GetValue<int>("RegistroPorPagina");
            int NumeroPagina = pagina ?? 1;

            var bancoClientes = _banco.Clientes.AsQueryable();
            if (!string.IsNullOrEmpty(pesquisa))
                bancoClientes = bancoClientes.Where(_ => _.Nome.Contains(pesquisa.Trim()));

            if (ordenacao == "A")
                bancoClientes = bancoClientes.OrderBy(_ => _.Nome);
            else if (ordenacao == "ID")
                bancoClientes = bancoClientes.OrderBy(_ => _.Id);

            return bancoClientes.ToPagedList<Cliente>(NumeroPagina, registroPorPagina);
        }

        public IPagedList<Cliente> ObterTodosClientes(int? pagina, string pesquisa, string ordenacao, bool status)
        {
            int registroPorPagina = _conf.GetValue<int>("RegistroPorPagina");
            int NumeroPagina = pagina ?? 1;

            var bancoClientes = _banco.Clientes.AsQueryable();
            if (!string.IsNullOrEmpty(pesquisa))
                bancoClientes = bancoClientes.Where(_ => _.Nome.Contains(pesquisa.Trim()));

            bancoClientes = bancoClientes.Where(_ => _.Status == status);

            if (ordenacao == "A")
                bancoClientes = bancoClientes.OrderBy(_ => _.Nome);
            else if (ordenacao == "ID")
                bancoClientes = bancoClientes.OrderBy(_ => _.Id);

            return bancoClientes.ToPagedList<Cliente>(NumeroPagina, registroPorPagina);
        }

        public IPagedList<Cliente> ObterTodosClientes(int? pagina, string ordenacao, bool status)
        {
            int registroPorPagina = _conf.GetValue<int>("RegistroPorPagina");
            int NumeroPagina = 1;

            var bancoClientes = _banco.Clientes.AsQueryable();

            bancoClientes = bancoClientes.Where(_ => _.Status == status);

            if (ordenacao == "A")
                bancoClientes = bancoClientes.OrderBy(_ => _.Nome);
            else if (ordenacao == "ID")
                bancoClientes = bancoClientes.OrderBy(_ => _.Id);

            return bancoClientes.ToPagedList<Cliente>(NumeroPagina, registroPorPagina);
        }
        #endregion
    }
}
