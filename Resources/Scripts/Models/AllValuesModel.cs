using System.Collections.ObjectModel;

namespace FunctionsFunctionsAndMoreFunctions.Resources.Scripts.Models
{
    internal class AllValuesModel
    {
        public double ARatio { get; set; } = 0;
        public double BRatio { get; set; } = 0;
        public int CRatioIndex { get; set; } = 0;

        public double X { get; set; } = 0;
        public double Y { get; set; } = 0;
        public double Result { get; set; } = 0;

        public ObservableCollection<DataItemModel> DataItems { get; set; } = new ObservableCollection<DataItemModel>();
    }
}
