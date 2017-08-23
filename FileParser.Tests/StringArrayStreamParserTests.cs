using System;
using System.IO;
using System.Linq;
using Converters;
using MeterData.Interfaces;
using MeterData.Interfaces.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FileParser.Tests
{
    [TestClass]
    public class StringArrayStreamParserTests
    {
        [TestMethod]
        public void Does_TouMeterData_Return_Energy()
        {   
            const decimal expected = 409646.700000m;

            var reader = CreateAndPopulateStream();
            
            var actual = (ITouMeterDataModel) new StringArrayStreamParser(TouReportConverter.Convert).Parse(reader).First();

            Assert.AreEqual(expected, actual.Energy);
        }

        [TestMethod]
        public void Does_TouMeterData_Return_DateTime()
        {
            DateTime expected = DateTime.Parse("11/09/2015 12:41:02 am");

            var reader = CreateAndPopulateStream();

            var actual = (ITouMeterDataModel)new StringArrayStreamParser(TouReportConverter.Convert).Parse(reader).First();

            Assert.AreEqual(expected, actual.DateTime);
        }

        [TestMethod]
        public void Does_TouMeterData_Return_Units()
        {
            const Units expected = Units.Kwh;

            var reader = CreateAndPopulateStream();

            var actual = (ITouMeterDataModel)new StringArrayStreamParser(TouReportConverter.Convert).Parse(reader).First();

            Assert.AreEqual(expected, actual.Units);
        }

        private static StreamReader CreateAndPopulateStream()
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);

            const string text = @"212621145,212621145,ED011300245,11/09/2015 00:41:02,Export Wh Total,409646.700000,1275.368000,30/12/1899 00:00:00,kwh,.....R....,Total,False,26,01/09/2015 00:00:00,Rate 1";

            writer.WriteLine(text);
            writer.Flush();

            stream.Seek(0, SeekOrigin.Begin);
            return new StreamReader(stream);
        }
    }
}
