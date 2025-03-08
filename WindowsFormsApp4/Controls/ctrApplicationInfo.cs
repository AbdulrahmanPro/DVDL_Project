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
using WindowsFormsApp4.GlobalClasses;
using WindowsFormsApp4.PeopleForms;

namespace WindowsFormsApp4.Controls
{
    public partial class ctrApplicationInfo : UserControl
    {
        private int _ApplicationID=-1;
        private ApplicationsBusiness _ApplicationInfo;
        public int ApplicationID {
            get { return _ApplicationID; }
        }

        
        public ctrApplicationInfo()
        {
            InitializeComponent();
        }
        public void ResetApplicationInfo()
        {
            _ApplicationID = -1;
            lblApplicationID.Text = "????";
            lblApplicationStatus.Text = "????";
            lblApplicationFees.Text = "????";
            lblApplicationType.Text = "????";
            lblApplicatnt.Text = "????";
            lblApplicationDate.Text = "????";
            lblStatusDate.Text = "????";
            lblCreatedBy.Text = "????";
        }

        private void _FillApplicationInfo()
        {
            _ApplicationID = _ApplicationInfo.ApplicationID;
            lblApplicationID.Text = _ApplicationInfo.ApplicationID.ToString();
            lblApplicationStatus.Text = _ApplicationInfo.StatusText;
            lblApplicationType.Text = _ApplicationInfo.ApplicationTypeInfo.ApplicationTypeTitle;
            lblApplicationFees.Text = _ApplicationInfo.PaidFees.ToString();
            lblApplicatnt.Text = _ApplicationInfo.ApplicantFullName;
            lblApplicationDate.Text = clsFormat.DateToShort(_ApplicationInfo.ApplicationDate);
            lblStatusDate.Text = clsFormat.DateToShort(_ApplicationInfo.LastStatusDate);
            lblCreatedBy.Text = _ApplicationInfo.CreateUserInfo.UserName;
        }
        public void LoadApplicationInfo(int ApplicationID)
        {
            _ApplicationInfo = ApplicationsBusiness.FindBaseApplication(ApplicationID);
            if (_ApplicationInfo == null)
            {
                ResetApplicationInfo();
                MessageBox.Show("The Application With Application ID" + ApplicationID + "Not Found", "Not Found",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            _FillApplicationInfo();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowPersonInfo frm = new ShowPersonInfo(_ApplicationInfo.ApplicationPersonID);
            frm.ShowDialog();
            LoadApplicationInfo(_ApplicationID);
        }
    }
}
