using GlamCentral.Database;
using GlamCentral.Models;
using GlamCentral.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using X.PagedList;

namespace GlamCentral.Repository
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private IConfiguration _conf;
        const int _registroPorPagina = 10;
        GCContext _banco;
        public CategoriaRepository(GCContext banco, IConfiguration conf)
        {
            _banco = banco;
            _conf = conf;
        }

        public void Atualizar(Categoria categoria)
        {
            _banco.Update(categoria);
            _banco.SaveChanges();
        }

        public void Cadastrar(Categoria categoria)
        {
            _banco.Add(categoria);
            _banco.SaveChanges();
        }

        public void Excluir(int Id)
        {
            Categoria categoria = ObterCategoria(Id);
            _banco.Remove(categoria);
            _banco.SaveChanges();
        }

        public Categoria ObterCategoria(int Id)
        {
            return _banco.Categorias.Find(Id);
        }

        public Categoria ObterCategoria(string slug)
        {
            return _banco.Categorias.Where(_ => _.Slug == slug).FirstOrDefault();
        }

        public IEnumerable<Categoria> ObterTodasCategorias()
        {
            return _banco.Categorias;
        }

        public IPagedList<Categoria> ObterTodasCategorias(int? pagina, string pesquisa)
        {
            return ObterTodasCategorias(pagina, pesquisa, "A");
        }

        public IPagedList<Categoria> ObterTodasCategorias(int? pagina, string pesquisa, string ordenacao)
        {
            int registroPorPagina = _conf.GetValue<int>("RegistroPorPagina");
            int NumeroPagina = pagina ?? 1;

            // Todo: rever o tipo AsQueryable
            var bancoCategorias = _banco.Categorias.AsQueryable();
            if (!string.IsNullOrEmpty(pesquisa))
                bancoCategorias = bancoCategorias.Where(_ => _.Nome.Contains(pesquisa.Trim()));

            if (ordenacao == "A")
                bancoCategorias = bancoCategorias.OrderBy(_ => _.Nome);
            else if (ordenacao == "ID")
                bancoCategorias = bancoCategorias.OrderBy(_ => _.Id);

            return bancoCategorias.ToPagedList<Categoria>(NumeroPagina, registroPorPagina);
        }

        public IPagedList<Categoria> ObterTodasCategorias(int? pagina, string pesquisa, string ordenacao, bool status)
        {
            int registroPorPagina = _conf.GetValue<int>("RegistroPorPagina");
            int NumeroPagina = pagina ?? 1;

            var bancoCategorias = _banco.Categorias.AsQueryable();
            if (!string.IsNullOrEmpty(pesquisa))
                bancoCategorias = bancoCategorias.Where(_ => _.Nome.Contains(pesquisa.Trim()));

            bancoCategorias = bancoCategorias.Where(_ => _.Status == status);

            if (ordenacao == "A")
                bancoCategorias = bancoCategorias.OrderBy(_ => _.Nome);
            else if (ordenacao == "ID")
                bancoCategorias = bancoCategorias.OrderBy(_ => _.Id);

            return bancoCategorias.ToPagedList<Categoria>(NumeroPagina, registroPorPagina);
        }        
    }
}
