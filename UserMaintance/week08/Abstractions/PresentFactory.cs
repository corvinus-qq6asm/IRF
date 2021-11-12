using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace week08.Abstractions
{
    class PresentFactory : Abstractions.IToyFactory
    {
        public Color Colour1 { get; set; }
        public Color Colour2 { get; set; }
        public Toy CreateNew()
        {
            return new Present(Colour1, Colour2);
        }
    }
}
