using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;
namespace GitReport.CLI
{
    class JsonConfig
    {
        public static IConfiguration Configuration { get; set; }
        Dictionary<string, string> ids = new Dictionary<string, string>();
        public JsonConfig()
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("jsconfig.json", optional: true, reloadOnChange: true);
            Configuration = builder.Build();

            CreateIdsDictionary();
        }
        public bool TryMatchPaths(string pathFromProcess, out string finalId)
        {
            foreach (var id in ids)
            {
                if (pathFromProcess.Contains(id.Value))
                {
                    finalId = id.Key;
                    return true;
                }
            }
            finalId = string.Empty;
            return false;
        }
        private void CreateIdsDictionary()
        {
            var pathKeys = Configuration.GetSection("components").GetChildren();
            char[] charsToTrim = {'*'};
            foreach (var key in pathKeys)
            {
                ids.Add(key["id"], key["paths"].TrimEnd(charsToTrim));
            }
        }
    }
}
