using GlamCentral.Models;
using GlamCentral.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GlamCentral.Controllers
{
    public class HomeController : Controller
    {
        #region "Propriedades Privadas"
        private IFuncionarioRepository _repository;
        #endregion

        #region "Construtor"
        public HomeController(IFuncionarioRepository repository)
        {
            _repository = repository;
        }
        #endregion

        #region "Métodos Públicos - (GET)"
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CadastroCliente()
        {
            return View();
        }

        public IActionResult CadastroFuncionario()
        {
            return View();
        }

        public IActionResult Pagamento()
        {
            return View();
        }
        #endregion

        #region "Métodos Públicos - (POST)"
        [HttpPost]
        public IActionResult CadastroFuncionario([FromForm]Funcionario funcionario)
        {
            if (ModelState.IsValid)
            {
                _repository.Cadastrar(funcionario);

                TempData["MSG_S"] = "Cadastro realizado com sucesso!";

                return RedirectToAction(nameof(CadastroFuncionario));
            }
            return View();
        }
        #endregion
    }
}
