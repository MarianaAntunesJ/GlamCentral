using GlamCentral.Models;
using Microsoft.EntityFrameworkCore;

namespace GlamCentral.Database
{
    // Quais dados serão persistidos no bando de dados.
    public class GCContext : DbContext
    {
        #region "Construtor"
        public GCContext(DbContextOptions options) : base(options)
        {
        }
        #endregion

        #region "Métodos Públicos"
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Imagem> Imagens { get; set; }
        public DbSet<Procedimento> Procedimentos { get; set; }
        public DbSet<Pagamento> Pagamentos { get; set; }
        public DbSet<Agenda> Agenda { get; set; }
        public DbSet<Empresa> Empresa { get; set; }
        #endregion
    }
}
