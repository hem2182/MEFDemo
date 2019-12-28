using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Primitives;
using System.Linq;

namespace MEFCustomProgrammingModelDemo
{
    class ConfiguredPartsCatalog : ComposablePartCatalog
    {
        IQueryable<ComposablePartDefinition> parts;

        public ConfiguredPartsCatalog()
        {
            List<ComposablePartDefinition> configuredParts = new List<ComposablePartDefinition>();

            var cfg = ConfiguredParts.Instance;

            foreach (PartConfig pc in cfg.Parts)
            {
                configuredParts.Add(new ConfiguredPartDefinition(pc));
            }

            this.parts = configuredParts.AsQueryable();
        }

        public override IQueryable<ComposablePartDefinition> Parts
        {
            get { return parts; }
        }

        public override IEnumerable<Tuple<ComposablePartDefinition, ExportDefinition>> GetExports(ImportDefinition definition)
        {
            var res = parts.Where(c => c.ExportDefinitions.Any(d => d.ContractName.Equals(definition.ContractName)));

            var e = (from p in res
                     select new Tuple<ComposablePartDefinition, ExportDefinition>(
                         p,
                         p.ExportDefinitions.First(c => c.ContractName.Equals(definition.ContractName)))
                    ).ToList();
            return e;
        }
    }
}