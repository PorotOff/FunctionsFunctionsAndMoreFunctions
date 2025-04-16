using FunctionsFunctionsAndMoreFunctions.Resources.Scripts.Models;

namespace FFFTesting
{
    public class FormulasTesting
    {
        #region Проверка работы формул с подстановкой обычных чисел

        /// <summary>
        /// Проверяет, что функция корректно вычисляет результат для обычных числовых входных параметров.
        /// "линейная": 2 * 5 + 3 * 1 + 4 = 10 + 3 + 4 = 17
        /// "квадратичная": 2 * 25 + 3 * 6 + 4 = 50 + 18 + 4 = 72
        /// "кубическая": 2 * 125 + 3 * 36 + 4 = 250 + 108 + 4 = 362
        /// "4-ой степени": 2 * 625 + 3 * 216 + 4 = 1250 + 648 + 4 = 1902
        /// "5-ой степени": 2 * 3125 + 3 * 1296 + 4 = 6250 + 3888 + 4 = 10142
        /// </summary>
        /// <param name="functionName">Название функции.</param>
        /// <param name="a">Коэффициент a.</param>
        /// <param name="b">Коэффициент b.</param>
        /// <param name="c">Коэффициент c.</param>
        /// <param name="x">Значение x.</param>
        /// <param name="y">Значение y.</param>
        /// <param name="expected">Ожидаемый результат.</param>
        [Theory]
        // "линейная": 2 * 5 + 3 * 1 + 4 = 10 + 3 + 4 = 17
        [InlineData("линейная", 2, 3, 4, 5, 6, 17)]
        // "квадратичная": 2 * 25 + 3 * 6 + 4 = 50 + 18 + 4 = 72
        [InlineData("квадратичная", 2, 3, 4, 5, 6, 72)]
        // "кубическая": 2 * 125 + 3 * 36 + 4 = 250 + 108 + 4 = 362
        [InlineData("кубическая", 2, 3, 4, 5, 6, 362)]
        // "4-ой степени": 2 * 625 + 3 * 216 + 4 = 1250 + 648 + 4 = 1902
        [InlineData("4-ой степени", 2, 3, 4, 5, 6, 1902)]
        // "5-ой степени": 2 * 3125 + 3 * 1296 + 4 = 6250 + 3888 + 4 = 10142
        [InlineData("5-ой степени", 2, 3, 4, 5, 6, 10142)]

        public void Formula_CalculatesExpectedResult(
            string functionName, double a, double b, double c, double x, double y, double expected)
        {
            // Arrange
            var model = new FunctionValuesParisModel();
            var formula = model.GetFormulaByFunctionName(functionName);

            // Act
            double result = formula(a, b, c, x, y);

            // Assert
            Assert.Equal(expected, result, precision: 5);
        }
        #endregion


        #region Проверка работы формул с подстановкой нулей
        /// <summary>
        /// Проверяет, что для каждой функции при нулевых коэффициентах результат равен 0, независимо от значений x и y.
        /// </summary>
        /// <param name="functionName">Название функции.</param>
        [Theory]
        [InlineData("линейная")]
        [InlineData("квадратичная")]
        [InlineData("кубическая")]
        [InlineData("4-ой степени")]
        [InlineData("5-ой степени")]
        public void Formula_WithZeroCoefficients_ReturnsZero(string functionName)
        {
            // Arrange
            var model = new FunctionValuesParisModel();
            var formula = model.GetFormulaByFunctionName(functionName);

            // Act
            double result = formula(0, 0, 0, 123, 456);

            // Assert
            Assert.Equal(0, result, precision: 10);
        }
        #endregion

        #region Проверка работы формул с подстановкой отрицательных чисел
        /// <summary>
        /// Проверяет, что функция корректно вычисляет результат для отрицательных значений входных параметров.
        /// Для "линейной": f(x, y) = a * x + b * 1 + c, например: -5 * (-2) + (-3) * 1 + 1 = 10 - 3 + 1 = 8.
        /// Для "квадратичной": f(x, y) = a * x^2 + b * y + c, например: 1 * 4 + 1 * (-3) + 10 = 4 - 3 + 10 = 11.
        /// Для "кубической": f(x, y) = a * x^3 + b * y^2 + c, например: 1 * (-8) + 1 * 4 + 10 = -8 + 4 + 10 = 6.
        /// </summary>
        /// <param name="functionName">Название функции.</param>
        /// <param name="a">Коэффициент a.</param>
        /// <param name="b">Коэффициент b.</param>
        /// <param name="c">Коэффициент c.</param>
        /// <param name="x">Значение x.</param>
        /// <param name="y">Значение y.</param>
        /// <param name="expected">Ожидаемый результат.</param>
        [Theory]
        [InlineData("линейная", -5, -3, 1, -2, -4, 8)]
        [InlineData("квадратичная", 1, 1, 10, -2, -3, 11)]
        [InlineData("кубическая", 1, 1, 10, -2, -2, 6)]

        public void Formula_WithNegativeInputs_CorrectlyCalculated(
            string functionName,
            double a, double b, double c,
            double x, double y,
            double expected)
        {
            // Arrange
            var model = new FunctionValuesParisModel();
            var formula = model.GetFormulaByFunctionName(functionName);

            // Act
            double result = formula(a, b, c, x, y);

            // Assert
            Assert.Equal(expected, result, precision: 10);
        }
        #endregion

        #region Проверка работы формул при подстановке больших значений (проверка переполнения и точности)
        /// <summary>
        /// Проверяет, что для функций с большими входными значениями происходит корректный расчет без переполнения.
        /// </summary>
        /// <param name="functionName">Название функции.</param>
        /// <param name="a">Коэффициент a.</param>
        /// <param name="b">Коэффициент b.</param>
        /// <param name="c">Коэффициент c.</param>
        /// <param name="x">Значение x.</param>
        /// <param name="y">Значение y.</param>
        /// <param name="expected">Ожидаемый результат.</param>
        [Theory]
        [InlineData("5-ой степени", 1, 0, 0, 10, 1, 100000)]
        [InlineData("5-ой степени", 1, 0, 0, 100, 1, 1e10)]
        public void Formula_WithLargeInputs_DoesNotOverflow(string functionName, double a, double b, double c, double x, double y, double expected)
        {
            // Arrange
            var model = new FunctionValuesParisModel();
            var formula = model.GetFormulaByFunctionName(functionName);

            // Act
            double result = formula(a, b, c, x, y);

            // Assert
            Assert.Equal(expected, result, precision: 0);
        }
        #endregion

        #region Проверка погрешности вычислений
        /// <summary>
        /// Проверяет согласованность округления вычислений для функции "линейная" с плавающей точностью.
        /// </summary>
        [Fact]
        public void Formula_RoundingPrecision_IsConsistent()
        {
            // Arrange
            var model = new FunctionValuesParisModel();
            var formula = model.GetFormulaByFunctionName("линейная");

            // Act
            double result = formula(1.1234567, 2.7654321, 0.0000001, 3.14159, 2.71828);

            // Assert
            Assert.Equal(1.1234567 * 3.14159 + 2.7654321 + 0.0000001, result, precision: 5);
        }
        #endregion
    }
}
