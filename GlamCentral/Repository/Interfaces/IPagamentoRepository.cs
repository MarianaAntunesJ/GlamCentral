using GlamCentral.Models;
using System.Collections.Generic;
using X.PagedList;

namespace GlamCentral.Repository.Interfaces
{
    public interface IPagamentoRepository
    {
        public void Cadastrar(Pagamento pagamento);
        public void Atualizar(Pagamento pagamento);
        public void Excluir(int id);
        public Pagamento ObterPagamento(int id);
        public IEnumerable<Pagamento> ObterTodosPagamentos();
        public IPagedList<Pagamento> ObterTodosPagamentos(int? pagina, string pesquisa);
        public IPagedList<Pagamento> ObterTodosPagamentos(int? pagina, string pesquisa, string ordenacao);
    }
}
