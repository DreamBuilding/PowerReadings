using System;
using System.Collections.Generic;
using System.Linq;
using MeterData.Interfaces;

namespace DataProcessor
{
    /// <summary>
    /// Process the energy report types to extract values based of the type of report
    /// </summary>
    public static class StatisticalProcessor
    {
        public static readonly Func<ICollection<IDataModel>, decimal> CalculateMedian = data =>
        {
            return data.Select(d => d.Value).ToList().Median();
        };

        public static readonly Func<IEnumerable<IDataModel>, decimal, decimal, IEnumerable<IDataModel>> OutOfGamutValues =
            (data, median, tolerance) =>
            {
                var items = data.ToList();
                var allowance = tolerance / 100 * median;

                var oog = items.Where(i => Math.Abs(i.Value - median) > allowance);

                return oog;
            };
    }
}
