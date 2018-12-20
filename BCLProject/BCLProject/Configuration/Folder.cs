using System.Configuration;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;

namespace BCLProject.Configuration
{
    class Folder: ConfigurationElement
    {
        [ConfigurationProperty("name", IsRequired = true)]
        public string Name => this["name"] as string;

        [ConfigurationProperty("path", IsRequired = true)]
        public string Path => this["path"] as string;
    }
}
