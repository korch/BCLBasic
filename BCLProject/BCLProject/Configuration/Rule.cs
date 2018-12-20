using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BCLProject.Configuration
{
    class Rule: ConfigurationElement
    {
        [ConfigurationProperty("filePatternName", IsRequired = true)]
        public string FilePatternName => (string)this["filePatternName"];

        [ConfigurationProperty("counter", IsRequired = true, DefaultValue = false)]
        public bool Counter => (bool)this["counter"];

        [ConfigurationProperty("movementDate", IsRequired = true, DefaultValue = false)]
        public bool MovementDate => (bool)this["movementDate"];

        [ConfigurationProperty("folderToSave", IsRequired = true)]
        public string FolderToSave => (string)this["folderToSave"];

        public int Count { get; set; }
    }
}
