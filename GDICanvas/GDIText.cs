using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Text;
using System.Xml;
using System.Xml.Serialization;

namespace GDICanvas
{
    public class GDIText : GDIObject
    {
        public float FontSize { get; set; }

        public bool EmbeddedFont { get; set; }

        public string FontLocation { get; set; }

        public string FontName { get; set; }

        public string FontStyle { get; set; }

        public string Text { get; set; }

        public GDIColor FontColor { get; set; }

        public float Padding { get; set; }

        public bool Border { get; set; }

        public bool WordWrap { get; set; }

        public StringFormatFlags Formatting { get; set; }

        public GDIText() : base()
        {
            Text = "New GDIText";
            FontStyle = "Regular";
            FontSize = 10;
            FontName = "Arial";
            EmbeddedFont = false;
            Border = false;
            Padding = 0;
            WordWrap = false;
        }

        public override void Draw(Graphics g)
        {
            if (!Border)
            {
                LineAlpha = 0;
                LineThickness = 0;
            }

            PrivateFontCollection collection = new PrivateFontCollection();
            FontFamily family = new FontFamily(FontName);

            System.Drawing.FontStyle style = System.Drawing.FontStyle.Regular;
            switch (FontStyle)
            {
                case "Bold":
                    style = System.Drawing.FontStyle.Bold;
                    break;
                case "Italic":
                    style = System.Drawing.FontStyle.Italic;
                    break;
                case "Underline":
                    style = System.Drawing.FontStyle.Underline;
                    break;
                case "Strikeout":
                    style = System.Drawing.FontStyle.Strikeout;
                    break;
                default:
                    style = System.Drawing.FontStyle.Regular;
                    break;
            }

            if (EmbeddedFont)
            {
                collection.AddFontFile(FontLocation);
                FontName = collection.Families[0].Name;
                family = collection.Families[0];
            }

            base.Draw(g);
            
            if (WordWrap)
                g.DrawString(Text, new Font(family, FontSize, style), new SolidBrush(FontColor.ToColor()), new RectangleF(X + LineThickness + Padding, Y + LineThickness + Padding, Width - Padding, Height - Padding), new StringFormat(Formatting));
            else
                g.DrawString(Text, new Font(family, FontSize, style), new SolidBrush(FontColor.ToColor()), X + LineThickness + Padding, Y + LineThickness + Padding);
        }

    }
}
