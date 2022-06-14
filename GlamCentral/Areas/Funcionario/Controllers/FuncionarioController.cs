using GlamCentral.Libraries.Email;
using GlamCentral.Libraries.Filter;
using GlamCentral.Libraries.Language;
using GlamCentral.Libraries.Text;
using GlamCentral.Models.Enums;
using GlamCentral.Models.ViewModels;
using GlamCentral.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using GlamCentral.Libraries.Login;
using System;

namespace GlamCentral.Areas.Funcionario.Controllers
{
    [Area("Funcionario")]
    [FuncionarioAutorizacao((int)CargoFuncionario.Gerente)]
    public class FuncionarioController : Controller
    {
        #region "Propriedades Privadas"
        private IFuncionarioRepository _repository;
        private GerenciarEmail _gerenciarEmail;
        private LoginFuncionario _loginFuncionario;
        #endregion

        #region "Construtor"
        public FuncionarioController(IFuncionarioRepository respository, GerenciarEmail gerenciarEmail, LoginFuncionario loginFuncionario)
        {
            _repository = respository;
            _gerenciarEmail = gerenciarEmail;
            _loginFuncionario = loginFuncionario;
        } 
        #endregion

        public IActionResult Index(int? pagina, string pesquisa, string status = "True", string ordenacao = "A")
        {
            var statusBool = Convert.ToBoolean(status);
            return View(new FuncionarioViewModel(_repository.ObterTodosFuncionarios(pagina, pesquisa, ordenacao, statusBool)));
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar([FromForm] Models.Funcionario funcionario)
        {
            ModelState.Remove("Senha");
            ModelState.Remove("ConfirmarSenha");
            if (ModelState.IsValid)
            {
                // Todo: fazer a verificação se o cadastro foi realizado antes
                _gerenciarEmail.EnviarSenhaParaColaboradorPorEmail(funcionario);

                funcionario.Senha = _loginFuncionario.GerarHashMd5(KeyGenerator.GetUniqueKey(8));
                
                _repository.Cadastrar(funcionario);                

                TempData["MSG_S"] = Mensagem.MSG_S;

                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpGet]
        [ValidateHttpReferer]
        public IActionResult GerarSenha(int id)
        {
            Models.Funcionario funcionario = _repository.ObterFuncionario(id);

            // Todo: fazer a verificação se o cadastro foi realizado antes
            _gerenciarEmail.EnviarSenhaParaColaboradorPorEmail(funcionario);

            funcionario.Senha = _loginFuncionario.GerarHashMd5(KeyGenerator.GetUniqueKey(8));

            _repository.AtualizarSenha(funcionario);

            

            TempData["MSG_S"] = Mensagem.MSG_S_SenhaGerada;

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Atualizar(int id)
        {
            return View(_repository.ObterFuncionario(id));
        }

        [HttpPost]
        public IActionResult Atualizar([FromForm] Models.Funcionario funcionario, int id)
        {
            ModelState.Remove("Senha");
            ModelState.Remove("ConfirmarSenha"); 
            if (ModelState.IsValid)
            {
                _repository.Atualizar(funcionario);

                TempData["MSG_S"] = Mensagem.MSG_S;

                return RedirectToAction(nameof(Index));
            }
            return View();
        }        

        // Usar: Método excluir
        /*[HttpGet]
        [ValidateHttpReferer]
        public IActionResult Excluir(int id)
        {
            _repository.Excluir(id);

            TempData["MSG_S"] = "Registro excluído com sucesso!";

            return RedirectToAction(nameof(Index));
        }*/

        [ValidateHttpReferer]
        public IActionResult GerenciarStatus(int id)
        {
            // Todo: Pegar login do funcionario
            var funcionarioAutorizado = _loginFuncionario.GetFuncionario();
            var funcionario = _repository.ObterFuncionario(id);

            if (funcionarioAutorizado.Id != funcionario.Id)
            {
                funcionario.Status = !funcionario.Status;
                _repository.Atualizar(funcionario);
                TempData["MSG_S"] = Mensagem.MSG_S;
                return RedirectToAction(nameof(Index));
            }
            TempData["MSG_S"] = Mensagem.MSG_E;
            return RedirectToAction(nameof(Index));
        }
    }
}
