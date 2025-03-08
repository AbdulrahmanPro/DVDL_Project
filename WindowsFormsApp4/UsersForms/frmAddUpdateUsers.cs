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
    public partial class frmAddUpdateUsers : Form
    {
        public frmAddUpdateUsers()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;
        }
        public frmAddUpdateUsers(int UserID)
        {
            InitializeComponent();
            _Mode = enMode.Update;
            _UserID = UserID;
        }
        enum enMode { AddNew=1, Update=2 };
        private enMode _Mode;
        private int _UserID = -1;
        
        clsUsersBusiness  UserInfo;
        private void _RestDefualtValue()
        {
            if (_Mode == enMode.AddNew)
            {
                lblTitel.Text = "Add New User";
                this.Text = "Add New User";
                UserInfo = new clsUsersBusiness();
                personInfoWithFilters1.FilterFocus();
            }
            else
            {
                lblTitel.Text = "Update User ";
                this.Text = "UpdateUser";
                tbControl.Enabled = true;
                btnSave.Enabled = true;

            }
            txtUserName.Text = "";
            txtPassword.Text = "";
            txtConfirmPassword.Text = "";
            ChIsActive.Checked = true;
        }
        private void _LoadData()
        {
            UserInfo = clsUsersBusiness.FindByUserID(_UserID);
            if (UserInfo == null)
            {
                MessageBox.Show("No User With ID =" + _UserID, "User Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            lblUserID.Text = UserInfo.UserID.ToString();
            txtUserName.Text = UserInfo.UserName;
            txtPassword.Text = UserInfo.Password;
            txtPassword.Text = UserInfo.Password;
            ChIsActive.Checked = UserInfo.IsActive;
            personInfoWithFilters1.LoadpersonInfo(UserInfo.PersonID);
        }
        private void personInfoWithFilters1_Load(object sender, EventArgs e)
        {

        }

       
        private void btnNext_Click(object sender, EventArgs e)
        {
            if (_Mode == enMode.Update)
            {
                btnSave.Enabled=true;
                tbControl.SelectedTab = tbControl.TabPages["tbLoginInfo"];
                return;
            }
            if (personInfoWithFilters1.PersonID != -1)
            {
                if (clsUsersBusiness.IsUserExistForPersonID(personInfoWithFilters1.PersonID))
                {
                    MessageBox.Show("Selected Person Already has a User, choose Another One", "Selected Person Already Exist", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    personInfoWithFilters1.FilterFocus();
                }
                else
                {
                    btnSave.Enabled = true;
                    tbControl.SelectedTab = tbControl.TabPages["tbLoginInfo"];
                }
            }
            else
            {
                MessageBox.Show("Please Selected a Person", "Select a Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                personInfoWithFilters1.FilterFocus();
            }
        }

        private void txtUserName_Validated(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtUserName.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtUserName, "User Name Can not be blank!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtUserName, null);
            }
            if (_Mode == enMode.AddNew)
            {
                if (clsUsersBusiness.IsUserExist(_UserID))
                {
                    e.Cancel = true;
                    errorProvider1.SetError(txtUserName, "UserName is  Alrealdy used");
                }
                else
                {
                    errorProvider1.SetError(txtUserName, null);
                };
            }
            else
            {
                if (UserInfo.UserName == txtUserName.Text.Trim())
                {
                    e.Cancel = true;
                    errorProvider1.SetError(txtUserName, "UserName is  Alrealdy used");
                }
                else
                {
                    errorProvider1.SetError(txtUserName, null);
                };
            }
        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtPassword, "The Password Can not be Null");
            }
            else
            {
                errorProvider1.SetError(txtPassword, null);
            }
        }

        private void txtConfirmPassword_Validating(object sender, CancelEventArgs e)
        {
            if (txtConfirmPassword.Text.Trim() != txtPassword.Text.Trim())
            {
                e.Cancel = true;
                errorProvider1.SetError(txtConfirmPassword, "Password Confirmation does not match Password!");
            }
            else
            {
                errorProvider1.SetError(txtConfirmPassword, null);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("some Fileds is not Valid","put mouse on the red Icons to see The Error"
                    , MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            UserInfo.PersonID = personInfoWithFilters1.PersonID;
            UserInfo.UserName = txtUserName.Text.Trim();
            UserInfo.Password = txtPassword.Text.Trim();
            UserInfo.IsActive = ChIsActive.Checked;
            if (UserInfo.Save())
            {
                lblUserID.Text = UserInfo.UserID.ToString();
                lblTitel.Text = "Update User";
                this.Text = "Update User";
                _Mode = enMode.Update;
                MessageBox.Show("Data Save Successfuly", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void frmAddUpdateUsers_Load(object sender, EventArgs e)
        {
            _RestDefualtValue();
            if (_Mode == enMode.Update)
                _LoadData();
        }

        private void personInfoWithFilters1_Load_1(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();

        }
    }
}
