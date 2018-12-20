using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCLProject.Configuration
{
    class RulesCollection : ConfigurationElementCollection
    {
        [ConfigurationProperty("defaultDirectory", IsRequired = true)]
        public string DefaultDirectory => (string)this["defaultDirectory"];

        protected override ConfigurationElement CreateNewElement()
        {
            return new Rule();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((Rule)element).FilePatternName;
        }
    }
}
