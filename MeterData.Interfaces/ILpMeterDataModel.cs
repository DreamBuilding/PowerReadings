namespace MeterData.Interfaces
{
    public interface ILpMeterDataModel: IMeterDataModel
    {
        decimal DataValue { get; }
    }
}