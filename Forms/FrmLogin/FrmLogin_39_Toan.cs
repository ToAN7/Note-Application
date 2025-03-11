using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NoteApp
{
    public partial class FrmLogin_39_Toan: Form
    {
        public FrmLogin_39_Toan()
        {
            InitializeComponent();
            lblTitle_39_Toan.Location = new Point((this.Width - lblTitle_39_Toan.Width) / 2, lblTitle_39_Toan.Location.Y);
        }

        private void btnClosed_39_Toan_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pnlGrab_39_Toan_MouseDown(object sender, MouseEventArgs e)
        {
            this.Opacity = 0.5;
            int xOffset = Cursor.Position.X - this.Location.X;
            int yOffset = Cursor.Position.Y - this.Location.Y;
            do 
            {
                this.Location = new Point(Cursor.Position.X - xOffset, Cursor.Position.Y - yOffset);
                Application.DoEvents();
            } while (MouseButtons == MouseButtons.Left);
        }

        private void pnlGrab_39_Toan_MouseUp(object sender, MouseEventArgs e)
        {
            this.Opacity = 1;
        }

        private void btnFolderPath_39_Toan_Click(object sender, EventArgs e)
        {
            DialogResult result_39_Toan = fldFolderPath_39_Toan.ShowDialog();
            if (result_39_Toan == DialogResult.OK)
            {
                txtFolderPath_39_Toan.Text = fldFolderPath_39_Toan.SelectedPath;

                // Hide the login form and show the main menu form
                this.Hide();
                FrmMainMenu_39_Toan mainMenu = new FrmMainMenu_39_Toan();

                // Inkove the function to load the selected path into the tree view
                // If loadFileIntoTreeView is called after the main menu form is shown, the tree view will not be updated
                mainMenu.loadFileIntoTreeView(fldFolderPath_39_Toan.SelectedPath);

                // Show the main menu form as a dialog so that the this.Close() method is not called until the main menu form is closed
                mainMenu.ShowDialog();
                this.Close();
            }
        }
    }
}
