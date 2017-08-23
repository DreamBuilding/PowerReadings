using System;
using MeterData.Interfaces;

namespace MeterData.Models
{
    public class DataModel : IDataModel
    {
        public DateTime DateTime { get; set; }
        public decimal Value { get; set; }
    }
}