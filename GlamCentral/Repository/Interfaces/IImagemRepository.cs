using GlamCentral.Models;
using System.Collections.Generic;

namespace GlamCentral.Repository.Interfaces
{
    public interface IImagemRepository
    {
        void CadastrarImagens(List<Imagem> imagens, int produtoId);
        void Cadastrar(Imagem imagem);
        void Excluir(int id);
        void ExcluirImagensDoProduto(int produtoId);
    }
}
