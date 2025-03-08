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
using WindowsFormsApp4.PeopleForms;
using WindowsFormsApp4.Controls;

namespace WindowsFormsApp4.Applications.LocalDrivingLicense.Controls
{
    public partial class ctrLocalDrivingInfo : UserControl
    {
        private clsLocalDrivingLicenseBusiness _LocalDrivingLicenseInfo;
        private int _LocalDrivingLicenseApplicationID;
        private int _LicenseID;
        public int LocalDrivingLicenseApplicationID { 
            get { return _LocalDrivingLicenseApplicationID; }
        }
        

        public ctrLocalDrivingInfo()
        {
            InitializeComponent();
        }
        private void _ResetLocalDrivingLicenseApplicationInfo()
        {
            lblDLApplicationID.Text = "????";
            lblAppenedForLicense.Text = "????";
            lblPassedTest.Text = "????";
            ctrApplicationInfo1.ResetApplicationInfo();
        }
        public void LoadApplicationInfoByLocalDrivingAppID(int LocalDrivingLicenseApplicationID)
        {
            _LocalDrivingLicenseInfo = clsLocalDrivingLicenseBusiness.FindByLocalDrivingLicenseApplication(LocalDrivingLicenseApplicationID);
            if (_LocalDrivingLicenseInfo == null)
            {
                _ResetLocalDrivingLicenseApplicationInfo();
                MessageBox.Show("No Application with Application ID " + LocalDrivingLicenseApplicationID, "Not Found!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _FillLocalDrivingLicenseApplicationInfo();
        }
        public void LoadApplicationInfoByApplicationID(int ApplicationID)
        {
            _LocalDrivingLicenseInfo = clsLocalDrivingLicenseBusiness.FindByApplicationID(ApplicationID);
            if (_LocalDrivingLicenseInfo == null)
            {
                _ResetLocalDrivingLicenseApplicationInfo();
                MessageBox.Show("No Application with Application ID " + LocalDrivingLicenseApplicationID, "Not Found!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _FillLocalDrivingLicenseApplicationInfo();
        }
        private void _FillLocalDrivingLicenseApplicationInfo()
        {
            _LicenseID = _LocalDrivingLicenseInfo.GetActiveLicenseID();
            llShowLicenseInfo.Enabled = (_LicenseID != -1);

            lblDLApplicationID.Text = _LocalDrivingLicenseInfo.LocalDrivingLicenseApplicationID.ToString();
            lblAppenedForLicense.Text = clsClassLicenseBusiness.Find(_LocalDrivingLicenseInfo.LicenseClassID).ToString();
            lblPassedTest.Text = _LocalDrivingLicenseInfo.GetPassedTestCount().ToString();
            ctrApplicationInfo1.LoadApplicationInfo(_LocalDrivingLicenseInfo.ApplicationID);
        }
        private void guna2HtmlLabel14_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel4_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowPersonInfo frm = new ShowPersonInfo(_LocalDrivingLicenseInfo.ApplicationPersonID);
            frm.ShowDialog();
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
           
        }

        private void llShowLicenseInfo_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }
    }
}
