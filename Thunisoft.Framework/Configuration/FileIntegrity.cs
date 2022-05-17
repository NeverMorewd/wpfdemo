using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thunisoft.Framework.Configuration
{
    public class FileIntegritySection : ConfigurationSection
    {
        [ConfigurationProperty("FileCollection", IsDefaultCollection = false)]
        public FileCollection FileCollection { get { return (FileCollection)base["FileCollection"]; } }
    }
    public class FileCollection : ConfigurationElementCollection
    {
        /// <summary>
        /// 预留属性，为以后完整性校验分包进行留作扩展
        /// </summary>
        [ConfigurationProperty("DomainName", IsRequired = false)]
        public string TrackerName
        {
            get { return (string)base["DomainName"]; }
            set { base["DomainName"] = value; }
        }
        protected override ConfigurationElement CreateNewElement()
        {
            return new FileItem();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((FileItem)element).Name;
        }
    }
    public class FileItem : ConfigurationElement
    {
        protected override bool OnDeserializeUnrecognizedAttribute(string name, string value)
        {
            return base.OnDeserializeUnrecognizedAttribute(name, value);
        }
        protected override bool OnDeserializeUnrecognizedElement(string elementName, System.Xml.XmlReader reader)
        {
            return base.OnDeserializeUnrecognizedElement(elementName, reader);
        }

        [ConfigurationProperty("Name", DefaultValue = "", IsRequired = true)]
        public string Name { get { return this["Name"].ToString(); } }

        [ConfigurationProperty("MD5", DefaultValue = "", IsRequired = true)]
        public string MD5 { get { return this["MD5"].ToString(); } }

        [ConfigurationProperty("IsNecessary", DefaultValue = "true", IsRequired = false)]
        public bool IsNecessary { get { return (bool)this["IsNecessary"]; } }
    }
}
