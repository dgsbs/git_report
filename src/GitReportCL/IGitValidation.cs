namespace GitReport.CLI
{
    public interface IGitValidation
    {
        bool AreDatesAndPathValid(string[] arguments);
    }
}
