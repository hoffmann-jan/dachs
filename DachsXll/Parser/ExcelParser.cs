using System;

using dachsXll.Interfaces;

namespace dachsXll
{
    /// <summary>
    /// Excel parser.
    /// </summary>
    public class ExcelParser : IParse
    {
        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Constructors
        /// <summary>
        /// Basis-Konstruktor
        /// </summary>
        public ExcelParser() { }

        #endregion

        #region Public Methods
        /// <summary>
        /// Parse Excel.
        /// </summary>
        /// <param name="path">Path to file.</param>
        /// <returns>Excel object as object.</returns>
        object IParse.Parse(string path)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Private Methods

        #endregion
    }
}
