using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace GDICanvas
{
    public class GDIObjectContainer : GDIObject
    {

        public List<GDIObject> Items { get; set; }

        public int ScrollX { get; set; }

        public int ScrollY { get; set; }

        public GDIObjectContainer(GDIObjectContainer parent = null) : base(parent)
        {
            Items = new List<GDIObject>();
            ScrollX = 0;
            ScrollY = 0;
        }

        public override void Draw(Graphics g)
        {   
            base.Draw(g);

            var bounds = g.ClipBounds;

            g.SetClip(new RectangleF(X + LineThickness, Y + LineThickness, Width - LineThickness, Height - LineThickness));
            
            foreach (GDIObject obj in Items)
            {
                obj.X += X + ScrollX;
                obj.Y += Y + ScrollY;

                obj.Draw(g);
            }

            g.SetClip(bounds);
        }

    }

}
