using System;
using MeterData.Interfaces;
using MeterData.Interfaces.Enums;
using MeterData.Models;

namespace Converters
{
    public static class TouReportConverter
    {
        public static ReportDataType DataType => ReportDataType.Tou;

        //Convert the CSV Columns to fields
        public static Func<string[], ITouMeterDataModel> Convert = s => new TouMeterDataModel
        {
            MeterPointCode = int.Parse(s[0]),
            SerialNumber = int.Parse(s[1]),
            PlantCode = s[2],
            DateTime = DateTime.Parse(s[3]),
            DataType = EnumExtension.GetValueFromCompactedName<DataTypes>(s[4]),
            Energy = decimal.Parse(s[5]),
            MaximiumDemand = decimal.Parse(s[6]),
            TimeOfMaximiumDemand = DateTime.Parse(s[7]),
            Units = EnumExtension.GetValueFromCompactedName<Units>(s[8]),
            Status = s[9],
            Period = EnumExtension.GetValueFromCompactedName<BillingPeriods>(s[10]),
            DlsActive = bool.Parse(s[11]),
            BillingReset = int.Parse(s[12]),
            BillingReseDateTime = DateTime.Parse(s[13]),
            Rate = s[14]
        };

        public static Func<IMeterDataModel, IDataModel> Normalize = m => new DataModel
        {
            DateTime = m.DateTime,
            Value = ((ITouMeterDataModel)m).Energy
        };
    }
}