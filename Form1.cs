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
    public partial class fMainMenu39Toan : Form
    {
        Timer timer;
        // Initialize the form
        public fMainMenu39Toan()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            rtxtTextContent39Toan.ReadOnly = true;
        }

        // Event handlers
        // Toggle the visibility of the left panel
        private void closeLeftPanel_Click(object sender, EventArgs e)
        {
            btnOpenLeftPanel39Toan.Visible = true;
            this.splcntMainMenu39Toan.Panel1Collapsed = !this.splcntMainMenu39Toan.Panel1Collapsed;
            rtxtTextContent39Toan.Size = new Size(this.splcntMainMenu39Toan.Panel2.Width - 6, this.splcntMainMenu39Toan.Panel2.Height - 6);
        }
        private void OpenLeftPanel_Click(object sender, EventArgs e)
        {
            this.splcntMainMenu39Toan.Panel1Collapsed = !this.splcntMainMenu39Toan.Panel1Collapsed;
            btnOpenLeftPanel39Toan.Visible = false;
        }

        // Load the selected path into the tree view
        private void loadFileIntoTreeView(String path)
        {
            // Clear the tree view in case there are existing nodes
            trvwFolderLocation39Toan.Nodes.Clear();

            // Create a root node for the tree view and add it to the tree view
            TreeNode root = new TreeNode(path);
            trvwFolderLocation39Toan.Nodes.Add(root);

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

        // Open folder dialog to select a folder
        private void btnOpenFolderDialog39Toan_Click(object sender, EventArgs e)
        {
            fldbrsdlgOpenFolder39Toan.ShowDialog();
            loadFileIntoTreeView(fldbrsdlgOpenFolder39Toan.SelectedPath);
            lblFolderName39Toan.Text = fldbrsdlgOpenFolder39Toan.SelectedPath.Trim().Substring(fldbrsdlgOpenFolder39Toan.SelectedPath.LastIndexOf('\\'));
        }

        // Load the selected file content into the text box
        private void trvwFolderLocation39Toan_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Text.Contains(".txt"))
            {
                rtxtTextContent39Toan.Text = System.IO.File.ReadAllText(e.Node.FullPath);
                rtxtTextContent39Toan.ReadOnly = false;
            }
        }

        private void splcntMainMenu39Toan_SplitterMoved(object sender, SplitterEventArgs e)
        {
            if (this.splcntMainMenu39Toan.SplitterDistance < this.splcntMainMenu39Toan.Panel1MinSize + 5)
            {
                closeLeftPanel_Click(sender, e);
                this.splcntMainMenu39Toan.SplitterDistance = 346;
            }
            else
            {
                this.trvwFolderLocation39Toan.Size = new Size(this.splcntMainMenu39Toan.Panel1.Width - 6, this.splcntMainMenu39Toan.Panel1.Height - 6 - this.tblpnlLayout39Toan.Height);
                this.rtxtTextContent39Toan.Size = new Size(this.splcntMainMenu39Toan.Panel2.Width - 6, this.splcntMainMenu39Toan.Panel2.Height - 6);
                this.tblpnlLayout39Toan.Size = new Size(this.splcntMainMenu39Toan.Panel1.Width - 6, this.tblpnlLayout39Toan.Height);
            }
        }

        // Auto save the content of the text box
        private void rtxtTextContent39Toan_AutoSave(object sender, EventArgs e)
        {
            timer = new Timer();
            timer.Interval = 3000;
            timer.Tick += (object sdr, EventArgs ent) =>
            {
                if (!rtxtTextContent39Toan.ReadOnly)
                {
                    System.IO.File.WriteAllText(trvwFolderLocation39Toan.SelectedNode.FullPath, rtxtTextContent39Toan.Text);
                }
            };
            timer.Start();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked) { 
                rtxtTextContent39Toan_AutoSave(sender, e);
            }
            else
            {
                timer.Stop();
            }
        }

        private void rtxtTextContent39Toan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (checkBox1.Checked)
            {
                timer.Stop();
                timer.Start();
            }
        }
    }
}
