using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MEFCatalogDemo
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ////Type Catalog
            //TypeCatalog catalog = new TypeCatalog(typeof(MenuSystem), typeof(SizeUpPlugins), typeof(SizeDownPlugins), typeof(ColorRedPlugin));

            //Assembly Catalog
            AssemblyCatalog catalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());
            CompositionContainer container = new CompositionContainer(catalog);

            MenuSystem form = container.GetExportedValue<MenuSystem>();

            form.ShowDialog();
        }
    }
}
