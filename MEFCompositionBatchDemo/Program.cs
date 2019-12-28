using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEFCompositionBatchDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            WorkStrategy workType = StrategyChooser.Choose();
            Worker worker = new Worker();

            CompositionContainer container = new CompositionContainer();
            container.ComposeParts(worker, workType);

            worker.GetToWork();

            Console.ReadKey();
        }
    }
    
    public class Worker
    {
        [Import]
        private WorkStrategy myWay;

        public void GetToWork()
        {
            myWay.DoWork();
        }
    }
}
