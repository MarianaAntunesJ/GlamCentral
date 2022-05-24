using GlamCentral.Libraries.Filter;
using GlamCentral.Libraries.Language;
using GlamCentral.Models;
using GlamCentral.Models.Enums;
using GlamCentral.Models.ViewModels;
using GlamCentral.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using X.PagedList;

namespace GlamCentral.Areas.Funcionario.Controllers
{
    [Area("Funcionario")]
    [FuncionarioAutorizacao((int)CargoFuncionario.Cabelereiro)]
    public class ProcedimentoController : Microsoft.AspNetCore.Mvc.Controller
    {
        #region "Propriedades Privadas"
        private IProcedimentoRepository _repository;
        private IProdutoRepository _produtoRepository;
        #endregion

        #region "Construtor"
        public ProcedimentoController(IProcedimentoRepository respository, IProdutoRepository produtoRepository)
        {
            _repository = respository;
            _produtoRepository = produtoRepository;
        }
        #endregion

        #region "GET - Métodos publicos" 
        public IActionResult Index(int? pagina, string pesquisa, string status = "True", string ordenacao = "A")
        {
            var statusBool = Convert.ToBoolean(status);
            return View(new ProcedimentoViewModel(_repository.ObterTodosProcedimentos(pagina, pesquisa, ordenacao, statusBool)));
        }

        [HttpGet]
        public IActionResult Cadastrar(int? pagina)
        {
            var procedimento = new Procedimento();
            PreencheHorasMinutos();
            return View(new ProcedimentoViewModel(_repository.ObterProdutos(pagina, procedimento.Id), procedimento));
        }

        [HttpGet]
        public IActionResult CadastrarRetorno(int? pagina)
        {
            var procedimento = JsonConvert.DeserializeObject<Procedimento>(TempData["procedimento"].ToString());
            var horas = procedimento.Duracao / 60;
            var minutos = procedimento.Duracao % 60;
            PreencheHorasMinutos(horas, minutos);

            var produtosSelecionados = JsonConvert.DeserializeObject<List<Produto>>(TempData["produtosSelecionados"].ToString());
            procedimento.AdicionaProdutos(produtosSelecionados);
            TempData.Clear();
            
            TempData["produtosSalvar"] = JsonConvert.SerializeObject(procedimento.Getprodutos(procedimento.Produtos));
            return View(new ProcedimentoViewModel(_repository.ObterProdutosSelecionados(pagina, procedimento), procedimento));

        }

        private void PreencheHorasMinutos(int horas = 0, int minutos = 0)
        {
            var horasSelectList = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8 };
            ViewBag.Horas = horasSelectList.Select(_ => _ != horas ? new SelectListItem(_.ToString(), _.ToString()) : new SelectListItem(_.ToString(), _.ToString(), true));
            var minutosSelectList = new List<int>() { 0, 5, 10, 15, 20, 25, 30, 35, 40, 45, 50, 55 };
            ViewBag.Minutos = minutosSelectList.Select(_ => _ != minutos ? new SelectListItem(_.ToString(), _.ToString()) : new SelectListItem(_.ToString(), _.ToString(), true));
        }

        [HttpGet]
        public IActionResult Atualizar(int? pagina, int id)
        {
            var horas = 0;
            var minutos = 0;
            var procedimento = _repository.ObterProcedimento(id);
            if (procedimento.Duracao > 0)
            {
                horas = procedimento.Duracao / 60;
                minutos = procedimento.Duracao % 60;
                PreencheHorasMinutos(horas, minutos);
            }

            PreencheHorasMinutos(horas, minutos);
            return View(new ProcedimentoViewModel(_repository.ObterProdutosSelecionados(pagina, procedimento), procedimento));
        }

