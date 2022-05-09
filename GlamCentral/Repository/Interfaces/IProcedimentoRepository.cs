using GlamCentral.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace GlamCentral.Repository.Interfaces
{
    public interface IProcedimentoRepository
    {
        void Cadastrar(Procedimento procedimento);
        void Atualizar(Procedimento procedimento);
        void Excluir(int id);
        Procedimento ObterProcedimento(int id);
        IEnumerable<Procedimento> ObterTodosProcedimentos();
        IPagedList<Procedimento> ObterTodosProcedimentos(int? pagina, string pesquisa);
        IPagedList<Procedimento> ObterTodosProcedimentos(int? pagina, string pesquisa, string ordenacao);
        IPagedList<Procedimento> ObterTodosProcedimentos(int? pagina, string pesquisa, string ordenacao, bool status);
        //IPagedList<Produto> ObterTodosProdutos(int? pagina, string pesquisa, string ordenacao, bool status);
        IPagedList<Produto> ObterProdutos(int? pagina, int procedimentoId);
        IPagedList<Produto> ObterProdutosSelecionados(int? pagina, Procedimento procedimento);
    }
}
