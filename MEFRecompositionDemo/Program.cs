using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MEFRecompositionDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Painter painter = new Painter();

            //Addng only 3 colors. 
            CompositionContainer container = new CompositionContainer();
            container.ComposeParts(new Red(), new Blue(), new Green ());
            container.SatisfyImportsOnce(painter);

            painter.Paint();

            //Adding the last color
            container.ComposeParts(new Yellow());

            painter.Paint();
            
            Console.ReadKey();
        }
    }

    public class Painter : IPartImportsSatisfiedNotification
    {
        [ImportMany(AllowRecomposition = false)]
        private IEnumerable<Color> pallette;

        public void OnImportsSatisfied()
        {
            Console.WriteLine("I have been composed.");
            if (pallette.Count() < 1)
            {
                throw new InvalidOperationException("No colors were added to the pallette");
            }
        }

        public void Paint()
        {
            Console.WriteLine("Painting with {0} colors", pallette.Count());
        }

    }

    [InheritedExport]
    public class Color { }

    public class Red : Color { }
    public class Blue : Color { }
    public class Green : Color { }
    public class Yellow : Color { }
}
