using GlamCentral.Models;
using System.Collections.Generic;
using X.PagedList;

namespace GlamCentral.Repository.Interfaces
{
    public interface IProdutoRepository
    {
        void Cadastrar(Produto produto);
        void Atualizar(Produto produto);
        void Excluir(int id);
        Produto ObterProduto(int id);
        List<Produto> ObterTodosProdutos();
        IEnumerable<Produto> ObterProdutosPorId(List<int> ids);
        IEnumerable<Produto> ObterProdutosEntreQuantidade(int min, int max);
        IEnumerable<Produto> ObterTodosProdutos(int categoriaId);
        IPagedList<Produto> ObterTodosProdutos(int? pagina, string pesquisa);
        IPagedList<Produto> ObterTodosProdutos(int? pagina, string pesquisa, string ordenacao);
        IPagedList<Produto> ObterTodosProdutos(int? pagina, string pesquisa, string ordenacao, bool status);
    }
}
