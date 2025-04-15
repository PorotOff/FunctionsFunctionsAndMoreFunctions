using CommunityToolkit.Mvvm.ComponentModel;
using FunctionsFunctionsAndMoreFunctions.Resources.Scripts.Models;
using FunctionsFunctionsAndMoreFunctions.Resources.Scripts.Validation;
using System;
using System.Collections.ObjectModel;
using System.Globalization;

namespace FunctionsFunctionsAndMoreFunctions.Resources.Scripts.ViewModels
{
    public class MainWindowViewModel : ObservableObject
    {
        private FunctionValuesParisModel functionValuesParisModel = new FunctionValuesParisModel();

        private ObservableCollection<string> functionNames = new ObservableCollection<string>();
        public ObservableCollection<string> FunctionNames
        {
            get { return functionNames; }
            set
            {
                OnPropertyChanged(nameof(FunctionNames));
            }
        }

        public ObservableCollection<double> AvailableCValues { get; set; } = new ObservableCollection<double>();

        #region Values
        public string ARatioRaw
        {
            get => aRatio.ToString(CultureInfo.InvariantCulture);
            set
            {
                ARatio = DoubleNumericValidator.Parse(value);
                OnPropertyChanged(nameof(ARatioRaw));
            }
        }

        private double aRatio;
        public double ARatio
        {
            get { return aRatio; }
            set
            {
                aRatio = value;
                if (!isOldValuesSetting) CalculateAllResults();
                OnPropertyChanged(nameof(ARatio));
                OnPropertyChanged(nameof(ARatioRaw));
            }
        }

        public string BRatioRaw
        {
            get => bRatio.ToString(CultureInfo.InvariantCulture);
            set
            {
                BRatio = DoubleNumericValidator.Parse(value);
                OnPropertyChanged(nameof(BRatioRaw));
            }
        }

        private double bRatio;
        public double BRatio
        {
            get { return bRatio; }
            set
            {
                bRatio = value;
                if (!isOldValuesSetting) CalculateAllResults();
                OnPropertyChanged(nameof(BRatio));
                OnPropertyChanged(nameof(BRatioRaw));
            }
        }

        private double cRatio;
        public double CRatio
        {
            get { return cRatio; }
            set
            {
                cRatio = value;
                if (!isOldValuesSetting) CalculateAllResults();
                OnPropertyChanged(nameof(CRatio));
            }
        }

        public ObservableCollection<DataItemModel> dataItems = new ObservableCollection<DataItemModel>();
        public ObservableCollection<DataItemModel> DataItems
        {
            get { return dataItems; }
            set
            {
                dataItems = value;
                if (!isOldValuesSetting) CalculateAllResults();
                OnPropertyChanged(nameof(DataItems));
            }
        }
        #endregion

        private string selectedFunction;
        public string SelectedFunction
        {
            get { return selectedFunction; }
            set
            {
                isOldValuesSetting = true;

                if (selectedFunction != null) SaveValues();

                selectedFunction = value;

                CalculateCValues();

                LoadValues();

                CalculateAllResults();

                isOldValuesSetting = false;

                OnPropertyChanged(nameof(SelectedFunction));
            }
        }

        private bool isOldValuesSetting = false;

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
            var allValuesModel = functionValuesParisModel.GetAllValuesByFunctionName(SelectedFunction);
            int selectedFunctionIndex = FunctionNames.IndexOf(SelectedFunction);

            ARatio = allValuesModel.ARatio;
            BRatio = allValuesModel.BRatio;
            CRatio = allValuesModel.CRatio == 0 ? AvailableCValues[0] : allValuesModel.CRatio;

            DataItems.Clear();
            foreach (var dataItem in allValuesModel.DataItems)
            {
                var newDataItem = new DataItemModel
                {
                    X = dataItem.X,
                    Y = dataItem.Y,
                    Result = dataItem.Result
                };
                newDataItem.SetRecalculationHandler(CalculateResult);

                DataItems.Add(newDataItem);
            }
        }

        private void SaveValues()
        {
            var allValuesModel = new AllValuesModel
            {
                ARatio = ARatio,
                BRatio = BRatio,
                CRatio = CRatio,
                DataItems = new ObservableCollection<DataItemModel>(DataItems)
            };

            functionValuesParisModel.SetValuesByFunctionName(SelectedFunction, allValuesModel);
        }

        private void CalculateCValues()
        {
            AvailableCValues.Clear();

            int selectedFunctionIndex = FunctionNames.IndexOf(SelectedFunction);
            int multiplier = (int)Math.Pow(10, selectedFunctionIndex);

            for (int i = 1; i <= FunctionNames.Count; i++)
            {
                AvailableCValues.Add(i * multiplier);
            }
        }

        private void CalculateResult(DataItemModel dataItem)
        {
            var formula = functionValuesParisModel.GetFormulaByFunctionName(SelectedFunction);

            dataItem.Result = formula(ARatio, BRatio, CRatio, dataItem.X, dataItem.Y);
        }

        private void CalculateAllResults()
        {
            foreach (var dataItem in DataItems)
            {
                CalculateResult(dataItem);
            }
        }
    }
}
