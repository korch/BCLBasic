using System.Collections.Generic;
using System.Globalization;
using BCLProject.Configuration;
using BCLProject.Interfaces;
using BCLProject.Models;
using BCLProject.Resources;

namespace BCLProject
{
    internal class FileWatcherService
    {
        private List<string> _folders;
        public List<string> Folders => _folders;

        private List<RuleModel> _rules;
        public List<RuleModel> Rules => _rules;

        private string _defaultFolder;
        public string DefaultFolder => _defaultFolder;

        private readonly ILogger _logger;
        public FileWatcherService(ILogger logger)
        {
            _logger = logger;
        }

        public void ReadConfiguration(CustomConfigurationSection config)
        {
            _folders = new List<string>(config.Directories.Count);
            _rules = new List<RuleModel>(config.Rules.Count);
            _defaultFolder = config.Rules.DefaultDirectory;

            CultureInfo.DefaultThreadCurrentCulture = config.Culture;
            CultureInfo.DefaultThreadCurrentUICulture = config.Culture;
            CultureInfo.CurrentUICulture = config.Culture;
            CultureInfo.CurrentCulture = config.Culture;

            foreach (Folder folder in config.Directories) {
                _folders.Add(folder.Path);
                _logger.Log(string.Format(Messages.AddFolderToCollection, folder.Name));
            }

            foreach (Rule rule in config.Rules)
            {
                _rules.Add(new RuleModel
                {
                    FilePatternName = rule.FilePatternName,
                    Counter = rule.Counter,
                    MovementDate = rule.MovementDate,
                    FolderToSave = rule.FolderToSave
                });
                _logger.Log(string.Format(Messages.AddRuleToCollection, rule.FilePatternName));
            }
        }
    }
}
