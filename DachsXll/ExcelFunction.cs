using System;
using System.Collections.Generic;
using System.Text;

using ExcelDna.Integration;

/// <summary>
/// dachs as *.xll file for MS Excel.
/// </summary>
namespace dachsXll
{
    /// <summary>
    /// Available dachs functions in MS Excel.
    /// </summary>
    public static class ExcelFunction
    {
        /// <summary>
        /// Function to recieve live all offical housenumber of a street of le.
        /// </summary>
        /// <param name="streetName">Name of the street.</param>
        /// <returns>available Numbers</returns>
        [ExcelFunction(Description = "Fragt alle offiziellen Hausnummern zu der markierten Leipziger Straße vom offiziellen Server[addressen.leipzig.de] ab.", Name = "Hausnummern Leipzig")]
        public static string SayHello(string streetName)
        {
            return "Hello " + streetName;
        }
    }
}
