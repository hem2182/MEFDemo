using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MEFDemoApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            AssemblyCatalog catalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());
            CompositionContainer container = new CompositionContainer(catalog);

            foreach (ComposablePartDefinition p in container.Catalog.Parts)
            {
                Console.WriteLine(p.ToString());
            }

            Console.ReadKey();
        }
    }

    [InheritedExport]
    class ClassA { }
    
    class ClassB : ClassA { }

    [InheritedExport]
    public interface IClass { }

    class ClassC : IClass { } 
}
