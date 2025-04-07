using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.ComponentModel;

public class RoundButton_39_Toan: Button
{
    // Fields
    private int borderSize_39_Toan = 0;
    private int borderRadius_39_Toan = 20;
    private Color borderColor_39_Toan = Color.Black;

    // Properties
    [Category("Custom Appearance")]
    [Description("Change Border Size")]
    public int BorderSize_39_Toan
    {
        get { return borderSize_39_Toan; }
        set {
            borderSize_39_Toan = value;
            this.Invalidate();
        }
    }

    [Category("Custom Appearance")]
    [Description("Change the radius of the border")]
    public int BorderRadius_39_Toan
    {
        get { return borderRadius_39_Toan; }
        set {
            borderRadius_39_Toan = value;
            this.Invalidate();
        }
    }

    [Category("Custom Appearance")]
    [Description("Change Border Color")]
    public Color BorderColor_39_Toan
    {
        get { return borderColor_39_Toan; }
        set { 
            borderColor_39_Toan = value;
            this.Invalidate();
        }
    }

    // Constructor
    public RoundButton_39_Toan()
    {
        this.FlatStyle = FlatStyle.Flat;
        this.FlatAppearance.BorderSize = 0;
        this.Size = new Size(150, 40);
        this.BackColor = Color.LightGray;
        this.ForeColor = Color.White;
        this.Resize += (sender, e) =>
        {
            if (borderRadius_39_Toan > this.Height)
                borderRadius_39_Toan = this.Height;
        };
    }

    // Methods

    private GraphicsPath CreateFigurePath_39_Toan(Rectangle rect, float radius)
    {
        GraphicsPath path_39_Toan = new GraphicsPath();
        path_39_Toan.StartFigure();

        float curveSize_39_Toan = radius * 2F;

        // rect position is defined by the top left corner of the rectangle which is X and Y
        // 180 is the starting of the top left corner
        // So we draw an arc with width and height of radius, starting from 180 to 270
        path_39_Toan.AddArc(rect.X, rect.Y, curveSize_39_Toan, curveSize_39_Toan, 180, 90);

        // top right corner
        path_39_Toan.AddArc(rect.Width - curveSize_39_Toan, rect.Y, curveSize_39_Toan, curveSize_39_Toan, 270, 90);

        // bottom right corner
        path_39_Toan.AddArc(rect.Width - curveSize_39_Toan, rect.Height - curveSize_39_Toan, curveSize_39_Toan, curveSize_39_Toan, 0, 90);

        // bottom left corner
        path_39_Toan.AddArc(rect.X, rect.Height - curveSize_39_Toan, curveSize_39_Toan, curveSize_39_Toan, 90, 90);
        
        path_39_Toan.CloseFigure();
        return path_39_Toan;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        Rectangle rectangleSurface_39_Toan = this.ClientRectangle;
        Rectangle rectangleBorder_39_Toan = Rectangle.Inflate(rectangleSurface_39_Toan, - borderSize_39_Toan / 2, - borderSize_39_Toan / 2);

        if (this.borderRadius_39_Toan > 2)
        {
            // using can be use in this way so that the resources are disposed of after the block is executed
            // its helpful in case of memory management
            using (GraphicsPath pathSurface_39_Toan = CreateFigurePath_39_Toan(rectangleSurface_39_Toan, borderRadius_39_Toan))
            using (GraphicsPath pathBorder_39_Toan = CreateFigurePath_39_Toan(rectangleBorder_39_Toan, borderRadius_39_Toan - borderSize_39_Toan))
            using (Pen penSurface_39_Toan = new Pen(this.Parent.BackColor, borderSize_39_Toan))
            using (Pen penBorder_39_Toan = new Pen(borderColor_39_Toan, borderSize_39_Toan))
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                this.Region = new Region(pathSurface_39_Toan);
                e.Graphics.DrawPath(penSurface_39_Toan, pathSurface_39_Toan);

                if (borderSize_39_Toan >= 1)
                {
                    e.Graphics.DrawPath(penBorder_39_Toan, pathBorder_39_Toan);
                }
            }
        }
        else
        {
            e.Graphics.SmoothingMode = SmoothingMode.None;
            this.Region = new Region(rectangleSurface_39_Toan);

            if (borderSize_39_Toan >= 1)
            {
                using (Pen penBorder_39_Toan = new Pen(borderColor_39_Toan, borderSize_39_Toan))
                {
                    penBorder_39_Toan.Alignment = PenAlignment.Inset;
                    e.Graphics.DrawRectangle(penBorder_39_Toan, 0, 0, this.Width - 1, this.Height - 1);
                }
            }
        }
    }

    protected override void OnHandleCreated(EventArgs e)
    {
        base.OnHandleCreated(e);
        this.Parent.BackColorChanged += new EventHandler(Container_BackColorChanged_39_Toan);
    }

    private void Container_BackColorChanged_39_Toan(object sender, EventArgs e)
    {
        this.Invalidate();
    }

    public static void SetRoundedRegion_39_Toan(Control control, int radius)
    {
        GraphicsPath path_39_Toan = new GraphicsPath();
        Rectangle rect_39_Toan = new Rectangle(0, 0, control.Width, control.Height);
        path_39_Toan.StartFigure();
        path_39_Toan.AddArc(rect_39_Toan.X, rect_39_Toan.Y, radius, radius, 180, 90);
        path_39_Toan.AddArc(rect_39_Toan.Right - radius, rect_39_Toan.Y, radius, radius, 270, 90);
        path_39_Toan.AddArc(rect_39_Toan.Right - radius, rect_39_Toan.Bottom - radius, radius, radius, 0, 90);
        path_39_Toan.AddArc(rect_39_Toan.X, rect_39_Toan.Bottom - radius, radius, radius, 90, 90);
        path_39_Toan.CloseFigure();
        control.Region = new Region(path_39_Toan);
    }
}
