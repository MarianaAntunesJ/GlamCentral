using GlamCentral.Repository.Interfaces;
using GlamCentral.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GlamCentral.Libraries.Validation
{
    public class EmailUnicoClienteAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string Email = (value as string).Trim();

            IClienteRepository _repository = (IClienteRepository)validationContext.GetService(typeof(IClienteRepository));
            List<Cliente> clientes = _repository.ObterClientePorEmail(Email);

            Cliente objcliente = (Cliente)validationContext.ObjectInstance;

            if (clientes.Count > 1)
            {
                return new ValidationResult("E-mail já existente!");
            }
            if (clientes.Count == 1 && objcliente.Id != clientes[0].Id)
            {
                return new ValidationResult("E-mail já existente!");
            }

            return ValidationResult.Success;
        }
    }
}
