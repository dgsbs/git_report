using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace GitCounter
{
    public class JsonConfig : IJsonConfig
    {
        public static IConfiguration Configuration { get; set; }
        Dictionary<string, string> jsonDictionary = new Dictionary<string, string>();
        public JsonConfig()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("jsconfig.json", optional: true, reloadOnChange: true);
            Configuration = builder.Build();
            CreateJsonDictionary();
        }
        public bool TryMatchPath(string pathFromProcess, out string finalId)
        {
            foreach (var jsonId in jsonDictionary)
            {
                if (pathFromProcess.Contains(jsonId.Value))
                {
                    finalId = jsonId.Key;
                    return true;
                }
            }
            finalId = string.Empty;
            return false;
        }
        private void CreateJsonDictionary()
        {
            var pathKeys = Configuration.GetSection("components").GetChildren();
            char[] charsToTrim = { '*' };
            foreach (var key in pathKeys)
            {
                jsonDictionary.Add(key["id"], key["paths"].TrimEnd(charsToTrim));
            }
        }
    }
}
