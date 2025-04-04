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
using System.IO;

namespace NoteApp
{
    public partial class FrmMainMenu_39_Toan : Form
    {
        // Fields
        String FolderPath_39_Toan = "";

        // Constructor
        public FrmMainMenu_39_Toan()
        {
            InitializeComponent();
        }

        public FrmMainMenu_39_Toan(String FolderPath_39_Toan)
        {
            InitializeComponent();
            this.FolderPath_39_Toan = FolderPath_39_Toan;
            loadFileIntoTreeView_39_Toan(FolderPath_39_Toan);
        }

        // Methods
        private void pnlDefault_39_Toan_Paint(object sender, PaintEventArgs e)
        {
            int widthRightPanel_39_Toan = (pnlDefault_39_Toan.Width - btnCreateNewDoodle_39_Toan.Width) / 2;
            int heightRightPanel_39_Toan = (pnlDefault_39_Toan.Height - btnCreateNewDoodle_39_Toan.Height) / 2;
            btnCreateNewFile_39_Toan.Location = new Point(widthRightPanel_39_Toan, heightRightPanel_39_Toan - 40);
            btnCreateNewDoodle_39_Toan.Location = new Point(widthRightPanel_39_Toan, btnCreateNewFile_39_Toan.Location.Y + btnCreateNewFile_39_Toan.Height + 20);
        }

