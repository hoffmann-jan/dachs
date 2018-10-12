using System.Collections.Generic;

namespace dachs.Interfaces
{
    /// <summary>
    /// File generator contract.
    /// </summary>
    public interface IFileGenerator
    {
        void Generate(IEnumerable<string> content);
    }
}
