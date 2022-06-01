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
            var id = 1;
            var caminhoRetorno = GerenciadorArquivo.CadastrarImagemProduto(file);
            if(id == 0)
            {
                TempData["CaminhoA"] = JsonConvert.SerializeObject(caminhoRetorno);
            }
            else
            {
                TempData["CaminhoB"] = JsonConvert.SerializeObject(caminhoRetorno);
            }

            if (caminhoRetorno.Length > 0)
                return Ok(new { caminho = caminhoRetorno });
            else
                return new StatusCodeResult(500);
        }

        public IActionResult Deletar()
        {
            var caminho = JsonConvert.DeserializeObject<string>(TempData["Caminho"]?.ToString());
            if (GerenciadorArquivo.ExcluirImagemProduto(caminho))
                return Ok();            
            return BadRequest();
        }
    }
}
