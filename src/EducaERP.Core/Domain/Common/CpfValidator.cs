using System;
using System.Linq;

namespace EducaERP.Core.Domain.Common
{
    public static class CpfValidator
    {
        public static bool IsValid(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                return false;

            // Remove caracteres não numéricos
            cpf = new string(cpf.Where(char.IsDigit).ToArray());

            // Verifica tamanho
            if (cpf.Length != 11)
                return false;

            // Verifica se todos os dígitos são iguais
            if (cpf.All(d => d == cpf[0]))
                return false;

            // Calcula e compara os dígitos verificadores
            return ValidateDigit(cpf, 9) && ValidateDigit(cpf, 10);
        }

        private static bool ValidateDigit(string cpf, int position)
        {
            var sum = 0;
            var length = position;

            for (int i = 0; i < length; i++)
            {
                sum += int.Parse(cpf[i].ToString()) * (length + 1 - i);
            }

            var remainder = sum % 11;
            var digit = remainder < 2 ? 0 : 11 - remainder;

            return int.Parse(cpf[position].ToString()) == digit;
        }

        public static string Format(string cpf)
        {
            if (!IsValid(cpf))
                return cpf; // Retorna original se inválido

            cpf = new string(cpf.Where(char.IsDigit).ToArray());
            return Convert.ToUInt64(cpf).ToString(@"000\.000\.000\-00");
        }
    }
}