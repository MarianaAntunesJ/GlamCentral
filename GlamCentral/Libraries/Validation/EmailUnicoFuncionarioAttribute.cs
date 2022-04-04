using GlamCentral.Models;
using GlamCentral.Repository.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GlamCentral.Libraries.Validation
{
    public class EmailUnicoFuncionarioAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                string Email = (value as string).Trim();

                IFuncionarioRepository _repository = (IFuncionarioRepository)validationContext.GetService(typeof(IFuncionarioRepository));
                List<Funcionario> funcionarios = _repository.ObterFuncionarioPorEmail(Email);

                Funcionario objfuncionario = (Funcionario)validationContext.ObjectInstance;

                if (funcionarios.Count > 1)
                {
                    return new ValidationResult("E-mail já existente!");
                }
                if (funcionarios.Count == 1 && objfuncionario.Id != funcionarios[0].Id)
                {
                    return new ValidationResult("E-mail já existente!");
                }

                return ValidationResult.Success;
            }
            return null;
        }
    }
}
