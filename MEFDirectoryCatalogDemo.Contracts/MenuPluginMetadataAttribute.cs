using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEFDirectoryCatalogDemo.Contracts
{
    [MetadataAttribute]
    public class MenuPluginMetadataAttribute : Attribute, IMenuPluginMetadata
    {
        public string MenuText { get; private set; }

        public MenuPluginMetadataAttribute(string menuText)
        {
            this.MenuText = menuText;
        }
    }
}