        private void btnOpenFolderDialog_39_Toan_Click(object sender, EventArgs e)
        {
            fldOpenFolder_39_Toan.ShowDialog();
            loadFileIntoTreeView_39_Toan(fldOpenFolder_39_Toan.SelectedPath);
            FolderPath_39_Toan = fldOpenFolder_39_Toan.SelectedPath;

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
                    ContentTypes_39_Toan getType_39_Toan = FrmContent_39_Toan.GetContentType_39_Toan(e.Node.Text);
                    if (pnlDefault_39_Toan.Controls[0] is FrmContent_39_Toan)
                    {
                        (pnlDefault_39_Toan.Controls[0] as FrmContent_39_Toan).LoadContent_39_Toan(e.Node.FullPath, getType_39_Toan);
                    }
                    else
                    {
                        pnlDefault_39_Toan.Controls.Clear();
                        FrmContent_39_Toan frmLoadForm_39_Toan = new FrmContent_39_Toan(e.Node.FullPath, getType_39_Toan);
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
                if (e.Node.FullPath.Contains('.'))
                {
                    ToolStripButton tsbDelete_39_Toan = new ToolStripButton("Delete File");
                    ToolStripButton tsbClose_39_Toan = new ToolStripButton("Close File");
                    ToolStripButton tsbCreateText_39_Toan = new ToolStripButton("Create Text File");
                    ToolStripButton tsbCreateDoodle_39_Toan = new ToolStripButton("Create Doodle File");
                    ToolStripButton tsbCreateFolder_39_Toan = new ToolStripButton("Create Folder");
                    ToolStripButton tsbRename_39_Toan = new ToolStripButton("Rename File");
                    ToolStripTextBox txtNewFileName_39_Toan = new ToolStripTextBox();

                    String FilePath_39_Toan = e.Node.FullPath;
                    txtNewFileName_39_Toan.Text = Path.GetFileNameWithoutExtension(FilePath_39_Toan);

                    tsbCreateFolder_39_Toan.Click += (s, ev) =>
                    {
                        CreateFolder_39_Toan(Path.GetDirectoryName(FilePath_39_Toan));
                    };

                    tsbDelete_39_Toan.Click += (s, ev) =>
                    {
                        if (MessageBox.Show("Are you sure you want to delete this file?", "Delete File", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            File.Delete(FilePath_39_Toan);
                            loadFileIntoTreeView_39_Toan(FolderPath_39_Toan);
                            if (pnlDefault_39_Toan.Controls[0] is FrmContent_39_Toan && (pnlDefault_39_Toan.Controls[0] as FrmContent_39_Toan).IsExistTab_39_Toan(Path.GetFileNameWithoutExtension(FilePath_39_Toan)))
                            {
                                (pnlDefault_39_Toan.Controls[0] as FrmContent_39_Toan).ClearTab_39_Toan(Path.GetFileNameWithoutExtension(FilePath_39_Toan));
                            }
                        }
                    };

                    tsbClose_39_Toan.Click += (s, ev) =>
                    {
                        if (pnlDefault_39_Toan.Controls[0] is FrmContent_39_Toan && (pnlDefault_39_Toan.Controls[0] as FrmContent_39_Toan).IsExistTab_39_Toan(Path.GetFileNameWithoutExtension(FilePath_39_Toan)))
                        {
                            (pnlDefault_39_Toan.Controls[0] as FrmContent_39_Toan).ClearTab_39_Toan(Path.GetFileNameWithoutExtension(FilePath_39_Toan));
                        }
                    };

                    tsbCreateText_39_Toan.Click += (s, ev) =>
                    {
                        if (pnlDefault_39_Toan.Controls[0] is FrmContent_39_Toan)
                        {
                            FrmContent_39_Toan frmLoadForm_39_Toan = (FrmContent_39_Toan)(pnlDefault_39_Toan.Controls[0]);
                            frmLoadForm_39_Toan.LoadContent_39_Toan(Path.GetDirectoryName(FilePath_39_Toan), ContentTypes_39_Toan.Text);
                            loadFileIntoTreeView_39_Toan(FolderPath_39_Toan);
                        }
                        else
                        {
                            pnlDefault_39_Toan.Controls.Clear();
                            CreateFile_39_Toan(Path.GetDirectoryName(FilePath_39_Toan), ContentTypes_39_Toan.Text);
                        }
                    };

                    tsbCreateDoodle_39_Toan.Click += (s, ev) => {
                        if (pnlDefault_39_Toan.Controls[0] is FrmContent_39_Toan)
                        {
                            FrmContent_39_Toan frmLoadForm_39_Toan = (FrmContent_39_Toan)(pnlDefault_39_Toan.Controls[0]);
                            frmLoadForm_39_Toan.LoadContent_39_Toan(Path.GetDirectoryName(FilePath_39_Toan), ContentTypes_39_Toan.Doodle);
                            loadFileIntoTreeView_39_Toan(FolderPath_39_Toan);
                        }
                        else
                        {
                            pnlDefault_39_Toan.Controls.Clear();
                            CreateFile_39_Toan(Path.GetDirectoryName(FilePath_39_Toan), ContentTypes_39_Toan.Doodle);
                        }
                    };

                    tsbRename_39_Toan.Click += (s, ev) =>
                    {
                        if (txtNewFileName_39_Toan.Text != "")
                        {
                            String NewFilePath_39_Toan = Path.GetDirectoryName(FilePath_39_Toan) + '\\' + txtNewFileName_39_Toan.Text + Path.GetExtension(FilePath_39_Toan);
                            File.Move(FilePath_39_Toan, NewFilePath_39_Toan);
                            
                            if (pnlDefault_39_Toan.Controls[0] is FrmContent_39_Toan && (pnlDefault_39_Toan.Controls[0] as FrmContent_39_Toan).IsExistTab_39_Toan(Path.GetFileNameWithoutExtension(FilePath_39_Toan)))
                            {
                                (pnlDefault_39_Toan.Controls[0] as FrmContent_39_Toan).ClearTab_39_Toan(Path.GetFileNameWithoutExtension(FilePath_39_Toan));
                                (pnlDefault_39_Toan.Controls[0] as FrmContent_39_Toan).LoadContent_39_Toan(NewFilePath_39_Toan, ContentTypes_39_Toan.None);
                            }
                            loadFileIntoTreeView_39_Toan(FolderPath_39_Toan);
                        }
                    };

                    ContextMenuStrip cmsOptions_39_Toan = new ContextMenuStrip()
                    {
                        Items = { tsbCreateText_39_Toan, tsbCreateDoodle_39_Toan, tsbCreateFolder_39_Toan, new ToolStripSeparator(), txtNewFileName_39_Toan, tsbRename_39_Toan, new ToolStripSeparator(), tsbClose_39_Toan, tsbDelete_39_Toan },
                        TopLevel = true,
                    };

                    cmsOptions_39_Toan.Show(trFolderLocation_39_Toan, e.Location);
                }
                else
                {
                    ToolStripButton tsbDelete_39_Toan = new ToolStripButton("Delete Folder");
                    ToolStripButton tsbCreateText_39_Toan = new ToolStripButton("Create Text File");
                    ToolStripButton tsbCreateDoodle_39_Toan = new ToolStripButton("Create Doodle File");
                    ToolStripButton tsbCreateFolder_39_Toan = new ToolStripButton("Create Folder");
                    ToolStripButton tsbRename_39_Toan = new ToolStripButton("Rename Folder");
                    ToolStripTextBox txtNewFolderName_39_Toan = new ToolStripTextBox();

                    String SubFolderPath_39_Toan = e.Node.FullPath;
                    txtNewFolderName_39_Toan.Text = SubFolderPath_39_Toan.Substring(e.Node.FullPath.LastIndexOf('\\') + 1);

                    tsbDelete_39_Toan.Click += (s, ev) =>
                    {
                        if (MessageBox.Show("Are you sure you want to delete this folder?", "Delete Folder", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            Directory.Delete(SubFolderPath_39_Toan, true);
                            loadFileIntoTreeView_39_Toan(SubFolderPath_39_Toan);
                            btnClearTab_39_Toan_Click(s, ev);
                        }
                    };

                    tsbCreateText_39_Toan.Click += (s, ev) =>
                    {
                        if (pnlDefault_39_Toan.Controls[0] is FrmContent_39_Toan)
                        {
                            FrmContent_39_Toan frmLoadForm_39_Toan = (FrmContent_39_Toan)(pnlDefault_39_Toan.Controls[0]);
                            frmLoadForm_39_Toan.LoadContent_39_Toan(SubFolderPath_39_Toan, ContentTypes_39_Toan.Text);
                            loadFileIntoTreeView_39_Toan(FolderPath_39_Toan);
                        }
                        else
                        {
                            pnlDefault_39_Toan.Controls.Clear();
                            CreateFile_39_Toan(SubFolderPath_39_Toan, ContentTypes_39_Toan.Text);
                        }
                    };

                    tsbCreateDoodle_39_Toan.Click += (s, ev) => {
                        if (pnlDefault_39_Toan.Controls[0] is FrmContent_39_Toan)
                        {
                            FrmContent_39_Toan frmLoadForm_39_Toan = (FrmContent_39_Toan)(pnlDefault_39_Toan.Controls[0]);
                            frmLoadForm_39_Toan.LoadContent_39_Toan(SubFolderPath_39_Toan, ContentTypes_39_Toan.Doodle);
                            loadFileIntoTreeView_39_Toan(FolderPath_39_Toan);
                        }
                        else
                        {
                            pnlDefault_39_Toan.Controls.Clear();
                            CreateFile_39_Toan(SubFolderPath_39_Toan, ContentTypes_39_Toan.Doodle);
                        }
                    };

                    tsbRename_39_Toan.Click += (s, ev) =>
                    {
                        if (txtNewFolderName_39_Toan.Text != "")
                        {
                            String NewFolderPath_39_Toan = Path.GetDirectoryName(SubFolderPath_39_Toan) + '\\' + txtNewFolderName_39_Toan.Text;
                            Directory.Move(SubFolderPath_39_Toan, NewFolderPath_39_Toan);
                            loadFileIntoTreeView_39_Toan(FolderPath_39_Toan);
                        }
                    };

                    ContextMenuStrip cmsOptions_39_Toan = new ContextMenuStrip()
                    {
                        Items = { tsbCreateText_39_Toan, tsbCreateDoodle_39_Toan, new ToolStripSeparator(), txtNewFolderName_39_Toan, tsbRename_39_Toan, new ToolStripSeparator(), tsbDelete_39_Toan },
                        TopLevel = true,
                    };

                    cmsOptions_39_Toan.Show(trFolderLocation_39_Toan, e.Location);
                }
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

        public void loadFileIntoTreeView_39_Toan(String path, TreeNode prevPath)
        {
            TreeNode root_39_Toan = new TreeNode(path);
            if (prevPath == null)
            {
                trFolderLocation_39_Toan.Nodes.Clear();
                trFolderLocation_39_Toan.Nodes.Add(root_39_Toan);
            }
            else 
            {
                root_39_Toan.Text = path.Substring(path.LastIndexOf('\\') + 1);
                prevPath.Nodes.Add(root_39_Toan);
            }

            try
            {
                String[] Files_39_Toan = Directory.GetFiles(path);
                String[] subFolders_39_Toan = Directory.GetDirectories(path);

                foreach (String FilePath_39_Toan in Files_39_Toan)
                {
                    if (FrmContent_39_Toan.GetContentType_39_Toan(FilePath_39_Toan) == ContentTypes_39_Toan.None)
                    {
                        continue;
                    }

                    TreeNode node_39_Toan = new TreeNode(FilePath_39_Toan);

                    node_39_Toan.Text = FilePath_39_Toan.Substring(FilePath_39_Toan.LastIndexOf('\\') + 1);
                    root_39_Toan.Nodes.Add(node_39_Toan);
                }

                foreach (String FolderPath_39_Toan in subFolders_39_Toan)
                {
                    loadFileIntoTreeView_39_Toan(FolderPath_39_Toan, root_39_Toan);
                }

                root_39_Toan.Expand();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        public void loadFileIntoTreeView_39_Toan(String path)
        {
            loadFileIntoTreeView_39_Toan(path, null);
        }

        private void CreateFile_39_Toan(String folderPath_39_Toan, ContentTypes_39_Toan contentType)
        {
            FrmContent_39_Toan frmLoadForm_39_Toan = new FrmContent_39_Toan(folderPath_39_Toan, contentType);
            frmLoadForm_39_Toan.TopLevel = false;
            frmLoadForm_39_Toan.Dock = DockStyle.Fill;
            frmLoadForm_39_Toan.AutoScroll = true;
            frmLoadForm_39_Toan.Show();
            pnlDefault_39_Toan.Controls.Add(frmLoadForm_39_Toan);
            loadFileIntoTreeView_39_Toan(FolderPath_39_Toan);
        }

        private void btnCreateNewFile_39_Toan_Click(object sender, EventArgs e)
        {
            pnlDefault_39_Toan.Controls.Clear();
            CreateFile_39_Toan(FolderPath_39_Toan, ContentTypes_39_Toan.Text);
        }

        private void btnCreateNewDoodle_39_Toan_Click(object sender, EventArgs e)
        {
            pnlDefault_39_Toan.Controls.Clear();
            CreateFile_39_Toan(FolderPath_39_Toan, ContentTypes_39_Toan.Doodle);
        }

        private void btnSave_39_Toan_Click(object sender, EventArgs e)
        {
            if (pnlDefault_39_Toan.Controls[0] is FrmContent_39_Toan)
            {
                (pnlDefault_39_Toan.Controls[0] as FrmContent_39_Toan).SaveContent_39_Toan();
            }
        }

        private void btnClearTab_39_Toan_Click(object sender, EventArgs e)
        {
            if (pnlDefault_39_Toan.Controls[0] is FrmContent_39_Toan)
            {
                (pnlDefault_39_Toan.Controls[0] as FrmContent_39_Toan).ClearTabs_39_Toan();
            }
        }

        private void CreateFolder_39_Toan(String folderPath_39_Toan)
        {
            String FolderName_39_Toan = folderPath_39_Toan + "\\New_Folder\\";
            int NameIndex_39_Toan = 0;
            do
            {
                NameIndex_39_Toan++;
                FolderName_39_Toan = folderPath_39_Toan + "\\New_Folder" + NameIndex_39_Toan + "\\";
            } while (Directory.Exists(FolderName_39_Toan));
            Directory.CreateDirectory(FolderName_39_Toan);
            loadFileIntoTreeView_39_Toan(FolderPath_39_Toan);
        }

        private void btnCreateFolder_39_Toan_Click(object sender, EventArgs e)
        {
            CreateFolder_39_Toan(FolderPath_39_Toan);
        }
    }
}