using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MEFCleaningUpPartsDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            AssemblyCatalog catalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());
            CompositionContainer container = new CompositionContainer(catalog);

            var classA = container.GetExport<ClassA>();
            var instance = classA.Value;

            //then we will use the generic ReleaseExport method and gie it the lazy instance we got from the container. 
            container.ReleaseExport<ClassA>(classA);
            //Finally we will dispose the container.
            container.Dispose();

            Console.ReadKey();
        }
    }

    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    class ClassA : IDisposable
    {
        [Import]
        private ClassB classB;
        public void Dispose()
        {
            Console.WriteLine("Disposing of A.");
        }
    }

    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    class ClassB : IDisposable
    {
        [Import]
        private ClassC classC;
        public void Dispose()
        {
            Console.WriteLine("Disposing of B.");
        }
    }

    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    class ClassC : IDisposable
    {
        public void Dispose()
        {
            Console.WriteLine("Disposing of C.");
        }
    }
}
