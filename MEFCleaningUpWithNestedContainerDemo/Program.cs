using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEFCleaningUpWithNestedContainerDemo
{
    class Program
    {
        //Adding a parent container as the class member.
        static CompositionContainer parentContainer;

        static void Main(string[] args)
        {
            //Setting up parent Catalog
            TypeCatalog parentCatalog = new TypeCatalog(typeof(Logger));
            //Since logging is something used throughout the application, it makes sense to have it in the parent container. 
            parentContainer = new CompositionContainer(parentCatalog);

            PerformWork();

            //Since we know that the child container is disposed, let's see what happens to the parentContainer.
            var logger = parentContainer.GetExportedValue<Logger>();
            logger.Log("Logger still works!");

            //Then we will dispose the parent container. 
            parentContainer.Dispose();

            Console.ReadKey();
        }

        static void PerformWork()
        {
            //We will use the child container in this method. 

            //First we will setup our catalog and pass in TemporaryWorker and ResourceLoader parts
            TypeCatalog childCatalog = new TypeCatalog(typeof(TemporaryWorker), typeof(MemoryIntensiveResourceLoader));

            //In the child container, not only we pass the catalog, but also pass the parentContianer reference.
            //All of the parts that parent container knows about, the child container knows about as well.

            //When the child container composes a part, it will use the parent container to satisfy the imports if necessary. 
            CompositionContainer childContainer = new CompositionContainer(childCatalog, parentContainer);


            //Then we will grab the temporary container from the child container and call the DoWork method.
            var worker = childContainer.GetExportedValue<TemporaryWorker>();
            worker.DoWork();

            //When the work is complete, we will dispose of the child container. 
            childContainer.Dispose();
        }
    }

    [Export]
    class Logger : IDisposable
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }

        public void Dispose()
        {
            Console.WriteLine("Disposing of Logger");
        }
    }

    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    class TemporaryWorker : IDisposable
    {
        [Import]
        private Logger _logger;

        [Import]
        private MemoryIntensiveResourceLoader _resources;

        public void DoWork()
        {
            _logger.Log("Starting work");
            _resources.Initialize();
            //Do work
            _logger.Log("Finished working");
        }

        public void Dispose()
        {
            Console.WriteLine("Disposing of TemporaryWorker");
        }
    }

    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    class MemoryIntensiveResourceLoader : IDisposable
    {
        [Import]
        private Logger _logger;

        public void Initialize()
        {
            _logger.Log("Started initializing");
            //Do work
            _logger.Log("Finished initializing");
        }

        public void Dispose()
        {
            Console.WriteLine("Disposing of MemoryIntensiveResourceLoader");
        }
    }
}
