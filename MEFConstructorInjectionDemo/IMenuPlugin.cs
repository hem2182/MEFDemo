using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MEFConstructorInjectionDemo
{
    public interface IMenuPlugin
    {
        void ChangeLabel(Label label);
    }
}
