using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEFMetadataDemo
{
    [MetadataAttribute]
    class MenuPluginMetadataAttribute : Attribute, IMenuPluginMetadata
    {
        public MenuPluginMetadataAttribute(string menuText)
        {
            this.MenuText = menuText;
        }

        public string MenuText
        {
            get;
            private set;
        }


    }
}
