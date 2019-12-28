using System;
using System.ComponentModel.Composition.Primitives;
using System.Configuration;

namespace MEFCustomProgrammingModelDemo
{
    public class ConfiguredParts : ConfigurationSection
    {
        [ConfigurationProperty("Parts", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(ConfiguredPartConfigCollection), AddItemName = "Part")]
        public ConfiguredPartConfigCollection Parts
        {
            get { return (ConfiguredPartConfigCollection)base["Parts"]; }
            set { }
        }

        private static ConfiguredParts _instance;
        public static ConfiguredParts Instance
        {
            get
            {
                if (_instance == null) { InitializeConfig(); }
                return _instance;
            }
        }

        private static void InitializeConfig()
        {
            _instance = (ConfiguredParts)ConfigurationManager.GetSection("ConfiguredParts");
        }

    }

    public class ConfiguredPartConfigCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new PartConfig();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((PartConfig)element).Type;
        }

        public void Add(PartConfig partConfig)
        {
            BaseAdd(partConfig);
        }
    }

    public class PartConfig : ConfigurationElement
    {
        [ConfigurationProperty("Type", IsRequired = true)]
        public string Type
        {
            get
            {
                return this["Type"].ToString();
            }
        }

        [ConfigurationProperty("Contract", IsRequired = true)]
        public string Contract
        {
            get
            {
                return this["Contract"].ToString();
            }
        }

        [ConfigurationProperty("Imports", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(ConfiguredPartImportDefinitionCollection), AddItemName = "Import")]
        public ConfiguredPartImportDefinitionCollection Imports
        {
            get { return (ConfiguredPartImportDefinitionCollection)base["Imports"]; }
        }

        [ConfigurationProperty("Metadata", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(ConfiguredPartImportDefinitionCollection), AddItemName = "Add")]
        public ConfiguredPartMetadataDefinitionCollection Metadata
        {
            get { return (ConfiguredPartMetadataDefinitionCollection)base["Metadata"]; }
        }
    }

    public class ConfiguredPartMetadataDefinitionCollection : ConfigurationElementCollection
    {

        protected override ConfigurationElement CreateNewElement()
        {
            return new ConfiguredPartMetadataDefinition();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ConfiguredPartMetadataDefinition)element).Name;
        }

        public void Add(ConfiguredPartMetadataDefinition partConfig)
        {
            BaseAdd(partConfig);
        }
    }

    public class ConfiguredPartMetadataDefinition : ConfigurationElement
    {
        [ConfigurationProperty("Name", IsRequired = true)]
        public string Name
        {
            get
            {
                return this["Name"].ToString();
            }
        }

        [ConfigurationProperty("Value", IsRequired = true)]
        public string Value
        {
            get
            {
                return this["Value"].ToString();
            }
        }

    }


    public class ConfiguredPartImportDefinitionCollection : ConfigurationElementCollection
    {

        protected override ConfigurationElement CreateNewElement()
        {
            return new ConfiguredPartImportDefinition();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ConfiguredPartImportDefinition)element).Type;
        }

        public void Add(ConfiguredPartImportDefinition partConfig)
        {
            BaseAdd(partConfig);
        }
    }

    public class ConfiguredPartImportDefinition : ConfigurationElement
    {
        [ConfigurationProperty("Type", IsRequired = true)]
        public string Type
        {
            get
            {
                return this["Type"].ToString();
            }
        }

        [ConfigurationProperty("Contract", IsRequired = true)]
        public string Contract
        {
            get
            {
                return this["Contract"].ToString();
            }
        }

        [ConfigurationProperty("AllowRecomposition", IsRequired = true)]
        public bool AllowRecomposition
        {
            get
            {
                return bool.Parse(this["AllowRecomposition"].ToString());
            }
        }

        [ConfigurationProperty("AllowDefault", IsRequired = true)]
        public bool AllowDefault
        {
            get
            {
                return bool.Parse(this["AllowDefault"].ToString());
            }
        }

        [ConfigurationProperty("Cardinality", IsRequired = true)]
        public ImportCardinality Cardinality
        {
            get
            {
                return (ImportCardinality)Enum.Parse(typeof(ImportCardinality), this["Cardinality"].ToString());
            }
        }
    }
}