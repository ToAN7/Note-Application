using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NoteApp
{
    public partial class Note : Form
    {
        public Note()
        {
            InitializeComponent();
        }

        private void closeLeftPanel_Click(object sender, EventArgs e)
        {
            do
            {
                this.mainMenu.SplitterDistance--;
                this.closeLeftPanel.Location = new Point(this.mainMenu.SplitterDistance - 34, this.Height - 73);
            }
            while (this.mainMenu.SplitterDistance > this.closeLeftPanel.Size.Width);

            OpenLeftPanel.Visible = true;
            this.mainMenu.Panel1Collapsed = !this.mainMenu.Panel1Collapsed;
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel1_Resize(object sender, EventArgs e)
        {
            this.closeLeftPanel.Location = new Point((int)(this.mainMenu.SplitterDistance * 0.97) - 34, (int)(this.mainMenu.Height*0.97) - 29);
        }

        private void OpenLeftPanel_Click(object sender, EventArgs e)
        {
            this.mainMenu.Panel1Collapsed = !this.mainMenu.Panel1Collapsed;
            OpenLeftPanel.Visible = false;

            do
            {
                this.mainMenu.SplitterDistance++;
                this.closeLeftPanel.Location = new Point(this.mainMenu.SplitterDistance - 34, this.Height - 73);
            }
            while (this.mainMenu.SplitterDistance <= 260);

        }
    }
}
