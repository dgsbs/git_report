using Microsoft.Extensions.Configuration;
using System.IO;
namespace GitReport.CLI
{
    class JsonConfigManager
    {
        public static IConfiguration Configuration { get; set; }
        public JsonConfigManager()
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("jsconfig.json", optional: true, reloadOnChange: true);
            Configuration = builder.Build();
        }
        public bool CheckIfPathMatchesPathsInJson(string pathFromProcess, out string finalId)
        {
            var pathKeys = Configuration.GetSection("components").GetChildren();
            foreach (var key in pathKeys)
            {
                char[] charsToTrim = {'*'};
                if (pathFromProcess.Contains(key["paths"].TrimEnd(charsToTrim)))
                {
                    finalId = key["id"];
                    return true;
                }
            }
            finalId = string.Empty;
            return false;
        }
    }
}
