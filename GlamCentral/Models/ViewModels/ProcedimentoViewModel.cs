using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using X.PagedList;

namespace GlamCentral.Models.ViewModels
{
    public class ProcedimentoViewModel
    {
        public IPagedList<Produto> Produtos { get; set; }
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

        public List<SelectListItem> Status
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

		public ProcedimentoViewModel(IPagedList<Produto> produtos, IList<int> produtosSelecionados) : this(produtos)
		{
			ProdutosSelecionados = produtosSelecionados;
		}

        public ProcedimentoViewModel(IList<Categoria> categorias)
        {
            Categorias = categorias;
        }
    }
}
