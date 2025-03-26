using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NoteApp.Forms.FrmText;

namespace NoteApp
{
    public partial class FrmMainMenu_39_Toan : Form
    {
        Timer timer;
        // Initialize the form
        public FrmMainMenu_39_Toan()
        {
            InitializeComponent();
        }
        private void FrmMainMenu_39_Toan_Load(object sender, EventArgs e)
        {
            //txtTextContent_39_Toan.ReadOnly = true;
        }

        // Event handlers
        // Toggle the visibility of the left panel
        private void btnCloseLeftPanel_39_Toan_Click(object sender, EventArgs e)
        {
            btnOpenLeftPanel_39_Toan.Visible = true;
            this.splMainMenu_39_Toan.Panel1Collapsed = !this.splMainMenu_39_Toan.Panel1Collapsed;
            //txtTextContent_39_Toan.Size = new Size(this.splMainMenu_39_Toan.Panel2.Width - 6, this.splMainMenu_39_Toan.Panel2.Height - 6);
        }
        private void btnOpenLeftPanel_39_Toan_Click(object sender, EventArgs e)
        {
            this.splMainMenu_39_Toan.Panel1Collapsed = !this.splMainMenu_39_Toan.Panel1Collapsed;
            btnOpenLeftPanel_39_Toan.Visible = false;
        }

        // Open folder dialog to select a folder
        private void btnOpenFolderDialog_39_Toan_Click(object sender, EventArgs e)
        {
            fldOpenFolder_39_Toan.ShowDialog();
            loadFileIntoTreeView(fldOpenFolder_39_Toan.SelectedPath);
            lblFolderName_39_Toan.Text = fldOpenFolder_39_Toan.SelectedPath.Trim().Substring(fldOpenFolder_39_Toan.SelectedPath.LastIndexOf('\\'));
        }

        // Load the selected file content into the text box
        private void trFolderLocation_39_Toan_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Text.Contains(".txt"))
            {
                //txtTextContent_39_Toan.Text = System.IO.File.ReadAllText(e.Node.FullPath);
                //txtTextContent_39_Toan.ReadOnly = false;
            }
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
                this.trFolderLocation_39_Toan.Size = new Size(this.splMainMenu_39_Toan.Panel1.Width - 6, this.splMainMenu_39_Toan.Panel1.Height - 6 - this.tblLayout_39_Toan.Height);
                //this.txtTextContent_39_Toan.Size = new Size(this.splMainMenu_39_Toan.Panel2.Width - 6, this.splMainMenu_39_Toan.Panel2.Height - 6);
                this.tblLayout_39_Toan.Size = new Size(this.splMainMenu_39_Toan.Panel1.Width - 6, this.tblLayout_39_Toan.Height);
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

        private void txtTextContent_39_Toan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (chkAutoSave_39_Toan.Checked)
            {
                timer.Stop();
                timer.Start();
            }
        }
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
                // Get all the text files in the selected folder and add them as child nodes to the root node
                String[] txtFiles = System.IO.Directory.GetFiles(path, "*.txt");
                String[] subFolders = System.IO.Directory.GetDirectories(path);

                foreach (String filePath in txtFiles)
                {
                    // Create a new tree node for each file and add it as a child node to the root node
                    TreeNode node = new TreeNode(filePath);

                    // Display only the file name in the tree view
                    node.Text = filePath.Substring(filePath.LastIndexOf('\\') + 1);
                    root.Nodes.Add(node);
                }

                foreach (String folderPath in subFolders)
                {
                    TreeNode node = new TreeNode(folderPath);
                    node.Text = folderPath.Substring(folderPath.LastIndexOf('\\') + 1);
                    String[] subTxtFiles = System.IO.Directory.GetFiles(folderPath, "*.txt");
                    foreach (String filePath in subTxtFiles)
                    {
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

        private void tabPage1_Click(object sender, EventArgs e)
        {
            Form fLoadForm_39_Toan = new FrmText_39_Toan();
            fLoadForm_39_Toan.TopLevel = false;
            fLoadForm_39_Toan.AutoScroll = true;
            fLoadForm_39_Toan.Show();
            fLoadForm_39_Toan.Dock = DockStyle.Fill;
            tabPage1.Controls.Add(fLoadForm_39_Toan);
        }
    }
}