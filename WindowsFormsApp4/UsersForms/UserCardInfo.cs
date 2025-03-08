using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVDLBusiness;

namespace WindowsFormsApp4.UsersForms
{
    public partial class UserCardInfo : UserControl
    {
        public UserCardInfo()
        {
            InitializeComponent();
        }
        private int _UserID;
        private clsUsersBusiness _UserInfo;
        public int UserID { get; }
        private void ResetUserInformation()
        {
            personInfo1.RestToDefualtvalues();
            lblUserID.Text = "[???]";
            lblUserName.Text = "[???]";
            lblIsActive.Text = "[???]";
        }
        public void LoadUserInfo(int UserID)
        {
            _UserID = UserID;
            _UserInfo = clsUsersBusiness.FindByUserID(UserID);
            if (_UserInfo == null)
            {
                MessageBox.Show("The User Is Not Exist", "Not Exist", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _FillUserInfo();
        }private void _FillUserInfo()
        {
            lblUserID.Text = _UserInfo.UserID.ToString();
            lblUserName.Text = _UserInfo.UserName;
            if (_UserInfo.IsActive)
                lblIsActive.Text = "Yes";
            else
                lblIsActive.Text = "No";
            personInfo1.LoadPersonInfo(_UserInfo.PersonID);
        }
    }
}
