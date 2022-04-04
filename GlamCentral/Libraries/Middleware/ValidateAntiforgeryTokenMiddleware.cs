using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace GlamCentral.Libraries.Middleware
{
    public class ValidateAntiforgeryTokenMiddleware
    {
        private RequestDelegate _next;
        private IAntiforgery _antiforgery;

        public ValidateAntiforgeryTokenMiddleware(RequestDelegate next, IAntiforgery antiforgery)
        {
            _next = next;
            _antiforgery = antiforgery;
        }

        public async Task Invoke(HttpContext context)
        {
            var cabecalho = context.Request.Headers["x-requested-with"];
            bool ajax = cabecalho == "XMLHttpRequest";

            if (HttpMethods.IsPost(context.Request.Method)
                && !(context.Request.Form.Files.Count == 1 && ajax))
            {
                    await _antiforgery.ValidateRequestAsync(context);         
            }                
            await _next(context);
        }
    }
}
