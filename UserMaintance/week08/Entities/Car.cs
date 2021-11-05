using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace week08.Entities
{
    public class Car : Abstractions.Toy
    {
        protected override void DrawImage(Graphics g)
        {
            Image imagefile = Image.FromFile("Images/kocsika.png");
            g.DrawImage(imagefile, new Rectangle(0, 0, Width, Height));
        }
    }
}
