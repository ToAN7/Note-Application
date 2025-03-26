using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NoteApp.Forms.FrmText
{
    public partial class FrmText_39_Toan: Form
    {
        // Initialize the form
        public FrmText_39_Toan()
        {
            InitializeComponent();
        }

        public void LoadFileContent(string filePath)
        {
            txtText_39_Toan.Text = System.IO.File.ReadAllText(filePath);
        }

        public void SaveFileContent(string filePath)
        {
            System.IO.File.WriteAllText(filePath, txtText_39_Toan.Text);
        }
    }
}
