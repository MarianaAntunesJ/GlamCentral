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

            //ViewBag.Data = DateTime.Now.ToString("dd/MM/yyyy");
            ViewBag.Data = DateTime.Now;
            ViewBag.Data = "2022/05/10";
            PreencheHorasMinutos();
            PreencheHorasMinutosDuracao();
            return View();
        }

        private void PreencheHorasMinutos(int horas = 0, int minutos = 0)
        {
            var horasSelectList = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
            ViewBag.Horas = horasSelectList.Select(_ => _ != horas ? new SelectListItem(_.ToString(), _.ToString()) : new SelectListItem(_.ToString(), _.ToString(), true));
            var minutosSelectList = new List<int>() { 0, 5, 10, 15, 20, 25, 30, 35, 40, 45, 50, 55 };
            ViewBag.Minutos = minutosSelectList.Select(_ => _ != minutos ? new SelectListItem(_.ToString(), _.ToString()) : new SelectListItem(_.ToString(), _.ToString(), true));
        }

        private void PreencheHorasMinutosDuracao(int horas = 0, int minutos = 0)
        {
            var horasSelectList = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
            ViewBag.HorasDuracao = horasSelectList.Select(_ => _ != horas ? new SelectListItem(_.ToString(), _.ToString()) : new SelectListItem(_.ToString(), _.ToString(), true));
            var minutosSelectList = new List<int>() { 0, 5, 10, 15, 20, 25, 30, 35, 40, 45, 50, 55 };
            ViewBag.MinutosDuracao = minutosSelectList.Select(_ => _ != minutos ? new SelectListItem(_.ToString(), _.ToString()) : new SelectListItem(_.ToString(), _.ToString(), true));
        }

        public JsonResult Buscar(Agenda agenda)
        {
            var events = _repository.ObterTodosAgendamentos().ToList();
            return new JsonResult(events);
        }

        [HttpGet]
        // tirei int? pagina
        public IActionResult Cadastrar()
        {

            //ViewBag.Funcionarios = _funcionarioRepository.ObterTodosFuncionarios().OrderBy(_ => _.Nome)
            //    .Select(a => new SelectListItem(a.Nome, a.Id.ToString()));

            //ViewBag.Clientes = _clienteRepository.ObterTodosClientes().OrderBy(_ => _.Nome)
            //    .Select(a => new SelectListItem(a.Nome, a.Id.ToString()));

            //ViewBag.Procedimentos = _procedimentoRepository.ObterTodosProcedimentos().OrderBy(_ => _.Nome)
            //    .Select(a => new SelectListItem(a.Nome, a.Id.ToString()));


            //return View(new AgendaViewModel(_clienteRepository.ObterTodosClientes(pagina),
            //    _funcionarioRepository.ObterTodosFuncionarios(pagina),
            //    _procedimentoRepository.ObterTodosProcedimentos(pagina));

            return View();
        }

        #endregion


        #region "POST - Métodos Públicos"
        [HttpPost]
        public JsonResult Cadastrar(int clienteId, int funcionarioId, int procedimentoId, string description,
            DateTime start, string themeColor, bool IsFullDay, int horasAgendamento, int minutosAgendamento, 
            int horasDuracao, int minutosDuracao)
        {
            Agenda agendamento = new Agenda()
			{
                Cliente = _clienteRepository.ObterCliente(clienteId),
                ClienteId = clienteId,
                Funcionario = _funcionarioRepository.ObterFuncionario(funcionarioId),
                FuncionarioId = funcionarioId,
                Procedimento = _procedimentoRepository.ObterProcedimento(procedimentoId),
                ProcedimentoId = procedimentoId,
                Description = description,
                ThemeColor = themeColor,
                IsFullDay = IsFullDay,
                Duracao = (horasDuracao * 60) + minutosDuracao,
                Start = new DateTime(start.Year, start.Month, start.Day,
                horasAgendamento, minutosAgendamento, 0)

            };
            var status = true;

            if (ModelState.IsValid)
            {
                if (_repository.Cadastrar(agendamento))
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
        public JsonResult Duracao(string procedimentoId)
        {
            if (procedimentoId != null && int.Parse(procedimentoId) > 0)
            {
                var procedimento = _procedimentoRepository.ObterProcedimento(int.Parse(procedimentoId));
                var duracao = procedimento.Duracao;
                if (duracao > 0)
                {
                    var horas = duracao / 60;
                    var minutos = duracao % 60;
                    var duration = new List<int>() { horas, minutos };
                    return new JsonResult(duration);
                }

            }
            return new JsonResult("Procedimento não possui duração.");
        }

        #region Front-end escolhas entidades
        private void DadosAgenda(Agenda agenda)
        {
            TempData["Agenda"] = JsonConvert.SerializeObject(agenda);
        }


        public IActionResult ListagemClientes([FromForm] Agenda agenda, int? pagina, string pesquisa, string status = "True", string ordenacao = "A")
        {
            var statusBool = Convert.ToBoolean(status);

            DadosAgenda(agenda);

            var a = new ClienteViewModel(_clienteRepository.ObterTodosClientes(pagina, pesquisa, ordenacao, statusBool));

            return View(a);
        }

        [HttpPost]
        public IActionResult SelecionaCliente(IEnumerable<string> id)
        {
            var clienteId = int.Parse(id.First());
            TempData["ClienteSelecionado"] = JsonConvert.SerializeObject(_clienteRepository.ObterCliente(clienteId));
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult ListagemFuncionarios([FromForm] Agenda agenda, int? pagina, string pesquisa, string status = "True", string ordenacao = "A")
        {
            var statusBool = Convert.ToBoolean(status);

            DadosAgenda(agenda);

            var a = new FuncionarioViewModel(_funcionarioRepository.ObterTodosFuncionarios(pagina, pesquisa, ordenacao, statusBool));

            return View(a);
        }

        [HttpPost]
        public IActionResult SelecionaFuncionario(IEnumerable<string> id)
        {
            var funcionarioId = int.Parse(id.First());
            TempData["FuncionarioSelecionado"] = JsonConvert.SerializeObject(_funcionarioRepository.ObterFuncionario(funcionarioId));
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult ListagemProcedimentos([FromForm] Agenda agenda, int? pagina, string pesquisa, string status = "True", string ordenacao = "A")
        {
            var statusBool = Convert.ToBoolean(status);

            DadosAgenda(agenda);

            var a = new ProcedimentoViewModel(_procedimentoRepository.ObterTodosProcedimentos(pagina, pesquisa, ordenacao, statusBool));

            return View(a);
        }

        [HttpPost]
        public IActionResult SelecionaProcedimento(IEnumerable<string> id)
        {
            var procedimentoId = int.Parse(id.First());
            TempData["ProcedimentoSelecionado"] = JsonConvert.SerializeObject(_procedimentoRepository.ObterProcedimento(procedimentoId));
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #endregion
    }
}
