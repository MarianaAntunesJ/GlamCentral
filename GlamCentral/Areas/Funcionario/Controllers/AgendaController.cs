using GlamCentral.Models;
using GlamCentral.Models.ViewModels;
using GlamCentral.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
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
            ViewBag.Funcionarios = _funcionarioRepository.ObterTodosFuncionarios().OrderBy(_ => _.Nome)
                .Select(a => new SelectListItem(a.Nome, a.Id.ToString()));

            ViewBag.Clientes = _clienteRepository.ObterTodosClientes().OrderBy(_ => _.Nome)
                .Select(a => new SelectListItem(a.Nome, a.Id.ToString()));

            ViewBag.Procedimentos = _procedimentoRepository.ObterTodosProcedimentos().OrderBy(_ => _.Nome)
                .Select(a => new SelectListItem(a.Nome, a.Id.ToString()));

            ViewBag.Data = DateTime.Now.ToString("dd/MM/yyyy");
            var horas = new List<int>() { 08, 09, 10, 11, 12, 13, 14, 15, 16, 17 };
            ViewBag.Horas = horas.Select(h => new SelectListItem(h.ToString(), h.ToString()));
            var minutos = new List<int>() { 00, 05, 10, 15, 20, 25, 30, 35, 40, 45, 50, 55 };
            ViewBag.Minutos = minutos.Select(m => new SelectListItem(m.ToString(), m.ToString()));

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
            ViewBag.Funcionarios = _funcionarioRepository.ObterTodosFuncionarios().OrderBy(_ => _.Nome)
                .Select(a => new SelectListItem(a.Nome, a.Id.ToString()));

            ViewBag.Clientes = _clienteRepository.ObterTodosClientes().OrderBy(_ => _.Nome)
                .Select(a => new SelectListItem(a.Nome, a.Id.ToString()));

            ViewBag.Procedimentos = _procedimentoRepository.ObterTodosProcedimentos().OrderBy(_ => _.Nome)
                .Select(a => new SelectListItem(a.Nome, a.Id.ToString()));
            return View();
        }

        #endregion


        #region "POST - Métodos Públicos"
        [HttpPost]
        public JsonResult Cadastrar(Agenda agenda, int Horas, int Minutos)
        {
            var status = false;

            agenda.Start = new DateTime(agenda.Start.Year, agenda.Start.Month, agenda.Start.Day, Horas, Minutos, 0);
            ModelState.Remove("Id");
            if (ModelState.IsValid)
            {
                if (_repository.Cadastrar(agenda))
                    status = true;
            }
            else
            {
                ViewBag.Funcionarios = _funcionarioRepository.ObterTodosFuncionarios().OrderBy(_ => _.Nome)
                    .Select(a => new SelectListItem(a.Nome, a.Id.ToString()));

                ViewBag.Clientes = _clienteRepository.ObterTodosClientes().OrderBy(_ => _.Nome)
                    .Select(a => new SelectListItem(a.Nome, a.Id.ToString()));

                ViewBag.Procedimentos = _procedimentoRepository.ObterTodosProcedimentos().OrderBy(_ => _.Nome)
                    .Select(a => new SelectListItem(a.Nome, a.Id.ToString()));
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

        [HttpPost]
        public JsonResult Duracao(IEnumerable<string> id)
        {            
            if (id.First() != null)
			{
                var idProc = int.Parse(id.First());

                if (idProc != 0)
                {
                    var procedimento = _procedimentoRepository.ObterProcedimento(idProc);
                    var duracao = procedimento.Duracao;
                    if (duracao > 0)
                        return new JsonResult(duracao);
                }
            }
            
			return new JsonResult(null);
        }

        private void DadosAgenda(Agenda agenda)
        {
            TempData["Agenda"] = JsonConvert.SerializeObject(agenda);
        }

        [HttpPost]
        public IActionResult ListagemClientes([FromForm] Agenda agenda, int? pagina, string pesquisa, string status = "True", string ordenacao = "A")
        {
            var produtosSelecionados = new List<int>();
            var statusBool = Convert.ToBoolean(status);

            DadosAgenda(agenda);

            var a = new ClienteViewModel(_clienteRepository.ObterTodosClientes(pagina, pesquisa, ordenacao, statusBool));

            return View(a);
        }

        [HttpPost]
        public IActionResult SelecionaCliente(IEnumerable<string> id)
        {
            //var cliente = Request.Form["produtoSelecionado"].Select(int.Parse).ToList();
            //TempData["produtosSelecionados"] = JsonConvert.SerializeObject(_produtoRepository.ObterProdutosPorId(produtosId));
            return RedirectToAction(nameof(Cadastrar));
        }
        #endregion
    }
}
