using System;
using MeterData.Interfaces;
using MeterData.Interfaces.Enums;

namespace MeterData.Models
{
    public abstract class MeterDataModelBase : IMeterDataModel
    {
        public int MeterPointCode { get; set; }
        public int SerialNumber { get; set; }
        public string PlantCode { get; set; }
        public DateTime DateTime { get; set; }
        public DataTypes DataType { get; set; }
        public Units Units { get; set; }
        public string Status { get; set; }

        public abstract ReportDataType ReportDataType { get; }
    }
}