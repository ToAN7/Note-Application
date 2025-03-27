using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NoteApp.Forms.FrmContent
{
    public partial class FrmDoodle_39_Toan : Form
    {
        int size = 0;
        List<PointF> getLocation = new List<PointF>();
        Pen pDoodle = new Pen(Color.Red, 3);
        public FrmDoodle_39_Toan()
        {
            InitializeComponent();
            File.Create(@"D:\tmp.json").Close();
        }

        // Properties
        public List<PointF> GetLocation
        {
            get { return getLocation; }
        }
        public Pen GetPen
        {
            get { return pDoodle; }
        }

        [Category("Custom Pen Properties")]
        public Color PenColor
        {
            get { return pDoodle.Color; }
            set { pDoodle.Color = value; }
        }

        [Category("Custom Pen Properties")]
        public float PenWidth
        {
            get { return pDoodle.Width; }
            set { pDoodle.Width = value; }
        }

        // Methods
        private void FrmDoodle_39_Toan_MouseDown(object sender, MouseEventArgs e)
        {
            if (getLocation.Capacity > 0)
            {
                getLocation.Clear();
            }
            getLocation.Add(e.Location);
            size++;
        }

        private void FrmDoodle_39_Toan_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                getLocation.Add(e.Location);
                size++;

                this.CreateGraphics().DrawLine(pDoodle, getLocation[size-2], getLocation[size-1]);
            }
        }

        private void FrmDoodle_39_Toan_MouseUp(object sender, MouseEventArgs e)
        {
            String content = "{\r\n" + $"\"doodle{size}\":";
            content += "[";
            for (int i = 0; i < getLocation.Count; i++)
            {
                content += "[" + getLocation[i].X.ToString() + ", " + getLocation[i].Y.ToString() + "]";
                if (i != getLocation.Count - 1)
                {
                    content += ",";
                }
            }
            content += "]";
            if (!File.Exists(@"D:\tmp.json"))
            {
                File.Create(@"D:\tmp.json").Close();
            }
            File.AppendAllText(@"D:\tmp.json", content);
            size = 0;
        }
        public void SaveDoodleAsFile_39_Toan(String path, String fileName)
        {

            String Content = "{\r\n\"doodle\":";
          
            Content += "}";
            File.Create(path + "\\" + fileName).Close();
            File.WriteAllText(path + "\\" + fileName, Content);
        }

        private void FrmDoodle_39_Toan_FormClosed(object sender, FormClosedEventArgs e)
        {
            String content = "}";
            File.Create(@"D:\tmp.json").Close();
            File.AppendAllText(@"D:\tmp.json", content);
        }

        public void RePaintDoodle_39_Toan(ref List<PointF> doodle)
        {
            //this.Refresh();
            for (int i = 0; i < doodle.Count - 1; i++)
            {
                this.CreateGraphics().Dispose();
                this.CreateGraphics().DrawLine(pDoodle, doodle[i], doodle[i + 1]);
            }
        }

        private void FrmDoodle_39_Toan_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
