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

namespace WindowsFormsApp4.UsersForms
{
    public partial class frmChangePassword : Form
    {
        private int _UserID;
        private clsUsersBusiness _UserInfo;
        public frmChangePassword(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;
        }
        private void _RestDefualtValue()
        {
            txtCurrentPassword.Focus();
            txtCurrentPassword.Text = "";
            txtNewPassword.Text = "";
            txtConfirmPassword.Text = "";
            this.Text = "Change Password";
        }
        private void frmChangePassword_Load(object sender, EventArgs e)
        {
            _RestDefualtValue();
            _UserInfo = clsUsersBusiness.FindByUserID(_UserID);
            if (_UserInfo == null)
            {
                MessageBox.Show("The User Is not Found","not Found",MessageBoxButtons.OK,MessageBoxIcon.Error);
                this.Close();
                return;
            }
            userCardInfo1.LoadUserInfo(_UserID);
        }

        private void txtCurrentPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtCurrentPassword.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtCurrentPassword, "The Current Password Can not be Null");
                return;
            }
            else
            {
                errorProvider1.SetError(txtCurrentPassword, null);
            };
            if (txtCurrentPassword.Text.Trim()!=_UserInfo.Password)
            {
                e.Cancel = true;
                errorProvider1.SetError(txtCurrentPassword, "The Current Password Wrong");
                return;
            }
            else
            {
                errorProvider1.SetError(txtCurrentPassword, null);
            };
        }

        private void txtNewPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNewPassword.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNewPassword, "The New Password Can not be Null");
                return;
            }
            else
            {
                errorProvider1.SetError(txtNewPassword, null);
            };
            

        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (txtConfirmPassword.Text.Trim()!= txtNewPassword.Text.Trim())
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmPassword, "The Confirm Password is not Match");
                return;
            }
            else
            {
                errorProvider1.SetError(txtConfirmPassword, null);
            };
        }

        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2ImageButton2_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("some fileds are Not Valide", "put The Mouse in The Red Icon To See Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _UserInfo.Password = txtNewPassword.Text.Trim();
            if (_UserInfo.Save())
            {
            
                MessageBox.Show("The Password Change it successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _RestDefualtValue();
                
            }
            else
            {
                MessageBox.Show("The Password Change Failed", "Password Change Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
    }
}
