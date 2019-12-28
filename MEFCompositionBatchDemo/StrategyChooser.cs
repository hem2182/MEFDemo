using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEFCompositionBatchDemo
{
    public class StrategyChooser
    {
        public static WorkStrategy Choose()
        {
            if (DateTime.Now.Second % 2 == 0)
            {
                return new TheHardWay();
            }
            else {
                return new TheEasyWay();
            }
        }
    }
}
