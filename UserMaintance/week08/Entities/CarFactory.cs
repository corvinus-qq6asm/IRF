using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using week08.Abstractions;
using System.Drawing;

namespace week08.Entities
{
    class CarFactory : Abstractions.IToyFactory
    {
        public Color BallColor { get; set; }
        public Toy CreateNew()
        {
            return new Ball(BallColor);
        }
    }
}
