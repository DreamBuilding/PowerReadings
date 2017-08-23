using System;
using System.Collections.Generic;
using System.Linq;

namespace DataProcessor
{
    public static class LinqExtensions
    {
        //Median is the mid point in the sorted array of elements or the average of the ajouning elements if the array contains an even number of elements
        public static decimal Median(this ICollection<decimal> source)
        {
            var sorted = source.OrderBy(n => n);
            var decimals = sorted.Count();

            if (decimals == 0) throw new InvalidOperationException("Sequence contains no elements");

            var midpoint = (decimals - 1) / 2;
                
            var median = sorted.ElementAt(midpoint);

            if (decimals % 2 == 0)
            {
                median = (median + sorted.ElementAt(midpoint + 1)) / 2;
            }

            return median;
        }
    }
}