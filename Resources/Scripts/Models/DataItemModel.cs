using CommunityToolkit.Mvvm.ComponentModel;
using FunctionsFunctionsAndMoreFunctions.Resources.Scripts.Validation;
using System;
using System.Globalization;

namespace FunctionsFunctionsAndMoreFunctions.Resources.Scripts.Models
{
    public class DataItemModel : ObservableObject
    {
        private Action<DataItemModel> recalculateCallback;

        public string XRaw
        {
            get => x.ToString(CultureInfo.InvariantCulture);
            set
            {
                X = DoubleNumericValidator.Parse(value); // присваиваем double-свойству
                OnPropertyChanged(nameof(XRaw));
            }
        }

        private double x = 0;
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

        public string YRaw
        {
            get => y.ToString(CultureInfo.InvariantCulture);
            set
            {
                Y = DoubleNumericValidator.Parse(value); // присваиваем double-свойству
                OnPropertyChanged(nameof(YRaw));
            }
        }

        private double y = 0;
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
        public double Result
        {
            get { return result; }
            set
            {
                result = value;
                OnPropertyChanged(nameof(Result));
            }
        }

        public void SetRecalculationHandler(Action<DataItemModel> handler)
        {
            recalculateCallback = handler;
        }
    }
}
