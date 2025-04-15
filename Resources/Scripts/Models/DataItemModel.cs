using CommunityToolkit.Mvvm.ComponentModel;
using System;

namespace FunctionsFunctionsAndMoreFunctions.Resources.Scripts.Models
{
    internal class DataItemModel : ObservableObject
    {
        private Action<DataItemModel> recalculateCallback;

        private double x = 0;
        public double X
        {
            get { return x; }
            set
            {
                x = value;
                recalculateCallback?.Invoke(this);
                OnPropertyChanged(nameof(X));
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
