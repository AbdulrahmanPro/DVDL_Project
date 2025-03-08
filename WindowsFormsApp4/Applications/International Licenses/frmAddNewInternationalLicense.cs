using DVDLBusiness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp4.Licensess.LocalDrivingLicense.Controls;
using WindowsFormsApp4.GlobalClasses;
namespace WindowsFormsApp4.Applications.International_Licenses
{
    public partial class frmAddNewInternationalLicense : Form
    {
        private int _InternationalLicenseID = -1;
        public frmAddNewInternationalLicense()
        {
            InitializeComponent();
        }

        private void frmAddNewInternationalLicense_Load(object sender, EventArgs e)
        {

        }

        private void ctrDrivingLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            int SelectedLicenseID = -1;

            lblLocalLicenseID.Text = SelectedLicenseID.ToString();
            llShowLicenseHistory.Enabled = (SelectedLicenseID != -1);
            if (SelectedLicenseID == -1)
            {
                return;
            }

            if (ctrDrivingLicenseInfoWithFilter1.SelectedLicenseInfo.LicenseClass != 3)
            {
                MessageBox.Show("Selected License should be class 3, Selected another one", "not alowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int ActiveInterntionalLicenseID = clsInternalLicensesBusiness.GetActiveInternationalLicenseIDByDriverID(ctrDrivingLicenseInfoWithFilter1.SelectedLicenseInfo.DriverID);
            if (ActiveInterntionalLicenseID != -1)
            {
                MessageBox.Show("Person already have an active international license with ID = " + ActiveInterntionalLicenseID.ToString(), "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                llShowLicenseInfo.Enabled = true;
                _InternationalLicenseID = ActiveInterntionalLicenseID;
                btnIssue.Enabled = false;
                return;
            }
            btnIssue.Enabled = true;
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Are you sure you want to issue the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }






            clsInternalLicensesBusiness InternationalLicense = new clsInternalLicensesBusiness();
            //those are the information for the base application, because it inhirts from application, they are part of the sub class.

            InternationalLicense.ApplicationPersonID = ctrDrivingLicenseInfoWithFilter1.SelectedLicenseInfo.DriverInfo.PersonID;
            InternationalLicense.ApplicationDate = DateTime.Now;
            InternationalLicense.ApplicationStatus = ApplicationsBusiness.enApplicationStatus.Completed;
            InternationalLicense.LastStatusDate = DateTime.Now;
            InternationalLicense.PaidFees = clsApplicationTypeBusiness.Find((int)ApplicationsBusiness.enApplicationType.NewInternationalLicense).ApplicationTypeFees;
            InternationalLicense.CreateByUserID = clsGlobal.CurrentUser.UserID;


            InternationalLicense.DriverID = ctrDrivingLicenseInfoWithFilter1.SelectedLicenseInfo.DriverID;
            InternationalLicense.IssuedUsingLocalLicenseID = ctrDrivingLicenseInfoWithFilter1.SelectedLicenseInfo.LicenseID;
            InternationalLicense.IssueDate = DateTime.Now;
            InternationalLicense.ExpirationDate = DateTime.Now.AddYears(1);

            InternationalLicense.CreateByUserID = clsGlobal.CurrentUser.UserID;

            if (!InternationalLicense.Save())
            {
                MessageBox.Show("Faild to Issue International License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
            }

            lblApplicationID.Text = InternationalLicense.ApplicationID.ToString();
            _InternationalLicenseID = InternationalLicense.InternationalLicenseID;
            lblInternationalLicenseID.Text = InternationalLicense.InternationalLicenseID.ToString();
            MessageBox.Show("International License Issued Successfully with ID=" + InternationalLicense.InternationalLicenseID.ToString(), "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnIssue.Enabled = false;
            ctrDrivingLicenseInfoWithFilter1.FilterEnabled = false;
            llShowLicenseInfo.Enabled = true;

        }

        private void ctrDrivingLicenseInfoWithFilter1_Load(object sender, EventArgs e)
        {

        }
    }
}
