using GlamCentral.Libraries.Email;
using GlamCentral.Libraries.Filter;
using GlamCentral.Libraries.Language;
using GlamCentral.Libraries.Login;
using GlamCentral.Libraries.Text;
using GlamCentral.Models.Enums;
using GlamCentral.Models.ViewModels;
using GlamCentral.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace GlamCentral.Areas.Funcionario.Controllers
{
    [Area("Funcionario")]
    public class HomeController : Controller
    {
        #region "Propriedades Privadas"
        private IFuncionarioRepository _repository;
        private LoginFuncionario _loginFuncionario;
        private GerenciarEmail _gerenciarEmail;
        private IProdutoRepository _produtoRepository;
        private ICategoriaRepository _categoriaRepository;
        private IClienteRepository _clienteRepository;
        private IEmpresaRepository _empresaRepository;
        #endregion

        #region "Construtor"
        public HomeController(IFuncionarioRepository repository, LoginFuncionario loginFuncionario,
            GerenciarEmail gerenciarEmail, ICategoriaRepository categoriaRepository,
            IProdutoRepository produtoRepository, IClienteRepository clienteRepository,
            IEmpresaRepository empresaRepository)
        {
            _repository = repository;
            _loginFuncionario = loginFuncionario;
            _gerenciarEmail = gerenciarEmail;
            _categoriaRepository = categoriaRepository;
            _produtoRepository = produtoRepository;
            _clienteRepository = clienteRepository;
            _empresaRepository = empresaRepository;
        }
        #endregion

        #region "Métodos Públicos - (GET)"
        public IActionResult Login()
        {
            ViewBag.ShowTopBar = false;
            return View();
        }

        [ValidateHttpReferer]
        public IActionResult Logout()
        {
            _loginFuncionario.Logout();
            return RedirectToAction("Login", "Home");
        }

        public IActionResult CadastrarSenha()
        {
            return View();
        }

        public IActionResult RecuperarSenha()
        {
            ViewBag.ShowTopBar = false;
            return View();
        }

        public IActionResult Painel()
        {
            return View();
        }

        // Todo: recuperar senha
        [HttpGet]
        [ValidateHttpReferer]
        public IActionResult EmailRecuperarSenha(string email)
        {
            Models.Funcionario funcionario = _repository.ObterFuncionarioPorEmail(email).FirstOrDefault();

            if(funcionario != null)
            {
                funcionario.Senha = KeyGenerator.GetUniqueKey(8);
                _repository.AtualizarSenha(funcionario);

                //_gerenciarEmail.EnviarSenhaParaColaboradorPorEmail(funcionario);

                ViewData["MSG_S"] = Mensagem.MSG_S_SenhaGerada;                
            }

            return View("Login");
        }

        public void BuscaFuncionarioPorEmail(string email)
        {
            if (_repository.ObterFuncionarioPorEmail(email) != null)
            {
                EmailRecuperarSenha(email);
            }
        }
        #endregion

        #region "Métodos Públicos - (POST)"
        [HttpPost]
        public IActionResult Login([FromForm] Models.Funcionario funcionario)
        {
            ViewBag.ShowTopBar = false;
            var funcionarioDB = _repository.Login(funcionario.Email, funcionario.Senha);
            if (funcionarioDB != null)
            {
                if (funcionarioDB.Status == false)
                {
                    ViewData["MSG_E"] = "Usuário inativo. Utilize um usuário que esteja ativo no sistema.";
                    return View();
                }

                _loginFuncionario.Login(funcionarioDB);
                return new RedirectResult(Url.Action(nameof(Painel)));
            }
            else
            {
                ViewData["MSG_E"] = "Usuário não encontrado. Verifique E-mail e Senha digitados, e tente novamente.";
                return View();
            }
        }
        #endregion

        // Todo: tirar grafico, relatorio e dados da empresa da home controller
        #region "Tirar"
        //[ValidateHttpReferer]
        public IActionResult Grafico()
        {
            ViewBag.Data = DateTime.Now;
            var categorias = _categoriaRepository.ObterTodasCategorias();
            var empresa = _empresaRepository.ObterEmpresa();
            return View(new GraficoProdutoViewModel(categorias.ToList(), empresa));
        }

        public IActionResult GraficoCategoriaJson(int categoriaId)
        {
            var produtos = _produtoRepository.ObterTodosProdutos(categoriaId).ToList();
            return Json(produtos);
        }

        public IActionResult GraficoQuantidadeJson(int min, int max)
        {
            var produtos = _produtoRepository.ObterProdutosEntreQuantidade(min, max).OrderBy(_ => _.Quantidade).ToList();
            return Json(produtos);
        }

        [FuncionarioAutorizacao((int)CargoFuncionario.Gerente)]
        public IActionResult Relatorio(int? pagina, string status = "True", string ordenacao = "A")
        {
            ViewBag.Data = DateTime.Now;
            var statusBool = Convert.ToBoolean(status);
            var empresa = _empresaRepository.ObterEmpresa();
            return View(new IndexViewModel(_clienteRepository.ObterTodosClientes(pagina, ordenacao, statusBool), empresa));
        }

        [FuncionarioAutorizacao((int)CargoFuncionario.Gerente)]
        public IActionResult IndexEmpresa()
        {
            //var empresaExiste = ;
            return View(_empresaRepository.ObterEmpresa());
        }

        [FuncionarioAutorizacao((int)CargoFuncionario.Gerente)]
        [HttpGet]
        public IActionResult AtualizarEmpresa()
        {
            return View(_empresaRepository.ObterEmpresa());
        }

        [HttpPost]
        public IActionResult AtualizarEmpresa([FromForm] Models.Empresa empresa, int id)
        {
            ModelState.Remove("Id");
            if (ModelState.IsValid)
            {
                _empresaRepository.Atualizar(empresa);

                TempData["MSG_S"] = Mensagem.MSG_S;
            }
            return RedirectToAction("IndexEmpresa");
        }
        #endregion
    }
}
