using GlamCentral.Database;
using GlamCentral.Models;
using GlamCentral.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace GlamCentral.Repository
{
    public class ProcedimentoRepository : IProcedimentoRepository
    {
        private IConfiguration _conf;

        #region "Propriedades Privadas"
        private GCContext _banco;
        #endregion

        #region "Construtor"
        public ProcedimentoRepository(GCContext banco, IConfiguration conf)
        {
            _banco = banco;
            _conf = conf;
        }
        #endregion

        #region "Métodos Públicos"
        public void Cadastrar(Procedimento procedimento)
        {
            var a = ObterProdutosPorId(procedimento.Produtos.Select(_ => _.Produto.Id).ToList()).ToList();
            procedimento.Produtos.Clear();
            procedimento.AdicionaProdutos(a);
            _banco.Add(procedimento);
            _banco.SaveChanges();
            //CadastrarA(procedimento.Produtos.Select(_ => _.Produto).ToList(), procedimento);
        }

        private void CadastrarA(List<Produto> produtos, Procedimento procedimento)
        {
            var produtosDeProcedimento = produtos.Select(_ => (new ProdutosDeProcedimento(_.Id, _, procedimento.Id, procedimento))).ToList();
            _banco.AddRange(produtosDeProcedimento);
            _banco.SaveChanges();
        }

        private IEnumerable<Produto> ObterProdutosPorId(List<int> ids)
        {
            return _banco.Produtos.Where(_ => ids.Contains(_.Id));
        }

        public void Atualizar(Procedimento procedimento)
        {
            _banco.Update(procedimento);
            _banco.SaveChanges();
        }

        public void Excluir(int id)
        {
            _banco.Remove(ObterProcedimento(id));
            _banco.SaveChanges();
        }
        public Procedimento ObterProcedimento(int id)
            => _banco.Procedimentos.OrderBy(_ => _.Nome).Where(_ => _.Id == id).FirstOrDefault();

        public IEnumerable<Procedimento> ObterTodosProcedimentos()
        => _banco.Procedimentos;

        public IPagedList<Procedimento> ObterTodosProcedimentos(int? pagina, string pesquisa)
        {
            return ObterTodosProcedimentos(pagina, pesquisa, "A");
        }

        public IPagedList<Procedimento> ObterTodosProcedimentos(int? pagina, string pesquisa, string ordenacao)
        {
            int registroPorPagina = _conf.GetValue<int>("RegistroPorPagina");
            int NumeroPagina = pagina ?? 1;

            var bancoProcedimento = _banco.Procedimentos.AsQueryable();
            if (!string.IsNullOrEmpty(pesquisa))
                bancoProcedimento = bancoProcedimento.Where(_ => _.Nome.Contains(pesquisa.Trim()));

            if (ordenacao == "A")
                bancoProcedimento = bancoProcedimento.OrderBy(_ => _.Nome);
            else if (ordenacao == "ID")
                bancoProcedimento = bancoProcedimento.OrderBy(_ => _.Id);

            return bancoProcedimento.ToPagedList<Procedimento>(NumeroPagina, registroPorPagina);
        }

        public IPagedList<Procedimento> ObterTodosProcedimentos(int? pagina, string pesquisa, string ordenacao, bool status)
        {
            int registroPorPagina = _conf.GetValue<int>("RegistroPorPagina");
            int NumeroPagina = pagina ?? 1;

            var bancoProcedimento = _banco.Procedimentos.AsQueryable();
            if (!string.IsNullOrEmpty(pesquisa))
                bancoProcedimento = bancoProcedimento.Where(_ => _.Nome.Contains(pesquisa.Trim()));

            bancoProcedimento = bancoProcedimento.Where(_ => _.Status == status);

            if (ordenacao == "A")
                bancoProcedimento = bancoProcedimento.OrderBy(_ => _.Nome);
            else if (ordenacao == "ID")
                bancoProcedimento = bancoProcedimento.OrderBy(_ => _.Id);

            return bancoProcedimento.ToPagedList<Procedimento>(NumeroPagina, registroPorPagina);
        }

        public IPagedList<Produto> ObterTodosProdutos(int? pagina, string pesquisa, string ordenacao, bool status)
        {
            int registroPorPagina = _conf.GetValue<int>("RegistroPorPagina");
            int NumeroPagina = pagina ?? 1;

            var bancoProcedimento = _banco.Produtos.AsQueryable();
            if (!string.IsNullOrEmpty(pesquisa))
                bancoProcedimento = bancoProcedimento.Where(_ => _.Nome.Contains(pesquisa.Trim()));

            bancoProcedimento = bancoProcedimento.Where(_ => _.Status == status);

            if (ordenacao == "A")
                bancoProcedimento = bancoProcedimento.OrderBy(_ => _.Nome);
            else if (ordenacao == "ID")
                bancoProcedimento = bancoProcedimento.OrderBy(_ => _.Id);

            return bancoProcedimento.ToPagedList<Produto>(NumeroPagina, registroPorPagina);
        }
        #endregion
    }
}
