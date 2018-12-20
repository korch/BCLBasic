using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCLProject.Configuration
{
    class CustomConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("culture", DefaultValue = "en-EN", IsRequired = false)]
        public CultureInfo Culture => (CultureInfo)this["culture"];

        [ConfigurationCollection(typeof(Folder), AddItemName = "folder")]
        [ConfigurationProperty("folders", IsRequired = false)]
        public Folders Directories => (Folders)this["folders"];

        [ConfigurationCollection(typeof(Rule), AddItemName = "rule")]
        [ConfigurationProperty("rules", IsRequired = true)]
        public RulesCollection Rules => (RulesCollection)this["rules"];
    }
}
