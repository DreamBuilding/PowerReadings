using System;
using System.Collections.Generic;
using System.IO;
using MeterData.Interfaces;

namespace FileParser
{
    /// <summary>
    /// Process the csv file and return the appropriate report data
    /// </summary>
    public class StringArrayStreamParser
    {
        private readonly Func<string[], IMeterDataModel> _handler;

        public StringArrayStreamParser(Func<string[], IMeterDataModel> handler)
        {
            _handler = handler;
        }

        public IEnumerable<IMeterDataModel> Parse(StreamReader stream)
        {
            while (!stream.EndOfStream)
            {
                //Read the line from the stream and convert to string array delimited by commas
                var line = stream.ReadLine().Split(",");

                var dataModel = _handler(line);

                //return the current processed line
                yield return dataModel;
            }
        }
    }
}
