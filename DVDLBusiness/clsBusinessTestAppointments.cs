using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DVDLDataAccess;

namespace DVDLBusiness
{
    public class clsBusinessTestAppointments
    {
        public enum enMode { AddNew = 1, Update = 2 };
        public enMode Mode = enMode.AddNew;
        public int TestAppointmentID { set; get; }
        public clsTestTypeBusiness.enTestType TestTypeID { set; get; }
        public int LocalDrivingLicenseApplicationID { set; get; }
        public DateTime AppointmentDate { set; get; }
        public float PaidFees { set; get; }
        public int CreateByUser { set; get; }
        public bool IsLocked { set; get; }
        public int RetakeTestApplicationID { set; get; }
        public ApplicationsBusiness RetakeTestApplicationInfo { set; get; }
        public int TestID {
        get { return _GetTestID(); }
        }

        public clsBusinessTestAppointments()
        {
            this.TestAppointmentID = -1;
            this.TestTypeID = clsTestTypeBusiness.enTestType.VisionTest;
            this.LocalDrivingLicenseApplicationID = -1;
            this.AppointmentDate = DateTime.Now;
            this.PaidFees = 0;
            this.CreateByUser = -1;
            this.IsLocked = false;
            this.RetakeTestApplicationID = -1;
            Mode = enMode.AddNew;

        }
        private clsBusinessTestAppointments(int TestAppointmentID, clsTestTypeBusiness.enTestType TestTypeID, int LocalDrivingLicenseApplicationID,  DateTime AppointmentDate,
            float PaidFees, int CreateByUser, bool IsLocked, int RetakeTestApplicationID)
        {
            this.TestAppointmentID = TestAppointmentID;
            this.TestTypeID = TestTypeID;
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID; 
            this.AppointmentDate = AppointmentDate;
            this.PaidFees = PaidFees;
            this.CreateByUser = CreateByUser;
            this.IsLocked = IsLocked;
            this.RetakeTestApplicationID = RetakeTestApplicationID;
            this.RetakeTestApplicationInfo = ApplicationsBusiness.FindBaseApplication(RetakeTestApplicationID);

            Mode = enMode.Update;
            
        }
        public static clsBusinessTestAppointments Find(int ID)
        {

            int  LocalDrivingLicenseApplicationID = -1, TestTypeID = -1;
            DateTime AppointmentDate = DateTime.Now;
            float PaidFees = 0;
            int CreateByUser = -1;
            bool IsLocked = false;
            int RetakeTestApplicationID = -1;
            bool isFound = clsTestAppointments.GetTestAppointmentInfoByID(ID, ref TestTypeID, ref LocalDrivingLicenseApplicationID, ref AppointmentDate, ref PaidFees, ref CreateByUser
            , ref IsLocked, ref RetakeTestApplicationID);

            if (isFound)
            {
                return new clsBusinessTestAppointments(ID, (clsTestTypeBusiness.enTestType )TestTypeID, LocalDrivingLicenseApplicationID,
                    AppointmentDate, PaidFees, CreateByUser, IsLocked, RetakeTestApplicationID);
            }
            else
                return null;



        }
        public static clsBusinessTestAppointments GetLastTestAppointment(int LocalDrivingLicenseApplicationID, clsTestTypeBusiness.enTestType TestTypeID)
        {
            int TestAppointmentID = -1;
            DateTime AppointmentDate = DateTime.Now; float PaidFees = 0;
            int CreatedByUserID = -1; bool IsLocked = false; int RetakeTestApplicationID = -1;

            if (clsTestAppointments.GetLastTestAppointment(TestAppointmentID,(int)TestTypeID,
                ref LocalDrivingLicenseApplicationID, ref AppointmentDate, ref PaidFees, ref CreatedByUserID, ref IsLocked, ref RetakeTestApplicationID))

                return new clsBusinessTestAppointments(TestAppointmentID, TestTypeID, LocalDrivingLicenseApplicationID,
             AppointmentDate, PaidFees, CreatedByUserID, IsLocked, RetakeTestApplicationID);
            else
                return null;

        }

        public static clsBusinessTestAppointments GetLastTestAppointment(int TestAppointment)
        {
            int  LocalDrivingLicenseApplicationID = -1, TestTypeID = -1;
            DateTime AppointmentDate = DateTime.Now;
            float PaidFees = 0;
            int CreateByUser = -1;
            bool IsLocked = false;
            int RetakeTestApplicationID = -1;

            bool isFound = clsTestAppointments.GetLastTestAppointment( TestAppointment, TestTypeID, ref LocalDrivingLicenseApplicationID, ref AppointmentDate, ref PaidFees, ref CreateByUser
            , ref IsLocked, ref RetakeTestApplicationID);

            if (isFound)
            {
                return new clsBusinessTestAppointments(TestAppointment,(clsTestTypeBusiness.enTestType) TestTypeID, LocalDrivingLicenseApplicationID,
             AppointmentDate, PaidFees, CreateByUser
            , IsLocked, RetakeTestApplicationID);
            }
            else
                return null;



        }

        private bool _AddNewPerson()
        {
            this.TestAppointmentID = clsTestAppointments.AddNewTestAppointments((int)this.TestTypeID, this.LocalDrivingLicenseApplicationID, this.AppointmentDate, this.PaidFees, this.CreateByUser,
                this.IsLocked, this.RetakeTestApplicationID);

            return TestAppointmentID != -1;
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewPerson())
                    {
                        Mode = enMode.Update;
                        return true;

                    }
                    else
                        return false;
                case enMode.Update:
                    return _UpdatePerson();
            }
            return false;
        }
        public bool _UpdatePerson()
        {
            return clsTestAppointments.UpdateTestAppointments(this.TestAppointmentID,(int) this.TestTypeID, this.LocalDrivingLicenseApplicationID, this.AppointmentDate, this.PaidFees, this.CreateByUser,
                 this.IsLocked, this.RetakeTestApplicationID);
        }

        public static DataTable GetAllTestAppointment()
        {
            return clsTestAppointments.GetAllTestAppointment();
        }
        public static DataTable GetApplicationTestAppointmentPerTestType(int LocalDrivingLicenseApplicationID, clsTestTypeBusiness.enTestType TestTypeID)
        {
            return clsTestAppointments.GetApplicationTestAppointmentPerTestType(LocalDrivingLicenseApplicationID,(int)TestTypeID);
        }
       private int _GetTestID()
        {
            return clsTestAppointments.GetTestID(TestAppointmentID);
        }
    }
}
