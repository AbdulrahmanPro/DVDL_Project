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
using WindowsFormsApp4.Licensess.LocalDrivingLicense;
using WindowsFormsApp4.Licensess;

namespace WindowsFormsApp4.Applications.DetainedLicenses
{
    public partial class ReleaseDetainedLicense : Form
    {
        private int _SelectedLicensID = -1;
        public ReleaseDetainedLicense()
        {
            InitializeComponent();
        }
        public ReleaseDetainedLicense(int LicenstID)
        {
            InitializeComponent();
            _SelectedLicensID = LicenstID;
            ctrDrivingLicenseInfoWithFilter1.LoadLicenseInfo(_SelectedLicensID);
            ctrDrivingLicenseInfoWithFilter1.FilterEnabled = false;
        }

        private void ReleaseDetainedLicense_Load(object sender, EventArgs e)
        {

        }

        private void ctrDrivingLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            _SelectedLicensID = obj;
            lblLicenseID.Text = _SelectedLicensID.ToString();
            llShowLicenseHistory.Enabled = (_SelectedLicensID != -1);
            if (_SelectedLicensID == -1)
            {
                return;
            }
            if (!ctrDrivingLicenseInfoWithFilter1.SelectedLicenseInfo.IsDetained)
            {
                MessageBox.Show("Selected License Is Not Detained", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            lblApplicationFees.Text = clsApplicationTypeBusiness.Find((int)ApplicationsBusiness.enApplicationType.ReleaseDetainedDrivingLicsense).ApplicationTypeFees.ToString();
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
            lblDetainID.Text = ctrDrivingLicenseInfoWithFilter1.SelectedLicenseInfo.DetainedInfo.DetainID.ToString();
            lblLicenseID.Text = ctrDrivingLicenseInfoWithFilter1.SelectedLicenseInfo.LicenseID.ToString();
            lblCreatedByUser.Text = ctrDrivingLicenseInfoWithFilter1.SelectedLicenseInfo.DetainedInfo.CreatedByUserInfo.UserName;
            lblDetainDate.Text = clsFormat.DateToShort(ctrDrivingLicenseInfoWithFilter1.SelectedLicenseInfo.DetainedInfo.DetainDate);
            lblFineFees.Text = ctrDrivingLicenseInfoWithFilter1.SelectedLicenseInfo.DetainedInfo.FineFees.ToString();
            lblTotalFees.Text = (Convert.ToSingle(lblApplicationFees.Text) + Convert.ToSingle(lblFineFees.Text)).ToString();

            btnRelease.Enabled = true;

        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
           if( MessageBox.Show("Are You sure you Want To Realse this Detained License", "confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            int ApplicationID = -1;

            lblApplicationID.Text = ApplicationID.ToString();
            bool IsRelease = ctrDrivingLicenseInfoWithFilter1.SelectedLicenseInfo.ReleaseDetainedLicense(clsGlobal.CurrentUser.UserID,ref ApplicationID);

            if (!IsRelease)
            {
                MessageBox.Show("Faild to Release License", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            MessageBox.Show("Detained License  Released successfully", "Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnRelease.Enabled = false;
            ctrDrivingLicenseInfoWithFilter1.FilterEnabled = false;
            llShowLicenseInfo.Enabled = true;

        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicenseInfo frm = new frmShowLicenseInfo(_SelectedLicensID);
            frm.ShowDialog();
        }

        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmShowLicensesPersonhistory frm = new frmShowLicensesPersonhistory(ctrDrivingLicenseInfoWithFilter1.SelectedLicenseInfo.DriverInfo.PersonID);
            frm.ShowDialog();
        }

        private void ReleaseDetainedLicense_Activated(object sender, EventArgs e)
        {
            ctrDrivingLicenseInfoWithFilter1.txtLicenseIDFocus();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ctrDrivingLicenseInfoWithFilter1_Load(object sender, EventArgs e)
        {

        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }
    }
}
