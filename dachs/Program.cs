using System;
using System.IO;

using dachs.Generators;
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

            string testInput = "baalsdorfer anger";

            var er = extractor.Extract(testInput);
            bool first = true;

            Console.WriteLine($"All available house numbers for {testInput}:");
            foreach (var r in er)
            {
                if (first)
                {
                    Console.Write(r);
                    first = false;
                }
                else
                    Console.Write(string.Concat(",",r));
            }

            Console.WriteLine();

            Console.WriteLine("Exporting excel file.");
            IFileGenerator generator = new ExcelGenerator(Directory.GetCurrentDirectory(), testInput);
            generator.Generate(er);
            Console.WriteLine("done!");

            Console.WriteLine("Exporting csv file.");
            IFileGenerator csvGenerator = new CsvGenerator(Directory.GetCurrentDirectory(), testInput);
            csvGenerator.Generate(er);
            Console.WriteLine("done!");


            if (Environment.OSVersion.Platform != PlatformID.Unix)
            {
                Console.Write("\n\npress any key to quit..\n");
                Console.ReadKey();
            }

        }

    }
}
