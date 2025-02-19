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
                mainMenu.SplitterDistance--;
                closeLeftPanel.Location = new Point(mainMenu.SplitterDistance - 34, this.Height - 73);
            }
            while (mainMenu.SplitterDistance > closeLeftPanel.Size.Width);

            mainMenu.Panel1Collapsed = !mainMenu.Panel1Collapsed;
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel1_Resize(object sender, EventArgs e)
        {
            closeLeftPanel.Location = new Point((int)(mainMenu.SplitterDistance * 0.97) - 34, (int)(mainMenu.Height*0.97) - 29);
        }
    }
}
