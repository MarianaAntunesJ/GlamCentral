using GlamCentral.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;

namespace GlamCentral.Libraries.Arquivo
{
    public class GerenciadorArquivo
    {
        public static string CadastrarImagemProduto(IFormFile file)
        {
            var nomeArquivo = Path.GetFileName(file.FileName);
            var caminho = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/temp", nomeArquivo);

            using (var stream = new FileStream(caminho, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            return Path.Combine("/uploads/temp", nomeArquivo).Replace("\\", "/");
        }

        public static bool ExcluirImagemProduto(string caminho)
        {
            var caminhoRetorno = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", caminho.TrimStart('/'));
            if (File.Exists(caminhoRetorno))
            {
                File.Delete(caminhoRetorno);
                return true;
            }
            else
                return false;
        }

        public static List<Imagem> MoverImagensProduto(List<string> caminhosTemporarios, int produtoId)
        {
            // Cria pasta
            var caminhoDefinitivoPastaDeProdutos = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", produtoId.ToString());
            if (!Directory.Exists(caminhoDefinitivoPastaDeProdutos))
                Directory.CreateDirectory(caminhoDefinitivoPastaDeProdutos);

            // Move arquivos
            var imagensDef = new List<Imagem>();
            foreach (var caminhoTemp in caminhosTemporarios)
            {
                if (!string.IsNullOrEmpty(caminhoTemp))
                {
                    var nomeArquivo = Path.GetFileName(caminhoTemp);

                    var caminhoDef = Path.Combine("/uploads", produtoId.ToString(), nomeArquivo).Replace("\\", "/");

                    if (caminhoDef != caminhoTemp)
                    {
                        var caminhoAbsolutoTemp = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/temp", nomeArquivo);
                        var caminhoAbsolutoDef = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", produtoId.ToString(), nomeArquivo);

                        if (File.Exists(caminhoAbsolutoTemp))
                        {

                            if (File.Exists(caminhoAbsolutoDef))
                            {
                                File.Delete(caminhoAbsolutoDef);
                            }

                            File.Copy(caminhoAbsolutoTemp, caminhoAbsolutoDef);

                            if (File.Exists(caminhoAbsolutoDef))
                            {
                                File.Delete(caminhoAbsolutoTemp);
                            }
                            imagensDef.Add(new Imagem() { Caminho = Path.Combine("/uploads", produtoId.ToString(), nomeArquivo).Replace("\\", "/"), ProdutoId = produtoId });
                        }
                        else
                            return null;
                    }
                    else
                    {
                        imagensDef.Add(new Imagem() { Caminho = Path.Combine("/uploads", produtoId.ToString(), nomeArquivo).Replace("\\", "/"), ProdutoId = produtoId });
                    }
                }
            }
            return imagensDef;
        }

        public static void ExcluirImagensProduto(List<Imagem> ListaImagem)
        {
            int produtoId = 0;
            foreach (var Imagem in ListaImagem)
            {
                ExcluirImagemProduto(Imagem.Caminho);
                produtoId = Imagem.ProdutoId;
            }

            var pastaProduto = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", produtoId.ToString());

            if (Directory.Exists(pastaProduto))
            {
                Directory.Delete(pastaProduto);
            }
        }
    }
}
