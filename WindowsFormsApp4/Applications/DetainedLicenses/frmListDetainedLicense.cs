using WindowsFormsApp4.PeopleForms;
using WindowsFormsApp4.Applications.DetainedLicenses;
using WindowsFormsApp4.Licensess;
using WindowsFormsApp4.Licensess.Detained_Licenses;
using WindowsFormsApp4.Licensess.LocalDrivingLicense;
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

namespace WindowsFormsApp4.Applications.DetainedLicenses
{
    public partial class frmListDetainedLicense : Form
    {
        private DataTable _dtDetainedLicense;

        public frmListDetainedLicense()
        {
            InitializeComponent();
        }

        private void guna2GradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void frmListDetainedLicense_Load(object sender, EventArgs e)
        {
            cbFilterBy.SelectedIndex = 0;
            _dtDetainedLicense = clsDetainedLicenses.GetAllDetainedLicenses();
            dgvDetainedLicense.DataSource = _dtDetainedLicense;
            lblTotalRecords.Text = dgvDetainedLicense.Rows.Count.ToString();


            if (dgvDetainedLicense.Rows.Count > 0)
            {
                dgvDetainedLicense.Columns[0].HeaderText = "D.ID";
                dgvDetainedLicense.Columns[0].Width = 90;

                dgvDetainedLicense.Columns[1].HeaderText = "L.ID";
                dgvDetainedLicense.Columns[1].Width = 90;

                dgvDetainedLicense.Columns[2].HeaderText = "D.Date";
                dgvDetainedLicense.Columns[2].Width = 160;

                dgvDetainedLicense.Columns[3].HeaderText = "Is Released";
                dgvDetainedLicense.Columns[3].Width = 110;

                dgvDetainedLicense.Columns[4].HeaderText = "Fine Fees";
                dgvDetainedLicense.Columns[4].Width = 110;

                dgvDetainedLicense.Columns[5].HeaderText = "Release Date";
                dgvDetainedLicense.Columns[5].Width = 160;

                dgvDetainedLicense.Columns[6].HeaderText = "N.No.";
                dgvDetainedLicense.Columns[6].Width = 90;

                dgvDetainedLicense.Columns[7].HeaderText = "Full Name";
                dgvDetainedLicense.Columns[7].Width = 330;

                dgvDetainedLicense.Columns[8].HeaderText = "Rlease App.ID";
                dgvDetainedLicense.Columns[8].Width = 150;

            }


        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmsApplications_Opening(object sender, CancelEventArgs e)
        {
            releaseDetainedLicenseToolStripMenuItem.Enabled = !(bool)dgvDetainedLicense.CurrentRow.Cells[3].Value;
        }

        private void PesonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            int LicenseID = (int)dgvDetainedLicense.CurrentRow.Cells[1].Value;
            int PersonID = clsLicenses.Find(LicenseID).DriverInfo.PersonID;

            ShowPersonInfo frm = new ShowPersonInfo(PersonID);
            frm.ShowDialog();
            frmListDetainedLicense_Load(null, null);

        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            frmShowLicenseInfo frm = new frmShowLicenseInfo((int)dgvDetainedLicense.CurrentRow.Cells[1].Value);
            frm.ShowDialog();
            frmListDetainedLicense_Load(null, null);

        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvDetainedLicense.CurrentRow.Cells[1].Value;
            int PersonID = clsLicenses.Find(LicenseID).DriverInfo.PersonID;

            frmShowLicensesPersonhistory frm = new frmShowLicensesPersonhistory(PersonID);
            frm.ShowDialog();
            frmListDetainedLicense_Load(null, null);

        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int LicenseID = (int)dgvDetainedLicense.CurrentRow.Cells[1].Value;

            ReleaseDetainedLicense frm = new ReleaseDetainedLicense(LicenseID);
            frm.ShowDialog();

            frmListDetainedLicense_Load(null, null);
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            //Map Selected Filter to real Column name 
            switch (cbFilterBy.Text)
            {
                case "Detain ID":
                    FilterColumn = "DetainID";
                    break;
                case "Is Released":
                    {
                        FilterColumn = "IsReleased";
                        break;
                    };

                case "National No.":
                    FilterColumn = "NationalNo";
                    break;


                case "Full Name":
                    FilterColumn = "FullName";
                    break;

                case "Release Application ID":
                    FilterColumn = "ReleaseApplicationID";
                    break;

                default:
                    FilterColumn = "None";
                    break;
            }
        }
        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cbFilterBy.Text == "Is Released")
            {
                txtFilterValue.Visible = false;
                cbIsReleased.Visible = true;
                cbIsReleased.Focus();
                cbIsReleased.SelectedIndex = 0;
            }

            else

            {

                txtFilterValue.Visible = (cbFilterBy.Text != "None");
                cbIsReleased.Visible = false;

                if (cbFilterBy.Text == "None")
                {
                    txtFilterValue.Enabled = false;
                    //_dtDetainedLicenses.DefaultView.RowFilter = "";
                    //lblTotalRecords.Text = dgvDetainedLicenses.Rows.Count.ToString();

                }
                else
                    txtFilterValue.Enabled = true;

                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }
            }

        private void cbIsReleased_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterColumn = "IsReleased";
            string FilterValue = cbIsReleased.Text;

            switch (FilterValue)
            {
                case "All":
                    break;
                case "Yes":
                    FilterValue = "1";
                    break;
                case "No":
                    FilterValue = "0";
                    break;
            }


            if (FilterValue == "All")
                _dtDetainedLicense.DefaultView.RowFilter = "";
            else
                //in this case we deal with numbers not string.
                _dtDetainedLicense.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, FilterValue);

            lblTotalRecords.Text = _dtDetainedLicense.Rows.Count.ToString();

        }

        private void btnDetainLicense_Click(object sender, EventArgs e)
        {
            frmDetainLicense frm = new frmDetainLicense();
            frm.ShowDialog();
            //refresh
            frmListDetainedLicense_Load(null, null);

        }

        private void btnReleaseDetainedLicense_Click(object sender, EventArgs e)
        {
            ReleaseDetainedLicense frm = new ReleaseDetainedLicense();
            frm.ShowDialog();
            frmListDetainedLicense_Load(null, null);
        }
    }
}
