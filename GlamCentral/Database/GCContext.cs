using GlamCentral.Models;
using Microsoft.EntityFrameworkCore;

namespace GlamCentral.Database
{
    // Quais dados serão persistidos no bando de dados.
    public class GCContext : DbContext
    {      
        #region "Atributos Públicos"
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Imagem> Imagens { get; set; }
        public DbSet<Procedimento> Procedimentos { get; set; }
        public DbSet<Pagamento> Pagamentos { get; set; }
        public DbSet<Agenda> Agenda { get; set; }
        public DbSet<Empresa> Empresa { get; set; }
        public DbSet<ProdutosDeProcedimento> ProdutosDeProcedimento { get; set; }
        #endregion

        #region "Construtor"
        public GCContext(DbContextOptions options) : base(options)
        {
        }
        #endregion

        // fluent API
        protected override void OnModelCreating(ModelBuilder mb)
		{
            mb.Entity<ProdutosDeProcedimento>().HasKey(_ => new { _.ProdutoId, _.ProcedimentoId });

            mb.Entity<ProdutosDeProcedimento>().HasOne(_ => _.Produto).WithMany(_ => _.Procedimentos).HasForeignKey(_ => _.ProdutoId);

            mb.Entity<ProdutosDeProcedimento>().HasOne(_ => _.Procedimento).WithMany(_ => _.Produtos).HasForeignKey(_ => _.ProcedimentoId);
        }
    }
}
