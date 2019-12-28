using MEFDirectoryCatalogDemo.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MEFDirectoryCatalogDemo.MorePlugins
{
    [Export("Color", typeof(IMenuPlugin))]
    [MenuPluginMetadata("Green")]
    class SizeUpPlugin : IMenuPlugin
    {
        public void ChangeLabel(Label label)
        {
            label.ForeColor = Color.Green;
        }
    }
}
