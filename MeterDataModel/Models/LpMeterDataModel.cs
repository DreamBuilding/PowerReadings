using MeterData.Interfaces;
using MeterData.Interfaces.Enums;

namespace MeterData.Models
{
    public class LpMeterDataModel : MeterDataModelBase, ILpMeterDataModel
    {
        public decimal DataValue { get; set; }

        public override ReportDataType ReportDataType => ReportDataType.Lp;

        //Implicitly convert to normalized data type
        public static implicit operator DataModel(LpMeterDataModel d)
        {
            return new DataModel
            {
                DateTime = d.DateTime,
                Value = d.DataValue
            };
        }
    }
}