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
using WindowsFormsApp4.PeopleForms;
using WindowsFormsApp4.Licensess;

namespace WindowsFormsApp4.Drivers
{
    public partial class frmListDrivers : Form
    {
      private  DataTable _dtAllDrivers;
        public frmListDrivers()
        {
            InitializeComponent();
        }

        private void guna2GradientPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2GradientPanel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2GradientPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void frmListDrivers_Load(object sender, EventArgs e)
        {
            cbFilterBy.SelectedIndex = 0;
            _dtAllDrivers = clsBusinessDrivers.GetAllDrivers();
            dgvAllDrivers.DataSource = _dtAllDrivers;
            lblRecordCount.Text = dgvAllDrivers.Rows.Count.ToString();

            if (dgvAllDrivers.Rows.Count > 0)
            {
                dgvAllDrivers.Columns[0].HeaderText = "Driver ID";
                dgvAllDrivers.Columns[0].Width = 120;

                dgvAllDrivers.Columns[1].HeaderText = "Person ID";
                dgvAllDrivers.Columns[1].Width = 120;

                dgvAllDrivers.Columns[2].HeaderText = "National No.";
                dgvAllDrivers.Columns[2].Width = 140;

                dgvAllDrivers.Columns[3].HeaderText = "Full Name";
                dgvAllDrivers.Columns[3].Width = 320;

                dgvAllDrivers.Columns[4].HeaderText = "Date";
                dgvAllDrivers.Columns[4].Width = 170;

                dgvAllDrivers.Columns[5].HeaderText = "Active Licenses";
                dgvAllDrivers.Columns[5].Width = 150;
            }

        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilter.Visible = (cbFilterBy.Text != "None");


            if (cbFilterBy.Text == "None")
            {
                txtFilter.Enabled = false;
            }
            else
            {
                txtFilter.Enabled = true;
            }
            txtFilter.Text = "";
            txtFilter.Focus();
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {

            string FilterColumn = "";
            //Map Selected Filter to real Column name 
            switch (cbFilterBy.Text)
            {
                case "Driver ID":
                    FilterColumn = "DriverID";
                    break;

                case "Person ID":
                    FilterColumn = "PersonID";
                    break;

                case "National No.":
                    FilterColumn = "NationalNo";
                    break;


                case "Full Name":
                    FilterColumn = "FullName";
                    break;

                default:
                    FilterColumn = "None";
                    break;

            }

            //Reset the filters in case nothing selected or filter value conains nothing.
            if (txtFilter.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtAllDrivers.DefaultView.RowFilter = "";
                lblRecordCount.Text = dgvAllDrivers.Rows.Count.ToString();
                return;
            }


            if (FilterColumn != "FullName" && FilterColumn != "NationalNo")
                //in this case we deal with numbers not string.
                _dtAllDrivers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilter.Text.Trim());
            else
                _dtAllDrivers.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilter.Text.Trim());

            lblRecordCount.Text = _dtAllDrivers.Rows.Count.ToString();
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "Driver ID" || cbFilterBy.Text == "Person ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

        }

        private void showPersonInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowPersonInfo frm = new ShowPersonInfo((int)dgvAllDrivers.CurrentRow.Cells[1].Value);
            frm.ShowDialog();
            frmListDrivers_Load(null, null);
        }

        private void dgvAllDrivers_DoubleClick(object sender, EventArgs e)
        {
            cmsDrivers.Show();
        }

        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void showPersonHToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowLicensesPersonhistory frm = new frmShowLicensesPersonhistory((int)dgvAllDrivers.CurrentRow.Cells[1].Value);
            frm.ShowDialog();
        
        }
    }
}
