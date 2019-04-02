using System.Collections.Generic;

namespace dachs.Interfaces
{
    /// <summary>
    /// Extractor.
    /// </summary>
    public interface IExtractor
    {
        /// <summary>
        /// Extract the specified streetOfLe.
        /// </summary>
        /// <returns>The extract.</returns>
        /// <param name="streetOfLe">Street of le.</param>
        IEnumerable<string> Extract(string streetOfLe);

        /// <summary>
        /// Extract all streets of Le.
        /// </summary>
        /// <returns>The extract.</returns>
        IEnumerable<string> ExtractAllStreets();        
    }
}
