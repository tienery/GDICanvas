using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Xml;
using System.Xml.Serialization;

namespace GDICanvas
{

    public class GDIObject
    {

        public float X { get; set; }

        public float Y { get; set; }

        public float Width { get; set; }

        public float Height { get; set; }

        public GDIColor LineColor { get; set; }

        public float LineThickness { get; set; }

        public int LineAlpha { get; set; }

        public GDIColor FillColor { get; set; }

        public int FillAlpha { get; set; }

        public float Rotation { get; set; }

        public PointF Skew { get; set; }

        public GDIObjectContainer Parent { get; private set; }

        public GDIObject(GDIObjectContainer parent = null)
        {
            X = 0;
            Y = 0;
            Width = 100;
            Height = 100;
            LineColor = new GDIColor(0, 0, 0);
            LineThickness = 1;
            LineAlpha = 255;
            FillAlpha = 255;
            FillColor = new GDIColor(255, 0, 0);
            Rotation = 0;
            Skew = PointF.Empty;

            Parent = parent;
            if (Parent != null)
                Parent.Items.Add(this);
        }

        public virtual void Draw(Graphics g)
        {
            Matrix matrix = new Matrix(1, 0, 0, 1, 0, 0);

            if (Rotation > 0)
            {
                matrix = ApplyRotation(matrix);
            }

            if (Skew != PointF.Empty)
            {
                matrix = ApplySkew(matrix);
            }

            g.Transform = matrix;

            g.DrawRectangle(new Pen(Color.FromArgb(LineAlpha, LineColor.ToColor()), LineThickness), X, Y, Width, Height);
            g.FillRectangle(new SolidBrush(Color.FromArgb(FillAlpha, FillColor.ToColor())), X + LineThickness - (LineThickness / 2), Y + LineThickness - (LineThickness / 2), Width - LineThickness, Height - LineThickness);

            g.ResetTransform();
        }

        private Matrix ApplyRotation(Matrix currentMatrix)
        {
            Matrix rotation = new Matrix(1, 0, 0, 1, 0, 0);

            if (currentMatrix != null)
                rotation = currentMatrix;

            PointF rotationPoint = new PointF(X + Width / 2, Y + Height / 2);

            rotation.RotateAt(Rotation, rotationPoint);

            return rotation;
        }
    
        private Matrix ApplySkew(Matrix currentMatrix)
        {
            Matrix skew = new Matrix(1, 0, 0, 1, 0, 0);

            if (currentMatrix != null)
                skew = currentMatrix;

            skew.Shear(Skew.X, Skew.Y);

            return skew;
        }

    }

    public struct GDIColor
    {

        public int Red;

        public int Green;

        public int Blue;

        public GDIColor(int red, int green, int blue)
        {
            Red = red;
            Green = green;
            Blue = blue;
        }

        public Color ToColor()
        {
            return Color.FromArgb(Red, Green, Blue);
        }

    }
}
