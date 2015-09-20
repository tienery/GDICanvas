using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Web.Helpers;
using System.Xml;
using System.Xml.Serialization;

namespace GDICanvas
{
    public class Canvas
    {

        public List<GDIObject> Objects;

        public Color BackgroundColor;

        public Point GridCellSize { get; set; }

        public bool ShowGrid { get; set; }

        public Pen GridColor { get; set; }

        public float RenderWidth;

        public float RenderHeight;

        public float CanvasX;

        public float CanvasY;

        public Canvas(Graphics g = null)
        {
            Objects = new List<GDIObject>();

            GridCellSize = new Point(32, 32);
            ShowGrid = false;
            GridColor = Pens.Black;

            BackgroundColor = Color.White;
        }

        public void Draw(Graphics g)
        {
            g.Clear(BackgroundColor);

            g.SetClip(new Rectangle(0, 0, (int)RenderWidth, (int)RenderHeight));

            foreach (GDIObject obj in Objects)
            {
                obj.X -= CanvasX;
                obj.Y -= CanvasY;

                obj.Draw(g);
            }

            g.ResetClip();

            if (ShowGrid)
            {
                for (var x = 0; x < RenderWidth + CanvasX; x++)
                {
                    if (x % GridCellSize.X == 0)
                        g.DrawLine(GridColor, x - CanvasX, 0, x - CanvasX, RenderHeight);
                }

                for (var y = 0; y < RenderHeight + CanvasY; y++)
                {
                    if (y % GridCellSize.Y == 0)
                        g.DrawLine(GridColor, 0, y - CanvasY, RenderWidth, y - CanvasY);
                }
            }
        }

        #region Utility Functions

        public GDIObject GetObjectAtPoint(Point p, bool ignoreChildren = true)
        {
            foreach (GDIObject obj in Objects)
            {
                if (p.X > obj.X && p.Y > obj.Y &&
                    p.X <= obj.X + obj.Width && p.Y <= obj.Y + obj.Height)
                {
                    if (obj.Parent != null && ignoreChildren)
                    {
                        return getRootObject(obj.Parent);
                    }
                    else
                    {
                        return obj;
                    }
                }
            }

            return null;
        }

        public List<GDIObject> GetObjectsUnderPoint(PointF pf, string direction = "both", bool ignoreChildren = true)
        {
            var objects = new List<GDIObject>();

            foreach (GDIObject obj in Objects)
            {
                if (ignoreChildren)
                    if (obj.Parent != null)
                        continue;

                switch (direction)
                {
                    case "both":
                        {
                            if (obj.X >= pf.X && obj.Y >= pf.Y)
                                objects.Add(obj);
                        } break;
                    case "down":
                        {
                            if (obj.Y >= pf.Y)
                                objects.Add(obj);
                        } break;
                    case "right":
                        {
                            if (obj.X >= pf.X)
                                objects.Add(obj);
                        } break;
                    default:
                        return null;
                }
            }

            return objects;
        }

        protected GDIObject getRootObject(GDIObjectContainer obj)
        {
            if (obj.Parent != null)
                return getRootObject(obj.Parent);
            else
                return obj;
        }

        public List<GDIObject> GetObjects(RectangleF rc, GDICanvasSelectionMode selectMode = GDICanvasSelectionMode.Normal)
        {
            var objs = new List<GDIObject>();

            foreach (GDIObject obj in Objects)
            {
                switch (selectMode)
                {
                    case GDICanvasSelectionMode.Strict:
                        {
                            if (obj.X + obj.Width < rc.X + rc.Width &&
                                obj.Y + obj.Height < rc.Y + rc.Height &&
                                obj.X > rc.X && obj.Y > rc.Y)
                                objs.Add(obj);

                        } break;
                    case GDICanvasSelectionMode.Normal:
                        {
                            if ((obj.X + obj.Width > rc.X && obj.Y + obj.Height > rc.Y) ||
                                (obj.X < rc.X + rc.Width && obj.Y < rc.Y + rc.Height))
                                objs.Add(obj);
                        } break;
                }
            }

            return objs;
        }

        public int GetMaxWidth()
        {
            int width = 0;
            foreach (var obj in Objects)
            {
                var val = obj.X + obj.Width;
                if (val > width)
                    width = (int)val;
            }
            return width;
        }

        public int GetMaxHeight()
        {
            int height = 0;
            foreach (var obj in Objects)
            {
                var val = obj.Y + obj.Height;
                if (val > height)
                    height = (int)val;
            }
            return height;
        }

#endregion

    }

    public enum GDICanvasSelectionMode
    {
        /// <summary>
        /// The selection rectangle must cover an entire object for it to be added to the returning list.
        /// </summary>
        Strict,
        /// <summary>
        /// The selection rectangle only needs to intersect a portion of an object to be added to the returning list.
        /// </summary>
        Normal
    }

}