        [HttpGet]
        public IActionResult AtualizarRetorno(int? pagina, int id)
        {
            var procedimento = JsonConvert.DeserializeObject<Procedimento>(TempData["procedimento"].ToString());
            var horas = procedimento.Duracao / 60;
            var minutos = procedimento.Duracao % 60;
            PreencheHorasMinutos(horas, minutos);

            var produtosSelecionados = JsonConvert.DeserializeObject<List<Produto>>(TempData["produtosSelecionados"].ToString());
            procedimento.AdicionaProdutos(produtosSelecionados);
            TempData.Clear();

            TempData["produtosSalvar"] = JsonConvert.SerializeObject(procedimento.Getprodutos(procedimento.Produtos));
            return View(new ProcedimentoViewModel(_repository.ObterProdutosSelecionados(pagina, procedimento), procedimento));
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
        #endregion

        [HttpPost]
        public IActionResult Cadastrar([FromForm] Procedimento procedimento)
        {
            var produtosSelecionados = JsonConvert.DeserializeObject<List<Produto>>(TempData["produtosSalvar"].ToString());
            // Todo: tornar dinâmico
            procedimento.AdicionaProdutos(produtosSelecionados);
            procedimento.Status = true;

            var horas = int.Parse(Request.Form["horas"]);
            var minutos = int.Parse(Request.Form["minutos"]);
            procedimento.InsereDuracao(horas, minutos);

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

        [HttpPost]
        public IActionResult Atualizar([FromForm] Procedimento procedimento, int id)
        {
            var produtosSelecionados = JsonConvert.DeserializeObject<List<Produto>>(TempData["produtosSalvar"].ToString());
            // Todo: tornar dinâmico
            procedimento.AdicionaProdutos(produtosSelecionados);
            procedimento.Status = true;

            var horas = int.Parse(Request.Form["horas"]);
            var minutos = int.Parse(Request.Form["minutos"]);
            procedimento.InsereDuracao(horas, minutos);

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

        [HttpPost]
        public IActionResult ListagemProdutos([FromForm] Procedimento procedimento, int? pagina, string pesquisa, string status = "True", string ordenacao = "A")
        {
            var produtosSelecionados = new List<int>();
            var statusBool = Convert.ToBoolean(status);
            var a = new ProcedimentoViewModel(_produtoRepository.ObterTodosProdutos(pagina, pesquisa, ordenacao, statusBool), produtosSelecionados);

            if (int.Parse(Request.Form["horas"]) > 0 && int.Parse(Request.Form["minutos"]) > 0)
            {
                var horas = int.Parse(Request.Form["horas"]);
                var minutos = int.Parse(Request.Form["minutos"]);
                procedimento.InsereDuracao(horas, minutos);
            }

            TempData["procedimento"] = JsonConvert.SerializeObject(procedimento);

            return View(a);
        }

        [HttpPost]
        public IActionResult ListagemProdutosAtualizar([FromForm] Procedimento procedimento, int? pagina, string pesquisa, string status = "True", string ordenacao = "A")
        {
            var produtosSelecionados = new List<int>();
            if (procedimento.Id > 0)
            {
                var procedimentoExistente = _repository.ObterProcedimento(procedimento.Id);
                if (procedimentoExistente.Produtos != null)
                {
                    produtosSelecionados = procedimentoExistente.Produtos.Select(_ => _.Produto.Id).ToList();
                }
            }
            var statusBool = Convert.ToBoolean(status);
            var a = new ProcedimentoViewModel(_produtoRepository.ObterTodosProdutos(pagina, pesquisa, ordenacao, statusBool), produtosSelecionados);

            if (int.Parse(Request.Form["horas"]) > 0 && int.Parse(Request.Form["minutos"]) > 0)
            {
                var horas = int.Parse(Request.Form["horas"]);
                var minutos = int.Parse(Request.Form["minutos"]);
                procedimento.InsereDuracao(horas, minutos);
            }

            TempData["procedimento"] = JsonConvert.SerializeObject(procedimento);

            return View(a);
        }

        [HttpPost]
        public IActionResult SalvarProdutos()
        {
            var produtosId = Request.Form["produtoSelecionado"].Select(int.Parse).ToList();
            TempData["produtosSelecionados"] = JsonConvert.SerializeObject(_produtoRepository.ObterProdutosPorId(produtosId));
            return RedirectToAction(nameof(CadastrarRetorno));
        }

        [HttpPost]
        public IActionResult SalvarProdutosAtualizar()
        {
            var produtosId = Request.Form["produtoSelecionado"].Select(int.Parse).ToList();
            TempData["produtosSelecionados"] = JsonConvert.SerializeObject(_produtoRepository.ObterProdutosPorId(produtosId));
            return RedirectToAction(nameof(AtualizarRetorno));
        }
    }
}
