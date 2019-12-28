using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MEFFilteredCatalogDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            AssemblyCatalog catalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());
            FilteredCatalog filteredCatalog = new FilteredCatalog(catalog, CheckPartIsTemporary);

            CompositionContainer container = new CompositionContainer(filteredCatalog);

            Console.ReadKey();
        }

        private static bool CheckPartIsTemporary(ComposablePartDefinition partDef)
        {
            string metadataKey = "IsTemporary";

            var keepPart = (from e in partDef.ExportDefinitions
                            where e.Metadata.ContainsKey(metadataKey)
                            select e).Any(p => bool.Parse(p.Metadata[metadataKey].ToString()));

            return keepPart;
        }
    }

    [Export]
    class PartA { }
    [Export]
    class PartB { }
    [Export]
    [ExportMetadata("IsTemporary", true)]
    class PartC { }

}
