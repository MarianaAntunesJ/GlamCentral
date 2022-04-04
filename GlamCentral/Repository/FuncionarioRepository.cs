using GlamCentral.Database;
using GlamCentral.Models;
using GlamCentral.Models.Enums;
using GlamCentral.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using X.PagedList;

namespace GlamCentral.Repository
{
    public class FuncionarioRepository : IFuncionarioRepository
    {
        private IConfiguration _conf;

        #region "Propriedades Privadas"
        private GCContext _banco;
        #endregion

        #region "Construtor"
        public FuncionarioRepository(GCContext banco, IConfiguration conf)
        {
            _banco = banco;
            _conf = conf;
        }
        #endregion

        #region "Métodos Públicos"
        public void Cadastrar(Funcionario funcionario)
        {
            _banco.Add(funcionario);
            _banco.SaveChanges();
        }

        public void Atualizar(Funcionario funcionario)
        {
            _banco.Update(funcionario);
            _banco.Entry(funcionario).Property(_ => _.Senha).IsModified = false;
            _banco.SaveChanges();
        }

        public void AtualizarSenha(Funcionario funcionario)
        {
            _banco.Update(funcionario);
            _banco.Entry(funcionario).Property(a => a.Nome).IsModified = false;
            _banco.Entry(funcionario).Property(a => a.CPF).IsModified = false;
            _banco.Entry(funcionario).Property(a => a.DataNascimento).IsModified = false;
            _banco.Entry(funcionario).Property(a => a.Telefone).IsModified = false;
            _banco.Entry(funcionario).Property(a => a.Status).IsModified = false;
            _banco.Entry(funcionario).Property(a => a.Email).IsModified = false;
            _banco.Entry(funcionario).Property(a => a.Cargo).IsModified = false;

            /*var lista = new List<string>() { nameof(funcionario.Nome), nameof(funcionario.CPF), nameof(funcionario.DataNascimento),
                    nameof(funcionario.Telefone), nameof(funcionario.Status), nameof(funcionario.Email), nameof(funcionario.Cargo)};
            _banco.Update(funcionario);
            foreach (var item in lista)
            {
                _banco.Entry(funcionario).Property(a => a.item).IsModified = false;
            }*/

            _banco.SaveChanges();
        }

        public void Excluir(int id)
        {
            _banco.Remove(ObterFuncionario(id));
            _banco.SaveChanges();
        }

        public Funcionario Login(string email, string senha)
        {
            return _banco.Funcionarios.Where(_ => _.Email == email && _.Senha == senha).FirstOrDefault();
        }            

        public Funcionario ObterFuncionario(int id)
            => _banco.Funcionarios.Find(id);        

        public List<Funcionario> ObterFuncionarioPorEmail(string email)
        {
            return _banco.Funcionarios.Where(_ => _.Email == email).AsNoTracking().ToList();
        }

        public IPagedList<Funcionario> ObterTodosFuncionarios(int? pagina, string pesquisa)
        {
            return ObterTodosFuncionarios(pagina, pesquisa, "A");
        }

        // Todo: Refatorar métodos - chamar Amor
        public IPagedList<Funcionario> ObterTodosFuncionarios(int? pagina, string pesquisa, string ordenacao)
        {
            int registroPorPagina = _conf.GetValue<int>("RegistroPorPagina");
            int NumeroPagina = pagina ?? 1;

            // Todo: rever o tipo AsQueryable
            var bancoFuncionarios = _banco.Funcionarios.AsQueryable();
            if (!string.IsNullOrEmpty(pesquisa))
                bancoFuncionarios = bancoFuncionarios.Where(_ => _.Nome.Contains(pesquisa.Trim()));

            if (ordenacao == "A")
                bancoFuncionarios = bancoFuncionarios.OrderBy(_ => _.Nome);
            else if (ordenacao == "ID")
                bancoFuncionarios = bancoFuncionarios.OrderBy(_ => _.Id);

            return bancoFuncionarios.ToPagedList<Funcionario>(NumeroPagina, registroPorPagina);
        }

        public IPagedList<Funcionario> ObterTodosFuncionarios(int? pagina, string pesquisa, string ordenacao, bool status)
        {
            int registroPorPagina = _conf.GetValue<int>("RegistroPorPagina");
            int NumeroPagina = pagina ?? 1;

            var bancoFuncionarios = _banco.Funcionarios.AsQueryable();
            if (!string.IsNullOrEmpty(pesquisa))
                bancoFuncionarios = bancoFuncionarios.Where(_ => _.Nome.Contains(pesquisa.Trim()));

            bancoFuncionarios = bancoFuncionarios.Where(_ => _.Status == status);

            if (ordenacao == "A")
                bancoFuncionarios = bancoFuncionarios.OrderBy(_ => _.Nome);
            else if (ordenacao == "ID")
                bancoFuncionarios = bancoFuncionarios.OrderBy(_ => _.Id);

            return bancoFuncionarios.ToPagedList<Funcionario>(NumeroPagina, registroPorPagina);
        }
        #endregion
    }

}

