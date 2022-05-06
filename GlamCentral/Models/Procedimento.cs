using GlamCentral.Libraries.Language;
using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GlamCentral.Models
{
    public class Procedimento
    {
        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E_Obrigatorio")]
        public string Nome { get; set; }
                
        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E_Obrigatorio")]
        public int Duracao { get; set; }

        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E_Obrigatorio")]
        public float Valor { get; set; }

        [Required(ErrorMessageResourceType = typeof(Mensagem), ErrorMessageResourceName = "MSG_E_Obrigatorio")]
        public bool Status { get; set; }

        public virtual List<ProdutosDeProcedimento> Produtos { get; set; }

		public Procedimento()
		{
            Produtos = new List<ProdutosDeProcedimento>();
		}

        public void AdicionaProdutos(List<Produto> produtos)
        {
            var produtosProcedimento = produtos.Select(_ => (new ProdutosDeProcedimento(_, this))).ToList();
            Produtos.AddRange(produtosProcedimento);
        }

        public void AdicionaProdutosId(List<Produto> produtos)
        {
            var produtosProcedimento = produtos.Select(_ => (new ProdutosDeProcedimento(_.Id, this.Id))).ToList();
            Produtos.AddRange(produtosProcedimento);
        }

        public void InsereDuracao(int horas, int minutos)
        {
            Duracao = minutos + (horas * 60);
        }
    }
}
