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

namespace WindowsFormsApp4.Licensess.LocalDrivingLicense
{
    public partial class IssueDrivingLicenseForFirstTime : Form
    {
        private int _LocalDrivingLicenseApplicationID = -1;
        private clsLocalDrivingLicenseBusiness _LocalDrivingLicenseInfo;
        public IssueDrivingLicenseForFirstTime(int LocalDrivingLicenseID)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseID;
        }

        private void IssueDrivingLicenseForFirstTime_Load(object sender, EventArgs e)
        {
            txtNotes.Focus();
            _LocalDrivingLicenseInfo = clsLocalDrivingLicenseBusiness.FindByLocalDrivingLicenseApplication(_LocalDrivingLicenseApplicationID);
            if (_LocalDrivingLicenseInfo == null)
            {
                MessageBox.Show("No Applicaion With ID =" + _LocalDrivingLicenseApplicationID.ToString(), "Not Found !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }
            if (!_LocalDrivingLicenseInfo.PassedAllTests())
            {
                MessageBox.Show("Person Should Pass All Test First", "Not Allowed !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;

            }
            int LicenseID = _LocalDrivingLicenseInfo.GetActiveLicenseID();
            if (LicenseID!=-1)
            {
                MessageBox.Show("Person Already Has License Before With ID= "+LicenseID.ToString(), "Already has License !", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;

            }

            ctrLocalDrivingInfo1.LoadApplicationInfoByLocalDrivingAppID(_LocalDrivingLicenseApplicationID);

        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            int LicenseID = _LocalDrivingLicenseInfo.IssueLicenseForTheFirtTime(txtNotes.Text.Trim(), clsGlobal.CurrentUser.UserID);
            if (LicenseID != -1)
            {
                MessageBox.Show("License Issued SuccessFully With License ID =" + LicenseID, "Saved Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else 
            {
                MessageBox.Show("License Was Not Issued !", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
    }
}
