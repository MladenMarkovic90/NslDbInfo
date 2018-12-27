using System;
using System.Drawing;
using System.Windows.Forms;

namespace NslDbInfo
{
    public partial class NslDbLogin : Form
    {
        public NslDbLogin()
        {
            this.InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (this.txtPassword.Text == "123")
            {
                this.txtPassword.Text = string.Empty;
                this.txtPassword.BackColor = Color.White;
                this.Hide();

                try
                {
                    new NslDbForm().ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }

                this.Show();
                this.txtPassword.Focus();
            }
            else
            {
                this.txtPassword.Text = string.Empty;
                this.txtPassword.BackColor = Color.FromArgb(255, 255, 150, 150);
                MessageBox.Show("Password not correct!");
                this.txtPassword.Focus();
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.btnLogin_Click(sender, e);
            }
        }
    }
}