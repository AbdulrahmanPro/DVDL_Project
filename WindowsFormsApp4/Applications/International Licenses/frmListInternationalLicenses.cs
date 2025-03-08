using WindowsFormsApp4.Licensess.InternationalLicenses;
using WindowsFormsApp4.Licensess;
using DVDLBusiness;
using WindowsFormsApp4.PeopleForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp4.Applications.International_Licenses
{
    public partial class frmListInternationalLicenses : Form
    {
        private DataTable _dtInterNationalLiceseApplications;
        public frmListInternationalLicenses()
        {
            InitializeComponent();
        }

        private void frmListInternationalLicenses_Load(object sender, EventArgs e)
        {
            _dtInterNationalLiceseApplications = clsInternalLicensesBusiness.GetAllInternationalLicenses();
            cbFilterBy.SelectedIndex = 0;
            dgvInternationalLicense.DataSource = _dtInterNationalLiceseApplications;
            lblInternationalLicensesRecords.Text = dgvInternationalLicense.Rows.Count.ToString();

            if (dgvInternationalLicense.Rows.Count > 0)
            {
                dgvInternationalLicense.Columns[0].HeaderText = "Int.License ID";
                dgvInternationalLicense.Columns[0].Width = 160;

                dgvInternationalLicense.Columns[1].HeaderText = "Application ID";
                dgvInternationalLicense.Columns[1].Width = 150;

                dgvInternationalLicense.Columns[2].HeaderText = "Driver ID";
                dgvInternationalLicense.Columns[2].Width = 130;

                dgvInternationalLicense.Columns[3].HeaderText = "L.License ID";
                dgvInternationalLicense.Columns[3].Width = 130;

                dgvInternationalLicense.Columns[4].HeaderText = "Issue Date";
                dgvInternationalLicense.Columns[4].Width = 180;

                dgvInternationalLicense.Columns[5].HeaderText = "Expiration Date";
                dgvInternationalLicense.Columns[5].Width = 180;

                dgvInternationalLicense.Columns[6].HeaderText = "Is Active";
                dgvInternationalLicense.Columns[6].Width = 120;

            }
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            frmAddNewInternationalLicense frm = new frmAddNewInternationalLicense();
            frm.ShowDialog();
            frmListInternationalLicenses_Load(null, null);
        }

        private void PesonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int DriverID =(int)dgvInternationalLicense.CurrentRow.Cells[2].Value;
            int PersonID = clsBusinessDrivers.FindByDriverID(DriverID).PersonID;
            ShowPersonInfo frm = new ShowPersonInfo(PersonID);
            frm.ShowDialog();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int InternationalLicenseID = (int)dgvInternationalLicense.CurrentRow.Cells[0].Value;
            frmShowInternationalLicenseInfo frm = new frmShowInternationalLicenseInfo(InternationalLicenseID);
            frm.ShowDialog();
            
        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {

            int DriverID = (int)dgvInternationalLicense.CurrentRow.Cells[2].Value;
            int PersonID = clsBusinessDrivers.FindByDriverID(DriverID).PersonID;
            frmShowLicensesPersonhistory frm = new frmShowLicensesPersonhistory(PersonID);
            frm.ShowDialog();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cbFilterBy.Text == "Is Active")
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
            string FilterColumn = "IsActive";
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
                _dtInterNationalLiceseApplications.DefaultView.RowFilter = "";
            else
                //in this case we deal with numbers not string.
                _dtInterNationalLiceseApplications.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, FilterValue);

            lblInternationalLicensesRecords.Text = _dtInterNationalLiceseApplications.Rows.Count.ToString();

        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {


            string FilterColumn = "";
            //Map Selected Filter to real Column name 
            switch (cbFilterBy.Text)
            {
                case "International License ID":
                    FilterColumn = "InternationalLicenseID";
                    break;
                case "Application ID":
                    {
                        FilterColumn = "ApplicationID";
                        break;
                    };

                case "Driver ID":
                    FilterColumn = "DriverID";
                    break;

                case "Local License ID":
                    FilterColumn = "IssuedUsingLocalLicenseID";
                    break;

                case "Is Active":
                    FilterColumn = "IsActive";
                    break;


                default:
                    FilterColumn = "None";
                    break;
            }


            //Reset the filters in case nothing selected or filter value conains nothing.
            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtInterNationalLiceseApplications.DefaultView.RowFilter = "";
                lblInternationalLicensesRecords.Text = dgvInternationalLicense.Rows.Count.ToString();
                return;
            }



            _dtInterNationalLiceseApplications.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());

            lblInternationalLicensesRecords.Text = _dtInterNationalLiceseApplications.Rows.Count.ToString();

        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

        }

        private void dgvInternationalLicense_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
