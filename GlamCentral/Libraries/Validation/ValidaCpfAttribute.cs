using System.ComponentModel.DataAnnotations;

namespace GlamCentral.Libraries.Validation
{
    public class ValidaCpfAttribute : ValidationAttribute
    {
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
            if (value != null)
			{
				string cpf = (value as string).Trim();

                string valor = cpf.Replace(".", "");
                valor = valor.Replace("-", "");

                if (valor.Length != 11)
                    return new ValidationResult("CPF inválido.");

                bool igual = true;
                for (int i = 1; i < 11 && igual; i++)
                    if (valor[i] != valor[0])
                        igual = false;

                if (igual || valor == "12345678909")
                    return new ValidationResult("CPF inválido.");

                int[] numeros = new int[11];

                for (int i = 0; i < 11; i++)
                    numeros[i] = int.Parse(
                      valor[i].ToString());

                int soma = 0;
                for (int i = 0; i < 9; i++)
                    soma += (10 - i) * numeros[i];

                int resultado = soma % 11;

                if (resultado == 1 || resultado == 0)
                {
                    if (numeros[9] != 0)
                        return new ValidationResult("CPF inválido.");
                }
                else if (numeros[9] != 11 - resultado)
                    return new ValidationResult("CPF inválido.");

                soma = 0;
                for (int i = 0; i < 10; i++)
                    soma += (11 - i) * numeros[i];

                resultado = soma % 11;

                if (resultado == 1 || resultado == 0)
                {
                    if (numeros[10] != 0)
                        return new ValidationResult("CPF inválido.");
                }
                else
                    if (numeros[10] != 11 - resultado)
                    return new ValidationResult("CPF inválido.");

                return ValidationResult.Success;
			}
			return new ValidationResult("CPF inválido.");
		}
	}
}
