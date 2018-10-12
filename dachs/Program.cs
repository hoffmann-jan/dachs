using System;
using dachs.Interfaces;

namespace dachs
{
    /// <summary>
    /// dachs  Programm
    /// </summary>
    class Program
    {
        /// <summary>
        /// Haupteinstiegspunkt.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            IExtractor extractor = new Extractor();

            var er = extractor.Extract("baalsdorfer anger");

            foreach (var r in er)
            {
                Console.WriteLine(r);
            }

            if (System.Environment.OSVersion.Platform != PlatformID.Unix)
            {
                Console.Write("\n\npress any key to quit..\n");
                Console.ReadKey();
            }

        }

    }
}
