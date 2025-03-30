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
        String FolderPath = "";

        // Constructor
        public FrmMainMenu_39_Toan()
        {
            InitializeComponent();
        }

        public FrmMainMenu_39_Toan(String FolderPath)
        {
            InitializeComponent();
            this.FolderPath = FolderPath;
            loadFileIntoTreeView(FolderPath);
        }
        // Methods
        private void FrmMainMenu_39_Toan_Load(object sender, EventArgs e)
        {
            int widthRightPanel = (pnlDefault_39_Toan.Width - btnCreateNewDoodle_39_Toan.Width) / 2;
            int heightRightPanel = (pnlDefault_39_Toan.Height - btnCreateNewDoodle_39_Toan.Height) / 2;
            btnCreateNewFile_39_Toan.Location = new Point(widthRightPanel, heightRightPanel - 40);
            btnCreateNewDoodle_39_Toan.Location = new Point(widthRightPanel, btnCreateNewFile_39_Toan.Location.Y + btnCreateNewFile_39_Toan.Height + 20);
        }

        private void pnlDefault_39_Toan_Paint(object sender, PaintEventArgs e)
        {
            int widthRightPanel = (pnlDefault_39_Toan.Width - btnCreateNewDoodle_39_Toan.Width) / 2;
            int heightRightPanel = (pnlDefault_39_Toan.Height - btnCreateNewDoodle_39_Toan.Height) / 2;
            btnCreateNewFile_39_Toan.Location = new Point(widthRightPanel, heightRightPanel - 40);
            btnCreateNewDoodle_39_Toan.Location = new Point(widthRightPanel, btnCreateNewFile_39_Toan.Location.Y + btnCreateNewFile_39_Toan.Height + 20);
        }

        private void btnCloseLeftPanel_39_Toan_Click(object sender, EventArgs e)
        {
            this.splMainMenu_39_Toan.Panel1Collapsed = !this.splMainMenu_39_Toan.Panel1Collapsed;
        }

        private void btnOpenFolderDialog_39_Toan_Click(object sender, EventArgs e)
        {
            fldOpenFolder_39_Toan.ShowDialog();
            loadFileIntoTreeView(fldOpenFolder_39_Toan.SelectedPath);
        }

        private void trFolderLocation_39_Toan_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
        }

        private void splMainMenu_39_Toan_SplitterMoved(object sender, SplitterEventArgs e)
        {
            if (this.splMainMenu_39_Toan.SplitterDistance < this.splMainMenu_39_Toan.Panel1MinSize + 5)
            {
                this.splMainMenu_39_Toan.SplitterDistance = this.splMainMenu_39_Toan.Panel1MinSize + 5;
            }
            pnlDefault_39_Toan.Invalidate();
        }

        private void txtTextContent_39_Toan_AutoSave(object sender, EventArgs e)
        {
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

        public void loadFileIntoTreeView(String path)
        {
            trFolderLocation_39_Toan.Nodes.Clear();

            TreeNode root = new TreeNode(path);
            trFolderLocation_39_Toan.Nodes.Add(root);

            try
            {
                String[] Files = System.IO.Directory.GetFiles(path, "*.*");
                String[] subFolders = System.IO.Directory.GetDirectories(path);

                foreach (String filePath in Files)
                {
                    if (FrmContent_39_Toan.GetContentType_39_Toan(filePath) == ContentTypes.None)
                    {
                        continue;
                    }

                    TreeNode node = new TreeNode(filePath);

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

                root.Expand();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void btnCreateNewFile_39_Toan_Click(object sender, EventArgs e)
        {
            pnlDefault_39_Toan.Controls.Clear();
            FrmContent_39_Toan frmLoadForm_39_Toan = new FrmContent_39_Toan();
            frmLoadForm_39_Toan.TopLevel = false;
            frmLoadForm_39_Toan.Dock = DockStyle.Fill;
            frmLoadForm_39_Toan.AutoScroll = true;
            frmLoadForm_39_Toan.LoadContent_39_Toan(FolderPath, ContentTypes.Text);
            frmLoadForm_39_Toan.Show();
            pnlDefault_39_Toan.Controls.Add(frmLoadForm_39_Toan);
        }

        private void btnCreateNewDoodle_39_Toan_Click(object sender, EventArgs e)
        {
            pnlDefault_39_Toan.Controls.Clear();
            FrmContent_39_Toan frmLoadForm_39_Toan = new FrmContent_39_Toan();
            frmLoadForm_39_Toan.TopLevel = false;
            frmLoadForm_39_Toan.Dock = DockStyle.Fill;
            frmLoadForm_39_Toan.AutoScroll = true;
            frmLoadForm_39_Toan.LoadContent_39_Toan(FolderPath, ContentTypes.Doodle);
            frmLoadForm_39_Toan.Show();
            pnlDefault_39_Toan.Controls.Add(frmLoadForm_39_Toan);
        }
    }
}