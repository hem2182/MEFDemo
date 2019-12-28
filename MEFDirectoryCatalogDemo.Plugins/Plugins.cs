using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.ComponentModel.Composition;
using MEFDirectoryCatalogDemo.Contracts;
using System.Windows.Forms;

namespace MEFDirectoryCatalogDemo.Plugins
{
    [Export("Size", typeof(IMenuPlugin))]
    [MenuPluginMetadata("Size +10")]
    class SizeUpPlugin : IMenuPlugin
    {
        public void ChangeLabel(
            Label label)
        {
            label.Font = new Font(label.Font.FontFamily, label.Font.Size + 10.0f);
        }
    }

    [Export("Size", typeof(IMenuPlugin))]
    [MenuPluginMetadata("Size -10")]
    class SizeDownPlugin : IMenuPlugin
    {
        public void ChangeLabel(Label label)
        {
            label.Font = new Font(label.Font.FontFamily, label.Font.Size - 10.0f);
        }
    }

    [Export("Color", typeof(IMenuPlugin))]
    [MenuPluginMetadata("Red")]
    class ColorRedPlugin : IMenuPlugin
    {
        public void ChangeLabel(Label label)
        {
            label.ForeColor = Color.Red;
        }
    }

    [Export("Color", typeof(IMenuPlugin))]
    [MenuPluginMetadata("Blue")]
    class ColorBluePlugin : IMenuPlugin
    {
        public void ChangeLabel(Label label)
        {
            label.ForeColor = Color.Blue;
        }
    }
}
