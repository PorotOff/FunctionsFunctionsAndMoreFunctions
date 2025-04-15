using System;
using System.ComponentModel;

namespace FunctionsFunctionsAndMoreFunctions.Resources.Scripts.Models
{
    internal class DataItemModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private double x;
        public double X
        {
            get => x;
            set
            {
                x = value;
                RecalculateResult();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(X)));
            }
        }

        private double y;
        public double Y
        {
            get => y;
            set
            {
                y = value;
                RecalculateResult();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Y)));
            }
        }

        private double result;
        public double Result
        {
            get { return result; }
            set
            {
                result = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Result)));
            }
        }

        private Action<DataItemModel> calculateResult;

        public void SetRecalculationHandler(Action<DataItemModel> handler)
        {
            calculateResult = handler;
        }

        private void RecalculateResult()
        {
            calculateResult?.Invoke(this);
        }
    }
}
