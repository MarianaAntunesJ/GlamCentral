using GlamCentral.Libraries.Login;
using GlamCentral.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;

namespace GlamCentral.Libraries.Filter
{
    public class FuncionarioAutorizacaoAttribute : Attribute, IAuthorizationFilter
    {
        private List<int> _cargoFuncionarioAutorizado = new List<int>();

        public FuncionarioAutorizacaoAttribute(int cargoFuncionarioAutorizado = (int)CargoFuncionario.Gerente)
        {
            _cargoFuncionarioAutorizado.Add(cargoFuncionarioAutorizado);
        }

        LoginFuncionario _loginFuncionario;


        #region "Métodos Públicos"
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!_cargoFuncionarioAutorizado.Contains(1))
            {
                _cargoFuncionarioAutorizado.Add(1);
            }

            _loginFuncionario = (LoginFuncionario)context.HttpContext.RequestServices.GetService(typeof(LoginFuncionario));
            var funcionario = _loginFuncionario.GetFuncionario();
            if (funcionario == null)
                context.Result = new RedirectToActionResult("Login", "Home", null);
            else
                if (!_cargoFuncionarioAutorizado.Contains(funcionario.Cargo))
                    context.Result = new ForbidResult();
        }
        #endregion
    }
}
