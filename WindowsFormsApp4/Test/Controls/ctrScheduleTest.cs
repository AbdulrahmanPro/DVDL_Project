using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVDLBusiness;
using WindowsFormsApp4.GlobalClasses;
namespace WindowsFormsApp4.Test.Controls
{
    public partial class ctrScheduleTest : UserControl
    {
        public ctrScheduleTest()
        {
            InitializeComponent();
        }
        public enum enMode { AddNew=1,Update=2};
        private enMode _Mode = enMode.AddNew;
        public enum enCreationMode { FirstTimeSchedule=0 ,RetakeTestSchedule=1};
        private enCreationMode _CreationMode = enCreationMode.FirstTimeSchedule;
        private clsTestTypeBusiness.enTestType _TestTypeID = clsTestTypeBusiness.enTestType.VisionTest;
        private clsLocalDrivingLicenseBusiness _LocalDrivingLicenseApplication;
        private int _LocalDrivingLicenseApplicationID=-1;
        private clsBusinessTestAppointments _TestAppointment;
        private int _TestAppointmentID=-1;
        public clsTestTypeBusiness.enTestType TestTypeID {
            get { return _TestTypeID; }
            set
            {
                _TestTypeID = value;
                switch (_TestTypeID)
                {
                    case clsTestTypeBusiness.enTestType.VisionTest:
                        gbTestType.Text = "Vision Test";
                        pbTestTypeImage.Image = Properties.Resources.Vision_512; break;
                    case clsTestTypeBusiness.enTestType.WrittenTest:
                        gbTestType.Text = "Written Test";
                        pbTestTypeImage.Image = Properties.Resources.Written_Test_512; break;
                    case clsTestTypeBusiness.enTestType.StreetTest:
                        gbTestType.Text = "Street Test";
                        pbTestTypeImage.Image = Properties.Resources.Street_Test_32; break;

                }
            }
          
        }

