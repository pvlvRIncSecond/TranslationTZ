using System.Collections.Generic;
using System.IO;
using System.Linq;
using Infrastructure.Services.Progress;

namespace Infrastructure.Services.Config
{
    public class ConfigReader : IConfigReader
    {
        private const string ConfigPath = "config.txt";

        private readonly IPersistentProgress _progress;

        private List<string> _keywords;
        private Dictionary<string, string> _configs = new Dictionary<string, string>();

        public ConfigReader(IPersistentProgress progress)
        {
            _progress = progress;
            _keywords = new List<string>()
            {
                "Address",
                "Port",
            };
        }

        public void ReadConfig()
        {
            List<string> text = ReadFile(ConfigPath);

            foreach (string keyword in _keywords)
                _configs[keyword] = text.SingleOrDefault(x => x.Contains(keyword))?.Split(": ").Last();

            _progress.ServerAddress = _configs["Address"];
            _progress.ServerPort = _configs["Port"];
        }

        private List<string> ReadFile(string path)
        {
            using StreamReader reader = new StreamReader(path);
            List<string> fileText = new List<string>();

            while (reader.ReadLine() is { } line)
                fileText.Add(line);

            return fileText;
        }
    }
}