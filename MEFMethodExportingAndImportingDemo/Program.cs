using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MEFMethodExportingAndImportingDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            AssemblyCatalog catalog = new AssemblyCatalog(Assembly.GetExecutingAssembly());
            CompositionContainer container = new CompositionContainer(catalog);

            var calculator = container.GetExportedValue<Calculator>();

            while (true)
            {
                Console.Write("Enter calculation: ");
                string input = Console.ReadLine();
                Console.WriteLine("Result {0} ", calculator.Calculate(input));
            }

        }
    }

    [Export]
    class Calculator
    {
        [ImportMany]
        IEnumerable<Lazy<Func<int, int, int>, Dictionary<string, object>>> computationMethods;

        [Import("CalculationParser")]
        Func<string, CalculationModal> parseCalculation;

        public int Calculate(string input)
        {
            int result = 0;

            var calculation = parseCalculation(input);
            try
            {
                var actionToRun = computationMethods.First(c => c.Metadata["Op"].ToString().Equals(calculation.Operation));
                result = actionToRun.Value(calculation.Input1, calculation.Input2);
            }
            catch {

            }
            return result;
        }
    }
}
