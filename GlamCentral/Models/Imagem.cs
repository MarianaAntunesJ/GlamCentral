﻿using System.ComponentModel.DataAnnotations.Schema;
namespace GlamCentral.Models
{
    public class Imagem
    {
        public int Id { get; set; }
        public string Caminho { get; set; }

        public int ProdutoId { get; set; }

        [ForeignKey("ProdutoId")]
        public virtual Produto Produto { get; set; }
    }
}
