using System;
using MeterData.Interfaces;
using MeterData.Interfaces.Enums;

namespace MeterData.Models
{
    public class TouMeterDataModel : MeterDataModelBase, ITouMeterDataModel
    {
        public decimal Energy { get; set; }

        public decimal MaximiumDemand { get; set; }

        public DateTime TimeOfMaximiumDemand { get; set; }

        public BillingPeriods Period { get; set; }

        public bool DlsActive { get; set; }

        public int BillingReset { get; set; }

        public DateTime BillingReseDateTime { get; set; }

        public string Rate { get; set; }

        //Implicitly convert to normalized data type
        public static implicit operator DataModel(TouMeterDataModel d)
        {
            return new DataModel
            {
                DateTime = d.DateTime,
                Value = d.Energy
            };
        }

        public override ReportDataType ReportDataType => ReportDataType.Tou;
    }
}