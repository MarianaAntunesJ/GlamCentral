namespace GlamCentral.Models
{
    public class ProdutosDeProcedimento
    {
        public int ProdutoId { get; set; }
        public virtual Produto Produto { get; set; }

        public int ProcedimentoId { get; set; }
        public virtual Procedimento Procedimento { get; set; }

		public ProdutosDeProcedimento()
		{
		}

		public ProdutosDeProcedimento(Produto produto, Procedimento procedimento)
		{
			Produto = produto;
			Procedimento = procedimento;
		}

        public ProdutosDeProcedimento(int produtoId, int procedimentoId)
        {
            ProdutoId = produtoId;
            ProcedimentoId = procedimentoId;
        }

        public ProdutosDeProcedimento(int produtoId, Produto produto, int procedimentoId, Procedimento procedimento)
        {
            ProdutoId = produtoId;
            Produto = produto;
            ProcedimentoId = procedimentoId;
            Procedimento = procedimento;
        }
    }
}
