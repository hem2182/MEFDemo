using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MEFMetadataDemo
{
    //class GlobalStrings
    //{
    //    public const string MENUTEXT_KEY = "MenuText";
    //}

    [Export("Size", typeof(IMenuPlugin))]
    [MenuPluginMetadata("Size +10")]
    class SizeUpPlugins : IMenuPlugin
    {
        public void ChangeLabel(Label label)
        {
            label.Font = new System.Drawing.Font(label.Font.FontFamily, label.Font.Size + 10.0f);
        }
    }

    [Export("Size", typeof(IMenuPlugin))]
    [MenuPluginMetadata("Size -10")]
    class SizeDownPlugins : IMenuPlugin
    {
        public void ChangeLabel(Label label)
        {
            label.Font = new System.Drawing.Font(label.Font.FontFamily, label.Font.Size - 10.0f);
        }
    }

    [Export("Color", typeof(IMenuPlugin))]
    [MenuPluginMetadata("Red")]
    class ColorRedPlugin : IMenuPlugin
    {
        public void ChangeLabel(System.Windows.Forms.Label label)
        {
            label.ForeColor = Color.Red;
        }
    }

    [Export("Color", typeof(IMenuPlugin))]
    [MenuPluginMetadata("Blue")]
    class ColorBluePlugin : IMenuPlugin
    {
        public void ChangeLabel(System.Windows.Forms.Label label)
        {
            label.ForeColor = Color.Blue;
        }
    }
}
