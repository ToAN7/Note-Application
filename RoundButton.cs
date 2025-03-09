using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;

public class RoundButton: Button
{
    // Fields
    private int borderSize = 0;
    private int borderRadius = 20;
    private Color borderColor = Color.Black;

    // Properties
    public int BorderSize
    {
        get { return borderSize; }
        set {
            if (value >= 0)
                borderSize = value; 
        }
    }

    public int BorderRadius
    {
        get { return borderRadius; }
        set { 
            if (value >= 0)
                borderRadius = value;
        }
    }

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

        Rectangle rectangleSurface = new Rectangle(0, 0, this.Width, this.Height);
        Rectangle rectangleBorder = new Rectangle(this.borderSize, this.borderSize, this.Width - this.borderSize, this.Height - this.borderSize);

        if (this.borderRadius > 2)
        {
            using (GraphicsPath pathSurface = CreateFigurePath(rectangleSurface, this.borderRadius))
            using (GraphicsPath pathBorder = CreateFigurePath(rectangleBorder, this.borderRadius - 1))
            using (Pen penSurface = new Pen(this.Parent.BackColor, this.borderSize))
            using (Pen penBorder = new Pen(this.borderColor, this.borderSize))
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                e.Graphics.DrawPath(penSurface, pathSurface);
                e.Graphics.DrawPath(penBorder, pathBorder);
            }
        }
        else
        {
            e.Graphics.DrawEllipse(new Pen(this.borderColor, this.borderSize), rectangleBorder);
            e.Graphics.FillEllipse(new SolidBrush(this.Parent.BackColor), rectangleSurface);
        }
    }
}
