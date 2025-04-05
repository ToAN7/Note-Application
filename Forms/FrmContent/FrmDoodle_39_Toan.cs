using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NoteApp.Forms.FrmContent
{
    public partial class FrmDoodle_39_Toan : Form
    {
        private List<PointF> getLocation_39_Toan = new List<PointF>();

        private int lineSize_39_Toan = 0;
        private List<String> lines_39_Toan = new List<String>();

        private Pen pDoodle_39_Toan = new Pen(Color.FromArgb(255,0,0),3);

        private String filePath_39_Toan = "";
        private String fileName_39_Toan = "DefaultDoodle" + DateTime.Now.ToString("ddMMyyyy_HHmmss");

        public FrmDoodle_39_Toan()
        {
            InitializeComponent();
        }

        // Properties
        public List<PointF> GetLocation_39_Toan
        {
            get { return getLocation_39_Toan; }
            set { 
                getLocation_39_Toan = value;
                // Raise the Paint event
                this.Invalidate();
            }
        }

        public String FilePath
        {
            get { return filePath_39_Toan; }
            set { filePath_39_Toan = value; }
        }

        public String FileName
        {
            get { return fileName_39_Toan; }
            set { fileName_39_Toan = value; }
        }

        [Category("Custom Pen Properties")]
        public Color PenColor
        {
            get { return pDoodle_39_Toan.Color; }
            set {
                pDoodle_39_Toan.Color = value;
                this.Invalidate();
            }
        }

        [Category("Custom Pen Properties")]
        public float PenWidth
        {
            get { return pDoodle_39_Toan.Width; }
            set {
                pDoodle_39_Toan.Width = value;
                this.Invalidate();
            }
        }

        // Methods
        private void FrmDoodle_39_Toan_MouseDown(object sender, MouseEventArgs e)
        {
            if (getLocation_39_Toan.Capacity > 0)
            {
                getLocation_39_Toan.Clear();
            }
            getLocation_39_Toan.Add(e.Location);
        }


        // Make fast smooth line_39_Toan drawing
        private void FrmDoodle_39_Toan_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                getLocation_39_Toan.Add(e.Location);
                int size_39_Toan = getLocation_39_Toan.Count;

                this.CreateGraphics().DrawLine(pDoodle_39_Toan, getLocation_39_Toan[size_39_Toan - 2], getLocation_39_Toan[size_39_Toan - 1]);
            }
        }

        private void FrmDoodle_39_Toan_MouseUp(object sender, MouseEventArgs e)
        {
            if (lineSize_39_Toan < lines_39_Toan.Count)
            {
                lines_39_Toan.RemoveRange(lineSize_39_Toan, lines_39_Toan.Count - lineSize_39_Toan);
            }
            lines_39_Toan.Add(ConvertGetLocationToString_39_Toan());
            lineSize_39_Toan++;
        }

        // I noticed that the Paint event raised when the form is loaded or leaved
        // Before, I do not use Paint event, instead I use RePaintDoodle method
        // This does not work, because the Paint event is not raised
        private void FrmDoodle_39_Toan_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            List<PointF> readLocations_39_Toan = new List<PointF>();
            if (lines_39_Toan.Count == 0)
            {
                return;
            }
            for (int i = 0; i < lineSize_39_Toan; i++)
            {
                String[] points_39_Toan = lines_39_Toan[i].Split('|');
                String[] point_39_Toan = points_39_Toan[0].Split(',');
                readLocations_39_Toan.Add(new PointF(float.Parse(point_39_Toan[0]), float.Parse(point_39_Toan[1])));
                for (int j = 1; j < points_39_Toan.Length; j++)
                {
                    point_39_Toan = points_39_Toan[j].Split(',');
                    readLocations_39_Toan.Add(new PointF(float.Parse(point_39_Toan[0]), float.Parse(point_39_Toan[1])));
                    e.Graphics.DrawLine(pDoodle_39_Toan, readLocations_39_Toan[j-1], readLocations_39_Toan[j]);
                }
                readLocations_39_Toan.Clear();
            }
        }

        private String ConvertGetLocationToString_39_Toan()
        {
            String Content_39_Toan = "";
            for (int i = 0; i < getLocation_39_Toan.Count; i++)
            {
                Content_39_Toan += getLocation_39_Toan[i].X.ToString() + ",";
                Content_39_Toan += getLocation_39_Toan[i].Y.ToString();
                if (i < getLocation_39_Toan.Count - 1)
                {
                    Content_39_Toan += "|";
                }
            }
            return Content_39_Toan;
        }

        // Save the doodle as a custom type file
        public void SaveDoodleAsFile_39_Toan(String Path, String fileName_39_Toan)
        {
            if (Path == "")
            {
                MessageBox.Show("Please select a folder to save the doodle", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.filePath_39_Toan = Path + "\\" + fileName_39_Toan;
            if (fileName_39_Toan != "") {
                this.fileName_39_Toan = fileName_39_Toan;
            }

            File.Create(Path + "\\" + this.fileName_39_Toan + ".doodle").Close();
            if (File.Exists(Path + "\\" + this.fileName_39_Toan + ".doodle"))
            {
                StreamWriter swWriteData_39_Toan = new StreamWriter(Path + "\\" + this.fileName_39_Toan + ".doodle");
                for (int i = 0; i < lineSize_39_Toan; i++)
                {
                    swWriteData_39_Toan.WriteLine(lines_39_Toan[i]);
                }
                swWriteData_39_Toan.Close();
            }
        }

        private void btnUndo_39_Toan_Click(object sender, EventArgs e)
        {
            lineSize_39_Toan--;
            if (lineSize_39_Toan < 0)
            {
                lineSize_39_Toan = 0;
            }
            this.Invalidate();
        }

        private void btnRedo_39_Toan_Click(object sender, EventArgs e)
        {
            lineSize_39_Toan++;
            if (lineSize_39_Toan > lines_39_Toan.Count)
            {
                lineSize_39_Toan = lines_39_Toan.Count;
            }
            this.Invalidate();
        }

        private void FrmDoodle_39_Toan_Load(object sender, EventArgs e)
        {
            btnRedo_39_Toan.Width = btnRedo_39_Toan.Height;
            btnUndo_39_Toan.Width = btnUndo_39_Toan.Height;
            btnRedo_39_Toan.BorderRadius_39_Toan = btnRedo_39_Toan.Height / 2;
            btnUndo_39_Toan.BorderRadius_39_Toan = btnUndo_39_Toan.Height / 2;
            btnClear_39_Toan.BorderRadius_39_Toan = btnClear_39_Toan.Height / 2;
            btnClear_39_Toan.Location = new Point(btnUndo_39_Toan.Location.X + btnUndo_39_Toan.Width + 10, 0);
            lblChooseColor_39_Toan.Location = new Point(btnClear_39_Toan.Location.X + btnClear_39_Toan.Width + 10, 0);
        }

        public void LoadDoodle_39_Toan(String filePath_39_Toan)
        {
            if (filePath_39_Toan == "")
            {
                MessageBox.Show("Please select a file to load the doodle", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            this.filePath_39_Toan = filePath_39_Toan;
            StreamReader srReadData_39_Toan = new StreamReader(filePath_39_Toan);
            String line_39_Toan = "";
            while ((line_39_Toan = srReadData_39_Toan.ReadLine()) != null)
            {
                lines_39_Toan.Add(line_39_Toan);
            }
            lineSize_39_Toan = lines_39_Toan.Count;
            srReadData_39_Toan.Close();
            this.Invalidate();
        }

        private void btnClear_39_Toan_Click(object sender, EventArgs e)
        {
            lineSize_39_Toan = 0;
            this.Invalidate();
        }

        private void lblChooseColor_39_Toan_Click(object sender, EventArgs e)
        {
            DialogResult isSelect_39_Toan = clrChooseColor_39_Toan.ShowDialog();
            if (isSelect_39_Toan == DialogResult.OK)
            {
                PenColor = clrChooseColor_39_Toan.Color;
                lblChooseColor_39_Toan.BackColor = clrChooseColor_39_Toan.Color;
            }
        }
    }
}
