using System.Globalization;
using System.Windows;

namespace FunctionsFunctionsAndMoreFunctions.Resources.Scripts.Validation
{
    /// <summary>
    /// Предоставляет методы для валидации и парсинга строковых значений в тип double.
    /// </summary>
    public static class DoubleNumbersValidation
    {
        /// <summary>
        /// Пытается распарсить входную строку в число типа double.
        /// При неудаче выводит модальное окно с ошибкой и возвращает 0.
        /// </summary>
        /// <param name="input">Входная строка для парсинга.</param>
        /// <returns>Распарсенное число или 0 при ошибке.</returns>
        public static double TryParseDoubleNumbers(string input)
        {
            if (double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out double result))
                return result;

            MessageBox.Show(
                "Введено недопустимое значение. Ожидалось число с плавающей точкой.",
                "Ошибка ввода",
                MessageBoxButton.OK,
                MessageBoxImage.Error
            );

            return 0;
        }
    }
}
