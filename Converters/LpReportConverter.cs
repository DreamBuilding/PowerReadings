using System;
using MeterData.Interfaces;
using MeterData.Interfaces.Enums;
using MeterData.Models;

namespace Converters
{
    public static class LpReportConverter
    {
        public static ReportDataType DataType => ReportDataType.Lp;

        //Convert the CSV Columns to fields
        public static Func<string[], ILpMeterDataModel> Convert = s =>
            new LpMeterDataModel
            {
                MeterPointCode = int.Parse(s[0]),
                SerialNumber = int.Parse(s[1]),
                PlantCode = s[2],
                DateTime = DateTime.Parse(s[3]),
                DataType = EnumExtension.GetValueFromCompactedName<DataTypes>(s[4]),
                DataValue = decimal.Parse(s[5]),
                Units = EnumExtension.GetValueFromCompactedName<Units>(s[6]),
                Status = s[7]
            };

        public static Func<IMeterDataModel, IDataModel> Normalize = m => new DataModel
        {
            DateTime = m.DateTime,
            Value = ((LpMeterDataModel)m).DataValue
        };
    }
}