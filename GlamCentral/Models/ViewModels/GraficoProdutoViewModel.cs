using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using X.PagedList;

namespace GlamCentral.Models.ViewModels
{
    public class GraficoProdutoViewModel
    {
        private IList<Categoria> _categoriasUnicas { get; set; }
        public Empresa Empresa { get; set; }

        public List<SelectListItem> CategoriasUnicas
        {
            get
            {
                return ConverteCategoriasParaSelectListItem();
            }
            private set { }
        }

        public GraficoProdutoViewModel(IList<Categoria> categoriasUnicas)
        {
            _categoriasUnicas = categoriasUnicas;
        }

        public GraficoProdutoViewModel(IList<Categoria> categoriasUnicas, Empresa empresa)
        {
            _categoriasUnicas = categoriasUnicas;
            Empresa = empresa;
        }

        private List<SelectListItem> ConverteCategoriasParaSelectListItem()
        {
            var categoriasSelectListItem = _categoriasUnicas.Select(_ => new { name = _.Nome, id = _.Id }).OrderBy(_ => _.name).ToList();

            var selectListItems = new List<SelectListItem>();
            foreach (var item in categoriasSelectListItem)
            {
                selectListItems.Add(new SelectListItem(item.name, item.id.ToString()));
            }

            return selectListItems;
        }
    }
}
