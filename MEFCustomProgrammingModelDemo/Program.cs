using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

namespace MEFCustomProgrammingModelDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            ConfiguredPartsCatalog cfgCatalog = new ConfiguredPartsCatalog();
            TypeCatalog typCatalog = new TypeCatalog(typeof(WorkerB));

            AggregateCatalog catalog = new AggregateCatalog(cfgCatalog, typCatalog);

            CompositionContainer container = new CompositionContainer(catalog);

            var workerA = container.GetExport<WorkerA, Dictionary<string, object>>();
            workerA.Value.DoWork();

            var workerB = container.GetExportedValue<WorkerB>();
            workerB.DoWork();

            Console.ReadKey();
        }
    }


    public class WorkerA
    {
        public Logger Logger { get; set; }

        public void DoWork()
        {
            Logger.Log("Logging from WorkerA");
        }
    }

    public class Logger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }

    [Export]
    public class WorkerB
    {
        [Import("ConsoleLogger")]
        public Logger Logger { get; set; }

        public void DoWork()
        {
            Logger.Log("Logging from WorkerB");
        }
    }

}
