using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BCLProject.Configuration;
using BCLProject.Interfaces;
using BCLProject.Models;
using BCLProject.Resources;
using static System.Console;

namespace BCLProject
{
    class Program
    {
        private static MonitoringHelper _helper;
        static void Main(string[] args)
        {
            ILogger logger = new Logger();
            var service = new FileWatcherService(logger);
            CustomConfigurationSection config = (CustomConfigurationSection)ConfigurationManager.GetSection("customSection");

            if (config == null) {
                WriteLine(Messages.ConfigNotFounded);
                return;
            } 

            service.ReadConfiguration(config);
            _helper = new MonitoringHelper(service.Rules, service.DefaultFolder, logger);
            var fileWatchers = new FileWatcher(service.Folders, logger);

            fileWatchers.Created += OnCreated;

            WriteLine(Messages.Hello_string);
            ReadKey(true);
        }

        private static async void OnCreated(object sender, CreatedFileEventArgs<FileModel> args)
        {
            await _helper.MoveAsync(args.FileItem);
        }
    }
}
