using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace ReportCreator
{
    public class JsonConfig : IJsonConfig
    {
        private static IConfiguration Configuration { get; set; }
        public List<string> Seperator { get; private set; }
        private Dictionary<string, string> JsonDictionary { get; set; }

        public JsonConfig()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("AllComponents.json", optional: true, reloadOnChange: true);
            Configuration = builder.Build();

            this.JsonDictionary = new Dictionary<string, string>();
            this.Seperator = new List<string>();

            CreateJsonDictionary();
            AssignOutPutCutters();
        }
        public bool TryMatchPath(string pathFromProcess, out string finalId)
        {
            foreach (var jsonId in JsonDictionary)
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
        public string FetchSepatator(bool whichSepatator)
        {
            if (whichSepatator)
            {
                return Seperator[0];
            }
            return Seperator[1];
        }
        private void CreateJsonDictionary()
        {
            var pathKeys = Configuration.GetSection("components").GetChildren();
            char[] charsToTrim = { '*' };
            foreach (var key in pathKeys)
            {
                JsonDictionary.Add(key["id"], key["paths"].TrimEnd(charsToTrim));
            }
        }
        private void AssignOutPutCutters()
        {
            var pathKeys = Configuration.GetSection("stringSeperator").GetChildren();
            
            foreach (var key in pathKeys)
            {
                Seperator.Add(key["separator"]);
            }
        }
    }
}
