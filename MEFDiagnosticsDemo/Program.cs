using Microsoft.ComponentModel.Composition.Diagnostics;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MEFDiagnosticsDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            AssemblyCatalog catalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());
            CompositionContainer container = new CompositionContainer(catalog);

            try
            {
                container.GetExportedValue<PluginConsumer>();
            }
            catch
            {
                CompositionInfo ci = new CompositionInfo(catalog, container);
                //we can use the GetPartDefinitionInfo to examine parts for potential issues.We can see if a part was rejected,
                // if its the primary rejection, then we can ask it to return a potential root causes. 
                var partDef = ci.GetPartDefinitionInfo(typeof(PluginConsumer));
                var possibleCauses = partDef.FindPossibleRootCauses();

                //If you want to get a detailed analysis then we can use the Write method provided by the 
                // CompositionInforTextFormatter
                CompositionInfoTextFormatter.Write(ci, Console.Out);
            }

            Console.ReadKey();
        }
    }

    [InheritedExport]
    public interface IPlugin { }

    class PluginA : IPlugin { }

    class PluginB : IPlugin { }

    [Export]
    class PluginConsumer
    {
        [Import("Version 2.0")]
        IPlugin singlePlugin;
    }
}
