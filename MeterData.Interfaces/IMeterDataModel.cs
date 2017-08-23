using System;
using MeterData.Interfaces.Enums;

namespace MeterData.Interfaces
{
    public interface IMeterDataModel
    {
        DataTypes DataType { get; }
        DateTime DateTime { get;  }
        Units Units { get;  }

        ReportDataType ReportDataType { get; }
    }
}