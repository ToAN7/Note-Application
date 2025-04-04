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
    public enum ContentTypes_39_Toan
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

        public FrmContent_39_Toan(String FilePath, ContentTypes_39_Toan contentType)
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

        public void LoadContent_39_Toan(String FilePath, ContentTypes_39_Toan contentType)
        {
            if (File.Exists(FilePath))
            {

                String FileName_39_Toan = System.IO.Path.GetFileNameWithoutExtension(FilePath);

                if (tbContent_39_Toan.TabPages.ContainsKey(FileName_39_Toan))
                {
                    tbContent_39_Toan.SelectedTab = tbContent_39_Toan.TabPages[FileName_39_Toan];
                    return;
                }

                // In case of user doesn't know the content type
                if (contentType == ContentTypes_39_Toan.None)
                {
                    contentType = GetContentType_39_Toan(FilePath);

                    // Stop loading unsupported file types
                    if (contentType == ContentTypes_39_Toan.None)
                    {
                        return;
                    }
                }

                tbContent_39_Toan.TabPages.Add(FileName_39_Toan, FileName_39_Toan);

                ToolStripButton tsbClose_39_Toan = new ToolStripButton("Close");
                tsbClose_39_Toan.Click += (sender, e) => { ClearTab_39_Toan(tbContent_39_Toan.SelectedTab.Name); };

                ToolStripButton tsbCloseAll_39_Toan = new ToolStripButton("Close All");
                tsbCloseAll_39_Toan.Click += (sender, e) => { ClearTabs_39_Toan(); };

                tbContent_39_Toan.TabPages[FileName_39_Toan].ContextMenuStrip = new ContextMenuStrip()
                {
                    Items = { tsbClose_39_Toan, tsbCloseAll_39_Toan },
                    TopLevel = true,
                };
                tbContent_39_Toan.TabPages[FileName_39_Toan].MouseClick += (sender, e) =>
                {
                    if (e.Button == MouseButtons.Right)
                    {
                        tbContent_39_Toan.ContextMenuStrip.Show();
                    }
                };

                switch (contentType)
                {
                    case ContentTypes_39_Toan.Text:
                        RichTextBox rtbContent_39_Toan = new RichTextBox();
                        rtbContent_39_Toan.Dock = DockStyle.Fill;
                        rtbContent_39_Toan.Text = File.ReadAllText(FilePath);
                        rtbContent_39_Toan.BorderStyle = BorderStyle.None;
                        rtbContent_39_Toan.Show();
                        tbContent_39_Toan.TabPages[FileName_39_Toan].Controls.Add(rtbContent_39_Toan);
                        break;
                    case ContentTypes_39_Toan.Image:
                        PictureBox pbContent_39_Toan = new PictureBox();
                        pbContent_39_Toan.Dock = DockStyle.Fill;
                        pbContent_39_Toan.Image = Image.FromFile(FilePath);
                        pbContent_39_Toan.SizeMode = PictureBoxSizeMode.Zoom;
                        pbContent_39_Toan.Show();
                        tbContent_39_Toan.TabPages[FileName_39_Toan].Controls.Add(pbContent_39_Toan);
                        break;
                    case ContentTypes_39_Toan.Doodle:
                        FrmDoodle_39_Toan frmContent_39_Toan = new FrmDoodle_39_Toan();
                        frmContent_39_Toan.LoadDoodle_39_Toan(FilePath);
                        frmContent_39_Toan.Dock = DockStyle.Fill;
                        frmContent_39_Toan.TopLevel = false;
                        frmContent_39_Toan.Tag = FilePath;
                        frmContent_39_Toan.Show();
                        tbContent_39_Toan.TabPages[FileName_39_Toan].Controls.Add(frmContent_39_Toan);
                        break;
                }

                tbContent_39_Toan.SelectedTab = tbContent_39_Toan.TabPages[FileName_39_Toan];
                tbContent_39_Toan.TabPages[FileName_39_Toan].Controls[0].Focus();
            }
            else
            {
                String fileName_39_Toan = "";
                switch (contentType)
                {
                    case ContentTypes_39_Toan.Text:
                        fileName_39_Toan = "NewText" +  DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".md";
                        break;
                    case ContentTypes_39_Toan.Doodle:
                        fileName_39_Toan = "NewDoodle" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".doodle";
                        break;
                };
                File.Create(FilePath + "\\" + fileName_39_Toan).Close();
                LoadContent_39_Toan(FilePath + "\\" + fileName_39_Toan, contentType);
            }
        }

        public void SaveContent_39_Toan(String Path)
        {
            String fileName_39_Toan = "";
            if (tbContent_39_Toan.SelectedTab == null)
            {
                return;
            }

            if (tbContent_39_Toan.SelectedTab.Controls.Count > 0)
            {
                if (tbContent_39_Toan.SelectedTab.Controls[0] is RichTextBox)
                {
                    fileName_39_Toan = tbContent_39_Toan.SelectedTab.Text + ".md";
                    File.WriteAllText(Path + "\\" + fileName_39_Toan, (tbContent_39_Toan.SelectedTab.Controls[0] as RichTextBox).Text);
                }
                else if (tbContent_39_Toan.SelectedTab.Controls[0] is FrmDoodle_39_Toan)
                {
                    (tbContent_39_Toan.SelectedTab.Controls[0] as FrmDoodle_39_Toan).SaveDoodleAsFile_39_Toan(Path, tbContent_39_Toan.SelectedTab.Text);
                }
            }
        }


        public static ContentTypes_39_Toan GetContentType_39_Toan(String filePath)
        {
            String fileExtension_39_Toan = System.IO.Path.GetExtension(filePath);
            if (fileExtension_39_Toan == ".md")
            {
                return ContentTypes_39_Toan.Text;
            }
            else if (fileExtension_39_Toan == ".jpg" || fileExtension_39_Toan == ".jpeg" || fileExtension_39_Toan == ".png" || fileExtension_39_Toan == ".gif")
            {
                return ContentTypes_39_Toan.Image;
            }
            else if (fileExtension_39_Toan == ".doodle")
            {
                return ContentTypes_39_Toan.Doodle;
            }
            return ContentTypes_39_Toan.None;
        }

        public bool IsExistTab_39_Toan(String tabName)
        {
            return tbContent_39_Toan.TabPages.ContainsKey(tabName);
        }
    }
}
