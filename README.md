# GDICanvas
GDICanvas is a set of utilities that allow drawing GDI+ graphics in an object-oriented fashion.

# Building from Source
Open the *.sln and build the GDICanvas project. You can use the Test project to see samples of this canvas in action.

Once built, you can use GDICanvas.dll in your own projects, commercial or otherwise, using any license of your choosing.

# Using the Library
The `Canvas` class is a simple class that accepts drawing routines exposed from a Paint event. You need to use a Paint event and pass in `e.Graphics` to draw objects.

    using GDICanvas;
    
    public partial class MainForm : Form
    {
        private Canvas canvas;
        
        public MainForm()
        {
            InitializeComponent();
            
            canvas = new Canvas();
            
            canvas.RenderWidth = 800;
            canvas.RenderHeight = 600;
        }
        
        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            canvas.Draw(e.Graphics);
        }
    }

The `RenderWidth` and `RenderHeight` need to be set, otherwise you will not see any results. This will automatically clip the region of the `Canvas` inside of the painted control.

There are other properties of the Canvas that may have some value to you:

 * `CanvasX` and `CanvasY` allows you to specify the location of the rendered surface. Positive values bring objects closer to 0,0 while negative pulls them away.
 * `ShowGrid` allows you to display a grid, which is relative to the CanvasX and CanvasY properties. You can specify the size of cells using `GridCellSize`, and specify the line color with `GridColor`.
 * `BackgroundColor` specifies what to clear the graphics with.

The canvas renders any objects you pass into the `Objects` variable of the `Canvas`. There are some basic components currently available:

 * GDIObject - The base class of all objects that can be drawn to the canvas, also draws basic rectangles and some basic rotations/skewing.
 * GDIText - Represents Text that can have a bounding rectangles, wrapping, different directions, and more.
 * GDILine - A basic line drawn from one point to another.
 * GDIBitmap - Represents an image that can be resized.
 * GDIObjectContainer - This is a container that can contain other GDIObject's.

There are several utility functions that the `Canvas` has to make life a little easier:

 * GetObjectAtPoint - Returns an object at a given point. Children are ignored by default, which means objects in a container will return it's root instead of itself.
 * GetObjectsUnderPoint - Returns a `List<GDIObject>` under a given point. By default, this will return objects in both directions: down and right. Or you can specify either `down` or `right` to only get objects from that point.
 * GetObjects - Returns a `List<GDIObject>` within a rectangular region, such as when selecting objects. You can set the selection mode, which by default is Normal, returning objects that intersect within the rectangle as well as those within. Strict only returns objects within the bounds of the region.

## Rectangles
Example code:

    GDIObject rect = new GDIObject();
    rect.X = 30;
    rect.Y = 30;
    rect.LineThickness = 15;
    rect.Rotation = 45;
    rect.Skew = new PointF(0.3f, 0);
    
    canvas.Objects.Add(rect);
    pnlCanvas.Invalidate(); //Invalidate so the painted control is redrawn

### Result
![Image](http://i.imgur.com/8GOsCHz.png)

## Text
Example Code:

    GDIText text = new GDIText();
    text.Text = "This is an example.";
    text.FontSize = 12;
    text.X = 30;
    text.Y = 30;
    text.FontColor = new GDIColor(0, 0, 0);
    text.WordWrap = true;
    text.Formatting = StringFormatFlags.DirectionVertical;

    canvas.Objects.Add(text);
    pnlCanvas.Invalidate();

### Result
![Image](http://i.imgur.com/6TbEmpq.png)

## Containers
Example Code:

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

### Result
![Image](http://i.imgur.com/R3lTVYN.png)
