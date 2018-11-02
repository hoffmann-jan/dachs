using System.Collections.Generic;
using System.IO;
using System.Text;
using dachsXll.Interfaces;

namespace dachsXll.Generators
{
    /// <summary>
    /// Csv generator.
    /// </summary>
    public class CsvGenerator : IFileGenerator
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
        /// <param name="content">Content.</param>
        void IFileGenerator.Generate(IEnumerable<string> content)
        {
            StringBuilder csv = new StringBuilder();

            foreach(string line in content)
            {
                csv.AppendLine(line);
            }

            File.WriteAllText(Path.Combine(_Path, string.Concat(_StreetName.Replace(' ', '_'), ".csv")), csv.ToString());
        }
        #endregion

    }
}
