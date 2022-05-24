using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using X.PagedList;

namespace GlamCentral.Models.ViewModels
{
    public class ProdutoViewModel
    {
        public IPagedList<Produto> Produtos { get; set; }
        public Produto Produto { get; set; }
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

        public ProdutoViewModel()
        {
        }

        public ProdutoViewModel(IPagedList<Produto> produtos)
        {
            Produtos = produtos;
        }

        public ProdutoViewModel(Produto produto)
        {
            Produto = produto;
        }

        public ProdutoViewModel(IList<Categoria> categorias)
        {
            Categorias = categorias;
        }

		public ProdutoViewModel(IPagedList<Produto> produtos, IList<Categoria> categorias) : this(produtos)
		{
			Categorias = categorias;
		}
	}
}
