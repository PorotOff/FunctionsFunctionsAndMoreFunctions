using System.Globalization;
using System.Windows;

namespace FunctionsFunctionsAndMoreFunctions.Resources.Scripts.Validation
{
    public static class DoubleNumericValidator
    {
        public static double Parse(string input)
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
