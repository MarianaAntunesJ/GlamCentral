using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using X.PagedList;

namespace GlamCentral.Models.ViewModels
{
    public class ProcedimentoViewModel
    {
        public IPagedList<Produto> Produtos { get; set; }
        public Produto Produto { get; set; }
        public IPagedList<Procedimento> Procedimentos { get; set; }
        public Procedimento Procedimento { get; set; }
        public IList<int> ProdutosSelecionados { get; set; }
        public IList<Categoria> Categorias { get; set; }

        public List<SelectListItem> Ordenacao
        {
            get
            {
                return new List<SelectListItem>()
                {
                    new SelectListItem("Alfabetica", "A"),
                    new SelectListItem("PorID", "ID")
                };
            }
            private set { }
        }

        public bool Status { get; set; }

        public List<SelectListItem> StatusList
        {
            get
            {
                return new List<SelectListItem>()
                {
                    new SelectListItem("Ativo", "True"),
                    new SelectListItem("Inativo", "False")
                };
            }
            private set { }
        }

        public ProcedimentoViewModel()
        {
        }

        public ProcedimentoViewModel(IPagedList<Produto> produtos)
        {
            Produtos = produtos;
        }

        public ProcedimentoViewModel(Produto produto)
        {
            Produto = produto;
        }

        public ProcedimentoViewModel(IPagedList<Produto> produtos, IList<int> produtosSelecionados) : this(produtos)
		{
			ProdutosSelecionados = produtosSelecionados;
		}

        public ProcedimentoViewModel(IList<Categoria> categorias)
        {
            Categorias = categorias;
        }

        public ProcedimentoViewModel(IPagedList<Procedimento> procedimentos)
        {
            Procedimentos = procedimentos;
        }

        public ProcedimentoViewModel(Procedimento procedimento)
        {
            Procedimento = procedimento;
        }

        public ProcedimentoViewModel(IPagedList<Produto> produtos, Procedimento procedimento) : this(produtos)
        {
            Procedimento = procedimento;
        }
    }
}
