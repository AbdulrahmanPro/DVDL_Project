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
namespace WindowsFormsApp4.Applications.ReplaceForDamageOrLost
{
    public partial class frmReplaceDamageOrLostLicenses : Form
    {
        private int _NewLicenseID = -1;
        public frmReplaceDamageOrLostLicenses()
        {
            InitializeComponent();
        }

        private void guna2GradientPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void frmReplaceDamageOrLostLicenses_Load(object sender, EventArgs e)
        {
            lblApplicationDate.Text = clsFormat.DateToShort(DateTime.Now);
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
            rbDamagedLicense.Checked = true;
        }

        private void rbDamagedLicense_CheckedChanged(object sender, EventArgs e)
        {
            lblTitle.Text = "Replacement For Damaged License";
            this.Text = lblTitle.Text;
            lblApplicationFees.Text = clsApplicationTypeBusiness.Find(_GetApplicationType()).ApplicationTypeFees.ToString();

        }
        private int _GetApplicationType()
        {
            if (rbDamagedLicense.Checked)
                return (int)ApplicationsBusiness.enApplicationType.ReplaceDamagedDrivingLicense;
            else
                return (int)ApplicationsBusiness.enApplicationType.ReplaceLostDrivingLicense;

        }
        private clsLicenses.enIssueReason _GetIssueReason()
        {
            if (rbDamagedLicense.Checked)
                return clsLicenses.enIssueReason.DamagedReplacement;
            else
                return clsLicenses.enIssueReason.LostReplacement;
        }

        private void rbLostLicense_CheckedChanged(object sender, EventArgs e)
        {
            lblTitle.Text = "Replacement For Lost License";
            this.Text = lblTitle.Text;
            lblApplicationFees.Text = clsApplicationTypeBusiness.Find(_GetApplicationType()).ApplicationTypeFees.ToString();

        }

        private void ctrDrivingLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            int SelectedLicenseID = obj;
            lblOldLicenseID.Text = SelectedLicenseID.ToString();
            llShowLicenseHistory.Enabled = (SelectedLicenseID != -1);
            if (SelectedLicenseID == -1)
            {
                return;
            }
            if (!ctrDrivingLicenseInfoWithFilter1.SelectedLicenseInfo.IsActive)
            {
                MessageBox.Show("Selected License Is Not Active, Choose Anothr License", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnIssueReplacement.Enabled = false;
                return;
            }
            btnIssueReplacement.Enabled = true;
        }

        private void btnIssueReplacement_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure You Want to Issue Replacement For The License", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            clsLicenses NewLicense = ctrDrivingLicenseInfoWithFilter1.SelectedLicenseInfo.Replace(_GetIssueReason(), clsGlobal.CurrentUser.UserID);
            if (NewLicense == null)
            {
                MessageBox.Show("Faild To Issue a Replacement for This License", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblApplicationID.Text = NewLicense.ApplicationID.ToString();
                _NewLicenseID = NewLicense.LicenseID;
                lblRreplacedLicenseID.Text = NewLicense.LicenseID.ToString();
                MessageBox.Show("License Rplacement Successfully With ID=" + _NewLicenseID.ToString(),"Successfully",MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnIssueReplacement.Enabled = false;
                ctrDrivingLicenseInfoWithFilter1.Enabled = false;
                gbReplacementFor.Enabled = false;
                llShowLicenseHistory.Enabled = true;
            }
        }

        private void bynClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
