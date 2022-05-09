using GlamCentral.Database;
using GlamCentral.Models;
using GlamCentral.Repository.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
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
            if(procedimento.Id == 0)
            {
                AddProdutos(procedimento);
                _banco.Add(procedimento);
                _banco.SaveChanges();
            }
            else
            {
                Atualizar(procedimento);
            }
        }        

        public void Atualizar(Procedimento procedimento)
        {
            var c = procedimento.Produtos.Select(_ => _.Produto.Id).ToList();
            var produtos = ObterProdutosPorId(c);


            procedimento.Produtos.Clear();

            procedimento.Produtos.AddRange(produtos.Select(_ => new ProdutosDeProcedimento(_, procedimento)));

            foreach(var produto in produtos)
            {
                produto.Procedimentos.Add(new ProdutosDeProcedimento(produto, procedimento));
            }

            //produtos = produtos.Select(produto => produto.Procedimentos.Add(new ProdutosDeProcedimento(produto, procedimento)));

            //_banco.Update(procedimento);
            _banco.SaveChanges();


            //_banco.AttachRange(procedimento.Produtos);
            //_banco.AddRange(procedimento.Produtos);
            //procedimento.Produtos.Select(_ => _banco.Entry<ProdutosDeProcedimento>(_).State = EntityState.Modified);


            //var a = _banco.Entry<Procedimento>(procedimento);
            //a.State = EntityState.Modified;
            //a.Collection(i => i.Produtos).Load();

            //AddProdutos(procedimento);

            //var b = new List<ProdutosDeProcedimento>(procedimento.Produtos);
            //procedimento.Produtos.
            //procedimento.Produtos.Clear();
            //AddProdutos1(procedimento);
            //if (procedimento.Produtos.Any() && procedimento.Produtos.Select(_ => _.ProdutoId != 0).Any())
            /*RemoveProdutos(procedimento);
            var d = _banco.ProdutosDeProcedimento;

            var produtos = ObterProdutosPorId(procedimento.Produtos.Select(_ => _.Produto.Id).ToList()).ToList();
            procedimento.Produtos.Clear();

            var produtosProcedimento = produtos.Select(_ => (new ProdutosDeProcedimento(_, procedimento))).ToList();

            procedimento.Produtos.AddRange(produtosProcedimento);

            //foreach (var produtoProcedimento in b)
            //{
            //    var produto = _banco.Produtos.Find(produtoProcedimento.Produto.Id);
            //    procedimento.Produtos.Add(new ProdutosDeProcedimento(produto.Id, procedimento.Id));
            //}
            ////procedimento.Produtos.Select(i => i.).Load();
            //var f  = _banco.Entry<Procedimento>(procedimento);

            _banco.Update(procedimento);
            //_banco.Attach(procedimento);
            _banco.SaveChanges();*/
        }

        public void RemoveProdutos(Procedimento procedimento)
        {
            var c =  _banco.ProdutosDeProcedimento.AsNoTracking().Where(_ => _.ProcedimentoId == procedimento.Id).Select(_ => new ProdutosDeProcedimento(_.Produto, _.Procedimento)).ToList();
            //_banco.ProdutosDeProcedimento.Remove(new ProdutosDeProcedimento() { ProcedimentoId = id}); //Where(_ => _.ProcedimentoId == id).Select(_ => new ProdutosDeProcedimento(_.Produto, _.Procedimento)).ToList();

            _banco.RemoveRange(c);
            //var local = _banco.Set<ProdutosDeProcedimento>()
            //.Local
            //.FirstOrDefault(entry => entry.Id.Equals(entryId));


            //var a = _banco.Entry<ProdutosDeProcedimento>(procedimento.Produtos.FirstOrDefault());
            //a.State = EntityState.Detached;
            //a.State = EntityState.Modified;

            
            //_banco.SaveChanges();
        }

        private void AddProdutos(Procedimento procedimento)
        {
            var produtos = ObterProdutosPorId(procedimento.Produtos.Select(_ => _.Produto.Id).ToList()).ToList();
            procedimento.Produtos.Clear();
            procedimento.AdicionaProdutos(produtos);
        }


        private void AddProdutos1(Procedimento procedimento)
        {
            var produtos = ObterProdutosPorId(procedimento.Produtos.Select(_ => _.ProdutoId).ToList()).ToList();
            procedimento.Produtos.Clear();
            procedimento.AdicionaProdutos(produtos);
        }


        private IEnumerable<Produto> ObterProdutosPorId(List<int> ids)
        {
            return _banco.Produtos.Where(_ => ids.Contains(_.Id));
        }

        public void Excluir(int id)
        {
            var procedimento = ObterProcedimento(id);
            ObterProdutos(procedimento);
            _banco.Remove(procedimento);
            _banco.RemoveRange(procedimento.Produtos.Select(_ => new ProdutosDeProcedimento(_.ProdutoId, _.ProcedimentoId)));
            _banco.SaveChanges();
        }

        public Procedimento ObterProcedimento(int id)
        {
            var procedimento = _banco.Procedimentos.OrderBy(_ => _.Nome).Where(_ => _.Id == id).FirstOrDefault();
            ObterProdutos(procedimento);
            return procedimento;
        }

        public IEnumerable<Procedimento> ObterTodosProcedimentos()
        {
            var procedimentos = _banco.Procedimentos;
            foreach(var procedimento in procedimentos)
            {
                ObterProdutos(procedimento);
            }
            return procedimentos;
        }

        private void ObterProdutos(Procedimento procedimento)
        {
            var produtosId = _banco.ProdutosDeProcedimento.Where(_ => _.ProcedimentoId == procedimento.Id).Select(_ => _.ProdutoId).ToList();
            var produtos = ObterProdutosPorId(produtosId).ToList();
            procedimento.AdicionaProdutos(produtos);
        }

        public IPagedList<Procedimento> ObterTodosProcedimentos(int? pagina, string pesquisa)
        {
            return ObterTodosProcedimentos(pagina, pesquisa, "A");
        }

        public IPagedList<Procedimento> ObterTodosProcedimentos(int? pagina, string pesquisa, string ordenacao)
        {
            int registroPorPagina = _conf.GetValue<int>("RegistroPorPagina");
            int NumeroPagina = pagina ?? 1;
            //var procedimentos = ObterTodosProcedimentos().AsQueryable();
            var procedimentos = ObterTodosProcedimentos();

            if (!string.IsNullOrEmpty(pesquisa))
                procedimentos = procedimentos.Where(_ => _.Nome.Contains(pesquisa.Trim()));

            if (ordenacao == "A")
                procedimentos = procedimentos.OrderBy(_ => _.Nome);
            else if (ordenacao == "ID")
                procedimentos = procedimentos.OrderBy(_ => _.Id);

            return procedimentos.ToPagedList<Procedimento>(NumeroPagina, registroPorPagina);
        }

        public IPagedList<Procedimento> ObterTodosProcedimentos(int? pagina, string pesquisa, string ordenacao, bool status)
        {
            int registroPorPagina = _conf.GetValue<int>("RegistroPorPagina");
            int NumeroPagina = pagina ?? 1;
            var procedimentos = ObterTodosProcedimentos();

            if (!string.IsNullOrEmpty(pesquisa))
                procedimentos = procedimentos.Where(_ => _.Nome.Contains(pesquisa.Trim()));

            procedimentos = procedimentos.Where(_ => _.Status == status);

            if (ordenacao == "A")
                procedimentos = procedimentos.OrderBy(_ => _.Nome);
            else if (ordenacao == "ID")
                procedimentos = procedimentos.OrderBy(_ => _.Id);

            return procedimentos.ToPagedList<Procedimento>(NumeroPagina, registroPorPagina);
        }

        public IPagedList<Produto> ObterProdutos(int? pagina, int procedimentoId)
        {
            int registroPorPagina = _conf.GetValue<int>("RegistroPorPagina");
            int NumeroPagina = pagina ?? 1;

            var produtosSelecionados = new List<Produto>();

            return produtosSelecionados.ToPagedList<Produto>(NumeroPagina, registroPorPagina);
        }

        public IPagedList<Produto> ObterProdutosSelecionados(int? pagina, Procedimento procedimento)
        {
            int registroPorPagina = _conf.GetValue<int>("RegistroPorPagina");
            int NumeroPagina = pagina ?? 1;

            var produtosSelecionados = new List<Produto>();
            if (procedimento != null)
            {
                foreach (var produtoProcedimento in procedimento.Produtos)
                {
                    produtosSelecionados.Add(produtoProcedimento.Produto);
                }
            }

            return produtosSelecionados.ToPagedList<Produto>(NumeroPagina, registroPorPagina);
        }
        #endregion
    }
}
