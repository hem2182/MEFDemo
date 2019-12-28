using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEFCompositionBatchDemo
{
    [InheritedExport]
    public abstract class WorkStrategy
    {
        public abstract void DoWork();
    }

    public class TheHardWay : WorkStrategy
    {
        public override void DoWork()
        {
            Console.WriteLine("Doing it the hard way :(");
        }
    }
    public class TheEasyWay : WorkStrategy
    {
        public override void DoWork()
        {
            Console.WriteLine("Take it easy. :)");
        }
    }
}
