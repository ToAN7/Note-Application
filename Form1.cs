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
        public fMainMenu39Toan()
        {
            InitializeComponent();
        }

        private void closeLeftPanel_Click(object sender, EventArgs e)
        {
            do
            {
                this.splcntMainMenu39Toan.SplitterDistance--;
                this.btnCloseLeftPanel39Toan.Location = new Point(this.splcntMainMenu39Toan.SplitterDistance - 34, this.Height - 73);
            }
            while (this.splcntMainMenu39Toan.SplitterDistance > this.btnCloseLeftPanel39Toan.Size.Width);

            btnOpenLeftPanel39Toan.Visible = true;
            this.splcntMainMenu39Toan.Panel1Collapsed = !this.splcntMainMenu39Toan.Panel1Collapsed;
            rtxtTextContent39Toan.Size = new Size(this.splcntMainMenu39Toan.Panel2.Width, this.splcntMainMenu39Toan.Panel2.Height);
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel1_Resize(object sender, EventArgs e)
        {
            this.trvwFolderLocation39Toan.Size = new Size(this.splcntMainMenu39Toan.Panel1.Width - 6, this.splcntMainMenu39Toan.Panel1.Height - 6 - this.tblpnlLayout39Toan.Height);
            //this.tblpnlLayout39Toan.Size = new Size(this.splcntMainMenu39Toan.Panel1.Width - 6, this.tblpnlLayout39Toan.Height);
        }

        private void OpenLeftPanel_Click(object sender, EventArgs e)
        {
            this.splcntMainMenu39Toan.Panel1Collapsed = !this.splcntMainMenu39Toan.Panel1Collapsed;
            btnOpenLeftPanel39Toan.Visible = false;

            do
            {
                this.splcntMainMenu39Toan.SplitterDistance++;
                this.btnCloseLeftPanel39Toan.Location = new Point(this.splcntMainMenu39Toan.SplitterDistance - 34, this.Height - 73);
            }
            while (this.splcntMainMenu39Toan.SplitterDistance <= 260);

        }

        private void loadFileIntoTreeView(String path)
        {
            trvwFolderLocation39Toan.Nodes.Clear();
            TreeNode root = new TreeNode(path);
            trvwFolderLocation39Toan.Nodes.Add(root);
            try
            {
                String[] txtFiles = System.IO.Directory.GetFiles(path, "*.txt");
                
                foreach (String filePath in txtFiles)
                {
                    TreeNode node = new TreeNode(filePath);
                    node.Text = filePath.Substring(filePath.LastIndexOf('\\') + 1);
                    root.Nodes.Add(node);
                }

                root.Expand();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnOpenFolderDialog39Toan_Click(object sender, EventArgs e)
        {
            fldbrsdlgOpenFolder39Toan.ShowDialog();
            loadFileIntoTreeView(fldbrsdlgOpenFolder39Toan.SelectedPath);
            lblFolderName39Toan.Text = fldbrsdlgOpenFolder39Toan.SelectedPath.Trim().Substring(fldbrsdlgOpenFolder39Toan.SelectedPath.LastIndexOf('\\'));
        }

        private void trvwFolderLocation39Toan_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Text != fldbrsdlgOpenFolder39Toan.SelectedPath)
            {
                rtxtTextContent39Toan.Text = System.IO.File.ReadAllText(e.Node.FullPath);
            }
        }
    }
}
