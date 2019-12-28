using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel.Composition;
using MEFDirectoryCatalogDemo.Contracts;

namespace MEFDirectoryCatalogDemo
{
    [Export]
    public partial class MenuSystem : Form, IPartImportsSatisfiedNotification
    {
        [ImportMany("Size", AllowRecomposition = true)]
        private IEnumerable<Lazy<IMenuPlugin, IMenuPluginMetadata>> sizePlugins;
        [ImportMany("Color", AllowRecomposition = true)]
        private IEnumerable<Lazy<IMenuPlugin, IMenuPluginMetadata>> colorPlugins;

        public MenuSystem()
        {
            InitializeComponent();

        }

        private void MenuSystem_Load(object sender, EventArgs e)
        {

        }

        private void InitializeMenus()
        {
            menuStrip1.Items.Clear();

            var sizeMenu = new ToolStripMenuItem("Size");
            var colorMenu = new ToolStripMenuItem("Color");

            foreach (var p in sizePlugins)
            {
                sizeMenu.DropDownItems.Add(BuildMenuItem(p));
            }

            foreach (var p in colorPlugins)
            {
                colorMenu.DropDownItems.Add(BuildMenuItem(p));
            }

            menuStrip1.Items.Add(sizeMenu);
            menuStrip1.Items.Add(colorMenu);
        }

        private ToolStripMenuItem BuildMenuItem(Lazy<IMenuPlugin, IMenuPluginMetadata> itemDefinition)
        {
            ToolStripMenuItem menuItem = new ToolStripMenuItem();
            menuItem.Text = itemDefinition.Metadata.MenuText;
            menuItem.Click += new EventHandler(menuItem_Click);
            menuItem.Tag = itemDefinition;

            return menuItem;
        }

        void menuItem_Click(object sender, EventArgs e)
        {
            Lazy<IMenuPlugin, IMenuPluginMetadata> plugin = ((ToolStripMenuItem)sender).Tag as Lazy<IMenuPlugin, IMenuPluginMetadata>;
            plugin.Value.ChangeLabel(this.lblMainMessage);
        }

        public void OnImportsSatisfied()
        {
            InitializeMenus();
        }
    }
}
