using GlamCentral.Libraries.Language;
using GlamCentral.Models;
using GlamCentral.Models.ViewModels;
using GlamCentral.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlamCentral.Areas.Funcionario.Controllers
{
    public class PagamentoController : Controller
    {
        #region "Propriedades Privadas"
        private IPagamentoRepository _repository;
        private IAgendaRepository _agendaRepository;
        #endregion

        #region "Construtor"
        public PagamentoController(IPagamentoRepository repository, IAgendaRepository agendaRepository)
        {
            _repository = repository;
            _agendaRepository = agendaRepository;
        }
        #endregion

        public IActionResult Index(int? pagina, string pesquisa, string ordenacao = "A")
        {
            return View(new PagamentoViewModel(_repository.ObterTodosPagamentos(pagina, pesquisa, ordenacao)));
        }

        [HttpGet]
        public IActionResult Cadastrar(int? pagina)
        {
            var pagamento = new Pagamento();
            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar([FromForm] Pagamento pagamento)
        {
            if (ModelState.IsValid)
            {
                _repository.Cadastrar(pagamento);

                TempData["MSG_S"] = Mensagem.MSG_S;

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(pagamento);
            }
        }
    }
}
