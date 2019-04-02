using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

using dachs.Generators;
using dachs.Interfaces;

namespace dachs
{
    /// <summary>
    /// Dachs-Console-Application
    /// </summary>
    class Program
    {
        #region private properties
        private static int _TotalNumbers;
        #endregion
        #region public properties
        public static int TotalStreets
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
        #endregion

        /// <summary>
        /// Haupteinstiegspunkt.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Global.Language = Global.Languages.English;
            //Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            if (args.Length == 0)
            {
                while (true)
                {
                    if (Menu())
                        break;
                }
            }
            else
            {
                PrintNumbers(args[0]);
            }
        }

        static bool Menu()
        {
            if (Global.Language == Global.Languages.English)
            {
                Console.Clear();
                Console.WriteLine("..:: dynamic address components help system ::..");
                Console.WriteLine("################################################");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("MENU - press a key to navigate");
                Console.WriteLine("==============================");
                Console.WriteLine("1 - show all numbers of a street");
                Console.WriteLine();
                Console.WriteLine("--  CSV EXPORT  --");
                Console.WriteLine("------------------");
                Console.WriteLine("3 - export all numbers of a street");
                Console.WriteLine("4 - export all numbers of all streets");
                Console.WriteLine();
                Console.WriteLine("--  EXCEL FILE EXPORT (Windows only)  --");
                Console.WriteLine("----------------------------------------");
                Console.WriteLine("6 - export all numbers of a street");
                Console.WriteLine("7 - export all numbers of all streets");
                Console.WriteLine();
                Console.WriteLine("--  LANGUAGE  --");
                Console.WriteLine("--------------------------");
                Console.WriteLine("g - German");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("q - quit");
            }
            else if (Global.Language == Global.Languages.German)
            {
                Console.Clear();
                Console.WriteLine("..:: dynamic address components help system ::..");
                Console.WriteLine("################################################");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("MENU - drücke eine Taste zum Navigieren");
                Console.WriteLine("=======================================");
                Console.WriteLine("1 - zeige alle Nummern einer Straße");
                Console.WriteLine();
                Console.WriteLine("--  CSV EXPORT  --");
                Console.WriteLine("------------------");
                Console.WriteLine("3 - exportiere alle Nummern einer Straße");
                Console.WriteLine("4 - exportiere alle Nummern aller Straßen");
                Console.WriteLine();
                Console.WriteLine("--  EXCEL FILE EXPORT (Nur Windows)  --");
                Console.WriteLine("---------------------------------------");
                Console.WriteLine("6 - exportiere alle Nummern einer Straße");
                Console.WriteLine("7 - exportiere alle Nummern aller Straßen");
                Console.WriteLine();
                Console.WriteLine("--  Sprache  --");
                Console.WriteLine("--------------------------");
                Console.WriteLine("e - English");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("q - Beenden");
            }

            ConsoleKeyInfo keyInfo = Console.ReadKey();
            switch (keyInfo.Key)
            {
                case ConsoleKey.Q:
                case ConsoleKey.Escape:
                    return true;
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:
                    GetAllNumbersOfAStreet();
                    break;
                case ConsoleKey.D3:
                case ConsoleKey.NumPad3:
                    GetAllNumbersOfAStreet(false, true);
                    break;
                case ConsoleKey.D4:
                case ConsoleKey.NumPad4:
                    GetAll(false);
                    break;
                case ConsoleKey.D6:
                case ConsoleKey.NumPad6:
                    GetAllNumbersOfAStreet(true, false);
                    break;
                case ConsoleKey.D7:
                case ConsoleKey.NumPad7:
                    GetAll(true);
                    break;
                case ConsoleKey.G:
                    Global.Language = Global.Languages.German;
                    break;
                case ConsoleKey.E:
                    Global.Language = Global.Languages.English;
                    break;
                default:
                    break;
            }
            return false;
        }

