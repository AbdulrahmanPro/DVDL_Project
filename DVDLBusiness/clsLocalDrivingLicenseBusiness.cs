using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVDLDataAccess;
using System.Data;


namespace DVDLBusiness
{
   public class clsLocalDrivingLicenseBusiness:ApplicationsBusiness
    {

        public enum enMode { AddNew = 1, Update = 2 };
        public enMode Mode = enMode.AddNew;

        public int LocalDrivingLicenseApplicationID { set; get; }
        public int LicenseClassID { set; get; }
        public clsClassLicenseBusiness LicenseClassInfo;
        public string PersonFullName
        {
            get
            {
                return base.PersonInfo.FullName;
            }

        }
         public clsLocalDrivingLicenseBusiness()
        {
            this.LocalDrivingLicenseApplicationID = -1;
            this.LicenseClassID = -1;
            Mode = enMode.AddNew;
        }
        private clsLocalDrivingLicenseBusiness(int LocalDrivingLicenseApplicationID, int ApplicationID, int ApplicationPersonID,
            DateTime ApplicationDate, int ApplicationTypeID,
             enApplicationStatus ApplicationStatus, DateTime LastStatusDate,
             float PaidFees, int CreateByUserID, int LicenseClassID)
        {
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.ApplicationID = ApplicationID;
            this.ApplicationPersonID = ApplicationPersonID;
            this.ApplicationDate = ApplicationDate;
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationStatus = ApplicationStatus;
            this.LastStatusDate = LastStatusDate;
            this.PaidFees = PaidFees;
            this.CreateByUserID = CreateByUserID;
            this.LicenseClassID = LicenseClassID;
            this.LicenseClassInfo = clsClassLicenseBusiness.Find(LicenseClassID);
            this.PersonInfo = PepoleBusiness.Find(this.ApplicationPersonID);
            Mode = enMode.Update;
        }
        private bool _AddNewLocalDrivingLicenseApplication()
        {
            this.LocalDrivingLicenseApplicationID = clsLoaclDrivingLicenseApplication.AddNewLocalDrivingLicenseApplication(
                this.ApplicationID, this.LicenseClassID);
            return (this.LocalDrivingLicenseApplicationID !=-1);
        }
        private bool _UpdateLocalDrivingLicenseApplication()
        {
            return clsLoaclDrivingLicenseApplication.UpdateLocalDrivingLicenseApplication(this.LocalDrivingLicenseApplicationID,
                this.ApplicationID, this.LicenseClassID);
        }
        public static clsLocalDrivingLicenseBusiness FindByLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID)
        {
            int ApplicationID = -1, LicenseClassID = -1;
            bool IsFound = clsLoaclDrivingLicenseApplication.GetLocalDrivingLicenseApplicationInfoByID(LocalDrivingLicenseApplicationID, ref ApplicationID, ref LicenseClassID);

            if (IsFound)
            {
                ApplicationsBusiness Application = ApplicationsBusiness.FindBaseApplication(ApplicationID);
                return new clsLocalDrivingLicenseBusiness(LocalDrivingLicenseApplicationID, ApplicationID, Application.ApplicationPersonID,
                    Application.ApplicationDate, Application.ApplicationTypeID, Application.ApplicationStatus, Application.LastStatusDate,
                    Application.PaidFees, Application.CreateByUserID, LicenseClassID
                    );
            }
            else
            {
                return null;
            }
        
        
        }
        public static clsLocalDrivingLicenseBusiness FindByApplicationID(int ApplicationID)
        {
            // 
            int LocalDrivingLicenseApplicationID = -1, LicenseClassID = -1;

            bool IsFound = clsLoaclDrivingLicenseApplication.GetLocalDrivingLicenseApplicationInfoByApplcationID
                (ApplicationID, ref LocalDrivingLicenseApplicationID, ref LicenseClassID);


            if (IsFound)
            {
                //now we find the base application
                ApplicationsBusiness Application = ApplicationsBusiness.FindBaseApplication(ApplicationID);

                //we return new object of that person with the right data
                return new clsLocalDrivingLicenseBusiness(
                    LocalDrivingLicenseApplicationID, Application.ApplicationID,
                    Application.ApplicationPersonID,
                                     Application.ApplicationDate, Application.ApplicationTypeID,
                                    (enApplicationStatus)Application.ApplicationStatus, Application.LastStatusDate,
                                     Application.PaidFees, Application.CreateByUserID, LicenseClassID);
            }
            else
                return null;


        }
        public bool Save()
        {
            base.Mode = (ApplicationsBusiness.enMode)this.Mode;
            if (!base.Save())
                return false;
            
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLocalDrivingLicenseApplication())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateLocalDrivingLicenseApplication();
            }
            return false;
        }
        public static DataTable GetAllLocalDrivingLicenseApplication()
        {
            return clsLoaclDrivingLicenseApplication.GetAllLocalDrivingLicenseApplications();
        }
        public bool Delete()
        {
            bool IsLocalDrivingApplicationDeleted = false;
            bool IsBaseApplicationDeleted = false;

            IsLocalDrivingApplicationDeleted = clsLoaclDrivingLicenseApplication.DeleteLocalDrivingLicenseApplication(this.LocalDrivingLicenseApplicationID);
            if (!IsLocalDrivingApplicationDeleted)
                return false;
            IsBaseApplicationDeleted = base.Delete();
            return IsBaseApplicationDeleted;
        }

        public bool DoesPassTestType(clsTestTypeBusiness.enTestType TestTypeID)

        {
            return clsLoaclDrivingLicenseApplication.DoesPassTestType(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public bool DoesPassPreviousTest(clsTestTypeBusiness.enTestType CurrentTestType)
        {

            switch (CurrentTestType)
            {
                case clsTestTypeBusiness.enTestType.VisionTest:
                    //in this case no required prvious test to pass.
                    return true;

                case clsTestTypeBusiness.enTestType.WrittenTest:
                    //Written Test, you cannot sechdule it before person passes the vision test.
                    //we check if pass visiontest 1.

                    return this.DoesPassTestType(clsTestTypeBusiness.enTestType.VisionTest);


                case clsTestTypeBusiness.enTestType.StreetTest:

                    //Street Test, you cannot sechdule it before person passes the written test.
                    //we check if pass Written 2.
                    return this.DoesPassTestType(clsTestTypeBusiness.enTestType.WrittenTest);

                default:
                    return false;
            }
        }

        public static bool DoesPassTestType(int LocalDrivingLicenseApplicationID, clsTestTypeBusiness.enTestType TestTypeID)

        {
            return clsLoaclDrivingLicenseApplication.DoesPassTestType(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public bool DoesAttendTestType(clsTestTypeBusiness.enTestType TestTypeID)

        {
            return clsLoaclDrivingLicenseApplication.DoesAttendTestType(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public byte TotalTrialsPerTest(clsTestTypeBusiness.enTestType TestTypeID)
        {
            return clsLoaclDrivingLicenseApplication.TotalTrialsPerTest(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public static byte TotalTrialsPerTest(int LocalDrivingLicenseApplicationID, clsTestTypeBusiness.enTestType TestTypeID)

        {
            return clsLoaclDrivingLicenseApplication.TotalTrialsPerTest(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public static bool AttendedTest(int LocalDrivingLicenseApplicationID, clsTestTypeBusiness.enTestType TestTypeID)

        {
            return clsLoaclDrivingLicenseApplication.TotalTrialsPerTest(LocalDrivingLicenseApplicationID, (int)TestTypeID) > 0;
        }

        public bool AttendedTest(clsTestTypeBusiness.enTestType TestTypeID)

        {
            return clsLoaclDrivingLicenseApplication.TotalTrialsPerTest(this.LocalDrivingLicenseApplicationID, (int)TestTypeID) > 0;
        }

        public static bool IsThereAnActiveScheduledTest(int LocalDrivingLicenseApplicationID, clsTestTypeBusiness.enTestType TestTypeID)

        {

            return clsLoaclDrivingLicenseApplication.IsThereAnActiveScheduledTest(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public bool IsThereAnActiveScheduledTest(clsTestTypeBusiness.enTestType TestTypeID)

        {

            return clsLoaclDrivingLicenseApplication.IsThereAnActiveScheduledTest(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }

        public clsBusinessTest GetLastTestPerTestType(clsTestTypeBusiness.enTestType TestTypeID)
        {
            return clsBusinessTest.FindLastTestPerPersonAndLicenseClass(this.ApplicationPersonID, this.LicenseClassID, TestTypeID);
        }

        public byte GetPassedTestCount()
        {
            return clsBusinessTest.getPassedTests(this.LocalDrivingLicenseApplicationID);
        }

        public static byte GetPassedTestCount(int LocalDrivingLicenseApplicationID)
        {
            return clsBusinessTest.getPassedTests(LocalDrivingLicenseApplicationID);
        }

        public bool PassedAllTests()
        {
            return clsBusinessTest.PassedAllTests(this.LocalDrivingLicenseApplicationID);
        }

        public static bool PassedAllTests(int LocalDrivingLicenseApplicationID)
        {
            //if total passed test less than 3 it will return false otherwise will return true
            return clsBusinessTest.PassedAllTests(LocalDrivingLicenseApplicationID);
        }

        public int IssueLicenseForTheFirtTime(string Notes, int CreateByUserID)
        {
            int DriverID = -1;

            clsBusinessDrivers Driver = clsBusinessDrivers.FindByPersonID(this.ApplicationPersonID);

            if (Driver == null)
            {
                //we check if the driver already there for this person.
                Driver = new clsBusinessDrivers();

                Driver.PersonID = this.ApplicationPersonID;
                Driver.CreateByUserID = CreateByUserID;
                if (Driver.Save())
                {
                    DriverID = Driver.DriverID;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                DriverID = Driver.DriverID;
            }
            //now we diver is there, so we add new licesnse

            clsLicenses License = new clsLicenses();
            License.ApplicationID = this.ApplicationID;
            License.DriverID = DriverID;
            License.LicenseClass = this.LicenseClassID;
            License.IssueDate = DateTime.Now;
            License.ExpirationDate = DateTime.Now.AddYears(this.LicenseClassInfo.DefaultValidityLength);
            License.Notes = Notes;
            License.PaidFees = this.LicenseClassInfo.ClassFees;
            License.IsActive = true;
            License.IssueReason = clsLicenses.enIssueReason.FirstTime;
            License.CreateByUserID = CreateByUserID;

            if (License.Save())
            {
                //now we should set the application status to complete.
                this.SetComplate();

                return License.LicenseID;
            }

            else
                return -1;
        }

        public bool IsLicenseIssued()
        {
            return (GetActiveLicenseID() != -1);
        }

        public int GetActiveLicenseID()
        {//this will get the license id that belongs to this application
            return clsLicenses.GetActiveLicenseIDByPersonID(this.ApplicationPersonID, this.LicenseClassID);
        }
    }
}
