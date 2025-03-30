using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NoteApp.Forms.FrmContent;

namespace NoteApp
{
    public partial class FrmMainMenu_39_Toan : Form
    {
        // Fields
        Timer timer;

        // Constructor
        public FrmMainMenu_39_Toan()
        {
            InitializeComponent();
        }
        private void FrmMainMenu_39_Toan_Load(object sender, EventArgs e)
        {
            int widthRightPanel = (pnlDefault_39_Toan.Width - btnCreateNewDoodle_39_Toan.Width) / 2;
            int heightRightPanel = (pnlDefault_39_Toan.Height - btnCreateNewDoodle_39_Toan.Height) / 2;
            btnCreateNewFile_39_Toan.Location = new Point(widthRightPanel, heightRightPanel - 40);
            btnCreateNewDoodle_39_Toan.Location = new Point(widthRightPanel, btnCreateNewFile_39_Toan.Location.Y + btnCreateNewFile_39_Toan.Height + 20);
        }

        // Methods
        private void btnCloseLeftPanel_39_Toan_Click(object sender, EventArgs e)
        {
            //btnOpenLeftPanel_39_Toan.Visible = true;
            this.splMainMenu_39_Toan.Panel1Collapsed = !this.splMainMenu_39_Toan.Panel1Collapsed;
            //txtTextContent_39_Toan.Size = new Size(this.splMainMenu_39_Toan.Panel2.Width - 6, this.splMainMenu_39_Toan.Panel2.Height - 6);
        }
        private void btnOpenLeftPanel_39_Toan_Click(object sender, EventArgs e)
        {
            this.splMainMenu_39_Toan.Panel1Collapsed = !this.splMainMenu_39_Toan.Panel1Collapsed;
            //btnOpenLeftPanel_39_Toan.Visible = false;
        }

        // Open folder dialog to select a folder
        private void btnOpenFolderDialog_39_Toan_Click(object sender, EventArgs e)
        {
            fldOpenFolder_39_Toan.ShowDialog();
            loadFileIntoTreeView(fldOpenFolder_39_Toan.SelectedPath);
            //lblFolderName_39_Toan.Text = fldOpenFolder_39_Toan.SelectedPath.Trim().Substring(fldOpenFolder_39_Toan.SelectedPath.LastIndexOf('\\'));
        }

        // Load the selected file content into the text box
        private void trFolderLocation_39_Toan_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            //if (e.Node.Text.Contains(".doodle"))
            //{
            //    //txtTextContent_39_Toan.Text = System.IO.File.ReadAllText(e.Node.FullPath);
            //    //txtTextContent_39_Toan.ReadOnly = false;
            //    tabPage1_Click(sender, e);
            //    (tabPage1.Controls[0] as FrmDoodle_39_Toan).LoadDoodle_39_Toan(e.Node.FullPath);
            //}
        }

        private void splMainMenu_39_Toan_SplitterMoved(object sender, SplitterEventArgs e)
        {
            if (this.splMainMenu_39_Toan.SplitterDistance < this.splMainMenu_39_Toan.Panel1MinSize + 5)
            {
                btnCloseLeftPanel_39_Toan_Click(sender, e);
                this.splMainMenu_39_Toan.SplitterDistance = 346;
            }
            else
            {
                this.trFolderLocation_39_Toan.Size = new Size(pnlDefault_39_Toan.Width - 6, pnlDefault_39_Toan.Height - 6 - this.tblLayout_39_Toan.Height);
                this.tblLayout_39_Toan.Size = new Size(pnlDefault_39_Toan.Width - 6, this.tblLayout_39_Toan.Height);
            }
        }

        // Auto save the content of the text box
        private void txtTextContent_39_Toan_AutoSave(object sender, EventArgs e)
        {
            timer = new Timer();
            timer.Interval = 3000;
            timer.Tick += (object sdr, EventArgs ent) =>
            {
                //if (!txtTextContent_39_Toan.ReadOnly)
                {
                    //System.IO.File.WriteAllText(trFolderLocation_39_Toan.SelectedNode.FullPath, txtTextContent_39_Toan.Text);
                }
            };
            timer.Start();
        }

