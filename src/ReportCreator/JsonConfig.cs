using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;

namespace ReportCreator
{
    public class JsonConfig : IJsonConfig
    {
        private static IConfiguration configuration;
        private Dictionary<string, string> idPathDictionary;
        public Dictionary<Separator, string> SeparatorDictionary { get; private set; }
        public JsonConfig()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("AllComponents.json", optional: true, reloadOnChange: true);
            configuration = builder.Build();

            this.idPathDictionary = new Dictionary<string, string>();
            SeparatorDictionary = new Dictionary<Separator, string>();

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
                    return SeparatorDictionary[Separator.Commit];
                }
                case Separator.Output:
                {
                    return SeparatorDictionary[Separator.Output];
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
            var outputSeparator = configuration.GetSection("outputSeparator");
            SeparatorDictionary.Add(Separator.Output, outputSeparator.Value.ToString());

            var commitSeparator = configuration.GetSection("commitSeparator");
            SeparatorDictionary.Add(Separator.Commit, commitSeparator.Value);
        }
        public enum Separator
        {
            Output,
            Commit
        }
    }
}
