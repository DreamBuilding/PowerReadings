namespace MeterData.Interfaces
{
    public interface ITouMeterDataModel: IMeterDataModel
    {
        decimal Energy { get; }
    }
}