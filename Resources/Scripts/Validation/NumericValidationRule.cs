using System.Globalization;
using System.Windows.Controls;

namespace FunctionsFunctionsAndMoreFunctions.Resources.Scripts.Validation
{
    public class NumericValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string input = value as string;

            // Разрешаем пустую строку для промежуточного ввода
            if (string.IsNullOrEmpty(input))
                return ValidationResult.ValidResult;

            // Разрешаем только цифры, точку и минус
            foreach (char c in input)
            {
                if (!char.IsDigit(c) && c != '.' && c != '-')
                    return new ValidationResult(false, "Допустимы только цифры и точка");
            }

            // Проверяем корректность числа
            if (double.TryParse(input, NumberStyles.Any, cultureInfo, out _))
                return ValidationResult.ValidResult;

            return new ValidationResult(false, "Некорректный формат числа");
        }
    }
}
