using System;
using dachs.Interfaces;

namespace dachs
{
    /// <summary>
    /// dachs  Programm
    /// </summary>
    class Program
    {
        #region Fields
        /// <summary>
        /// URL zur Webseite.
        /// </summary>
        const string _URL = "https://adressen.leipzig.de/";
        #endregion

        /// <summary>
        /// Haupteinstiegspunkt.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            IExtractor extractor = new Extractor();

            var er = extractor.Extract("baalsdorfer anger");

            foreach(var r in er )
            {
                Console.WriteLine(r);
            }

            Console.Write("\n\npress any key to quit..\n");
            Console.ReadKey();
        }

    }
}
