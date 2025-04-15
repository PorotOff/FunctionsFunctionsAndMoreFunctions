using System.Collections.ObjectModel;

namespace FunctionsFunctionsAndMoreFunctions.Resources.Scripts.Models
{
    public class AllValuesModel
    {
        public double ARatio { get; set; } = 0;
        public double BRatio { get; set; } = 0;
        public double CRatio { get; set; } = 0;

        public ObservableCollection<DataItemModel> DataItems { get; set; } = new ObservableCollection<DataItemModel>
        {
            new DataItemModel()
        };
    }
}
