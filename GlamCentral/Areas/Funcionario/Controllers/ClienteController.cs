using GlamCentral.Libraries.Filter;
using GlamCentral.Libraries.Language;
using GlamCentral.Models.Enums;
using GlamCentral.Models.ViewModels;
using GlamCentral.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace GlamCentral.Areas.Funcionario.Controllers
{
    [Area("Funcionario")]
    [FuncionarioAutorizacao((int)CargoFuncionario.Gerente)]
    public class ClienteController : Controller
    {
        #region "Propriedades Privadas"
        private IClienteRepository _repository;
        #endregion

        #region "Construtor"
        public ClienteController(IClienteRepository respository)
        {
            _repository = respository;
        } 
        #endregion


        public IActionResult Index(int? pagina, string pesquisa, string status = "True", string ordenacao = "A")
        {
            var statusBool = Convert.ToBoolean(status);
            return View(new ClienteViewModel(_repository.ObterTodosClientes(pagina, pesquisa, ordenacao, statusBool)));
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar([FromForm] Models.Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _repository.Cadastrar(cliente);


                TempData["MSG_S"] = Mensagem.MSG_S;

                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpGet]
        public IActionResult Atualizar(int id)
        {
            return View(_repository.ObterCliente(id));
        }

        [HttpPost]
        public IActionResult Atualizar([FromForm] Models.Cliente cliente, int id)
        {
            if (ModelState.IsValid)
            {
                _repository.Atualizar(cliente);

                TempData["MSG_S"] = Mensagem.MSG_S;

                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [ValidateHttpReferer]
        public IActionResult GerenciarStatus(int id)
        {
            var cliente = _repository.ObterCliente(id);
            cliente.Status = !cliente.Status;
            _repository.Atualizar(cliente);
            TempData["MSG_S"] = Mensagem.MSG_S;
            return RedirectToAction(nameof(Index));
        }
    }
}
