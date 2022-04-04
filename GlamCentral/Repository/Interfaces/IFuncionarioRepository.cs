using GlamCentral.Models;
using System.Collections.Generic;
using X.PagedList;

namespace GlamCentral.Repository.Interfaces
{
    public interface IFuncionarioRepository
    {
        Funcionario Login(string email, string senha);

        void Cadastrar(Funcionario funcionario);
        void Atualizar(Funcionario funcionario);
        void AtualizarSenha(Funcionario funcionario);
        void Excluir(int id);
        Funcionario ObterFuncionario(int id);
        List<Funcionario> ObterFuncionarioPorEmail(string email);
        IPagedList<Funcionario> ObterTodosFuncionarios(int? pagina, string pesquisa);
        IPagedList<Funcionario> ObterTodosFuncionarios(int? pagina, string pesquisa, string ordenacao);
        IPagedList<Funcionario> ObterTodosFuncionarios(int? pagina, string pesquisa, string ordenacao, bool status);
    }
}
