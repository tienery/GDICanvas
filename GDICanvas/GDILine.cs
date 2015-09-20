using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Xml.Serialization;

namespace GDICanvas
{
    public class GDILine : GDIObject
    {

        public float X1 { get; set; }

        public float X2 { get; set; }

        public float Y1 { get; set; }

        public float Y2 { get; set; }

        public GDILine(GDIObjectContainer parent = null) : base(parent)
        {

        }

        public override void Draw(Graphics g)
        {
            if (X2 == X1 || Y2 == Y1)
                return;

            g.DrawLine(new Pen(Color.FromArgb(LineAlpha, LineColor.ToColor()), LineThickness),
                        new PointF(X1, Y1), new PointF(X2, Y2));
        }

    }
}
