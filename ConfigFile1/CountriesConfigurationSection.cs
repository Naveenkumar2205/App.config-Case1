using System.Configuration;

namespace ConfigFile1
{
     public class KeyValueConfigurationElement : ConfigurationElement            
     {
        [ConfigurationProperty("CountryName", IsKey = true, IsRequired = true)]  
        public string CountryName                                                //it have properties of elements.
        {  
            get { return (string)base["CountryName"]; }
            set { base["CountryName"] = value; }
        }

        [ConfigurationProperty("CountryCode", IsRequired = true)]   
        public string CountryCode
        {
            get { return (string)base["CountryCode"]; }
            set { base["CountryCode"] = value; }
        }
     }
    [ConfigurationCollection(typeof(KeyValueConfigurationElement))]    
    public class KeyValueConfigurationCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()      //for creating instances of elements.add or editing.
        {
            return new KeyValueConfigurationElement();
        }
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((KeyValueConfigurationElement)element).CountryName;
        }
    }
    public class CountriesConfigurationSection : ConfigurationSection 
    {
        [ConfigurationProperty("", IsDefaultCollection = true)]      
        public KeyValueConfigurationCollection Countries               //for accessing the instances of elements.
        {
            get { return (KeyValueConfigurationCollection)this[""]; }
        }
    }
}
