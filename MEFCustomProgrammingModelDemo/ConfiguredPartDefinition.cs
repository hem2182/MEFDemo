using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Primitives;

namespace MEFCustomProgrammingModelDemo
{
    public class ConfiguredPartDefinition : ComposablePartDefinition
    {
        private Type partType;
        private List<ExportDefinition> exports;
        private List<ImportDefinition> imports;

        public ConfiguredPartDefinition(PartConfig partConfig)
        {
            partType = Type.GetType(partConfig.Type);
            exports = new List<ExportDefinition>();
            imports = new List<ImportDefinition>();

            exports.Add(new ExportDefinition(partConfig.Contract, GetMetaData(partConfig.Metadata)));

            if (partConfig.Imports.Count > 0)
            {
                ProcessImportDefinitions(partConfig);
            }
        }

        private Dictionary<string, object> GetMetaData(ConfiguredPartMetadataDefinitionCollection metadataDefinitions)
        {
            var metadata = new Dictionary<string, object>()
            {
                { "ExportTypeIdentity", this.partType.FullName }
            };

            if (metadataDefinitions != null)
            {
                foreach (ConfiguredPartMetadataDefinition md in metadataDefinitions)
                {
                    metadata.Add(md.Name, md.Value);
                }
            }

            return metadata;
        }

        private void ProcessImportDefinitions(PartConfig partConfig)
        {
            foreach (ConfiguredPartImportDefinition pid in partConfig.Imports)
            {
                ImportDefinition importDef = new ImportDefinition(
                    i => i.ContractName.Equals(pid.Contract),
                    pid.Contract,
                    pid.Cardinality,
                    pid.AllowRecomposition,
                    pid.AllowDefault
                );

                this.imports.Add(importDef);
            }
        }

        public override ComposablePart CreatePart()
        {
            return new ConfiguredPart(this.exports, this.imports);
        }

        public override IEnumerable<ExportDefinition> ExportDefinitions
        {
            get { return this.exports; }
        }

        public override IEnumerable<ImportDefinition> ImportDefinitions
        {
            get { return this.imports; }
        }
    }
}