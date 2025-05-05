using System;
using System.Linq;

namespace EducaERP.Core.Domain.Common
{
    public static class CnpjValidator
    {
        public static bool IsValid(string cnpj)
        {
            if (string.IsNullOrWhiteSpace(cnpj))
                return false;

            // Remove caracteres não numéricos
            cnpj = new string(cnpj.Where(char.IsDigit).ToArray());

            // Verifica tamanho
            if (cnpj.Length != 14)
                return false;

            // Verifica se todos os dígitos são iguais
            if (cnpj.All(d => d == cnpj[0]))
                return false;

            // Calcula e compara os dígitos verificadores
            return ValidateDigit(cnpj, 12) && ValidateDigit(cnpj, 13);
        }

        private static bool ValidateDigit(string cnpj, int position)
        {
            int[] multipliers1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multipliers2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            var sum = 0;
            var length = position - 1;

            for (int i = 0; i < length; i++)
            {
                sum += int.Parse(cnpj[i].ToString()) *
                      (position == 13 ? multipliers2[i] : multipliers1[i]);
            }

            var remainder = sum % 11;
            var digit = remainder < 2 ? 0 : 11 - remainder;

            return int.Parse(cnpj[position].ToString()) == digit;
        }

        public static string Format(string cnpj)
        {
            if (!IsValid(cnpj))
                return cnpj; // Retorna original se inválido

            cnpj = new string(cnpj.Where(char.IsDigit).ToArray());
            return Convert.ToUInt64(cnpj).ToString(@"00\.000\.000\/0000\-00");
        }
    }
}