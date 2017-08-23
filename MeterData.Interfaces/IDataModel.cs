using System;

namespace MeterData.Interfaces
{
    public interface IDataModel
    {
        DateTime DateTime { get; }

        decimal Value { get; }
    }
}