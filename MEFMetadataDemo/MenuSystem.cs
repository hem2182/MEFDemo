﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MEFMetadataDemo
{
    public partial class MenuSystem : Form
    {
        [ImportMany("Size")]
        private IEnumerable<Lazy<IMenuPlugin, IMenuPluginMetadata>> sizePlugins;
        [ImportMany("Color")]
        private IEnumerable<Lazy<IMenuPlugin, IMenuPluginMetadata>> colorPlugins;

        public MenuSystem()
        {
            InitializeComponent();
        }

        private void MenuSystem_Load(object sender, EventArgs e)
        {
            InitializeMenus();
        }

        private void InitializeMenus()
        {
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

        private void menuItem_Click(object sender, EventArgs e)
        {
            Lazy<IMenuPlugin, IMenuPluginMetadata> plugin = ((ToolStripMenuItem)sender).Tag as Lazy<IMenuPlugin, IMenuPluginMetadata>;
            plugin.Value.ChangeLabel(this.lblMainMessage);
        }
    }
}
