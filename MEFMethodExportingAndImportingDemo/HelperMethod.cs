using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MEFMethodExportingAndImportingDemo
{
    [Export("CalculationParser")]
    class HelperMethod
    {
        [Export("CalculationParser")]
        public CalculationModal Parse(string input)
        {
            Regex regex = new Regex(@"(\d+)(.)(\d+)");
            Match match = regex.Match(input);

            CalculationModal calculation = new CalculationModal()
            {
                Input1 = int.Parse(match.Groups[1].Value),
                Input2 = int.Parse(match.Groups[3].Value),
                Operation = match.Groups[2].Value
            };

            return calculation;
        }
    }
}
