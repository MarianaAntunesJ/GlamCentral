using GlamCentral.Libraries.Arquivo;
using GlamCentral.Libraries.Filter;
using GlamCentral.Models.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GlamCentral.Areas.Funcionario.Controllers
{
    [Area("Funcionario")]
    [FuncionarioAutorizacao((int)CargoFuncionario.Gerente)]
    public class ImagemController : Controller
    {
        [HttpPost]
        public IActionResult Armazenar(IFormFile file)
        {
            var caminhoRetorno = GerenciadorArquivo.CadastrarImagemProduto(file);

            if (caminhoRetorno.Length > 0)
                return Ok(new { caminho = caminhoRetorno });
            else
                return new StatusCodeResult(500);
        }

        public IActionResult Deletar(string caminho)
        {
            if (GerenciadorArquivo.ExcluirImagemProduto(caminho))
                return Ok();
            else
                return BadRequest();
        }
    }
}
