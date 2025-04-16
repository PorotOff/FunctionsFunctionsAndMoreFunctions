using System;
using System.Collections.Generic;
using System.Linq;

namespace FunctionsFunctionsAndMoreFunctions.Resources.Scripts.Models
{
    /// <summary>
    /// Модель, содержащая словарь функций и связанных с ними значений.
    /// </summary>
    public class FunctionValuesParisModel
    {
        private Dictionary<string, (Func<double, double, double, double, double, double> Formula, AllValuesModel Values)> functionsValues =
            new Dictionary<string, (Func<double, double, double, double, double, double>, AllValuesModel)>
            {
                { "линейная", ((a, b, c, x, y) => a * x + b * Math.Pow(y, 0) + c, new AllValuesModel()) },
                { "квадратичная", ((a, b, c, x, y) => a * Math.Pow(x, 2) + b * Math.Pow(y, 1) + c, new AllValuesModel()) },
                { "кубическая", ((a, b, c, x, y) => a * Math.Pow(x, 3) + b * Math.Pow(y, 2) + c, new AllValuesModel()) },
                { "4-ой степени", ((a, b, c, x, y) => a * Math.Pow(x, 4) + b * Math.Pow(y, 3) + c, new AllValuesModel()) },
                { "5-ой степени", ((a, b, c, x, y) => a * Math.Pow(x, 5) + b * Math.Pow(y, 4) + c, new AllValuesModel()) }
            };

        /// <summary>
        /// Получает список названий всех доступных функций.
        /// </summary>
        /// <returns>Список строк с названиями функций.</returns>
        public List<string> GetFunctionNames()
        {
            return functionsValues.Keys.ToList();
        }

        /// <summary>
        /// Получает все сохраненные значения для заданной функции.
        /// </summary>
        /// <param name="functionName">Название функции.</param>
        /// <returns>Экземпляр AllValuesModel с сохраненными значениями.</returns>
        public AllValuesModel GetAllValuesByFunctionName(string functionName)
        {
            return functionsValues[functionName].Values;
        }

        /// <summary>
        /// Сохраняет значения для указанной функции.
        /// </summary>
        /// <param name="functionName">Название функции.</param>
        /// <param name="values">Значения для сохранения.</param>
        public void SetValuesByFunctionName(string functionName, AllValuesModel values)
        {
            var formula = functionsValues[functionName].Formula;
            functionsValues[functionName] = (formula, values);
        }

        /// <summary>
        /// Получает формулу для вычислений по заданной функции.
        /// </summary>
        /// <param name="functionName">Название функции.</param>
        /// <returns>Функцию для вычислений, принимающую коэффициенты и значения.</returns>
        public Func<double, double, double, double, double, double> GetFormulaByFunctionName(string functionName)
        {
            return functionsValues[functionName].Formula;
        }
    }
}
