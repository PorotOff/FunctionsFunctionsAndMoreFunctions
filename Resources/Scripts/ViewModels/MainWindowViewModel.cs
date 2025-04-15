using CommunityToolkit.Mvvm.ComponentModel;
using FunctionsFunctionsAndMoreFunctions.Resources.Scripts.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace FunctionsFunctionsAndMoreFunctions.Resources.Scripts.ViewModels
{
    internal class MainWindowViewModel : ObservableObject, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private FunctionValuesParisModel functionValuesParisModel = new FunctionValuesParisModel();

        private ObservableCollection<string> functionNames = new ObservableCollection<string>();
        public ObservableCollection<string> FunctionNames
        {
            get { return functionNames; }
            set
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FunctionNames)));
            }
        }
        public ObservableCollection<double> AvailableCValues { get; set; } = new ObservableCollection<double>();

        #region Values
        private double aRatio;
        public double ARatio
        {
            get { return aRatio; }
            set
            {
                aRatio = value;
                SaveValues();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ARatio)));
            }
        }

        private double bRatio;
        public double BRatio
        {
            get { return bRatio; }
            set
            {
                bRatio = value;
                SaveValues();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BRatio)));
            }
        }

        private double cRatio;
        public double CRatio
        {
            get { return cRatio; }
            set
            {
                cRatio = value;
                SaveValues();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CRatio)));
            }
        }

        private double x;
        public double X
        {
            get { return x; }
            set
            {
                x = value;
                SaveValues();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(X)));
            }
        }

        private double y;
        public double Y
        {
            get { return y; }
            set
            {
                y = value;
                SaveValues();
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
                SaveValues();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Result)));
            }
        }

        public ObservableCollection<DataItemModel> DataItems { get; private set; } = new ObservableCollection<DataItemModel>();
        #endregion

        private string selectedFunction;
        public string SelectedFunction
        {
            get { return selectedFunction; }
            set
            {
                selectedFunction = value;

                RecalculateCValues();

                LoadValues();

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedFunction)));
            }
        }

        public MainWindowViewModel()
        {
            FillFunctionsCollection();

            SelectedFunction = FunctionNames[0];
        }

        private void FillFunctionsCollection()
        {
            foreach (var functionName in functionValuesParisModel.GetFunctionNames())
            {
                FunctionNames.Add(functionName);
            }
        }

        private void LoadValues()
        {
            AllValuesModel allValuesModel = functionValuesParisModel.GetAllValuesByFunctionName(SelectedFunction);
            int selectedFunctionIndex = FunctionNames.IndexOf(SelectedFunction);

            ARatio = allValuesModel.ARatio;
            BRatio = allValuesModel.BRatio;
            CRatio = AvailableCValues[allValuesModel.CRatioIndex];

            foreach (var dataItem in DataItems)
            {
                DataItems.Add(new DataItemModel
                {
                    X = dataItem.X,
                    Y = dataItem.Y,
                    Result = dataItem.Result
                });
            }

            SetCalculationHandlers();
        }

        private void SaveValues()
        {
            AllValuesModel allValuesModel = new AllValuesModel
            {
                ARatio = ARatio,
                BRatio = BRatio,
                CRatioIndex = AvailableCValues.IndexOf(CRatio),
                DataItems = new ObservableCollection<DataItemModel>(DataItems)
            };

            functionValuesParisModel.SetValuesByFunctionName(SelectedFunction, allValuesModel);
        }

        private void RecalculateCValues()
        {
            AvailableCValues.Clear();

            int selectedFunctionIndex = FunctionNames.IndexOf(SelectedFunction);
            int multiplier = (int)Math.Pow(10, selectedFunctionIndex);

            for (int i = 1; i <= FunctionNames.Count; i++)
            {
                AvailableCValues.Add(i * multiplier);
            }
        }

        private void SetCalculationHandlers()
        {
            foreach (var item in DataItems)
            {
                item.SetRecalculationHandler(CalculateItemResult);
            }

            DataItems.CollectionChanged += (s, e) =>
            {
                if (e.NewItems != null)
                {
                    foreach (DataItemModel newItem in e.NewItems)
                    {
                        newItem.SetRecalculationHandler(CalculateItemResult);
                    }
                }
            };
        }

        private void CalculateItemResult(DataItemModel item)
        {
            var formula = functionValuesParisModel.GetFormulaByFunctionName(SelectedFunction);
            item.Result = formula(ARatio, BRatio, CRatio, item.X, item.Y);
        }

    }
}