        private void chkAutoSave_39_Toan_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAutoSave_39_Toan.Checked) { 
                txtTextContent_39_Toan_AutoSave(sender, e);
            }
            else
            {
                timer.Stop();
            }
        }

        //private void txtTextContent_39_Toan_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (chkAutoSave_39_Toan.Checked)
        //    {
        //        timer.Stop();
        //        timer.Start();
        //    }
        //}
        // Custom controls

        // Load the selected path into the tree view
        public void loadFileIntoTreeView(String path)
        {
            // Clear the tree view in case there are existing nodes
            trFolderLocation_39_Toan.Nodes.Clear();

            // Create a root node for the tree view and add it to the tree view
            TreeNode root = new TreeNode(path);
            trFolderLocation_39_Toan.Nodes.Add(root);

            try
            {
                // Get all supported files in the selected folder
                String[] Files = System.IO.Directory.GetFiles(path, "*.*");
                String[] subFolders = System.IO.Directory.GetDirectories(path);

                foreach (String filePath in Files)
                {
                    // Create a new tree node for each file and add it as a child node to the root node
                    if (FrmContent_39_Toan.GetContentType_39_Toan(filePath) == ContentTypes.None)
                    {
                        continue;
                    }

                    TreeNode node = new TreeNode(filePath);

                    // Display only the file name in the tree view
                    node.Text = filePath.Substring(filePath.LastIndexOf('\\') + 1);
                    root.Nodes.Add(node);
                }

                foreach (String folderPath in subFolders)
                {
                    TreeNode node = new TreeNode(folderPath);
                    node.Text = folderPath.Substring(folderPath.LastIndexOf('\\') + 1);
                    String[] subFiles = System.IO.Directory.GetFiles(folderPath, "*.*");
                    foreach (String filePath in subFiles)
                    {
                        if (FrmContent_39_Toan.GetContentType_39_Toan(filePath) == ContentTypes.None)
                        {
                            continue;
                        }

                        TreeNode subNode = new TreeNode(filePath);
                        subNode.Text = filePath.Substring(filePath.LastIndexOf('\\') + 1);
                        node.Nodes.Add(subNode);
                    }
                    root.Nodes.Add(node);
                }

                // Expand the root node to show the child nodes
                root.Expand();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCreateNewFile_39_Toan_Click(object sender, EventArgs e)
        {
            pnlDefault_39_Toan.Controls.Clear();
            Form fLoadForm_39_Toan = new FrmContent_39_Toan();
            fLoadForm_39_Toan.TopLevel = false;
            fLoadForm_39_Toan.Dock = DockStyle.Fill;
            fLoadForm_39_Toan.AutoScroll = true;
            fLoadForm_39_Toan.Show();
            pnlDefault_39_Toan.Controls.Add(fLoadForm_39_Toan);
        }

        private void btnCreateNewDoodle_39_Toan_Click(object sender, EventArgs e)
        {
            pnlDefault_39_Toan.Controls.Clear();
        }

        //private void tabPage1_Click(object sender, EventArgs e)
        //{
        //    Console.WriteLine("Create new Frm");
        //    Form fLoadForm_39_Toan = new FrmDoodle_39_Toan();
        //    fLoadForm_39_Toan.TopLevel = false;
        //    fLoadForm_39_Toan.AutoScroll = true;
        //    fLoadForm_39_Toan.Show();
        //    fLoadForm_39_Toan.Dock = DockStyle.Fill;
        //    tabPage1.Controls.Clear();
        //    tabPage1.Controls.Add(fLoadForm_39_Toan);

        //    //tblLayout_39_Toan.Controls.
        //}

        //private void tabPage2_Click(object sender, EventArgs e)
        //{
        //    PictureBox pbContent_39_Toan = new PictureBox();
        //    pbContent_39_Toan.Dock = DockStyle.Fill;
        //    pbContent_39_Toan.Image = Image.FromFile(@"D:\Meme\KHAPepeSleep.PNG");
        //    pbContent_39_Toan.SizeMode = PictureBoxSizeMode.Zoom;
        //    pbContent_39_Toan.Show();
        //    tabPage2.Controls.Add(pbContent_39_Toan);
        //}

        //private void tbMainMenu_39_Toan_Selected(object sender, TabControlEventArgs e)
        //{
        //    if (e.TabPageIndex == 0)
        //    {
        //        Console.WriteLine("Selected tab 0");
        //        Console.WriteLine(e.TabPage.Controls.Count.ToString());
        //    }
        //}

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    (tabPage1.Controls[0] as FrmDoodle_39_Toan).SaveDoodleAsFile_39_Toan(@"C:\Users\ctoan\Desktop", "");
        //    loadFileIntoTreeView(fldOpenFolder_39_Toan.SelectedPath);
        //}
    }
}