using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace GlamCentral.Models.ViewModels
{
    public class IndexViewModel
    {
        public IPagedList<Produto> Produtos { get; set; }
        public IPagedList<Procedimento> Procedimentos { get; set; }
        public IPagedList<Funcionario> Funcionarios { get; set; }
        public IPagedList<Cliente> Clientes { get; set; }
        public IPagedList<Categoria> CategoriasList { get; set; }
        public IList<Categoria> Categorias { get; set; }
        public IList<Produto> ProdutosList { get; set; }
        public IList<Cliente> ClientesList { get; set; }
        public Procedimento Procedimento { get; set; }
        public Empresa Empresa { get; set; }

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

        public IndexViewModel(IPagedList<Procedimento> procedimentos)
        {
            Procedimentos = procedimentos;
        }

        public IndexViewModel(Procedimento procedimento)
        {
            Procedimento = procedimento;
        }

        public IndexViewModel(IPagedList<Cliente> clientes, Empresa empresa)
        {
            Clientes = clientes;
            Empresa = empresa;
        }

        public IndexViewModel(IPagedList<Funcionario> funcionarios)
        {
            Funcionarios = funcionarios;
        }

        public IndexViewModel(IPagedList<Cliente> clientes)
        {
            Clientes = clientes;
        }

        public IndexViewModel(IPagedList<Categoria> categorias)
        {
            CategoriasList = categorias;
        }

        public IndexViewModel(IPagedList<Produto> produtos, IList<Categoria> categorias)
        {
            Produtos = produtos;
            Categorias = categorias;
        }

        public IndexViewModel(IList<Produto> produtosList)
        {
            ProdutosList = produtosList;
        }

        public IndexViewModel(IList<Cliente> clientesList)
        {
            ClientesList = clientesList;
        }
    }
}
