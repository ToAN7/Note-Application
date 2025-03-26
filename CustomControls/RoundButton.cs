using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.ComponentModel;

public class RoundButton: Button
{
    // Fields
    private int borderSize = 0;
    private int borderRadius = 20;
    private Color borderColor = Color.Black;

    // Properties
    [Category("Custom Appearance")]
    [Description("Change Border Size")]
    public int BorderSize
    {
        get { return borderSize; }
        set {
            borderSize = value;
            this.Invalidate();
        }
    }

    [Category("Custom Appearance")]
    [Description("Change the radius of the border")]
    public int BorderRadius
    {
        get { return borderRadius; }
        set {
            borderRadius = value;
            this.Invalidate();
        }
    }

    [Category("Custom Appearance")]
    [Description("Change Border Color")]
    public Color BorderColor
    {
        get { return borderColor; }
        set { 
            borderColor = value;
            this.Invalidate();
        }
    }

    // Constructor
    public RoundButton()
    {
        this.FlatStyle = FlatStyle.Flat;
        this.FlatAppearance.BorderSize = 0;
        this.Size = new Size(150, 40);
        this.BackColor = Color.LightGray;
        this.ForeColor = Color.White;
        this.Resize += (sender, e) =>
        {
            if (borderRadius > this.Height)
                borderRadius = this.Height;
        };
    }

    // Methods

    private GraphicsPath CreateFigurePath(Rectangle rect, float radius)
    {
        GraphicsPath path = new GraphicsPath();
        path.StartFigure();

        float curveSize = radius * 2F;

        // rect position is defined by the top left corner of the rectangle which is X and Y
        // 180 is the starting of the top left corner
        // So we draw an arc with width and height of radius, starting from 180 to 270
        path.AddArc(rect.X, rect.Y, curveSize, curveSize, 180, 90);

        // top right corner
        path.AddArc(rect.Width - curveSize, rect.Y, curveSize, curveSize, 270, 90);

        // bottom right corner
        path.AddArc(rect.Width - curveSize, rect.Height - curveSize, curveSize, curveSize, 0, 90);

        // bottom left corner
        path.AddArc(rect.X, rect.Height - curveSize, curveSize, curveSize, 90, 90);
        
        path.CloseFigure();
        return path;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        Rectangle rectangleSurface = this.ClientRectangle;
        Rectangle rectangleBorder = Rectangle.Inflate(rectangleSurface, - this.borderSize / 2, - this.borderSize / 2);

        if (this.borderRadius > 2)
        {
            // using can be use in this way so that the resources are disposed of after the block is executed
            // its helpful in case of memory management
            using (GraphicsPath pathSurface = CreateFigurePath(rectangleSurface, this.borderRadius))
            using (GraphicsPath pathBorder = CreateFigurePath(rectangleBorder, this.borderRadius - this.borderSize))
            using (Pen penSurface = new Pen(this.Parent.BackColor, this.borderSize))
            using (Pen penBorder = new Pen(this.borderColor, this.borderSize))
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                this.Region = new Region(pathSurface);
                e.Graphics.DrawPath(penSurface, pathSurface);

                if (borderSize >= 1)
                {
                    e.Graphics.DrawPath(penBorder, pathBorder);
                }
            }
        }
        else
        {
            e.Graphics.SmoothingMode = SmoothingMode.None;
            this.Region = new Region(rectangleSurface);

            if (borderSize >= 1)
            {
                using (Pen penBorder = new Pen(this.borderColor, this.borderSize))
                {

                    penBorder.Alignment = PenAlignment.Inset;
                    e.Graphics.DrawRectangle(penBorder, 0, 0, this.Width - 1, this.Height - 1);
                }
            }
        }
    }

    protected override void OnHandleCreated(EventArgs e)
    {
        base.OnHandleCreated(e);
        this.Parent.BackColorChanged += new EventHandler(Container_BackColorChanged);
    }

    private void Container_BackColorChanged(object sender, EventArgs e)
    {
        this.Invalidate();
    }
}
