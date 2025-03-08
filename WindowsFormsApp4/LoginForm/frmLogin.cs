using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVDLBusiness;
using WindowsFormsApp4.GlobalClasses;

namespace WindowsFormsApp4.LoginForm
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            clsUsersBusiness UserInfo = clsUsersBusiness.FindUserByUserNameAndPassword(txtUserName.Text.Trim(), txtPassword.Text.Trim());
            if (UserInfo != null)
            {
                if (chkRemamberMe.Checked)
                {
                    clsGlobal.RememberUsernameAndPassword(txtUserName.Text.Trim(), txtPassword.Text.Trim());
                }
                else
                {
                    clsGlobal.RememberUsernameAndPassword("", "");

                }
                if (!UserInfo.IsActive)
                {
                    txtUserName.Focus();
                    MessageBox.Show("Your Account Is Not Active, Contact To Admin", "Is Not Active", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                clsGlobal.CurrentUser = UserInfo;
                this.Hide();
                Form1 frm = new Form1(this);
                frm.ShowDialog();
            }
            else
            {
                txtUserName.Focus();
                MessageBox.Show("Invalid UserName / Password.", "Wrong Credintials", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            string UserName = "", Password = "";
            if (clsGlobal.GetStoredCredential(ref UserName, ref Password))
            {
                txtUserName.Text = UserName;
                txtPassword.Text = Password;
                chkRemamberMe.Checked = true;
            }
            else
                chkRemamberMe.Checked = false;
        }
    }
}
