using CommunityToolkit.Mvvm.ComponentModel;
using FunctionsFunctionsAndMoreFunctions.Resources.Scripts.Validation;
using System.Globalization;

namespace FunctionsFunctionsAndMoreFunctions.Resources.Scripts.Models
{
    /// <summary>
    /// Модель, для хранения строковых значений в формате результат, x, y.
    /// </summary>
    public class DataItemModel : ObservableObject
    {
        private Action<DataItemModel> recalculateCallback;

        /// <summary>
        /// Автосвойство, хранящее значение X в строковом представлении.
        /// Используется для ввода пользователем и реагирует на изменения для привязки данных.
        /// </summary>
        public string XRaw
        {
            get => x.ToString(CultureInfo.InvariantCulture);
            set
            {
                X = DoubleNumbersValidation.TryParseDoubleNumbers(value);
                OnPropertyChanged(nameof(XRaw));
            }
        }

        private double x = 0;

        /// <summary>
        /// Автосвойство, хранящее значение X.
        /// Используется в вычислениях и еагирует на изменения для привязки данных.
        /// </summary>
        public double X
        {
            get { return x; }
            set
            {
                x = value;
                recalculateCallback?.Invoke(this);
                OnPropertyChanged(nameof(X));
                OnPropertyChanged(nameof(XRaw));
            }
        }

        /// <summary>
        /// Автосвойство, хранящее значение Y в строковом представлении.
        /// Используется для ввода пользователем и реагирует на изменения для привязки данных.
        /// </summary>
        public string YRaw
        {
            get => y.ToString(CultureInfo.InvariantCulture);
            set
            {
                Y = DoubleNumbersValidation.TryParseDoubleNumbers(value);
                OnPropertyChanged(nameof(YRaw));
            }
        }

        private double y = 0;

        /// <summary>
        /// Автосвойство, хранящее значение Y.
        /// Используется в вычислениях и еагирует на изменения для привязки данных.
        /// </summary>
        public double Y
        {
            get { return y; }
            set
            {
                y = value;
                recalculateCallback?.Invoke(this);
                OnPropertyChanged(nameof(Y));
                OnPropertyChanged(nameof(YRaw));
            }
        }

        private double result = 0;

        /// <summary>
        /// Автосвойство, хранящее результат вычислений по функции.
        /// Реагирует на изменения для привязки данных.
        /// </summary>
        public double Result
        {
            get { return result; }
            set
            {
                result = value;
                OnPropertyChanged(nameof(Result));
            }
        }

        /// <summary>
        /// Устанавливает обработчик для перерасчёта результата при изменении входных данных.
        /// </summary>
        /// <param name="handler">Делегат, вызываемый для перерасчёта.</param>
        public void SetRecalculationHandler(Action<DataItemModel> handler)
        {
            recalculateCallback = handler;
        }
    }
}
