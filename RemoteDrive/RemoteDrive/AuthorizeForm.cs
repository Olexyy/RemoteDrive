using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RemoteDrive
{
    public partial class AuthorizeForm : Form
    {
        public enum FormTypes { Login, SignUp };
        private FormTypes FormType { get; set; }
        public string Login { get; set; }
        public string Password { get; set; } 
        public AuthorizeForm(FormTypes formType)
        {
            this.InitializeComponent();
            this.DialogResult = DialogResult.Cancel;
            this.FormType = formType;
            this.Text = formType.ToString();
            this.groupBox.Text = formType.ToString();
            this.buttonOk.Text = formType.ToString();
        }
        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(this.textBoxLogin.Text.Trim()) && !String.IsNullOrEmpty(this.textBoxPassword.Text.Trim()))
            {
                this.DialogResult = DialogResult.OK;
                this.Login = this.textBoxLogin.Text.Trim();
                this.Password = this.textBoxPassword.Text.Trim();
                this.Close();
            }
            else
                MessageBox.Show("Fields can't be empty");
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
