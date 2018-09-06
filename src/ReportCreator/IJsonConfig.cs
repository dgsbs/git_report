namespace ReportCreator
{
    public interface IJsonConfig
    {
        bool TryMatchPath(string pathFromProcess, out string finalId);
        string GetSeparator(Separator separator);
    }
}
