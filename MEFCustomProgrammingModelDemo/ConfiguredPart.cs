using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using System.Reflection;

namespace MEFCustomProgrammingModelDemo
{
    public class ConfiguredPart : ComposablePart
    {
        private List<ExportDefinition> exports;
        private List<ImportDefinition> imports;

        private readonly Dictionary<ImportValueKey, object> _importedValues = new Dictionary<ImportValueKey, object>();

        public ConfiguredPart(IEnumerable<ExportDefinition> exports, IEnumerable<ImportDefinition> imports)
        {
            this.exports = exports.ToList();
            this.imports = imports.ToList();
        }

        public override IEnumerable<ExportDefinition> ExportDefinitions
        {
            get { return exports; }
        }

        public override IEnumerable<ImportDefinition> ImportDefinitions
        {
            get { return imports; }
        }

        public override void SetImport(ImportDefinition definition, IEnumerable<Export> exports)
        {
            var partToImport = exports.FirstOrDefault(c => c.Definition.ContractName.Equals(definition.ContractName));

            if (definition.IsPrerequisite && partToImport == null)
            {
                throw new ImportCardinalityMismatchException();
            }

            if (partToImport == null) { return; }

            ImportValueKey key = new ImportValueKey(partToImport.Value.GetType().FullName, definition.ContractName, definition);

            if (definition.Cardinality != ImportCardinality.ExactlyOne)
            {
                throw new InvalidOperationException("Only supported cardinality is ExactlyOne");
            }

            if (exports.Count() != 1 || _importedValues.ContainsKey(key))
            {
                throw new ImportCardinalityMismatchException();
            }

            _importedValues.Add(key, partToImport.Value);
        }

        public override object GetExportedValue(ExportDefinition definition)
        {
            string typeName = definition.Metadata["ExportTypeIdentity"].ToString();

            Type partType = Type.GetType(typeName);
            var partInstance = Activator.CreateInstance(partType);

            SatisfyImports(partInstance);

            return partInstance;
        }

        private void SatisfyImports(object partInstance)
        {
            var partMembers = partInstance.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (FieldInfo p in partMembers)
            {
                var key = _importedValues.Keys.Where(
                    c => c.TypeName.Equals(p.FieldType.FullName)
                ).FirstOrDefault();

                if (key == null) { continue; }

                ((FieldInfo)p).SetValue(partInstance, _importedValues[key]);
            }
        }
    }
}