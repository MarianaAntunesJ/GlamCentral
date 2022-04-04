using GlamCentral.Libraries.Filter;
using GlamCentral.Libraries.Language;
using GlamCentral.Models;
using GlamCentral.Models.Enums;
using GlamCentral.Models.ViewModels;
using GlamCentral.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlamCentral.Areas.Funcionario.Controllers
{
    [Area("Funcionario")]
    [FuncionarioAutorizacao((int)CargoFuncionario.Cabelereiro)]
    public class ProcedimentoController : Microsoft.AspNetCore.Mvc.Controller
    {
        private IProcedimentoRepository _repository;
        private IProdutoRepository _produtoRepository;

        public ProcedimentoController(IProcedimentoRepository respository, IProdutoRepository produtoRepository)
        {
            _repository = respository;
            _produtoRepository = produtoRepository;
        }

        // Todo: conferir para ver se é uma troca melhor para ViewBag/ViewData
        public IActionResult Index(int? pagina, string pesquisa, string status = "True", string ordenacao = "A")
        {
            var statusBool = Convert.ToBoolean(status);
            return View(new IndexViewModel(_repository.ObterTodosProcedimentos(pagina, pesquisa, ordenacao, statusBool)));
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
            ViewBag.Produtos = _produtoRepository.ObterTodosProdutos().OrderBy(_ => _.Nome).Select(a => new SelectListItem(a.Nome, a.Id.ToString()));
            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar([FromForm] Procedimento procedimento)
        {
            ModelState.Remove("Id");
            if (ModelState.IsValid)
            {
                _repository.Cadastrar(procedimento);

                TempData["MSG_S"] = Mensagem.MSG_S;

                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewBag.Produtos = _produtoRepository.ObterTodosProdutos().Select(a => new SelectListItem(a.Nome, a.Id.ToString()));
                return View(procedimento);
            }
        }

        [HttpGet]
        public IActionResult Atualizar(int id)
        {
            ViewBag.Produtos = _produtoRepository.ObterTodosProdutos().Select(a => new SelectListItem(a.Nome, a.Id.ToString()));
            return View(_repository.ObterProcedimento(id));
        }

        [HttpPost]
        public IActionResult Atualizar([FromForm] Procedimento procedimento, int id)
        {
            if (ModelState.IsValid)
            {
                //produto.Id = id;
                _repository.Atualizar(procedimento);

                TempData["MSG_S"] = Mensagem.MSG_S;

                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewBag.Produtos = _produtoRepository.ObterTodosProdutos().Select(a => new SelectListItem(a.Nome, a.Id.ToString()));
                return View(procedimento);
            }
        }

        [ValidateHttpReferer]
        public IActionResult GerenciarStatus(int id)
        {
            var procedimento = _repository.ObterProcedimento(id);

            procedimento.Status = !procedimento.Status;
            _repository.Atualizar(procedimento);
            TempData["MSG_S"] = Mensagem.MSG_S;
            return RedirectToAction(nameof(Index));
        }
    }
}
