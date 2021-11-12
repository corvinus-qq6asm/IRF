using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace week08
{
         public  class Present:Abstractions.Toy
    {
        public SolidBrush PresentColour1 { get; set; }
        public SolidBrush PresentColour2 { get; set; }
        public Present(Color colour1, Color colour2)
        {
            PresentColour1 = new SolidBrush(colour1);
            PresentColour2 = new SolidBrush(colour2);
        }

        protected override void DrawImage(Graphics g)
        {
            g.FillRectangle(PresentColour1,0,0,Width,Height);
            g.FillRectangle(PresentColour1, 0, (Height/5)*2, Width, Height/5);
            g.FillRectangle(PresentColour1, 0, (Width/5)*2, Width/5, Height);
        }


    }
}
