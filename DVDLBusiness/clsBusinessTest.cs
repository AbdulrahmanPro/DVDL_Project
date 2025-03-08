using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVDLDataAccess;
using System.Data;

namespace DVDLBusiness
{
  public  class clsBusinessTest
    {

        enum enMode { AddNew = 1, Update = 2 };
        enMode Mode = enMode.AddNew;

        public int TestID { set; get; }
        public int  TestAppointmentID { set; get; }
        public bool TestResult { set; get; }
        public string Notes { set; get; }
        public int CreateByUser { set; get; }
        public clsBusinessTestAppointments TestAppointmentsInfo { set; get; }

        public clsBusinessTest()
        {
            this.TestID = -1;
            this.TestResult = false;
            this.Notes = "";
            this.CreateByUser = -1;
            Mode = enMode.AddNew;

        }

        private clsBusinessTest(int TestID, int  TestAppointmentID, bool TestResult, string Notes, int CreateByUser)
        {
            this.TestID = TestID;
            this. TestAppointmentID =  TestAppointmentID;
            TestAppointmentsInfo = clsBusinessTestAppointments.Find(TestAppointmentID);
            this.TestResult = TestResult;
            this.Notes = Notes;
            this.CreateByUser = CreateByUser;

            Mode = enMode.Update;
        }

        private bool _AddNewUser()
        {
            this.TestID = clsTest.AddNewTest(this. TestAppointmentID, this.TestResult, this.Notes, this.CreateByUser);
            return (this.TestID != -1);
        }
        private bool _UpdateUser()
        {
            return clsTest.UpdateUser(this.TestID, this. TestAppointmentID, this.TestResult, this.Notes, this.CreateByUser);
        }
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewUser())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    if (_UpdateUser())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
            }
            return false;

        }
        public static DataTable GetAllUsers()
        {
            return clsTest.GetAllTests();
        }
        public static clsBusinessTest FindByTestID(int TestID)
        {
            bool isFound = false;
            int  TestAppointmentID = 0;
            bool TestResult =false;
            string Notes = "";
            int CreateByUser = -1;
            isFound = clsTest.GetTestByTestID(TestID, ref  TestAppointmentID, ref TestResult
               , ref Notes, ref CreateByUser);
            if (isFound)
            {
                return new clsBusinessTest(TestID,  TestAppointmentID, TestResult, Notes, CreateByUser);
            }
            else
            {
                return null;
            }
        }
        
        public static clsBusinessTest FindLastTestPerPersonAndLicenseClass(int PersonID, int LicenseClassID, clsTestTypeBusiness.enTestType TestTypeID)
        {
            bool isFound = false;
            int TestID = -1;
            int TestAppointmentID = 0;
            bool TestResult = false;
            string Notes = "";
            int CreateByUser = -1;
            isFound = clsTest.GetLastTestByPersonAndTestTypeAndLicenseClass(PersonID, LicenseClassID, (int)TestTypeID, ref TestID, ref TestAppointmentID, ref TestResult, ref Notes, ref CreateByUser);
            if (isFound)
            {
                return new clsBusinessTest(TestID, TestAppointmentID, TestResult, Notes, CreateByUser);
            }
            else
            {
                return null;
            }
        }

        public static byte getPassedTests(int LoaclDrivingLicenseApplicationID)
        {
            return clsTest.GetPassedTestCount(LoaclDrivingLicenseApplicationID);
        }
        public static bool IsPassedAllTests(int LoaclDrivingLicenseApplicationID)
        {
            return clsTest.GetPassedTestCount(LoaclDrivingLicenseApplicationID)==3;
        }
        public static bool PassedAllTests(int LocalDrivingLicenseApplicationID)
        {
            //if total passed test less than 3 it will return false otherwise will return true
            return getPassedTests(LocalDrivingLicenseApplicationID) == 3;
        }

    }
}
