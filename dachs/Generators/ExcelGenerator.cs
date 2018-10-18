using System;
using System.Collections.Generic;
using System.IO;

using dachs.Interfaces;

using OfficeOpenXml;

namespace dachs.Generators
{
    /// <summary>
    /// Excel generator.
    /// </summary>
    public class ExcelGenerator : IFileGenerator
    {
        #region Fields
        private string _Path;
        private string _StreetName;
        #endregion

        #region Properties

        #endregion

        #region Constructors
        /// <summary>
        /// Basis-Konstruktor
        /// </summary>
        public ExcelGenerator(string path, string streetName) 
        {
            _Path = path;
            _StreetName = streetName;
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// Generate the Street of Le Excel File.
        /// </summary>
        /// <param name="streetsOfLe">pairs</param>
        public void GenerateStreetsOfLe(Dictionary<string, IEnumerable<string>> streetsOfLe)
        {
            ExcelPackage package = new ExcelPackage();

            package.Workbook.Properties.Title = "Street of Le";
            package.Workbook.Properties.Author = "dachs";
            package.Workbook.Properties.Subject = "All Straßen von Leipzig.";
            package.Workbook.Properties.Keywords = "Leipzig,Straßenname,Hausnummern";


            var worksheet = package.Workbook.Worksheets.Add(_StreetName);

            //First add the headers
            worksheet.Cells[1, 1].Value = "Straßenname";
            worksheet.Cells[1, 2].Value = "Hausnummer";

            int row = 2;


            foreach (KeyValuePair<string, IEnumerable<string>> keyValuePair in streetsOfLe)
            {
                bool first = true;
                int col = 1;

                foreach (string num in keyValuePair.Value)
                {
                    if (first)
                    {
                        worksheet.Cells[row, col++].Value = keyValuePair.Key;
                        worksheet.Cells[row, col++].Value = num;
                        first = false;
                    }
                    else
                    {
                        worksheet.Cells[row, col++].Value = num;
                    }

                }

                row++;

            }

            SaveToFile(package, nameof(streetsOfLe));
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Save the excel package to file on disk.
        /// </summary>
        /// <param name="package">Excel</param>
        /// <param name="fileName">Name</param>
        private void SaveToFile(ExcelPackage package, string fileName)
        {
            using (Stream stream = new FileStream(Path.Combine(_Path, string.Concat(fileName.Replace(' ', '_'), ".xlsx")), FileMode.Create))
            {
                package.SaveAs(stream);
            }
        }
        #endregion

        #region IFileGenerator
        /// <summary>
        /// Dachs.s the interfaces. IFile generator. generate.
        /// </summary>
        /// <param name="content">Content.</param>
        void IFileGenerator.Generate(IEnumerable<string> content)
        {
            ExcelPackage package = new ExcelPackage();

            package.Workbook.Properties.Title = "Street of Le";
            package.Workbook.Properties.Author = "dachs";
            package.Workbook.Properties.Subject = _StreetName;
            package.Workbook.Properties.Keywords = "Leipzig,Straßenname,Hausnummern";


            var worksheet = package.Workbook.Worksheets.Add(_StreetName);

            //First add the headers
            worksheet.Cells[1, 1].Value = "Straßenname";
            worksheet.Cells[1, 2].Value = "Hausnummer";

            bool first = true;
            int index = 2;

            foreach(string number in content)
            {
                if (first)
                {
                    worksheet.Cells[index, 1].Value = _StreetName;
                    worksheet.Cells[index, 2].Value = number;
                    first = false;
                }
                else
                {
                    worksheet.Cells[index, 2].Value = number;
                }

                index++;
            }

            SaveToFile(package, _StreetName);
        }

        #endregion
    }
}
