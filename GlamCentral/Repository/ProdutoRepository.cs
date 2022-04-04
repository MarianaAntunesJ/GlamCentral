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
    public class ProdutoRepository : IProdutoRepository
    {
        private IConfiguration _conf;

        #region "Propriedades Privadas"
        private GCContext _banco;
        #endregion

        #region "Construtor"
        public ProdutoRepository(GCContext banco, IConfiguration conf)
        {
            _banco = banco;
            _conf = conf;
        }
        #endregion

        #region "Métodos Públicos"
        public void Cadastrar(Produto produto)
        {
            _banco.Add(produto);
            _banco.SaveChanges();
        }

        public void Atualizar(Produto produto)
        {
            _banco.Update(produto);
            _banco.SaveChanges();
        }

        public void Excluir(int id)
        {
            _banco.Remove(ObterProduto(id));
            _banco.SaveChanges();
        }
        public Produto ObterProduto(int id)
            => _banco.Produtos.Include(_ => _.Imagens).OrderBy(_ => _.Nome).Where(_ => _.Id == id).FirstOrDefault();

        public List<Produto> ObterTodosProdutos()
        {
            return _banco.Produtos.ToList();
        }

        public IPagedList<Produto> ObterTodosProdutos(int? pagina, string pesquisa)
        {
            return ObterTodosProdutos(pagina, pesquisa, "A"); 
        }

        public IPagedList<Produto> ObterTodosProdutos(int? pagina, string pesquisa, string ordenacao)
        {
            int registroPorPagina = _conf.GetValue<int>("RegistroPorPagina");
            int NumeroPagina = pagina ?? 1;

            var bancoProduto = _banco.Produtos.AsQueryable();
            if (!string.IsNullOrEmpty(pesquisa))
                bancoProduto = bancoProduto.Where(_ => _.Nome.Contains(pesquisa.Trim()));

            if(ordenacao == "A")
                bancoProduto = bancoProduto.OrderBy(_ => _.Nome);
            else if (ordenacao == "ID")
                bancoProduto = bancoProduto.OrderBy(_ => _.Id);

            return bancoProduto.Include(_ => _.Imagens).ToPagedList<Produto>(NumeroPagina, registroPorPagina);
        }

        public IPagedList<Produto> ObterTodosProdutos(int? pagina, string pesquisa, string ordenacao, bool status)
        {
            int registroPorPagina = _conf.GetValue<int>("RegistroPorPagina");
            int NumeroPagina = pagina ?? 1;

            var bancoProduto = _banco.Produtos.AsQueryable();
            if (!string.IsNullOrEmpty(pesquisa))
                bancoProduto = bancoProduto.Where(_ => _.Nome.Contains(pesquisa.Trim()));

            bancoProduto = bancoProduto.Where(_ => _.Status == status);

            if (ordenacao == "A")
                bancoProduto = bancoProduto.OrderBy(_ => _.Nome);
            else if (ordenacao == "ID")
                bancoProduto = bancoProduto.OrderBy(_ => _.Id);

            return bancoProduto.Include(_ => _.Imagens).ToPagedList<Produto>(NumeroPagina, registroPorPagina);
        }

        public IEnumerable<Produto> ObterTodosProdutos(int categoriaId)
        {
            return _banco.Produtos.Where(_ => _.CategoriaId == categoriaId);
        }

        public IEnumerable<Produto> ObterProdutosEntreQuantidade(int min, int max)
        {
            return _banco.Produtos.Where(_ => _.Quantidade >= min && _.Quantidade <= max);
        }
        #endregion
    }
}
