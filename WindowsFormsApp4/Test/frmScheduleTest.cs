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
    public partial class frmScheduleTest : Form
    {
        private int _AppointmentID = -1;
        private int _LoaclDrivingLicenseID=-1;
        private clsTestTypeBusiness.enTestType _TestTypeID = clsTestTypeBusiness.enTestType.VisionTest;
        public frmScheduleTest(int LoaclDrivingLicenseID, clsTestTypeBusiness.enTestType TestTypeID ,int AppointmentID=-1)
        {
            InitializeComponent();
            _AppointmentID = AppointmentID;
            _LoaclDrivingLicenseID = LoaclDrivingLicenseID;
            _TestTypeID = TestTypeID;
        }

        private void frmScheduleTest_Load(object sender, EventArgs e)
        {
            ctrScheduleTest1.TestTypeID = _TestTypeID;
            ctrScheduleTest1.LoadInfo(_LoaclDrivingLicenseID,_AppointmentID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
 }

