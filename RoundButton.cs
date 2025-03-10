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
            if (value >= 0)
                borderSize = value; 
        }
    }

    [Category("Custom Appearance")]
    [Description("Change the radius of the border")]
    public int BorderRadius
    {
        get { return borderRadius; }
        set {
            if (value >= 0 && value < this.Height)
                borderRadius = value;
        }
    }

    [Category("Custom Appearance")]
    [Description("Change Border Color")]
    public Color BorderColor
    {
        get { return borderColor; }
        set { borderColor = value; }
    }

    // Constructor
    public RoundButton()
    {
        this.FlatStyle = FlatStyle.Flat;
        this.FlatAppearance.BorderSize = 0;
        this.Size = new Size(2*borderRadius + 50, 2*borderRadius);
        this.BackColor = Color.LightGray;
        this.ForeColor = Color.White;
    }

    // Methods

    private GraphicsPath CreateFigurePath(Rectangle rect, float radius)
    {
        GraphicsPath path = new GraphicsPath();
        path.StartFigure();

        // rect position is defined by the top left corner of the rectangle which is X and Y
        // 180 is the starting of the top left corner
        // So we draw an arc with width and height of radius, starting from 180 to 270
        path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);

        // top right corner
        path.AddArc(rect.Width - radius, rect.Y, radius, radius, 270, 90);

        // bottom right corner
        path.AddArc(rect.Width - radius, rect.Height - radius, radius, radius, 0, 90);

        // bottom left corner
        path.AddArc(rect.X, rect.Height - radius, radius, radius, 90, 90);

        path.CloseFigure();
        return path;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

        Rectangle rectangleSurface = new Rectangle(0, 0, this.Width, this.Height);
        Rectangle rectangleBorder = new Rectangle(1, 1, this.Width - 1, this.Height - 1);

        if (this.borderRadius > 2)
        {
            // using can be use in this way so that the resources are disposed of after the block is executed
            // its helpful in case of memory management
            using (GraphicsPath pathSurface = CreateFigurePath(rectangleSurface, this.borderRadius))
            using (GraphicsPath pathBorder = CreateFigurePath(rectangleBorder, this.borderRadius - 1))
            using (Pen penSurface = new Pen(this.Parent.BackColor, 2))
            using (Pen penBorder = new Pen(this.borderColor, this.borderSize))
            {
                penBorder.Alignment = PenAlignment.Inset;
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
            this.Region = new Region(rectangleSurface);

            if (borderSize >= 1)
            {
                using (Pen penBorder = new Pen(this.borderColor, this.borderSize))
                {
                    penBorder.Alignment = PenAlignment.Inset;
                    e.Graphics.DrawRectangle(penBorder, 0, 0, this.Width - borderSize, this.Height - borderSize);
                }
            }
        }
    }
}
