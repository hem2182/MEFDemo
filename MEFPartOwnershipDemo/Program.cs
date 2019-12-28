using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MEFPartOwnershipDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            AssemblyCatalog catalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());
            CompositionContainer container = new CompositionContainer(catalog);

            for (int i = 0; i < 10; i++)
            {
                var tmp = container.GetExportedValue<DisposablePart>();
                //releasing the instance by setting it to null
                tmp = null;
            }

            GC.Collect();
            GC.WaitForPendingFinalizers();

            container.Dispose();

            Console.ReadKey();
        }
    }
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    class DisposablePart
    {
        public void Dispose()
        {
            Console.WriteLine("Disposing the DisposablePart.");
        }
        ~DisposablePart()
        {
            Console.WriteLine("Finalizing DisposablePart.");
        }
    }
}
