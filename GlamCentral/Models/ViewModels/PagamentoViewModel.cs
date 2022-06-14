using System.Collections.Generic;
using X.PagedList;

namespace GlamCentral.Models.ViewModels
{
    public class PagamentoViewModel
    {
        public Cliente Cliente { get; set; }
        public Agenda Agendamento { get; set; }
        public Funcionario Funcionario { get; set; }
        public Procedimento Procedimento { get; set; }
        public List<Produto> Produtos { get; set; }
        public List<int> FormaPagamento { get; set; }
        public IPagedList<Pagamento> Pagamentos { get; set; }
        public IList<int> ProdutosUtilizados { get; set; }

        public PagamentoViewModel(IPagedList<Pagamento> pagamentos)
        {
            Pagamentos = pagamentos;
        }
    }
}
