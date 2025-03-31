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
            loadFileIntoTreeView_39_Toan(FolderPath);
        }
        // Methods

        private void pnlDefault_39_Toan_Paint(object sender, PaintEventArgs e)
        {
            int widthRightPanel = (pnlDefault_39_Toan.Width - btnCreateNewDoodle_39_Toan.Width) / 2;
            int heightRightPanel = (pnlDefault_39_Toan.Height - btnCreateNewDoodle_39_Toan.Height) / 2;
            btnCreateNewFile_39_Toan.Location = new Point(widthRightPanel, heightRightPanel - 40);
            btnCreateNewDoodle_39_Toan.Location = new Point(widthRightPanel, btnCreateNewFile_39_Toan.Location.Y + btnCreateNewFile_39_Toan.Height + 20);
        }

        private void btnCloseLeftPanel_39_Toan_Click(object sender, EventArgs e)
        {
            this.splMainMenu_39_Toan.SplitterDistance = this.splMainMenu_39_Toan.Panel1MinSize + 5;
        }

        private void btnOpenFolderDialog_39_Toan_Click(object sender, EventArgs e)
        {
            fldOpenFolder_39_Toan.ShowDialog();
            loadFileIntoTreeView_39_Toan(fldOpenFolder_39_Toan.SelectedPath);
            FolderPath = fldOpenFolder_39_Toan.SelectedPath;

            if (pnlDefault_39_Toan.Controls[0] is FrmContent_39_Toan)
            {
                (pnlDefault_39_Toan.Controls[0] as FrmContent_39_Toan).ClearTabs_39_Toan();
            }
        }

        private void trFolderLocation_39_Toan_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (e.Node.Text.Contains("."))
                {
                    ContentTypes getType = FrmContent_39_Toan.GetContentType_39_Toan(e.Node.Text);
                    if (pnlDefault_39_Toan.Controls[0] is FrmContent_39_Toan)
                    {
                        (pnlDefault_39_Toan.Controls[0] as FrmContent_39_Toan).LoadContent_39_Toan(e.Node.FullPath, getType);
                    }
                    else
                    {
                        pnlDefault_39_Toan.Controls.Clear();
                        FrmContent_39_Toan frmLoadForm_39_Toan = new FrmContent_39_Toan(e.Node.FullPath, getType);
                        frmLoadForm_39_Toan.TopLevel = false;
                        frmLoadForm_39_Toan.Dock = DockStyle.Fill;
                        frmLoadForm_39_Toan.AutoScroll = true;
                        frmLoadForm_39_Toan.Show();
                        pnlDefault_39_Toan.Controls.Add(frmLoadForm_39_Toan);
                    }
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                ToolStripButton tsbDelete = new ToolStripButton("Delete File");
                ToolStripButton tsbClose = new ToolStripButton("Close File");
                ToolStripButton tsbCreateText = new ToolStripButton("Create Text File");
                ToolStripButton tsbCreateDoodle = new ToolStripButton("Create Doodle File");
                ToolStripButton tsbRename = new ToolStripButton("Rename File");
                ToolStripTextBox txtNewFileName = new ToolStripTextBox();

                String FilePath = e.Node.FullPath;
                tsbDelete.Click += (s, ev) =>
                {
                    if (MessageBox.Show("Are you sure you want to delete this file?", "Delete File", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        System.IO.File.Delete(FilePath);
                        loadFileIntoTreeView_39_Toan(FolderPath);
                        if (pnlDefault_39_Toan.Controls[0] is FrmContent_39_Toan && (pnlDefault_39_Toan.Controls[0] as FrmContent_39_Toan).IsExistTab_39_Toan(e.Node.Text.Substring(0, e.Node.Text.IndexOf("."))))
                        {
                            (pnlDefault_39_Toan.Controls[0] as FrmContent_39_Toan).ClearTab_39_Toan(e.Node.Text.Substring(0, e.Node.Text.IndexOf(".")));
                        }
                    }
                };

                tsbClose.Click += (s, ev) =>
                {
                    if (pnlDefault_39_Toan.Controls[0] is FrmContent_39_Toan && (pnlDefault_39_Toan.Controls[0] as FrmContent_39_Toan).IsExistTab_39_Toan(e.Node.Text.Substring(0, e.Node.Text.IndexOf("."))))
                    {
                        (pnlDefault_39_Toan.Controls[0] as FrmContent_39_Toan).ClearTab_39_Toan(e.Node.Text.Substring(0, e.Node.Text.IndexOf(".")));
                    }
                };

                tsbCreateText.Click += (s, ev) =>
                {
                    if (pnlDefault_39_Toan.Controls[0] is FrmContent_39_Toan)
                    {
                        FrmContent_39_Toan frmLoadForm_39_Toan = new FrmContent_39_Toan();
                        frmLoadForm_39_Toan.TopLevel = false;
                        frmLoadForm_39_Toan.Dock = DockStyle.Fill;
                        frmLoadForm_39_Toan.AutoScroll = true;
                        frmLoadForm_39_Toan.LoadContent_39_Toan(FolderPath, ContentTypes.Text);
                        frmLoadForm_39_Toan.Show();
                        pnlDefault_39_Toan.Controls.Add(frmLoadForm_39_Toan);
                        loadFileIntoTreeView_39_Toan(FolderPath);
                    }
                    else
                    {
                        btnCreateNewFile_39_Toan_Click(s, ev);
                    }
                };

                tsbCreateDoodle.Click += (s, ev) => {
                    if (pnlDefault_39_Toan.Controls[0] is FrmContent_39_Toan)
                    {
                        FrmContent_39_Toan frmLoadForm_39_Toan = new FrmContent_39_Toan();
                        frmLoadForm_39_Toan.TopLevel = false;
                        frmLoadForm_39_Toan.Dock = DockStyle.Fill;
                        frmLoadForm_39_Toan.AutoScroll = true;
                        frmLoadForm_39_Toan.LoadContent_39_Toan(FolderPath, ContentTypes.Doodle);
                        frmLoadForm_39_Toan.Show();
                        pnlDefault_39_Toan.Controls.Add(frmLoadForm_39_Toan);
                        loadFileIntoTreeView_39_Toan(FolderPath);
                    }
                    else
                    {
                        btnCreateNewDoodle_39_Toan_Click(s, ev);
                    }
                };

                if (!e.Node.Text.Contains("."))
                {
                    tsbRename.Enabled = false;
                    txtNewFileName.Enabled = false;
                }
                else
                {
                    txtNewFileName.Text = e.Node.Text.Substring(0, e.Node.Text.IndexOf("."));
                }

                tsbRename.Click += (s, ev) =>
                {
                    if (txtNewFileName.Text != "")
                    {
                        System.IO.File.Move(FilePath, FilePath.Substring(0, FilePath.LastIndexOf('\\') + 1) + txtNewFileName.Text + e.Node.Text.Substring(e.Node.Text.IndexOf(".")));
                        loadFileIntoTreeView_39_Toan(FolderPath);
                    }
                };

                ContextMenuStrip cmsOptions = new ContextMenuStrip()
                {
                    Items = { tsbCreateText, tsbCreateDoodle, new ToolStripSeparator(), txtNewFileName, tsbRename, new ToolStripSeparator(), tsbClose, tsbDelete },
                    TopLevel = true,
                };

                cmsOptions.Show(trFolderLocation_39_Toan, e.Location);
            }
        }

        private void splMainMenu_39_Toan_SplitterMoved(object sender, SplitterEventArgs e)
        {
            if (this.splMainMenu_39_Toan.SplitterDistance < this.splMainMenu_39_Toan.Panel1MinSize + 5)
            {
                this.splMainMenu_39_Toan.SplitterDistance = this.splMainMenu_39_Toan.Panel1MinSize + 5;
            }
            pnlDefault_39_Toan.Invalidate();
        }

        public void loadFileIntoTreeView_39_Toan(String path)
        {
            trFolderLocation_39_Toan.Nodes.Clear();

            TreeNode root = new TreeNode(path);
            trFolderLocation_39_Toan.Nodes.Add(root);

            try
            {
                String[] Files = System.IO.Directory.GetFiles(path);
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
                    String[] subFiles = System.IO.Directory.GetFiles(folderPath);
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
            loadFileIntoTreeView_39_Toan(FolderPath);
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
            loadFileIntoTreeView_39_Toan(FolderPath);
        }

        private void btnSave_39_Toan_Click(object sender, EventArgs e)
        {
            if (pnlDefault_39_Toan.Controls[0] is FrmContent_39_Toan)
            {
                (pnlDefault_39_Toan.Controls[0] as FrmContent_39_Toan).SaveContent_39_Toan(FolderPath);
            }
        }

        private void btnClearTab_39_Toan_Click(object sender, EventArgs e)
        {
            if (pnlDefault_39_Toan.Controls[0] is FrmContent_39_Toan)
            {
                (pnlDefault_39_Toan.Controls[0] as FrmContent_39_Toan).ClearTabs_39_Toan();
            }
        }
    }
}