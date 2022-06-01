namespace GlamCentral.Models
{
    public class ProdutosDeProcedimento
    {
        #region Atributos Públicos
        public int ProdutoId { get; set; }
        public virtual Produto Produto { get; set; }

        public int ProcedimentoId { get; set; }
        public virtual Procedimento Procedimento { get; set; }
        #endregion

        #region Construtores
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
        #endregion
    }
}
