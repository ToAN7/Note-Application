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
            this.closeLeftPanel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.mainMenu)).BeginInit();
            this.mainMenu.Panel1.SuspendLayout();
            this.mainMenu.SuspendLayout();
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
            this.mainMenu.Panel1.Controls.Add(this.closeLeftPanel);
            this.mainMenu.Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Panel1_Paint);
            this.mainMenu.Panel1.Resize += new System.EventHandler(this.splitContainer1_Panel1_Resize);
            this.mainMenu.Size = new System.Drawing.Size(800, 450);
            this.mainMenu.SplitterDistance = 266;
            this.mainMenu.TabIndex = 0;
            // 
            // closeLeftPanel
            // 
            this.closeLeftPanel.Location = new System.Drawing.Point(229, 411);
            this.closeLeftPanel.Name = "closeLeftPanel";
            this.closeLeftPanel.Size = new System.Drawing.Size(34, 29);
            this.closeLeftPanel.TabIndex = 1;
            this.closeLeftPanel.Text = "<<";
            this.closeLeftPanel.UseVisualStyleBackColor = true;
            this.closeLeftPanel.Click += new System.EventHandler(this.closeLeftPanel_Click);
            // 
            // Note
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.mainMenu);
            this.Name = "Note";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Note";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.mainMenu.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainMenu)).EndInit();
            this.mainMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer mainMenu;
        private System.Windows.Forms.Button closeLeftPanel;
    }
}

