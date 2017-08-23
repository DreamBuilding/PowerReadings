using System;
using System.Linq;
using MeterData.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataProcessor.Tests
{
    [TestClass]
    public class StatisticalProcessorTests
    {
        [TestMethod]
        public void Does_Median_Return_CorrectPointOdd()
        {
            var expected = 10.5m;

            var values = new[]
            {
                new DataModel{DateTime = DateTime.Parse("2007-12-10 12:30:00am"), Value = 10},
                new DataModel{DateTime = DateTime.Parse("2007-12-10 12:30:00am"), Value = 12},
                new DataModel{DateTime = DateTime.Parse("2007-12-10 12:30:00am"), Value = 1},
                new DataModel{DateTime = DateTime.Parse("2007-12-10 12:30:00am"), Value = 100},
                new DataModel{DateTime = DateTime.Parse("2007-12-10 12:30:00am"), Value = 10.5m},
                new DataModel{DateTime = DateTime.Parse("2007-12-10 12:30:00am"), Value = 10.25m},
                new DataModel{DateTime = DateTime.Parse("2007-12-10 12:30:00am"), Value = 100}
            };

            var actual = StatisticalProcessor.CalculateMedian(values);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Does_Median_Return_CorrectPointEven()
        {
            var expected = 10.375m;

            var values = new[]
            {
                new DataModel{DateTime = DateTime.Parse("2007-12-10 12:30:00am"), Value = 10},
                new DataModel{DateTime = DateTime.Parse("2007-12-10 12:30:00am"), Value = 1},
                new DataModel{DateTime = DateTime.Parse("2007-12-10 12:30:00am"), Value = 100},
                new DataModel{DateTime = DateTime.Parse("2007-12-10 12:30:00am"), Value = 10.5m},
                new DataModel{DateTime = DateTime.Parse("2007-12-10 12:30:00am"), Value = 10.25m},
                new DataModel{DateTime = DateTime.Parse("2007-12-10 12:30:00am"), Value = 100}
            };

            var actual = StatisticalProcessor.CalculateMedian(values);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Does_OutOfGamut_Return_CorrectValues()
        {
            var expected = new[]
            {
                new DataModel{DateTime = DateTime.Parse("2007-12-10 12:30:00am"), Value = 1},
                new DataModel{DateTime = DateTime.Parse("2007-12-11 12:30:00am"), Value = 100},
                new DataModel {DateTime = DateTime.Parse("2007-12-14 12:30:00am"), Value = 100}
            };

            var values = new[]
            {
                new DataModel{DateTime = DateTime.Parse("2007-12-08 12:30:00am"), Value = 10},
                new DataModel{DateTime = DateTime.Parse("2007-12-09 12:30:00am"), Value = 12},
                new DataModel{DateTime = DateTime.Parse("2007-12-10 12:30:00am"), Value = 1},
                new DataModel{DateTime = DateTime.Parse("2007-12-11 12:30:00am"), Value = 100},
                new DataModel{DateTime = DateTime.Parse("2007-12-12 12:30:00am"), Value = 10.5m},
                new DataModel{DateTime = DateTime.Parse("2007-12-13 12:30:00am"), Value = 10.25m},
                new DataModel{DateTime = DateTime.Parse("2007-12-14 12:30:00am"), Value = 100}
            };

            var median = StatisticalProcessor.CalculateMedian(values);

            var actual = StatisticalProcessor.OutOfGamutValues(values, median, 50).ToList();

            Assert.IsTrue(expected.All(a=>actual.Select(v=>v.DateTime).Contains(a.DateTime)));
        }
    }
}
