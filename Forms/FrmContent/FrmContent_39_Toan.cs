using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NoteApp.Forms.FrmContent
{
    public enum ContentTypes
    {
        None,
        Text,
        Image,
        Doodle
    }
    public partial class FrmContent_39_Toan : Form
    {
        // CONSTRUCTOR
        public FrmContent_39_Toan()
        {
            InitializeComponent();
        }

        public FrmContent_39_Toan(String FilePath, ContentTypes contentType)
        {
            InitializeComponent();
            LoadContent_39_Toan(FilePath, contentType);
        }

        // METHODS
        public void ClearTabs_39_Toan()
        {
            tbContent_39_Toan.TabPages.Clear();
        }

        public void ClearTab_39_Toan(String tabName)
        {
            tbContent_39_Toan.TabPages.RemoveByKey(tabName);
        }

        public void LoadContent_39_Toan(String FilePath, ContentTypes contentType)
        {
            if (File.Exists(FilePath))
            {
                // C://Users/.../Desktop/FileName.md
                // ~~~~~~~~~~~~~~~~~~~~~~           LastIndexOf('\\')
                // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~  IndexOf('.')
                String FileName = FilePath.Substring(FilePath.LastIndexOf('\\') + 1, FilePath.IndexOf('.') - FilePath.LastIndexOf('\\') - 1);

                if (tbContent_39_Toan.TabPages.ContainsKey(FileName))
                {
                    tbContent_39_Toan.SelectedTab = tbContent_39_Toan.TabPages[FileName];
                    return;
                }

                // In case of user doesn't know the content type
                if (contentType == ContentTypes.None)
                {
                    contentType = GetContentType_39_Toan(FilePath);

                    // Stop loading unsupported file types
                    if (contentType == ContentTypes.None)
                    {
                        return;
                    }
                }

                tbContent_39_Toan.TabPages.Add(FileName, FileName);

                ToolStripButton tsbClose = new ToolStripButton("Close");
                tsbClose.Click += (sender, e) => { ClearTab_39_Toan(tbContent_39_Toan.SelectedTab.Name); };

                ToolStripButton tsbCloseAll = new ToolStripButton("Close All");
                tsbCloseAll.Click += (sender, e) => { ClearTabs_39_Toan(); };

                tbContent_39_Toan.TabPages[FileName].ContextMenuStrip = new ContextMenuStrip()
                {
                    Items = { tsbClose, tsbCloseAll },
                    TopLevel = true,
                };
                tbContent_39_Toan.TabPages[FileName].MouseClick += (sender, e) =>
                {
                    if (e.Button == MouseButtons.Right)
                    {
                        tbContent_39_Toan.ContextMenuStrip.Show();
                    }
                };

                switch (contentType)
                {
                    case ContentTypes.Text:
                        RichTextBox rtbContent_39_Toan = new RichTextBox();
                        rtbContent_39_Toan.Dock = DockStyle.Fill;
                        rtbContent_39_Toan.Text = File.ReadAllText(FilePath);
                        rtbContent_39_Toan.Show();
                        tbContent_39_Toan.TabPages[FileName].Controls.Add(rtbContent_39_Toan);
                        break;
                    case ContentTypes.Image:
                        PictureBox pbContent_39_Toan = new PictureBox();
                        pbContent_39_Toan.Dock = DockStyle.Fill;
                        pbContent_39_Toan.Image = Image.FromFile(FilePath);
                        pbContent_39_Toan.SizeMode = PictureBoxSizeMode.Zoom;
                        pbContent_39_Toan.Show();
                        tbContent_39_Toan.TabPages[FileName].Controls.Add(pbContent_39_Toan);
                        break;
                    case ContentTypes.Doodle:
                        FrmDoodle_39_Toan frmContent_39_Toan = new FrmDoodle_39_Toan();
                        frmContent_39_Toan.LoadDoodle_39_Toan(FilePath);
                        frmContent_39_Toan.Dock = DockStyle.Fill;
                        frmContent_39_Toan.TopLevel = false;
                        frmContent_39_Toan.Tag = FilePath;
                        frmContent_39_Toan.Show();
                        tbContent_39_Toan.TabPages[FileName].Controls.Add(frmContent_39_Toan);
                        break;
                }

                tbContent_39_Toan.SelectedTab = tbContent_39_Toan.TabPages[FileName];
                tbContent_39_Toan.TabPages[FileName].Controls[0].Focus();
            }
            else
            {
                String fileName = "";
                switch (contentType)
                {
                    case ContentTypes.Text:
                        fileName = DateTime.Now.ToString("dd_MM_yyyy") + "t.md";
                        break;
                    case ContentTypes.Doodle:
                        fileName = DateTime.Now.ToString("dd_MM_yyyy") + "d.doodle";
                        break;
                };
                File.Create(FilePath + "\\" + fileName).Close();
                LoadContent_39_Toan(FilePath + "\\" + fileName, contentType);
            }
        }

        public void SaveContent_39_Toan(String Path)
        {
            String fileName = "";
            if (tbContent_39_Toan.SelectedTab.Controls.Count > 0)
            {
                if (tbContent_39_Toan.SelectedTab.Controls[0] is RichTextBox)
                {
                    fileName = tbContent_39_Toan.SelectedTab.Text + ".md";
                    File.WriteAllText(Path + "\\" + fileName, (tbContent_39_Toan.SelectedTab.Controls[0] as RichTextBox).Text);
                }
                else if (tbContent_39_Toan.SelectedTab.Controls[0] is FrmDoodle_39_Toan)
                {
                    (tbContent_39_Toan.SelectedTab.Controls[0] as FrmDoodle_39_Toan).SaveDoodleAsFile_39_Toan(Path, tbContent_39_Toan.SelectedTab.Text);
                }
            }
        }

        // GETTERS
        public String GetContent_39_Toan(TabPage tpContent_39_Toan)
        {
            if (tpContent_39_Toan.Controls.Count > 0)
            {
                if (tpContent_39_Toan.Controls[0] is RichTextBox)
                {
                    return (tpContent_39_Toan.Controls[0] as RichTextBox).Text;
                }
                else if (tpContent_39_Toan.Controls[0] is FrmDoodle_39_Toan)
                {
                    return (tpContent_39_Toan.Controls[0] as FrmDoodle_39_Toan).Tag.ToString();
                }
            }
            return "";
        }

        public static ContentTypes GetContentType_39_Toan(String filePath)
        {
            String fileExtension = filePath.Substring(filePath.IndexOf('.'));
            if (fileExtension == ".md")
            {
                return ContentTypes.Text;
            }
            else if (fileExtension == ".jpg" || fileExtension == ".jpeg" || fileExtension == ".png" || fileExtension == ".gif")
            {
                return ContentTypes.Image;
            }
            else if (fileExtension == ".doodle")
            {
                return ContentTypes.Doodle;
            }
            return ContentTypes.None;
        }

        public bool IsExistTab_39_Toan(String tabName)
        {
            return tbContent_39_Toan.TabPages.ContainsKey(tabName);
        }
    }
}
