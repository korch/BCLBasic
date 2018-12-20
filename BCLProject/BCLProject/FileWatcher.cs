using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCLProject.Interfaces;
using BCLProject.Models;
using BCLProject.Resources;

namespace BCLProject
{
    class FileWatcher
    {
        private List<FileSystemWatcher> _fileWatchers;
        private readonly ILogger _logger;

        public FileWatcher(IEnumerable<string> folders, ILogger logger)
        {
            _fileWatchers = folders.Select(CreateFileWatcher).ToList();
            _logger = logger;
        }

        public event EventHandler<CreatedFileEventArgs<FileModel>> Created;

        private FileSystemWatcher CreateFileWatcher(string path)
        {
            var fileSystemWatcher = new FileSystemWatcher(path) {
                    NotifyFilter = NotifyFilters.FileName,
                    IncludeSubdirectories = false,
                    EnableRaisingEvents = true
                };

            fileSystemWatcher.Created += (sender, fileSystemEventArgs) =>
            {
                _logger.Log(string.Format(Messages.FoundedTemplate, fileSystemEventArgs.Name));
                OnCreated(new FileModel { FullName = fileSystemEventArgs.FullPath, Name = fileSystemEventArgs.Name });
            };

            return fileSystemWatcher;
        }

        private void OnCreated(FileModel file)
        {
            Created?.Invoke(this, new CreatedFileEventArgs<FileModel> { FileItem = file });
        }
    }
}
