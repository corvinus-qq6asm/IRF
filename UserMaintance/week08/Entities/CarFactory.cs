using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using week08.Abstractions;

namespace week08.Entities
{
    class CarFactory : Abstractions.IToyFactory
    {
        public Toy CreateNew()
        {
            return new Ball();
        }
    }
}
