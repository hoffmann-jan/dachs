namespace dachsXll.Interfaces
{
    /// <summary>
    /// I can parse Excel or CSV.
    /// </summary>
    interface IParse
    {
        object Parse(string path);
    }
}
