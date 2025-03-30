using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NoteApp
{
    public partial class FrmLogin_39_Toan : Form
    {
        public FrmLogin_39_Toan()
        {
            InitializeComponent();
        }

        private void FrmLogin_39_Toan_Load(object sender, EventArgs e)
        {
            lblTitle_39_Toan.Location = new Point((this.Width - lblTitle_39_Toan.Width) / 2, lblTitle_39_Toan.Location.Y);

            if (File.Exists(Application.UserAppDataPath + "\\FolderPath.txt"))
            {
                String[] lines = File.ReadAllLines(Application.UserAppDataPath + "\\FolderPath.txt").Distinct().ToArray();
                foreach (String line in lines)
                {
                    Button RecentFolders = new Button()
                    {
                        Text = line,
                        AutoSize = true,
                        TextAlign = ContentAlignment.MiddleLeft,
                        Dock = DockStyle.Top,
                        Font = new Font("Arial", 12),
                        Cursor = Cursors.Hand
                    };

                    RecentFolders.Click += (s, ev) =>
                    {
                        txtFolderPath_39_Toan.Text = RecentFolders.Text;
                        btnFolderPath_39_Toan_Click(s,ev);

                    };
                    grpRecentFol_39_Toan.Controls.Add(RecentFolders);
                }
            }
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
            if (Directory.Exists(txtFolderPath_39_Toan.Text))
            {
                fldFolderPath_39_Toan.SelectedPath = txtFolderPath_39_Toan.Text;
            }
            else 
            { 
                DialogResult result_39_Toan = fldFolderPath_39_Toan.ShowDialog();
                if (result_39_Toan == DialogResult.Cancel)
                {
                    return;
                }
            }

            this.Hide();
            FrmMainMenu_39_Toan mainMenu = new FrmMainMenu_39_Toan(fldFolderPath_39_Toan.SelectedPath);

            String SaveRecentFolderFile = Application.UserAppDataPath + "\\FolderPath.txt";
            if (!File.Exists(SaveRecentFolderFile))
            {
                File.Create(SaveRecentFolderFile).Close();
                MessageBox.Show(SaveRecentFolderFile);
            }
            File.AppendAllText(SaveRecentFolderFile, fldFolderPath_39_Toan.SelectedPath + "\n");

            // Show the main menu form as a dialog so that the this.Close() method is not called until the main menu form is closed
            mainMenu.ShowDialog();
            this.Close();
        }
    }
}
