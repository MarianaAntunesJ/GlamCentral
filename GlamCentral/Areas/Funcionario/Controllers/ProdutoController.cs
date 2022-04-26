using GlamCentral.Libraries.Arquivo;
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

namespace GlamCentral.Areas.Funcionario.Controllers
{
    [Area("Funcionario")]
    [FuncionarioAutorizacao((int)CargoFuncionario.Estoquista)]
    public class ProdutoController : Controller
    {
        #region "Propriedades Privadas"
        private IProdutoRepository _repository;
        private ICategoriaRepository _categoriaRepository;
        private IImagemRepository _imagemRepository;
        #endregion

        #region "Construtor"
        public ProdutoController(IProdutoRepository respository, ICategoriaRepository categoriaRepository, IImagemRepository imagemRepository)
        {
            _repository = respository;
            _categoriaRepository = categoriaRepository;
            _imagemRepository = imagemRepository;
        }
        #endregion

        #region "GET - Métodos publicos" 

        // Todo: conferir para ver se é uma troca melhor para ViewBag/ViewData
        public IActionResult Index(int? pagina, string pesquisa, string status = "True", string ordenacao = "A")
        {
            var statusBool = Convert.ToBoolean(status);
            var categorias = _categoriaRepository.ObterTodasCategorias();
            return View(new IndexViewModel(_repository.ObterTodosProdutos(pagina, pesquisa, ordenacao, statusBool), categorias.ToList()));
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
            ViewBag.Categorias = _categoriaRepository.ObterTodasCategorias().Select(a => new SelectListItem(a.Nome, a.Id.ToString()));
            ViewBag.TipoPeso = TipoPeso.ObterTiposPesos().Select(a => new SelectListItem(a, a));
            return View();
        }

        [HttpGet]
        public IActionResult Atualizar(int id)
        {
            ViewBag.Categorias = _categoriaRepository.ObterTodasCategorias().Select(a => new SelectListItem(a.Nome, a.Id.ToString()));
            ViewBag.TipoPeso = TipoPeso.ObterTiposPesos().Select(a => new SelectListItem(a, a));

            return View(_repository.ObterProduto(id));
        }


        [ValidateHttpReferer]
        public IActionResult GerenciarStatus(int id)
        {
            var produto = _repository.ObterProduto(id);

            GerenciadorArquivo.ExcluirImagensProduto(produto.Imagens.ToList());
            _imagemRepository.ExcluirImagensDoProduto(id);

            produto.Status = !produto.Status;
            _repository.Atualizar(produto);
            TempData["MSG_S"] = Mensagem.MSG_S;
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("/Produto/Categoria/{slug}")]
        public IActionResult ListagemCategoria(string slug)
        {
            var categoriaPrincipal = _categoriaRepository.ObterCategoria(slug);
            var lista = GetCategorias(_categoriaRepository.ObterTodasCategorias().ToList(), categoriaPrincipal);
            return View();
        }
        private List<Categoria> GetCategorias(List<Categoria> categorias, Categoria categoriaPrincipal)
        {
            /*if(categorias.Where(_ => _.CategoriaPaiID = categoriaPrincipal.Id).Count() > 0)
            {

            }*/
            return null;
        }

        public IActionResult Produtos(int? pagina, string pesquisa, string status = "True", string ordenacao = "A")
        {
            //var statusBool = Convert.ToBoolean(status);
            //return View(new IndexViewModel(_repository.ObterTodosProdutos(pagina, pesquisa, ordenacao, statusBool)));
            return View();
        } 
        #endregion

        [HttpPost]
        public IActionResult Cadastrar([FromForm] Produto produto)
        {
            if (ModelState.IsValid)
            {
                _repository.Cadastrar(produto);    
                
                var imagensDef = GerenciadorArquivo.MoverImagensProduto(new List<string>(Request.Form["imagem"]), produto.Id);

                _imagemRepository.CadastrarImagens(imagensDef, produto.Id);

                TempData["MSG_S"] = Mensagem.MSG_S;

                return RedirectToAction(nameof(Index));
            }
            else
            {             
                ViewBag.Categorias = _categoriaRepository.ObterTodasCategorias().Select(a => new SelectListItem(a.Nome, a.Id.ToString()));
                ViewBag.TipoPeso = TipoPeso.ObterTiposPesos().Select(a => new SelectListItem(a, a));
                produto.Imagens = new List<string>(Request.Form["imagem"]).Where(_ => _.Trim().Length > 0).Select(_ => new Imagem() { Caminho = _ }).ToList();

                return View(produto);
            }            
        }        

        [HttpPost]
        public IActionResult Atualizar([FromForm] Produto produto, int id)
        {
            if (ModelState.IsValid)
            {
                //produto.Id = id;
                _repository.Atualizar(produto);

                var imagensDef = GerenciadorArquivo.MoverImagensProduto(new List<string>(Request.Form["imagem"]), produto.Id);

                _imagemRepository.ExcluirImagensDoProduto(produto.Id);
                _imagemRepository.CadastrarImagens(imagensDef, produto.Id);

                TempData["MSG_S"] = Mensagem.MSG_S;

                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewBag.Categorias = _categoriaRepository.ObterTodasCategorias().Select(a => new SelectListItem(a.Nome, a.Id.ToString()));
                ViewBag.TipoPeso = TipoPeso.ObterTiposPesos().Select(a => new SelectListItem(a, a));
                produto.Imagens = new List<string>(Request.Form["imagem"]).Where(_ => _.Trim().Length > 0).Select(_ => new Imagem() { Caminho = _ }).ToList();

                return View(produto);
            }
        }        
    }
}
