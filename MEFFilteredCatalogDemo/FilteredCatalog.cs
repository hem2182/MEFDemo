using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEFFilteredCatalogDemo
{
    class FilteredCatalog : ComposablePartCatalog
    {
        private ComposablePartCatalog sourceCatalog;
        private Func<ComposablePartDefinition, bool> inclusiveFilter;
        public FilteredCatalog(ComposablePartCatalog sourceCatalog, Func<ComposablePartDefinition, bool> inclusiveFilter)
        {
            this.inclusiveFilter = inclusiveFilter;
            this.sourceCatalog = sourceCatalog;
        }

        private IQueryable<ComposablePartDefinition> FilterParts()
        {
            var includedParts = (from p in sourceCatalog.Parts
                                 let keepPart = inclusiveFilter(p)
                                 where keepPart == true
                                 select p);
            return includedParts;
        }

        public override IQueryable<ComposablePartDefinition> Parts
        {
            get { return FilterParts(); }
        }
    }
}
