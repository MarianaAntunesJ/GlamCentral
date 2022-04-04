using GlamCentral.Database;
using GlamCentral.Models;
using GlamCentral.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace GlamCentral.Repository
{
    public class ImagemRepository : IImagemRepository
    {
        #region "Propriedades Privadas"
        private GCContext _banco;
        #endregion

        #region "Construtor"
        public ImagemRepository(GCContext banco)
        {
            _banco = banco;
        }
        #endregion

        public void CadastrarImagens(List<Imagem> imagens, int produtoId)
        {
            if (imagens != null && imagens.Count > 0)
            {
                foreach (var imagem in imagens)
                    Cadastrar(imagem);
            }
        }

        public void Cadastrar(Imagem imagem)
        {
            _banco.Add(imagem);
            _banco.SaveChanges();
        }

        public void Excluir(int id)
        {
            _banco.Remove(_banco.Imagens.Find(id));
            _banco.SaveChanges();
        }

        public void ExcluirImagensDoProduto(int produtoId)
        {
            var imagens = _banco.Imagens.Where(_ => _.ProdutoId == produtoId).ToList();
            foreach (Imagem imagem in imagens)
                _banco.Remove(imagem);
            _banco.SaveChanges();
        }
    }
}
