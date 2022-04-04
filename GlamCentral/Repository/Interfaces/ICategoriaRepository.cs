using GlamCentral.Models;
using System.Collections.Generic;
using X.PagedList;

namespace GlamCentral.Repository.Interfaces
{
    public interface ICategoriaRepository
    {
        void Cadastrar(Categoria categoria);
        void Atualizar(Categoria categoria);
        void Excluir(int Id);
        Categoria ObterCategoria(int Id);
        Categoria ObterCategoria(string slug);
        IEnumerable <Categoria> ObterTodasCategorias();
        IPagedList <Categoria> ObterTodasCategorias(int? pagina, string pesquisa);
        IPagedList <Categoria> ObterTodasCategorias(int? pagina, string pesquisa, string ordenacao);
        IPagedList <Categoria> ObterTodasCategorias(int? pagina, string pesquisa, string ordenacao, bool status);
    }
}
