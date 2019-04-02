using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using dachs.Interfaces;

namespace dachs.Generators
{
    /// <summary>
    /// Csv generator.
    /// </summary>
    public class CsvGenerator : IFileGenerator
    {
        #region Fields
        private readonly string _Path;
        private string _StreetName;
        #endregion

        #region Properties

        #endregion

        #region Constructors
        /// <summary>
        /// Basis-Konstruktor
        /// </summary>
        public CsvGenerator(string path, string streetName)
        {
            _Path = path;
            _StreetName = streetName;
        }

        #endregion

        #region Public Methods

        #endregion

        #region Private Methods

        #endregion

        #region IFileGenerators
        /// <summary>
        /// Dachs.s the interfaces. IFile generator. generate.
        /// </summary>
        /// <param name="streetsNumbers">Key:Street;Value:Numbers</param>
        void IFileGenerator.Generate(Dictionary<string, string> streetsNumbers)
        {
            StringBuilder csv = new StringBuilder();

            if (streetsNumbers.Count == 1)
                _StreetName = streetsNumbers.Keys.First();

            foreach (KeyValuePair<string, string> keyValuePair in streetsNumbers)
            {
                csv.Append($"{keyValuePair.Key},");
                csv.AppendLine($"{keyValuePair.Value}");
            }

            File.WriteAllText(Path.Combine(_Path, string.Concat(_StreetName.Replace(' ', '_'), ".csv")), csv.ToString());
        }
        #endregion

    }
}
