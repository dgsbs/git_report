using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;

namespace ReportCreator
{
    public class JsonConfig : IJsonConfig
    {
        private static IConfiguration configuration;
        private Dictionary<string, string> idPathDictionary;
        public List<string> SeperatorList { get; private set; }
        public JsonConfig()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("AllComponents.json", optional: true, reloadOnChange: true);
            configuration = builder.Build();

            this.idPathDictionary = new Dictionary<string, string>();
            this.SeperatorList = new List<string>();

            CreateIdPathDictionary();
            CreateSeparatorList();
        }
        public bool TryMatchPath(string pathFromProcess, out string finalId)
        {
            foreach (var idPath in this.idPathDictionary)
            {
                if (pathFromProcess.Contains(idPath.Value))
                {
                    finalId = idPath.Key;
                    return true;
                }
            }
            finalId = string.Empty;
            return false;
        }
        public string GetSeparator(Separator separator)
        {
            switch (separator)
            {
                case Separator.Commit:
                    {
                        return SeperatorList[1];
                    }
                case Separator.Output:
                    {
                        return SeperatorList[0];
                    }
            }
            return string.Empty;
        }
        private void CreateIdPathDictionary()
        {
            var pathKeys = configuration.GetSection("components").GetChildren();
            char[] charsToTrim = { '*' };

            foreach (var key in pathKeys)
            {
                this.idPathDictionary.Add(key["id"], key["paths"].TrimEnd(charsToTrim));
            }
        }
        private void CreateSeparatorList()
        {
            var pathKeys = configuration.GetSection("stringSeperator").GetChildren();
            
            foreach (var key in pathKeys)
            {
                SeperatorList.Add(key["separator"]);
            }
        }
        public enum Separator
        {
            Output,
            Commit
        }
    }
}