        static bool GetAllNumbersOfAStreet(bool toExcel = false, bool toCSV = false)
        {
            if (Global.Language == Global.Languages.English)
            {
                Console.Clear();
                Console.WriteLine("..:: dynamic address components help system ::..");
                Console.WriteLine("################################################");
                Console.WriteLine();
                Console.WriteLine("show all numbers of a street");
                Console.WriteLine("----------------------------");
                Console.WriteLine("Enter the streetname or enter nothing to go back, please.");
                Console.WriteLine();
            }
            else if (Global.Language == Global.Languages.German)
            {
                Console.Clear();
                Console.WriteLine("..:: dynamic address components help system ::..");
                Console.WriteLine("################################################");
                Console.WriteLine();
                Console.WriteLine("Zeige alle Nummern einer Straße");
                Console.WriteLine("-------------------------------");
                Console.WriteLine("Gebe den Straßennamen ein oder drücke gleich Enter um zurück zu gehen.");
                Console.WriteLine();
            }
            string street = Console.ReadLine();
            if (street == string.Empty)
                return false;
            if (!toExcel && !toCSV)
                PrintNumbers(street);

            Dictionary<string, string> result = new Dictionary<string, string>
            {
                { street, GetNumbers(street) }
            };

            string fileExtension = string.Empty;

            if (toExcel)
            {
                Console.WriteLine();
                Console.Write("Excel-Export");
                fileExtension = ".xlsx";

            }

            if (toCSV)
            {
                ToCSV(result);
                Console.WriteLine();
                Console.Write("CSV-Export");
                fileExtension = ".csv";
            }

            if (toCSV || toExcel)
            {
                if (Global.Language == Global.Languages.English)
                {
                    Console.Write($", done.{Environment.NewLine}");
                    Console.WriteLine($"Path: {Environment.CurrentDirectory}{Path.VolumeSeparatorChar}{street}{fileExtension}");
                    Console.WriteLine("press any key to continue ..");
                    Console.ReadKey();
                }
                else if (Global.Language == Global.Languages.German)
                {
                    Console.Write($", abgeschlossen.{Environment.NewLine}");
                    Console.WriteLine($"Pfad: {Environment.CurrentDirectory}{Path.VolumeSeparatorChar}{street}{fileExtension}");
                    Console.WriteLine("Drücke eine Taste zum Fortfahren ..");
                    Console.ReadKey();
                }
            }

            return true;
        }

