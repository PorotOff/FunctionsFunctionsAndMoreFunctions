using System.Collections.ObjectModel;

namespace FunctionsFunctionsAndMoreFunctions.Resources.Scripts.Models
{
    /// <summary>
    /// Модель, содержащая значения коэффициентов и коллекцию элементов данных.
    /// </summary>
    public class AllValuesModel
    {
        /// <summary>
        /// Автосвойство, хранящее коэффициент A.
        /// При создании нового объекта оно будет равно 0.
        /// </summary>
        public double ARatio { get; set; } = 0;

        /// <summary>
        /// Автосвойство, хранящее коэффициент B.
        /// При создании нового объекта оно будет равно 0.
        /// </summary>
        public double BRatio { get; set; } = 0;

        /// <summary>
        /// Автосвойство, хранящее коэффициент C.
        /// При создании нового объекта оно будет равно 0.
        /// </summary>
        public double CRatio { get; set; } = 0;

        /// <summary>
        /// Автосвойство-коллекция для хранения строк из таблицы данных.
        /// При создании нового объекта оно будет иметь один стандартный элемент.
        /// </summary>
        public ObservableCollection<DataItemModel> DataItems { get; set; } = new ObservableCollection<DataItemModel>
        {
            new DataItemModel()
        };
    }
}
