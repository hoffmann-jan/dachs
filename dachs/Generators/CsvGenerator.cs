using System.Collections.Generic;
using System.IO;
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
        #endregion

        #region Properties

        #endregion

        #region Constructors
        /// <summary>
        /// Basis-Konstruktor
        /// </summary>
        public CsvGenerator(string path) 
        {
            _Path = path;
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

            File.WriteAllText(_Path, csv.ToString());
        }
        #endregion

    }
}
