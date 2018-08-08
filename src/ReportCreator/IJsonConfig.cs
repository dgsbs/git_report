namespace GitCounter
{
    public interface IJsonConfig
    {
        bool TryMatchPath(string pathFromProcess, out string finalId);
    }
}
