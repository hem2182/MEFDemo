using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MEFContractsDemo
{
    //[InheritedExport]
    interface IMenuPlugin
    {
        string MenuText { get; }
        void ChangeLabel(Label label);
    }
}
