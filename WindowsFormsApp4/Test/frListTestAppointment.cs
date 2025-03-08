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

namespace WindowsFormsApp4.Test
{
    public partial class frListTestAppointment : Form
    {
      private  DataTable _dtLicenseTestAppointments;
      private int _LocalDrivingLicenseApplicationID;
      private clsTestTypeBusiness.enTestType _TestTypeID;

        public frListTestAppointment(int LocalDrivingLicenseApplicationID,clsTestTypeBusiness.enTestType TestTypeID)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            _TestTypeID = TestTypeID;
        }
        private void _LoadImageAndTitleTestType()
        {
            switch (_TestTypeID)
            {
                case clsTestTypeBusiness.enTestType.VisionTest:
                    lblTitle.Text = "Vision Test Appointments";
                    pbImageTestType.Image = Properties.Resources.Vision_512;return;
                case clsTestTypeBusiness.enTestType.WrittenTest:
                    lblTitle.Text = "Written Test Appointments";
                    pbImageTestType.Image = Properties.Resources.Written_Test_512; return;
                case clsTestTypeBusiness.enTestType.StreetTest:
                    lblTitle.Text = "Street Test Appointments";
                    pbImageTestType.Image = Properties.Resources.Street_Test_32; return;

            }

        }
        private void frListTestAppointment_Load(object sender, EventArgs e)
        {
            _LoadImageAndTitleTestType();
            ctrLocalDrivingInfo1.LoadApplicationInfoByLocalDrivingAppID(_LocalDrivingLicenseApplicationID);
            _dtLicenseTestAppointments = clsBusinessTestAppointments.GetApplicationTestAppointmentPerTestType(_LocalDrivingLicenseApplicationID,_TestTypeID);
            dgvLicenseAppointmentTest.DataSource = _dtLicenseTestAppointments;
            lblRecordCount.Text = dgvLicenseAppointmentTest.Rows.Count.ToString();
            if (dgvLicenseAppointmentTest.Rows.Count > 0)
            {
                dgvLicenseAppointmentTest.Columns[0].HeaderText = "Appointment ID";
                dgvLicenseAppointmentTest.Columns[0].Width = 150;

                dgvLicenseAppointmentTest.Columns[1].HeaderText = "Appointment Date";
                dgvLicenseAppointmentTest.Columns[1].Width = 200;
              
                dgvLicenseAppointmentTest.Columns[2].HeaderText = "Paid Fees";
                dgvLicenseAppointmentTest.Columns[2].Width = 150;
               
                dgvLicenseAppointmentTest.Columns[2].HeaderText = "Is Locked";
                dgvLicenseAppointmentTest.Columns[2].Width = 100;

            }
        }

        private void AddNewScheduleTest_Click(object sender, EventArgs e)
        {
            clsLocalDrivingLicenseBusiness LocalDrivingLicenseInfo = clsLocalDrivingLicenseBusiness.FindByLocalDrivingLicenseApplication(_LocalDrivingLicenseApplicationID);

            if (LocalDrivingLicenseInfo.IsThereAnActiveScheduledTest(_TestTypeID))
            {
                MessageBox.Show("Person Already Have an Activ Appointments for this Test", "Already Have Appointments", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            clsBusinessTest LastTest = LocalDrivingLicenseInfo.GetLastTestPerTestType(_TestTypeID);
            if (LastTest == null)
            {
                frmScheduleTest frm1 = new frmScheduleTest(_LocalDrivingLicenseApplicationID,_TestTypeID);
                frm1.ShowDialog();
                frListTestAppointment_Load(null, null);
                return;
            }
            if (LastTest.TestResult == true)
            {
                MessageBox.Show("The Person Already Passed on The Test Before ", "Already Passed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            frmScheduleTest frm2 = new frmScheduleTest(_LocalDrivingLicenseApplicationID, _TestTypeID);
            frm2.ShowDialog();
            frListTestAppointment_Load(null, null);
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmScheduleTest frm = new frmScheduleTest((int)dgvLicenseAppointmentTest.CurrentRow.Cells[0].Value, _TestTypeID);
            frm.ShowDialog();
            frListTestAppointment_Load(null, null);
        }

        private void dgvLicenseAppointmentTest_DoubleClick(object sender, EventArgs e)
        {
            contextMenuStrip1.Show();
        }

        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void takeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTakeTest frm = new frmTakeTest((int)dgvLicenseAppointmentTest.CurrentRow.Cells[0].Value,_TestTypeID);
            frm.ShowDialog();
            frListTestAppointment_Load(null, null);
        }
    }
}
