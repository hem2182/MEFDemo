using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MEFImportAttributeDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            AssemblyCatalog catalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());
            CompositionContainer container = new CompositionContainer(catalog);

            ImporterClass importerClass = new ImporterClass();
            container.ComposeParts(importerClass);
            Console.ReadKey();
        }
    }

    class ImporterClass
    {
        [Import(AllowDefault = true)]
        private SingleExport singleExport;

        [ImportMany]
        private IEnumerable<ExportBaseClass> multipleExports;
    }

    [InheritedExport]
    class ExportBaseClass { }

    class MultipleExport1 : ExportBaseClass { }

    class MultipleExport2 : ExportBaseClass { }

    //[Export]
    class SingleExport {

    }
}
