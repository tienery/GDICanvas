using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using GDICanvas;

namespace Test
{
    public partial class MainForm : Form
    {

        private Canvas canvas;
        private bool mouseDown;
        private Point previousMouseLocation;

        public MainForm()
        {
            InitializeComponent();

            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);

            canvas = new Canvas();

            canvas.RenderWidth = 300;
            canvas.RenderHeight = 250;

            //TestCaseRectangle();
            //TestCaseText();    
            TestCaseContainer();
            //TestCaseLine();
        }

        private void TestCaseRectangle()
        {
            GDIObject rect = new GDIObject();
            rect.X = 30;
            rect.Y = 30;
            rect.LineThickness = 15;
            rect.Rotation = 45;
            rect.Skew = new PointF(0.3f, 0);

            canvas.Objects.Clear();
            canvas.Objects.Add(rect);
            pnlCanvas.Invalidate();
        }

        private void TestCaseText()
        {
            GDIText text = new GDIText();
            text.Text = "This is an example.";
            text.FontSize = 12;
            text.X = 30;
            text.Y = 30;
            text.FontColor = new GDIColor(0, 0, 0);
            text.WordWrap = true;
            text.Formatting = StringFormatFlags.DirectionVertical;

            canvas.Objects.Clear();
            canvas.Objects.Add(text);
            pnlCanvas.Invalidate();
        }

        private void TestCaseContainer()
        {
            GDIObjectContainer container = new GDIObjectContainer();
            container.Width = 200;
            container.Height = 200;
            container.FillColor = new GDIColor(0, 255, 0);
            container.X = 30;
            container.Y = 30;

            GDIObject objA = new GDIObject(container);
            objA.FillColor = new GDIColor(255, 0, 0);
            objA.X = 170;
            objA.Y = 170;

            canvas.Objects.Clear();
            canvas.Objects.Add(container);
            pnlCanvas.Invalidate();
        }

        private void TestCaseLine()
        {
            GDILine line = new GDILine();
            line.X1 = 5;
            line.Y1 = 5;
            line.X2 = 45;
            line.Y2 = 45;
            line.LineThickness = 2;
            line.LineColor = new GDIColor(0, 0, 0);

            canvas.Objects.Clear();
            canvas.Objects.Add(line);
            pnlCanvas.Invalidate();
        }

        private void pnlCanvas_Paint(object sender, PaintEventArgs e)
        {
            canvas.Draw(e.Graphics);
        }

        private void pnlCanvas_MouseEnter(object sender, EventArgs e)
        {

        }

        private void pnlCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            previousMouseLocation = e.Location;
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                mouseDown = true;
                Cursor = Cursors.SizeAll;
            }
        }

        private void pnlCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Default;
        }
    }
}
