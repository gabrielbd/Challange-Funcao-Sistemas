using System;
using System.ComponentModel.DataAnnotations;

namespace WebAtividadeEntrevista.Utils
{
    public class CPFAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var cpf = value as string;
            if (cpf == null)
            {
                return false;
            }

            cpf = RemoverFormatacao(cpf);

            if (cpf.Length != 11 || !long.TryParse(cpf, out _))
            {
                return false;
            }

            if (!ValidarDigitosVerificadores(cpf))
            {
                return false;
            }

            return true;
        }

        private bool ValidarDigitosVerificadores(string cpf)
        {
            int[] multiplicadores1 = new int[] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicadores2 = new int[] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCPF = cpf.Substring(0, 9);

            int soma = 0;
            for (int i = 0; i < 9; i++)
            {
                soma += int.Parse(tempCPF[i].ToString()) * multiplicadores1[i];
            }

            int resto = soma % 11;
            int digito1 = resto < 2 ? 0 : 11 - resto;

            soma = 0;
            for (int i = 0; i < 9; i++)
            {
                soma += int.Parse(tempCPF[i].ToString()) * multiplicadores2[i];
            }
            soma += digito1 * multiplicadores2[9];

            resto = soma % 11;
            int digito2 = resto < 2 ? 0 : 11 - resto;

            return digito1 == int.Parse(cpf[9].ToString()) && digito2 == int.Parse(cpf[10].ToString());
        }

        private string RemoverFormatacao(string cpf)
        {
            return cpf.Replace(".", "").Replace("-", "");
        }

        public override string FormatErrorMessage(string name)
        {
            return "O CPF deve ter o formato 000.000.000-00 e ser válido.";
        }
    }
}