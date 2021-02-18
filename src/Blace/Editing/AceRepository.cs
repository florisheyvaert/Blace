using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Blace.Editing
{
    public class AceRepository
    {
        private readonly string _location;

        public AceRepository()
        {
            _location = Path.Combine(Path.GetDirectoryName(Assembly.GetCallingAssembly().Location), "wwwroot", "ace");
        }

        public async Task<List<string>> GetThemes()
        {
            var result = GetLocationWithRegex("theme-");
            var themes = new List<string>();
            foreach (var theme in result)
                themes.Add($"ace/theme/{theme}");
            return await Task.FromResult(themes);
        }

        public async Task<List<string>> GetModes()
        {
            var result = GetLocationWithRegex("mode-");
            var modes = new List<string>();
            foreach (var mode in result)
                modes.Add($"ace/mode/{mode}");
            return await Task.FromResult(modes);
        }

        private List<string> GetLocationWithRegex(string pattern)
        {
            if (!Directory.Exists(_location))
                return new List<string>();

            var files = Directory.GetFiles(_location);
            var result = new List<string>();
            foreach (var file in files)
            {
                var regex = new Regex($"(?<={pattern}).*?(?=\\.js)");
                var match = regex.Match(file);
                if (match.Success)
                    result.Add(match.Value);
            }
            return result;
        }
    }
}