        static bool GetAll(bool toExcel)
        {
            if (Global.Language == Global.Languages.English)
            {
                Console.Clear();
                Console.WriteLine("..:: dynamic address components help system ::..");
                Console.WriteLine("################################################");
                Console.WriteLine("");
                Console.WriteLine("export all numbers of all streets");
                Console.WriteLine("---------------------------------");
            }
            else if (Global.Language == Global.Languages.German)
            {
                Console.Clear();
                Console.WriteLine("..:: dynamic address components help system ::..");
                Console.WriteLine("################################################");
                Console.WriteLine("");
                Console.WriteLine("exportiere alle Nummern aller Straßen");
                Console.WriteLine("-------------------------------------");
            }

            Dictionary<string, string> result = new Dictionary<string, string>();
            string fileExtension = string.Empty;

            IExtractor extractor = new Extractor();

            IEnumerable<string> streets = extractor.ExtractAllStreets();
            TotalStreets = 0;

            foreach (string street in streets)
            {
                TotalStreets++;
            }

            if (Global.Language == Global.Languages.English)
            {
                Console.WriteLine($"number of known streets: {TotalStreets}");
                Console.WriteLine("processing.. please wait(~10-15 min)");
            }
            else if (Global.Language == Global.Languages.German)
            {
                Console.WriteLine($"Anzahl aller bekannten Straßen: {TotalStreets}");
                Console.WriteLine("Bearbeite.. Bitte warten(~10-15 Minuten)");
            }

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Reset();
            stopwatch.Start();

            int count = 0;
            foreach (string name in streets)
            {
                var extract = extractor.Extract(name);
                StringBuilder numbers = new StringBuilder();

                foreach (var r in extract)
                {
                    count++;
                    if (numbers.Length == 0)
                    {
                        numbers.Append(r);
                    }
                    else
                        numbers.Append(string.Concat(",", r));
                }

                result.Add(name, numbers.ToString());
            }

            stopwatch.Stop();

            if (Global.Language == Global.Languages.English)
            {
                Console.WriteLine($"done in {stopwatch.Elapsed.ToString("h'h 'm'm 's's'")}!");
                Console.WriteLine($"total numbers: {count}.");
            }
            else if (Global.Language == Global.Languages.German)
            {
                Console.WriteLine($"Erledigt in {stopwatch.Elapsed.ToString("h'h 'm'm 's's'")}!");
                Console.WriteLine($"Anzahl aller Hausnummern: {count}.");
            }

            Console.WriteLine(Environment.NewLine);
            if (toExcel)
            {
                Console.Write("Excel-Export");
                fileExtension = ".xlsx";
            }
            else
            {
                ToCSV(result);
                Console.Write("CSV-Export");
                fileExtension = ".csv";
            }

            if (Global.Language == Global.Languages.English)
            {
                Console.Write($", done.{Environment.NewLine}");
                Console.WriteLine($"Path: {Environment.CurrentDirectory}{Path.PathSeparator}dachs{fileExtension}");
                Console.WriteLine("press any key to continue ..");
                Console.ReadKey();
            }
            else if (Global.Language == Global.Languages.German)
            {
                Console.Write($", abgeschlossen.{Environment.NewLine}");
                Console.WriteLine($"Pfad: {Environment.CurrentDirectory}{Path.PathSeparator}dachs{fileExtension}");
                Console.WriteLine("Drücke eine Taste zum Fortfahren ..");
                Console.ReadKey();
            }

            Console.ReadKey();

            return true;
        }

        static void ToCSV(Dictionary<string, string> streetsNumbers)
        {
            IFileGenerator csvGenerator = new CsvGenerator(Directory.GetCurrentDirectory(), "dachs");
            csvGenerator.Generate(streetsNumbers);
        }

        static string GetNumbers(string street)
        {
            try
            {
                TotalStreets = 0;
                IExtractor extractor = new Extractor();
                IEnumerable<string> er = extractor.Extract(street);
                bool first = true;
                StringBuilder result = new StringBuilder();

                foreach (var r in er)
                {
                    TotalStreets++;
                    if (first)
                    {
                        result.Append(r);
                        first = false;
                    }
                    else
                        result.Append(string.Concat(",", r));
                }
                return result.ToString();
            }
            catch (Exception exception)
            {
                if (Global.Language == Global.Languages.English)
                {
                    Console.Error.WriteLine($"ERROR: {exception.Message}");
                }
                else if (Global.Language == Global.Languages.German)
                {
                    Console.Error.WriteLine($"FEHLER: {exception.Message}");
                }
                return string.Empty;
            }
        }

        static void PrintNumbers(string street)
        {
            string numbers = GetNumbers(street);
            if (Global.Language == Global.Languages.English)
            {
                Console.WriteLine($"All available house numbers for {street}:");
                Console.Write("=================================");
                for (int i = 1; i <= street.Length; i++)
                {
                    Console.Write("=");
                }
                Console.WriteLine();
                Console.WriteLine(numbers);
                Console.WriteLine($"total: {TotalStreets}");

                Console.WriteLine();
                Console.WriteLine("press any key to continue ..");
            }
            else if (Global.Language == Global.Languages.German)
            {
                Console.WriteLine($"Alle verfügbaren Hausnummern für {street}:");
                Console.Write("==================================");
                for (int i = 1; i <= street.Length; i++)
                {
                    Console.Write("=");
                }
                Console.WriteLine();
                Console.WriteLine(numbers);
                Console.WriteLine($"Anzahl Hausnummern: {TotalStreets}");

                Console.WriteLine();
                Console.WriteLine("Drücke eine Taste zum Fortfahren ..");
            }
            Console.ReadKey();
        }
    }
}
