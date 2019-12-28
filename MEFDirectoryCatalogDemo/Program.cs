using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MEFDirectoryCatalogDemo
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //after removing the filter to load everything, we don't have to worry about MEF loading every file.
            DirectoryCatalog directorycatalog = new DirectoryCatalog(@".\");
            TypeCatalog typeCatalog = new TypeCatalog(typeof(MenuSystem));
            AggregateCatalog catalog = new AggregateCatalog(directorycatalog, typeCatalog);

            //We can continue to add more plugins if we want. for example, if user wants to configure multiple 
            // plugin folders, we can support that using the aggregate catalog. 

            CompositionContainer container = new CompositionContainer(catalog);

            MenuSystem form = container.GetExportedValue<MenuSystem>();

            form.ShowDialog();
        }
    }
}
