using System.Collections.Generic;

namespace dachs.Interfaces
{
    /// <summary>
    /// File generator contract.
    /// </summary>
    public interface IFileGenerator
    {
        /// <summary>
        /// Generates a file with given content.
        /// </summary>
        /// <param name="streetsNumbers">Key:Street;Value:Numbers</param>
        void Generate(Dictionary<string, string> streetsNumbers);
    }
}
