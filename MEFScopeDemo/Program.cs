using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MEFScopeDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            AssemblyCatalog catalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());
            CompositionContainer container = new CompositionContainer(catalog);

            var counter1 = container.GetExportedValue<BeanCounter1>();
            var counter2 = container.GetExportedValue<BeanCounter2>();

            counter1.CountBeans();
            counter2.CountBeans();

            //var bag = container.GetExportedValue<BeanBag>();

            //Console.WriteLine(bag.TotalBeans);

            var counter3 = container.GetExportedValue<BeanCounter1>();
            var counter4 = container.GetExportedValue<BeanCounter2>();

            counter3.CountBeans();
            counter4.CountBeans();

            Console.ReadKey();
        }
    }

    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    class BeanCounter1
    {
        [Import]
        private BeanBag myBeanBag;

        public void CountBeans()
        {
            myBeanBag.AddBeans(10);
            Console.WriteLine("Bean Counter 1 has {0} beans", this.myBeanBag.TotalBeans);
        }
    }

    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    class BeanCounter2
    {
        [Import(RequiredCreationPolicy = CreationPolicy.NonShared)]
        private BeanBag myBeanBag;

        public void CountBeans()
        {
            myBeanBag.AddBeans(6);
            Console.WriteLine("Bean Counter 2 has {0} beans", this.myBeanBag.TotalBeans);
        }
    }

    [Export]
    class BeanBag
    {
        public int TotalBeans { get; private set; }

        public BeanBag()
        {
            TotalBeans = 0;
        }

        public void AddBeans(int beans)
        {
            TotalBeans += beans;
        }

        public void RemoveBeans(int beans)
        {
            TotalBeans -= beans;
        }

    }
}
