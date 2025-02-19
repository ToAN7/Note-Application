namespace NoteApp
{
    partial class Note
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mainMenu = new System.Windows.Forms.SplitContainer();
            this.OpenLeftPanel = new System.Windows.Forms.Button();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.closeLeftPanel = new System.Windows.Forms.Button();
            this.FolderName = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            ((System.ComponentModel.ISupportInitialize)(this.mainMenu)).BeginInit();
            this.mainMenu.Panel1.SuspendLayout();
            this.mainMenu.Panel2.SuspendLayout();
            this.mainMenu.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            // 
            // mainMenu.Panel1
            // 
            this.mainMenu.Panel1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.mainMenu.Panel1.Controls.Add(this.treeView1);
            this.mainMenu.Panel1.Controls.Add(this.tableLayoutPanel1);
            this.mainMenu.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel1_Paint);
            this.mainMenu.Panel1.Resize += new System.EventHandler(this.splitContainer1_Panel1_Resize);
            // 
            // mainMenu.Panel2
            // 
            this.mainMenu.Panel2.Controls.Add(this.OpenLeftPanel);
            this.mainMenu.Size = new System.Drawing.Size(784, 561);
            this.mainMenu.SplitterDistance = 260;
            this.mainMenu.TabIndex = 0;
            // 
            // OpenLeftPanel
            // 
            this.OpenLeftPanel.Location = new System.Drawing.Point(0, 16);
            this.OpenLeftPanel.Margin = new System.Windows.Forms.Padding(0);
            this.OpenLeftPanel.Name = "OpenLeftPanel";
            this.OpenLeftPanel.Size = new System.Drawing.Size(39, 35);
            this.OpenLeftPanel.TabIndex = 2;
            this.OpenLeftPanel.Text = ">>";
            this.OpenLeftPanel.UseVisualStyleBackColor = true;
            this.OpenLeftPanel.Visible = false;
            this.OpenLeftPanel.Click += new System.EventHandler(this.OpenLeftPanel_Click);
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(3, 3);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(254, 511);
            this.treeView1.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.closeLeftPanel, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.FolderName, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 520);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(254, 38);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // closeLeftPanel
            // 
            this.closeLeftPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.closeLeftPanel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.closeLeftPanel.Location = new System.Drawing.Point(214, 4);
            this.closeLeftPanel.Margin = new System.Windows.Forms.Padding(4);
            this.closeLeftPanel.Name = "closeLeftPanel";
            this.closeLeftPanel.Size = new System.Drawing.Size(36, 30);
            this.closeLeftPanel.TabIndex = 1;
            this.closeLeftPanel.Text = "<<";
            this.closeLeftPanel.UseVisualStyleBackColor = true;
            this.closeLeftPanel.Click += new System.EventHandler(this.closeLeftPanel_Click);
            // 
            // FolderName
            // 
            this.FolderName.AutoSize = true;
            this.FolderName.Location = new System.Drawing.Point(3, 0);
            this.FolderName.Name = "FolderName";
            this.FolderName.Size = new System.Drawing.Size(36, 13);
            this.FolderName.TabIndex = 2;
            this.FolderName.Text = "Folder";
            // 
            // Note
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.mainMenu);
            this.IsMdiContainer = true;
            this.Name = "Note";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Note";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.mainMenu.Panel1.ResumeLayout(false);
            this.mainMenu.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainMenu)).EndInit();
            this.mainMenu.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer mainMenu;
        private System.Windows.Forms.Button closeLeftPanel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Label FolderName;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button OpenLeftPanel;
    }
}

