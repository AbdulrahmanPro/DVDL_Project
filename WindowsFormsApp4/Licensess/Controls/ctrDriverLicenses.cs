using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp4.Licensess.LocalDrivingLicense;
using DVDLBusiness;

namespace WindowsFormsApp4.Licensess.Controls
{
    public partial class ctrDriverLicenses : UserControl
    {
        public ctrDriverLicenses()
        {
            InitializeComponent();
        }
        private int _DriverID = -1;
        private clsBusinessDrivers _DriverInfo;
        private DataTable _dtDriverLocalLicenseHistory;
        private DataTable _dtDriverInternationalLicenseHistory;

        public void LoadInfo(int DriverID)
        {
            _DriverID = DriverID;
            _DriverInfo = clsBusinessDrivers.FindByDriverID(DriverID);
            if (_DriverInfo == null)
            {
                MessageBox.Show("There is No Driver with Id =" + _DriverID.ToString(), "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _LoadLocalDriverLicenseInfo();
            _LoadInteernationalDriverLicenseInfo();
        }


        public void LoadInfoByPersonID(int PersonID)
        {
            _DriverInfo = clsBusinessDrivers.FindByPersonID(PersonID);
            if (_DriverInfo == null)
            {
                MessageBox.Show("There is No Driver Linked with Person ID =" + PersonID.ToString(), "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _DriverID = _DriverInfo.DriverID;
            _LoadLocalDriverLicenseInfo();
            _LoadInteernationalDriverLicenseInfo();
        }

        private void _LoadLocalDriverLicenseInfo()
        {
            _dtDriverLocalLicenseHistory = clsBusinessDrivers.GetLicenses(_DriverID);
            dgvLocalLicensesHistory.DataSource = _dtDriverLocalLicenseHistory;
            lblLocalLicensesRecords.Text = dgvLocalLicensesHistory.Rows.Count.ToString();

            if (dgvLocalLicensesHistory.Rows.Count > 0)
            {
                dgvLocalLicensesHistory.Columns[0].HeaderText = "Lic.ID";
                dgvLocalLicensesHistory.Columns[0].Width = 110;

                dgvLocalLicensesHistory.Columns[1].HeaderText = "App.ID";
                dgvLocalLicensesHistory.Columns[1].Width = 110;

                dgvLocalLicensesHistory.Columns[2].HeaderText = "Class Name";
                dgvLocalLicensesHistory.Columns[2].Width = 270;

                dgvLocalLicensesHistory.Columns[3].HeaderText = "Issue Date";
                dgvLocalLicensesHistory.Columns[3].Width = 170;

                dgvLocalLicensesHistory.Columns[4].HeaderText = "Expiration Date";
                dgvLocalLicensesHistory.Columns[4].Width = 170;

                dgvLocalLicensesHistory.Columns[5].HeaderText = "Is Active";
                dgvLocalLicensesHistory.Columns[5].Width = 110;

            }

        }
        private void _LoadInteernationalDriverLicenseInfo()
        {
            _dtDriverInternationalLicenseHistory = clsBusinessDrivers.GetInternationalLicenses(_DriverID);
            dgvInternationalLicensesHistory.DataSource = _dtDriverInternationalLicenseHistory;
            if (dgvInternationalLicensesHistory.Rows.Count > 0)
            {
                dgvInternationalLicensesHistory.Columns[0].HeaderText = "Int.License ID";
                dgvInternationalLicensesHistory.Columns[0].Width = 160;

                dgvInternationalLicensesHistory.Columns[1].HeaderText = "Application ID";
                dgvInternationalLicensesHistory.Columns[1].Width = 130;

                dgvInternationalLicensesHistory.Columns[2].HeaderText = "L.License ID";
                dgvInternationalLicensesHistory.Columns[2].Width = 130;

                dgvInternationalLicensesHistory.Columns[3].HeaderText = "Issue Date";
                dgvInternationalLicensesHistory.Columns[3].Width = 180;

                dgvInternationalLicensesHistory.Columns[4].HeaderText = "Expiration Date";
                dgvInternationalLicensesHistory.Columns[4].Width = 180;

                dgvInternationalLicensesHistory.Columns[5].HeaderText = "Is Active";
                dgvInternationalLicensesHistory.Columns[5].Width = 120;

            }

        }
        public void Clear()
        {
            _dtDriverInternationalLicenseHistory.Clear();
            _dtDriverLocalLicenseHistory.Clear();
        }
        private void ctrDriverLicenses_Load(object sender, EventArgs e)
        {

        }

        private void showLicenseInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
           frmShowLicenseInfo frm = new frmShowLicenseInfo((int)dgvLocalLicensesHistory.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void dgvLocalLicensesHistory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