        private bool _LoadTestAppointmentData()
        {
            _TestAppointment = clsBusinessTestAppointments.Find(_TestAppointmentID);
            if (_TestAppointment == null)
            {
                MessageBox.Show("Error: No Test Appointment with ID" + _TestAppointmentID, "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSave.Enabled = false;
                return false;
            }
            lblFees.Text = _TestAppointment.PaidFees.ToString();
            if (DateTime.Compare(DateTime.Now, _TestAppointment.AppointmentDate) > 0)
                dtpTestDate.MinDate = DateTime.Now;
            else
               dtpTestDate.MinDate= _TestAppointment.AppointmentDate;

            if (_TestAppointment.RetakeTestApplicationID == -1)
            {
                lblRetakeAppFees.Text = "0";
                lblRetakeTestAppID.Text = "N/A";
            }
            else
            {
                lblRetakeAppFees.Text = _TestAppointment.RetakeTestApplicationInfo.PaidFees.ToString();
                gbRetakeTestInfo.Enabled = true;
                lblTitle.Text = "Schedule Retake Test";
                lblRetakeTestAppID.Text = _TestAppointment.RetakeTestApplicationID.ToString();
            }

            return true;
        }

        private bool _HandleActiveTestAppointmentConstraint()
        {
            if(_Mode==enMode.AddNew && clsLocalDrivingLicenseBusiness.IsThereAnActiveScheduledTest(_LocalDrivingLicenseApplicationID, TestTypeID))
            {
                lblUserMessage.Text = "Person Already Have  an Active Appointment for this Test";
                btnSave.Enabled = false;
                dtpTestDate.Enabled = false;
                return false;
            }
            return true;
        }
        private bool _HandleAppointmentLockConstraint()
        {
            if (_TestAppointment.IsLocked)
            {
                lblUserMessage.Visible = true;
                lblUserMessage.Text = "Person Already sat for the test, appointment is Lock";
                btnSave.Enabled = false;
                dtpTestDate.Enabled = false;
                return false;

            }
            else 
                lblUserMessage.Visible = false;
            return true;
        }

        private bool _HandlePrviousTestConstraint()
        {
            switch (TestTypeID) {
                case clsTestTypeBusiness.enTestType.VisionTest:
                    lblUserMessage.Visible = false;
                    return false;
                case clsTestTypeBusiness.enTestType.WrittenTest:
                    if (!_LocalDrivingLicenseApplication.DoesPassTestType(clsTestTypeBusiness.enTestType.VisionTest))
                    {
                        lblUserMessage.Visible = true;
                        btnSave.Enabled = false;
                        dtpTestDate.Enabled = false;
                        return false;
                    }
                    else
                    {
                        lblUserMessage.Visible = false;
                        btnSave.Enabled = true;
                        dtpTestDate.Enabled = true;
                    }
                    return true;
                case clsTestTypeBusiness.enTestType.StreetTest:
                    if (!_LocalDrivingLicenseApplication.DoesPassTestType(clsTestTypeBusiness.enTestType.WrittenTest))
                    {
                        lblUserMessage.Text = "Cannot schedule, Written test should be Passed first";
                        lblUserMessage.Visible = true;
                        btnSave.Enabled = false;
                        dtpTestDate.Enabled = false;
                        return false;
                    }
                    else
                    {
                        lblUserMessage.Visible = false;
                        btnSave.Enabled = true;
                        dtpTestDate.Enabled = true;
                    }
                    return true;
            }
            return true;

        }
        public void LoadInfo(int LocalDrivingLicenseApplicationID,int AppoinmentID)
        {

            if (AppoinmentID == -1)
                _Mode = enMode.AddNew;
            else
                _Mode = enMode.Update;

            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseBusiness.FindByLocalDrivingLicenseApplication(LocalDrivingLicenseApplicationID);
            if (_LocalDrivingLicenseApplication == null)
            {
                MessageBox.Show("Error: No Local driving License Application With ID" + LocalDrivingLicenseApplicationID, "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnSave.Enabled = false;
                return;
            }
            if (_LocalDrivingLicenseApplication.DoesAttendTestType(_TestTypeID))
                _CreationMode = enCreationMode.RetakeTestSchedule;
            else
                _CreationMode = enCreationMode.FirstTimeSchedule;

            if (_CreationMode == enCreationMode.RetakeTestSchedule)
            {
                lblRetakeAppFees.Text = clsApplicationTypeBusiness.Find((int)ApplicationsBusiness.enApplicationType.RetakeTest).ApplicationTypeFees.ToString();
                gbRetakeTestInfo.Enabled = true;
                lblTitle.Text = "Schedulee Test";
                lblRetakeTestAppID.Text = "0";
            }
            else
            {
                lblRetakeAppFees.Text = "0";
                gbRetakeTestInfo.Enabled = false;
                lblTitle.Text = "Schedulee Test";
                lblRetakeTestAppID.Text = "N/A";

            }
            lblLocalDrivingLicenseAppID.Text = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID.ToString();
            lblDrivingClass.Text = _LocalDrivingLicenseApplication.LicenseClassInfo.ClassName;
            lblFullName.Text = _LocalDrivingLicenseApplication.PersonFullName;

            lblTotalFees.Text = _LocalDrivingLicenseApplication.TotalTrialsPerTest(TestTypeID).ToString();

            if (_Mode == enMode.AddNew)
            {
                lblFees.Text = clsTestTypeBusiness.Find(TestTypeID).TestTypeFees.ToString();
                dtpTestDate.MinDate = DateTime.Now;
                lblRetakeTestAppID.Text = "N/A";

                _TestAppointment = new clsBusinessTestAppointments();
            }
            else
            {
                if (!_LoadTestAppointmentData())
                {
                    return;
                }
                lblTotalFees.Text = (Convert.ToSingle(lblRetakeAppFees.Text) + Convert.ToSingle(lblFees.Text)).ToString();

                if (!_HandleActiveTestAppointmentConstraint())
                    return;
                if(_HandleAppointmentLockConstraint())
                    return;
                if (_HandlePrviousTestConstraint())
                    return;
            }
        }

        
        private bool _HandleRetakeApplication()
        {
            if(_Mode==enMode.AddNew && _CreationMode == enCreationMode.RetakeTestSchedule)
            {
                ApplicationsBusiness Application = new ApplicationsBusiness();
                Application.ApplicationPersonID = _LocalDrivingLicenseApplication.ApplicationPersonID;
                Application.ApplicationDate = DateTime.Now;
                Application.ApplicationTypeID = (int)ApplicationsBusiness.enApplicationType.RetakeTest;
                Application.ApplicationStatus = ApplicationsBusiness.enApplicationStatus.Completed;
                Application.LastStatusDate =DateTime.Now;
                Application.PaidFees = clsApplicationTypeBusiness.Find((int)ApplicationsBusiness.enApplicationType.RetakeTest).ApplicationTypeFees;
                Application.CreateByUserID = clsGlobal.CurrentUser.UserID;

                if (!Application.Save())
                {
                    _TestAppointment.RetakeTestApplicationID = -1;
                    MessageBox.Show("Faild to Create application", "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;

                }
                _TestAppointment.RetakeTestApplicationID = Application.ApplicationID;

            }
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!_HandleRetakeApplication())
                return;

            _TestAppointment.TestTypeID = _TestTypeID;
            _TestAppointment.LocalDrivingLicenseApplicationID = _LocalDrivingLicenseApplication.LocalDrivingLicenseApplicationID;
            _TestAppointment.AppointmentDate = dtpTestDate.Value;
            _TestAppointment.PaidFees = Convert.ToSingle(lblFees.Text);
            _TestAppointment.CreateByUser = clsGlobal.CurrentUser.UserID;

            if (_TestAppointment.Save())
            {
                _Mode = enMode.Update;
                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void guna2GradientPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2GradientPanel2_Paint(object sender, PaintEventArgs e)
        {

        }


       
    }
}
