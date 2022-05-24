using X.PagedList;

namespace GlamCentral.Models.ViewModels
{
    public class HomeViewModel
    {
        public Empresa Empresa { get; set; }
        public IPagedList<Cliente> Clientes { get; set; }

        public HomeViewModel()
        {
        }
        public HomeViewModel(IPagedList<Cliente> clientes, Empresa empresa)
        {
            Clientes = clientes;
            Empresa = empresa;
        }
    }
}
