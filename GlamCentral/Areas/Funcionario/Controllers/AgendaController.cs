using GlamCentral.Models;
using GlamCentral.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace GlamCentral.Areas.Funcionario.Controllers
{
    [Area("Funcionario")]
    //[FuncionarioAutorizacao((int)CargoFuncionario.Gerente)]
    public class AgendaController : Controller
    {
        #region "Propriedades Privadas"
        private IAgendaRepository _repository;
        private IFuncionarioRepository _funcionarioRepository;
        private IClienteRepository _clienteRepository;
        private IProcedimentoRepository _procedimentoRepository;
        #endregion

        #region "Construtores"
        public AgendaController(IAgendaRepository repository, IFuncionarioRepository funcionarioRepository,
            IClienteRepository clienteRepository, IProcedimentoRepository procedimentoRepository)
        {
            _repository = repository;
            _funcionarioRepository = funcionarioRepository;
            _clienteRepository = clienteRepository;
            _procedimentoRepository = procedimentoRepository;
        }
        #endregion

        #region "GET - Métodos publicos"        

        public IActionResult Index()
        {
            ViewBag.Funcionarios = _funcionarioRepository.ObterTodosFuncionarios().OrderBy(_ => _.Nome).Select(a => new SelectListItem(a.Nome, a.Id.ToString()));
            ViewBag.Clientes = _clienteRepository.ObterTodosClientes().OrderBy(_ => _.Nome).Select(a => new SelectListItem(a.Nome, a.Id.ToString()));
            ViewBag.Procedimentos = _procedimentoRepository.ObterTodosProcedimentos().OrderBy(_ => _.Nome).Select(a => new SelectListItem(a.Nome, a.Id.ToString()));
            return View();
        }

        public JsonResult Buscar(Agenda agenda)
        {
            var events = _repository.ObterTodosAgendamentos().ToList();
            return new JsonResult(events);
        }

        /*var v = new
        {
            subject = "teste",
            start = "2022-04-04T03:00:00+00:00",
            end = "2022-04-04T04:00:00+00:00",
            description = "Progressiva"
        };

        // Jeito de certo de salvar o objeto
        var events = new Teste
        {
            EventID = 1,
            Subject = "teste teste teste teste testetesteteste",
            Start = DateTime.Parse("2022-04-04 03:00"),
            End = DateTime.Parse("2022-04-04 04:00"),
            Description = "Progressiva"
        };

        var s = "[{ 'title':'All Day Event','start':'2022-04-01'},{ 'title':'Long Event','start':'2022-04-07','end':'2022-04-10'},{ 'groupId':'999','title':'Repeating Event','start':'2022-04-09T16:00:00+00:00'},{ 'groupId':'999','title':'Repeating Event','start':'2022-04-16T16:00:00+00:00'},{ 'title':'Conference','start':'2022-04-03','end':'2022-04-05'},{ 'title':'Meeting','start':'2022-04-04T10:30:00+00:00','end':'2022-04-04T12:30:00+00:00'},{ 'title':'Lunch','start':'2022-04-04T12:00:00+00:00'},{ 'title':'Birthday Party','start':'2022-04-05T07:00:00+00:00'},{ 'url':'http','title':'Click for Google','start':'2022-04-28'}]";

        var lista = new List<Teste>() { events };

        //return new JsonResult(_agendaRepository.ObterTodosAgendamentos());
        return new JsonResult(lista);
                }*/

        [HttpGet]
        public IActionResult Cadastrar()
        {
            ViewBag.Funcionarios = _funcionarioRepository.ObterTodosFuncionarios().OrderBy(_ => _.Nome).Select(a => new SelectListItem(a.Nome, a.Id.ToString()));
            ViewBag.Clientes = _clienteRepository.ObterTodosClientes().OrderBy(_ => _.Nome).Select(a => new SelectListItem(a.Nome, a.Id.ToString()));
            ViewBag.Procedimentos = _procedimentoRepository.ObterTodosProcedimentos().OrderBy(_ => _.Nome).Select(a => new SelectListItem(a.Nome, a.Id.ToString()));
            return View();
        }

        #endregion


        #region "POST - Métodos Públicos"
        [HttpPost]
        public JsonResult Cadastrar(Agenda agenda)
        {
            var status = false;

            ModelState.Remove("Id");
            if (ModelState.IsValid)
            {
                if (_repository.Cadastrar(agenda))
                    status = true;
            }
            else
            {
                ViewBag.Funcionarios = _funcionarioRepository.ObterTodosFuncionarios().OrderBy(_ => _.Nome).Select(a => new SelectListItem(a.Nome, a.Id.ToString()));
                ViewBag.Clientes = _clienteRepository.ObterTodosClientes().OrderBy(_ => _.Nome).Select(a => new SelectListItem(a.Nome, a.Id.ToString()));
                ViewBag.Procedimentos = _procedimentoRepository.ObterTodosProcedimentos().OrderBy(_ => _.Nome).Select(a => new SelectListItem(a.Nome, a.Id.ToString()));
            }

            return new JsonResult(status);
        }

        [HttpPost]
        public JsonResult Excluir(int id)
        {
            var status = false;
            if (_repository.Excluir(id))
                status = true;

            return new JsonResult(status);
        }
        #endregion
    }
}
