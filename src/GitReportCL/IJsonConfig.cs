namespace GitReport.CLI
{
    public interface IJsonConfig
    {
        bool TryMatchPath(string pathFromProcess, out string finalId);
    }
}