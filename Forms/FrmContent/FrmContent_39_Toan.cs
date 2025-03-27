using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
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

        // METHODS
        public void LoadContent_39_Toan(String FilePath, ContentTypes contentType)
        {
            if (File.Exists(FilePath))
            {
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

                // C://Users/.../Desktop/FileName.md
                // ~~~~~~~~~~~~~~~~~~~~~~           LastIndexOf('\\')
                // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~  IndexOf('.')
                String FileName = FilePath.Substring(FilePath.LastIndexOf('\\') + 1, FilePath.IndexOf('.') - FilePath.LastIndexOf('\\') - 1);
                tbContent_39_Toan.TabPages.Add(FileName);
                switch (contentType)
                {
                    case ContentTypes.Text:
                        RichTextBox rtbContent_39_Toan = new RichTextBox();
                        rtbContent_39_Toan.Dock = DockStyle.Fill;
                        rtbContent_39_Toan.Text = File.ReadAllText(FilePath);
                        tbContent_39_Toan.TabPages[FileName].Controls.Add(rtbContent_39_Toan);
                        break;
                    case ContentTypes.Image:
                        PictureBox pbContent_39_Toan = new PictureBox();
                        pbContent_39_Toan.Dock = DockStyle.Fill;
                        pbContent_39_Toan.ImageLocation = FilePath;
                        pbContent_39_Toan.SizeMode = PictureBoxSizeMode.Zoom;
                        tbContent_39_Toan.TabPages[FileName].Controls.Add(pbContent_39_Toan);
                        break;
                    case ContentTypes.Doodle:
                        Form frmContent_39_Toan = new FrmDoodle_39_Toan();
                        frmContent_39_Toan.Dock = DockStyle.Fill;
                        frmContent_39_Toan.Tag = FilePath;
                        tbContent_39_Toan.TabPages[FileName].Controls.Add(frmContent_39_Toan);
                        break;
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
                else if (tpContent_39_Toan.Controls[0] is PictureBox)
                {
                    return (tpContent_39_Toan.Controls[0] as PictureBox).ImageLocation;
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

    }
}
