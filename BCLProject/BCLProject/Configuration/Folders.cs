using System.Configuration;


namespace BCLProject.Configuration
{
    class Folders : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new Folder();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((Folder)element).Name;
        }
    }
}
