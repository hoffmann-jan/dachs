using System.Collections.Generic;

namespace dachsXll.Interfaces
{
    /// <summary>
    /// File generator contract.
    /// </summary>
    public interface IFileGenerator
    {
        void Generate(IEnumerable<string> content);
    }
}
