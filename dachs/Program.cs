using System;
using dachsXll;
using dachsXll.Interfaces;

namespace dachs
{    
    /// <summary>
    /// dachs  Programm
    /// </summary>
    class Program
    {
        private static ulong _TotalNumbers;
        public static ulong TotalNumbers
        {
            get
            {
                return _TotalNumbers;
            }
            set
            {
                _TotalNumbers = value;
            }
        }

        /// <summary>
        /// Haupteinstiegspunkt.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            IExtractor extractor = new Extractor();

            string testInput = "Talstraße";

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

            //Console.WriteLine();

            //Console.WriteLine("Exporting excel file.");
            //IFileGenerator generator = new ExcelGenerator(Directory.GetCurrentDirectory(), testInput);
            //generator.Generate(er);
            //Console.WriteLine("done!");

            //Console.WriteLine("Exporting csv file.");
            //IFileGenerator csvGenerator = new CsvGenerator(Directory.GetCurrentDirectory(), testInput);
            //csvGenerator.Generate(er);
            //Console.WriteLine("done!");

            //Console.WriteLine("Get all available streets of le:");
            //IEnumerable<string> streets = extractor.ExtractAllStreets();
            //int count = 0;

            //foreach(string street in streets)
            //{
            //    Console.WriteLine(street);
            //    count++;
            //}
            //Console.WriteLine($"count:{count}");
            //Console.WriteLine("done!");


            //Console.WriteLine("Get AAAAALLLLLLLLL!");
            //Console.WriteLine("Stopwatch starting.");
            //Stopwatch stopwatch = Stopwatch.StartNew();
            //Dictionary<string, IEnumerable<string>> streetsOfLe = new Dictionary<string, IEnumerable<string>>();



            //int zCount = 0;
            //foreach (string name in streets)
            //{
            //    var extract = extractor.Extract(name);
            //    streetsOfLe.Add(name, extract);

            //    foreach (var v in extract)
            //        TotalNumbers++;

            //    //if (zCount ) fortschrittsbalken einfügen

            //    zCount++;
            //}

            //stopwatch.Stop();
            //Console.WriteLine($"done in {stopwatch.Elapsed.ToString("h'h 'm'm 's's'")}!");
            //Console.WriteLine($"total streets {count}.");
            //Console.WriteLine($"total numbers: {TotalNumbers}.");


            if (Environment.OSVersion.Platform != PlatformID.Unix)
            {
                Console.Write("\n\npress any key to quit..\n");
                Console.ReadKey();
            }

        }

    }
}
