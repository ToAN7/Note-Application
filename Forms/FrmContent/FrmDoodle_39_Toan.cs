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
        private bool isSave = false;
        private List<PointF> getLocation = new List<PointF>();

        private int lineSize = 0;
        private List<String> lines = new List<String>();

        private Pen pDoodle = new Pen(Color.Red, 3);

        private String filePath = "";
        private String fileName = "DefaultDoodle" + DateTime.Now.ToString("ddMMyyyy_HHmmss");

        public FrmDoodle_39_Toan()
        {
            InitializeComponent();
        }

        // Properties
        public List<PointF> GetLocation
        {
            get { return getLocation; }
            set { 
                getLocation = value;
                // Raise the Paint event
                this.Invalidate();
            }
        }

        public String FilePath
        {
            get { return filePath; }
            set { filePath = value; }
        }

        public String FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }

        [Category("Custom Pen Properties")]
        public Color PenColor
        {
            get { return pDoodle.Color; }
            set {
                pDoodle.Color = value;
                this.Invalidate();
            }
        }

        [Category("Custom Pen Properties")]
        public float PenWidth
        {
            get { return pDoodle.Width; }
            set {
                pDoodle.Width = value;
                this.Invalidate();
            }
        }

        // Methods
        private void FrmDoodle_39_Toan_MouseDown(object sender, MouseEventArgs e)
        {
            if (getLocation.Capacity > 0)
            {
                getLocation.Clear();
            }
            getLocation.Add(e.Location);
        }


        // Make fast smooth line drawing
        private void FrmDoodle_39_Toan_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                getLocation.Add(e.Location);
                int size = getLocation.Count;

                this.CreateGraphics().DrawLine(pDoodle, getLocation[size - 2], getLocation[size - 1]);
            }
        }

        private void FrmDoodle_39_Toan_MouseUp(object sender, MouseEventArgs e)
        {
            if (lineSize < lines.Count)
            {
                lines.RemoveRange(lineSize, lines.Count - lineSize);
            }
            lines.Add(ConvertGetLocationToString_39_Toan());
            lineSize++;
        }

        // I noticed that the Paint event raised when the form is loaded or leaved
        // Before, I do not use Paint event, instead I use RePaintDoodle method
        // This does not work, because the Paint event is not raised
        private void FrmDoodle_39_Toan_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            List<PointF> readLocations = new List<PointF>();
            if (lines.Count == 0)
            {
                return;
            }
            for (int i = 0; i < lineSize; i++)
            {
                String[] points = lines[i].Split('|');
                String[] point = points[0].Split(',');
                readLocations.Add(new PointF(float.Parse(point[0]), float.Parse(point[1])));
                for (int j = 1; j < points.Length; j++)
                {
                    point = points[j].Split(',');
                    readLocations.Add(new PointF(float.Parse(point[0]), float.Parse(point[1])));
                    e.Graphics.DrawLine(pDoodle, readLocations[j-1], readLocations[j]);
                }
                readLocations.Clear();
            }
        }

        private String ConvertGetLocationToString_39_Toan()
        {
            String Content = "";
            for (int i = 0; i < getLocation.Count; i++)
            {
                Content += getLocation[i].X.ToString() + ",";
                Content += getLocation[i].Y.ToString();
                if (i < getLocation.Count - 1)
                {
                    Content += "|";
                }
            }
            Content += "\r\n";
            return Content;
        }

        // Save the doodle as a custom type file
        public void SaveDoodleAsFile_39_Toan(String filePath, String fileName)
        {
            if (filePath == "")
            {
                MessageBox.Show("Please select a folder to save the doodle", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.filePath = filePath;
            if (fileName != "") {
                this.fileName = fileName;
            }

            File.Create(filePath + "\\" + this.fileName + ".doodle").Close();
            if (File.Exists(filePath + "\\" + this.fileName + ".doodle"))
            {
                isSave = true;
                StreamWriter sw = new StreamWriter(filePath + "\\" + this.fileName + ".doodle");
                for (int i = 0; i < lineSize; i++)
                {
                    sw.Write(lines[i]);
                }
                sw.Close();
            }
        }

        private void btnUndo_39_Toan_Click(object sender, EventArgs e)
        {
            lineSize--;
            if (lineSize < 0)
            {
                lineSize = 0;
            }
            this.Invalidate();
        }

        private void btnRedo_39_Toan_Click(object sender, EventArgs e)
        {
            lineSize++;
            if (lineSize > lines.Count)
            {
                lineSize = lines.Count;
            }
            this.Invalidate();
        }

        private void FrmDoodle_39_Toan_Load(object sender, EventArgs e)
        {
            btnRedo_39_Toan.Width = btnRedo_39_Toan.Height;
            btnUndo_39_Toan.Width = btnUndo_39_Toan.Height;
            btnRedo_39_Toan.BorderRadius = btnRedo_39_Toan.Height / 2;
            btnUndo_39_Toan.BorderRadius = btnUndo_39_Toan.Height / 2;
        }

        public void LoadDoodle_39_Toan(String filePath)
        {
            if (filePath == "")
            {
                MessageBox.Show("Please select a file to load the doodle", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            this.filePath = filePath;
            StreamReader sr = new StreamReader(filePath);
            String line = "";
            while ((line = sr.ReadLine()) != null)
            {
                lines.Add(line);
            }
            lineSize = lines.Count;
            sr.Close();
            this.Invalidate();
        }
    }
}
