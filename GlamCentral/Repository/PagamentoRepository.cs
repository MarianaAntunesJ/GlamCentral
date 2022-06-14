using GlamCentral.Database;
using GlamCentral.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using X.PagedList;

namespace GlamCentral.Repository
{
    public class PagamentoRepository
    {
        private IConfiguration _conf;

        #region "Propriedades Privadas"
        private GCContext _banco;
        #endregion

        #region "Construtor"
        public PagamentoRepository(GCContext banco, IConfiguration conf)
        {
            _banco = banco;
            _conf = conf;
        }
        #endregion

        #region "Métodos Públicos"
        public void Cadastrar(Pagamento pagamento)
        {
            _banco.Add(pagamento);
            _banco.SaveChanges();
        }

        public void Atualizar(Pagamento pagamento)
        {
            _banco.Update(pagamento);
            _banco.SaveChanges();
        }

        public void Excluir(int id)
        {
            _banco.Remove(ObterPagamento(id));
            _banco.SaveChanges();
        }

        public Pagamento ObterPagamento(int id)
            => _banco.Pagamentos.Find(id);

        public IEnumerable<Pagamento> ObterTodosPagamentos()
            => _banco.Pagamentos;

        public IPagedList<Pagamento> ObterTodosPagamentos(int? pagina, string pesquisa)
        {
            return ObterTodosPagamentos(pagina, pesquisa, "A");
        }

        // Todo: Refatorar métodos - chamar Amor
        public IPagedList<Pagamento> ObterTodosPagamentos(int? pagina, string pesquisa, string ordenacao)
        {
            int registroPorPagina = _conf.GetValue<int>("RegistroPorPagina");
            int NumeroPagina = pagina ?? 1;

            // Todo: rever o tipo AsQueryable
            var bancoPagamento = _banco.Pagamentos.AsQueryable();
            if (!string.IsNullOrEmpty(pesquisa))
                bancoPagamento = bancoPagamento.Where(_ => _.Agendamento.Cliente.Nome.Contains(pesquisa.Trim()));

            if (ordenacao == "A")
                bancoPagamento = bancoPagamento.OrderBy(_ => _.Agendamento.Cliente.Nome);
            else if (ordenacao == "ID")
                bancoPagamento = bancoPagamento.OrderBy(_ => _.Id);

            return bancoPagamento.ToPagedList<Pagamento>(NumeroPagina, registroPorPagina);
        }

        //public IPagedList<Pagamento> ObterTodosFuncionarios(int? pagina, string pesquisa, string ordenacao, bool status)
        //{
        //    int registroPorPagina = _conf.GetValue<int>("RegistroPorPagina");
        //    int NumeroPagina = pagina ?? 1;

        //    // Todo: rever o tipo AsQueryable
        //    var bancoPagamento = _banco.Pagamentos.AsQueryable();
        //    if (!string.IsNullOrEmpty(pesquisa))
        //        bancoPagamento = bancoPagamento.Where(_ => _.Agendamento.Cliente.Nome.Contains(pesquisa.Trim()));

        //    bancoPagamento = bancoPagamento.Where(_ => _.Status == status);

        //    if (ordenacao == "A")
        //        bancoPagamento = bancoPagamento.OrderBy(_ => _.Agendamento.Cliente.Nome);
        //    else if (ordenacao == "ID")
        //        bancoPagamento = bancoPagamento.OrderBy(_ => _.Id);

        //    return bancoPagamento.ToPagedList<Pagamento>(NumeroPagina, registroPorPagina);
        //}
        #endregion
    }
}
