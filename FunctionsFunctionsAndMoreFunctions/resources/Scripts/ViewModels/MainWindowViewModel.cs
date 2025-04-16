using CommunityToolkit.Mvvm.ComponentModel;
using FunctionsFunctionsAndMoreFunctions.Resources.Scripts.Models;
using FunctionsFunctionsAndMoreFunctions.Resources.Scripts.Validation;
using System;
using System.Collections.ObjectModel;
using System.Globalization;

namespace FunctionsFunctionsAndMoreFunctions.Resources.Scripts.ViewModels
{
    /// <summary>
    /// Представляет ViewModel для основного окна приложения.
    /// Отвечает за связывание данных с представлением и обработку логики пересчёта.
    /// </summary>
    public class MainWindowViewModel : ObservableObject
    {
        private FunctionValuesParisModel functionValuesParisModel = new FunctionValuesParisModel();

        private ObservableCollection<string> functionNames = new ObservableCollection<string>();

        /// <summary>
        /// Автосвтойство-коллекция для хранения названий функций
        /// Реагирует на изменения для привязки данных.
        /// </summary>
        public ObservableCollection<string> FunctionNames
        {
            get { return functionNames; }
            set
            {
                OnPropertyChanged(nameof(FunctionNames));
            }
        }

        /// <summary>
        /// Автосвтойство-коллекция для хранения соответствующих названию функции значений коэффициента C.
        /// Реагирует на изменения для привязки данных.
        /// </summary>
        public ObservableCollection<double> AvailableCValues { get; set; } = new ObservableCollection<double>();

        #region Values

        /// <summary>
        /// Автосвойство, хранящее коэффициент A в строковом представлении.
        /// Используется для ввода пользователем и реагирует на изменения для привязки данных.
        /// </summary>
        public string ARatioRaw
        {
            get => aRatio.ToString(CultureInfo.InvariantCulture);
            set
            {
                ARatio = DoubleNumbersValidation.TryParseDoubleNumbers(value);
                OnPropertyChanged(nameof(ARatioRaw));
            }
        }

        private double aRatio;

        /// <summary>
        /// Автосвойство, хранящее коэффициент A.
        /// Используется в вычислениях и еагирует на изменения для привязки данных.
        /// </summary>
        public double ARatio
        {
            get { return aRatio; }
            set
            {
                aRatio = value;
                if (!isValuesSetting) CalculateAllResults();
                OnPropertyChanged(nameof(ARatio));
                OnPropertyChanged(nameof(ARatioRaw));
            }
        }

        /// <summary>
        /// Автосвойство, хранящее коэффициент B в строковом представлении.
        /// Используется для ввода пользователем и реагирует на изменения для привязки данных.
        /// </summary>
        public string BRatioRaw
        {
            get => bRatio.ToString(CultureInfo.InvariantCulture);
            set
            {
                BRatio = DoubleNumbersValidation.TryParseDoubleNumbers(value);
                OnPropertyChanged(nameof(BRatioRaw));
            }
        }

        private double bRatio;

        /// <summary>
        /// Автосвойство, хранящее коэффициент B.
        /// Используется в вычислениях и еагирует на изменения для привязки данных.
        /// </summary>
        public double BRatio
        {
            get { return bRatio; }
            set
            {
                bRatio = value;
                if (!isValuesSetting) CalculateAllResults();
                OnPropertyChanged(nameof(BRatio));
                OnPropertyChanged(nameof(BRatioRaw));
            }
        }

        private double cRatio;

        /// <summary>
        /// Автосвойство, хранящее коэффициент C.
        /// Используется в вычислениях, для ввода пользователем и еагирует на изменения для привязки данных.
        /// </summary>
        public double CRatio
        {
            get { return cRatio; }
            set
            {
                cRatio = value;
                if (!isValuesSetting) CalculateAllResults();
                OnPropertyChanged(nameof(CRatio));
            }
        }

        /// <summary>
        /// Автосвойство, хранящее коллекцию экземпляров DataItemModel.
        /// Используется для заполнения данных в представлении и еагирует на изменения для привязки данных.
        /// </summary>
        public ObservableCollection<DataItemModel> DataItems
        {
            get { return dataItems; }
            set
            {
                dataItems = value;
                if (!isValuesSetting) CalculateAllResults();
                OnPropertyChanged(nameof(DataItems));
            }
        }
        private ObservableCollection<DataItemModel> dataItems = new ObservableCollection<DataItemModel>();

        #endregion

        private string selectedFunction;

        /// <summary>
        /// Автосвойство, хранящее выбранную функцию.
        /// При изменении производится загрузка и пересчет значений.
        /// </summary>
        public string SelectedFunction
        {
            get { return selectedFunction; }
            set
            {
                isValuesSetting = true;

                if (selectedFunction != null) SaveValues();

                selectedFunction = value;

                CalculateCValues();

                LoadValues();

                CalculateAllResults();

                isValuesSetting = false;

                OnPropertyChanged(nameof(SelectedFunction));
            }
        }

        private bool isValuesSetting = false;

        /// <summary>
        /// Инициализирует новый экземпляр класса MainWindowViewModel.
        /// Заполняет коллекцию функций и устанавливает выбранную функцию.
        /// </summary>
        public MainWindowViewModel()
        {
            FillFunctionsCollection();

            SelectedFunction = FunctionNames[0];
        }

        /// <summary>
        /// Заполняет коллекцию FunctionNames, получая названия функций из модели с функциями.
        /// </summary>
        private void FillFunctionsCollection()
        {
            foreach (var functionName in functionValuesParisModel.GetFunctionNames())
            {
                FunctionNames.Add(functionName);
            }
        }

        /// <summary>
        /// Загружает значения для выбранной функции и обновляет DataItems.
        /// </summary>
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

        /// <summary>
        /// Сохраняет текущие значения для выбранной функции в модель.
        /// </summary>
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

        /// <summary>
        /// Вычисляет и заполняет коллекцию доступных значений коэффициента c.
        /// </summary>
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

        /// <summary>
        /// Пересчитывает значение для переданного объекта DataItemModel.
        /// </summary>
        /// <param name="dataItem">Экземпляр DataItemModel для пересчёта.</param>
        private void CalculateResult(DataItemModel dataItem)
        {
            var formula = functionValuesParisModel.GetFormulaByFunctionName(SelectedFunction);

            dataItem.Result = formula(ARatio, BRatio, CRatio, dataItem.X, dataItem.Y);
        }

        /// <summary>
        /// Пересчитывает значения для всех объектов в коллекции DataItems.
        /// </summary>
        private void CalculateAllResults()
        {
            foreach (var dataItem in DataItems)
            {
                CalculateResult(dataItem);
            }
        }
    }
}