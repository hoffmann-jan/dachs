using System;
using System.Text;
using dachsXll.Interfaces;
using ExcelDna.Integration;

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
        [ExcelFunction(
            Description = "Fragt alle offiziellen Hausnummern zu der markierten Leipziger Straße vom offiziellen Server[addressen.leipzig.de] ab.", 
            IsHidden = false,
            IsMacroType = true,
            Category = "Leipzig",
            ExplicitRegistration = false)]
        public static string Hausnummern(string streetName)
        {
            try
            {
                IExtractor extractor = new Extractor();                
                var result = extractor.Extract(streetName);
                StringBuilder stringBuilder = new StringBuilder();

                foreach (var r in result)
                {
                    if (stringBuilder.Length == 0)
                    {
                        stringBuilder.Append(r);
                    }
                    else
                        stringBuilder.Append(string.Concat(",", r));
                }
                return stringBuilder.ToString();
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
        }

        /// <summary>
        /// Count housenumbers of a street in Leipzig.
        /// </summary>
        /// <param name="streetName">Name of a street in Leipzig.</param>
        /// <returns>Count of house numbers.</returns>
        [ExcelFunction(
            Description = "Fragt alle offiziellen Hausnummern zu der markierten Leipziger Straße vom offiziellen Server[addressen.leipzig.de] ab und Summiert die Anzahl.",
            IsHidden = false,
            IsMacroType = true,
            Category = "Leipzig",
            ExplicitRegistration = false)]
        public static int AnzahlHausnummern(string streetName)
        {
            try
            {
                int counter = 0;
                IExtractor extractor = new Extractor();
                var result = extractor.Extract(streetName);

                foreach (var r in result)
                {
                    counter++;
                }
                return counter;
            }
            catch
            {
                return -1;
            }

        }
    }
}
