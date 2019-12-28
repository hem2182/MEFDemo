using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MEFContractsDemo
{
    [Export("Size", typeof(IMenuPlugin))]
    class SizeUpPlugins : IMenuPlugin
    {
        public string MenuText { get { return "Size +10";  } }

        public void ChangeLabel(Label label)
        {
            label.Font = new System.Drawing.Font(label.Font.FontFamily, label.Font.Size + 10.0f);
        }
    }

    [Export("Size", typeof(IMenuPlugin))]
    class SizeDownPlugins : IMenuPlugin
    {
        public string MenuText { get { return "Size -10"; } }

        public void ChangeLabel(Label label)
        {
            label.Font = new System.Drawing.Font(label.Font.FontFamily, label.Font.Size - 10.0f);
        }
    }

    [Export("Color",typeof(IMenuPlugin))]
    class ColorRedPlugin : IMenuPlugin
    {
        public string MenuText
        {
            get { return "Red"; }
        }

        public void ChangeLabel(System.Windows.Forms.Label label)
        {
            label.ForeColor = Color.Red;
        }
    }

    [Export("Color", typeof(IMenuPlugin))]
    class ColorBluePlugin : IMenuPlugin
    {
        public string MenuText
        {
            get { return "Blue"; }
        }

        public void ChangeLabel(System.Windows.Forms.Label label)
        {
            label.ForeColor = Color.Blue;
        }
    }
}
