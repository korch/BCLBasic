using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BCLProject.Configuration;
using BCLProject.Interfaces;
using BCLProject.Models;
using BCLProject.Resources;

namespace BCLProject
{
    class MonitoringHelper
    {
        private readonly ILogger _logger;
        private readonly List<RuleModel> _rules;
        private readonly string _defaultFolder;
        private const int TIMEOUT = 1000;

        public MonitoringHelper(List<RuleModel> rules, string defaultFolder, ILogger logger)
        {
            _rules = rules;
            _logger = logger;
            _defaultFolder = defaultFolder;
        }

        public async Task MoveAsync(FileModel item)
        {
            var from = item.FullName;
            foreach (var rule in _rules) {
                var match = Regex.Match(item.Name, rule.FilePatternName);

                if (match.Success && match.Length == item.Name.Length) {
                    rule.Count++;
                    string to = FormDestinationPath(item, rule);
                    _logger.Log(Messages.MatchWithRule);
                    await MoveFileAsync(from, to);
                    _logger.Log(string.Format(Messages.MovedTemplate, item.FullName, to));
                    return;
                }
            }

            string destination = Path.Combine(_defaultFolder, item.Name);
            _logger.Log(Messages.NoMatchedRule);
            await MoveFileAsync(from, destination);
            _logger.Log(string.Format(Messages.MovedTemplate, item.FullName, destination));
        }

        private async Task MoveFileAsync(string from, string to)
        {
            string dir = Path.GetDirectoryName(to);
            Directory.CreateDirectory(dir);
            bool cannotAccessFile = true;
            do {
                try {
                    if (File.Exists(to))
                    {
                        File.Delete(to);
                    }
                    File.Move(from, to);
                    cannotAccessFile = false;
                } catch (FileNotFoundException) {
                    _logger.Log(Messages.FileNotFounded);
                    break;
                } catch (IOException ioex) {
                    var t = ioex.GetType();
                    await Task.Delay(TIMEOUT);
                }
            } while (cannotAccessFile);
        }

        private string FormDestinationPath(FileModel file, RuleModel rule)
        {
            var filename = Path.GetFileNameWithoutExtension(file.Name);
            var extension = Path.GetExtension(file.Name);
            var destination = new StringBuilder();

            destination.Append(Path.Combine(rule.FolderToSave, filename));

            if (rule.MovementDate) {
                var dateTimeFormat = CultureInfo.CurrentCulture.DateTimeFormat;
                dateTimeFormat.DateSeparator = ".";
                destination.Append($"{destination}_{DateTime.Now.ToLocalTime().ToString(dateTimeFormat.ShortDatePattern)}");
            }

            if (rule.Counter) {
                destination.Append($"_{rule.Counter}");
            }

            destination.Append(extension);
            return destination.ToString();
        }
    }
}
