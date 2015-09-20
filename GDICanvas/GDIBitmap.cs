using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

namespace GDICanvas
{
    public class GDIBitmap : GDIObject
    {

        public string ImageLocation { get; set; }

        public float ScaleX { get; set; }

        public float ScaleY { get; set; }

        private string previousLocation;
        private Image img;

        public GDIBitmap(GDIObjectContainer parent = null) : base(parent)
        {
            ScaleY = 1;
            ScaleX = 1;


        }

        public override void Draw(Graphics g)
        {
            if (ImageLocation != "" && previousLocation != ImageLocation)
            {
                if (File.Exists(ImageLocation))
                {
                    img = Image.FromFile(ImageLocation);
                    previousLocation = ImageLocation;
                }
            }

            if (img != null)
            {
                if (ScaleX > 1 || ScaleY > 1 || ScaleX < 1 || ScaleY < 1)
                    g.DrawImage(img, X, Y, img.Width * ScaleX, img.Height * ScaleY);
                else
                    g.DrawImageUnscaled(img, (int)X, (int)Y);
            }
        }

    }
}
