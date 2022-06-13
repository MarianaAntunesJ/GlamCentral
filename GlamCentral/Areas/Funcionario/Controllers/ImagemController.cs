using GlamCentral.Libraries.Arquivo;
using GlamCentral.Libraries.Filter;
using GlamCentral.Models.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GlamCentral.Areas.Funcionario.Controllers
{
    [Area("Funcionario")]
    [FuncionarioAutorizacao((int)CargoFuncionario.Gerente)]
    public class ImagemController : Controller
    {
        [HttpPost]
        public IActionResult Armazenar(IFormFile file, string imagem)
        {
            var caminho = GerenciadorArquivo.CadastrarImagemProduto(file);
            SerializeCaminho(caminho, imagem);
            if (caminho.Length > 0)
                return Ok(new { caminho = caminho });
            else
                return new StatusCodeResult(500);
        }

        [HttpPost]
        public IActionResult Deletar(string imagem)
        {
            var caminho = DeserializeCaminho(imagem);
            if (GerenciadorArquivo.ExcluirImagemProduto(caminho))
                return Ok(new { caminho = caminho });            
            return BadRequest();
        }

        private void SerializeCaminho(string caminho, string imagem)
        {
            if (imagem == "0")
            {
                TempData["CaminhoA"] = JsonConvert.SerializeObject(caminho);
            }
            else
            {
                TempData["CaminhoB"] = JsonConvert.SerializeObject(caminho);
            }
        }

        private string DeserializeCaminho(string imagem)
        {
            if (imagem == "0")
            {
                return JsonConvert.DeserializeObject<string>(TempData["CaminhoA"]?.ToString());
            }
            else
            {
                return JsonConvert.DeserializeObject<string>(TempData["CaminhoB"]?.ToString());
            }
        }
    }
}
