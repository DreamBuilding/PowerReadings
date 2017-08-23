using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Converters;
using DataProcessor;
using FileParser;
using MeterData.Interfaces;
using MeterData.Interfaces.Enums;

namespace PowerReadings
{
    internal static class Program
    {
        static void Main(string[] args)
        {
            var path = args[0];

            //TODO: In a production application this would not be a console app and the handlers would inject through IOC
            var conversionHandlers = new Dictionary<ReportDataType, Func<string[], IMeterDataModel>>
            {
                {
                    TouReportConverter.DataType,
                    TouReportConverter.Convert
                },
                {
                    LpReportConverter.DataType,
                    LpReportConverter.Convert
                }
            };

            var dataProcessingHandlers = new Dictionary<ReportDataType, Func<IMeterDataModel, IDataModel>>
            {
                {
                    TouReportConverter.DataType,
                    TouReportConverter.Normalize
                },
                {
                    LpReportConverter.DataType,
                    LpReportConverter.Normalize
                }
            };

            foreach (var file in new DirectoryInfo(path).GetFiles("*.csv"))
            {
                try
                {
                    var fileType = file.Name.Split("_")[0].ToLowerInvariant(); //Extract type by convention type_numericdata_data and only use type

                    var reportType = (ReportDataType) Enum.Parse(typeof(ReportDataType), fileType, true); //parse against the available types

                    var parser = new StringArrayStreamParser(conversionHandlers[reportType]); //create a new reader using on report type to build the report data


                    using (var stream = file.OpenText())
                    {
                        //Read the line 1 headers. 
                        //TODO: This could be used to set the column mappings instead of the hard wired positions in the converters in future sprints.
                        var header = stream.ReadLine();

                        //Parse the file and return an IEnumerable of the Meter Data Model. Since this is yielded future optimizations and queue
                        //this data for map/reduce segemented processing when reading very large files.
                        var data = parser.Parse(stream);

                        //For simplicity the entire is read for this example
                        var readall = data.ToList();

                        //Since the units are not normilizable due to difference scope of units i.e. consumption units (kwh, kvarh), Phase, Voltage, Amperage etc
                        //group by the units and report on each set until a conversion is available to connect related data sets and allow such conversions to a normalized value.
                        var groupByUnits = readall.GroupBy(k => k.Units);

                        foreach (var group in groupByUnits)
                        {
                            //normalize between the two current report formats to date/value of IDataModel
                            var normalizer = dataProcessingHandlers[reportType];

                            //extract all the values from the group and enumerate
                            var values = group.Select(g => normalizer(g)).ToList();

                            //calcluate the median value
                            var median = StatisticalProcessor.CalculateMedian(values);

                            //use values, median and tolerance to return out of gamut values
                            var oog = StatisticalProcessor.OutOfGamutValues(values, median, 20);

                            //display each oog value to the console by filename & datatype
                            foreach (var item in oog)
                            {
                                Console.WriteLine("{0} {1} {2} {3} {4}", file.Name, group.Key, item.DateTime, item.Value, median);
                            }

                            Console.WriteLine();
                        }

                        stream.Close();

                        Console.WriteLine();
                    }
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("File {0} has unrecognised format", file.Name);
                    Console.WriteLine();
                }
            }

            Console.WriteLine("Hit any key to end");
            Console.ReadLine();
        }
    }
}