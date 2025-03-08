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


namespace WindowsFormsApp4.Test
{
    public partial class frmTakeTest : Form
    {
        private int _AppointmentID = -1;
        private int _TestID = -1;
        clsTestTypeBusiness.enTestType _TestType;
        clsBusinessTest _TestInfo;
        public frmTakeTest(int AppointmentID,clsTestTypeBusiness.enTestType TestType)
        {
            InitializeComponent();
            _TestType = TestType;
            _AppointmentID = AppointmentID;
        }

        private void ctrScheduledTest1_Load(object sender, EventArgs e)
        {

        }

        private void frmTakeTest_Load(object sender, EventArgs e)
        {
            
                ctrScheduledTest1.TestTypeID = _TestType;
                ctrScheduledTest1.LoadInfo(_AppointmentID);
            if (ctrScheduledTest1.TestAppointmentID == -1)
                btnSave.Enabled = false;
            else
                btnSave.Enabled = true;

            int TestID = ctrScheduledTest1.TestID;
            if (TestID != -1)
            {
                _TestInfo = clsBusinessTest.FindByTestID(TestID);
                if (_TestInfo.TestResult)
                {
                    rbPassed.Checked = true;
                }
                else
                {
                    rbFaild.Checked = true;
                }
                txtNotes.Text = _TestInfo.Notes;
                lblUserMassage.Visible = true;
                rbFaild.Enabled = false;
                rbPassed.Enabled = false;
            }
            else
            {
                _TestInfo = new clsBusinessTest();
            }


        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are You Sure You Want to Save? After That you Cannot Change The Result of Test", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
            {
                return;
            }

            _TestInfo.TestAppointmentID = _AppointmentID;
            _TestInfo.TestResult = rbPassed.Checked;
            _TestInfo.Notes = txtNotes.Text.Trim();

            _TestInfo.CreateByUser = clsGlobal.CurrentUser.UserID;

            if (_TestInfo.Save())
            {
                MessageBox.Show("Data Saved Successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnSave.Enabled = false;
            }
            else
            {
                MessageBox.Show("Data  is Not Saved Successfully", "Not Saved!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
