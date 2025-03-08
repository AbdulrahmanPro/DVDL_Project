using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVDLDataAccess;
using System.Data;
using DVDLBusiness;
namespace DVDLBusiness
{
    public class ApplicationsBusiness
    {
        public enum enApplicationType
        {
            NewDrivingLicense = 1, RenewDrivingLicense = 2, ReplaceLostDrivingLicense = 3,
            ReplaceDamagedDrivingLicense = 4, ReleaseDetainedDrivingLicsense = 5, NewInternationalLicense = 6, RetakeTest = 7
        };

        public enum enApplicationStatus { New = 1, Cancelled = 2, Completed = 3 };

        public enum enMode { AddNew = 1, Update = 2 };
        public enMode Mode = enMode.AddNew;
        public int ApplicationID { set; get; }
        public int ApplicationPersonID { set; get; }
        public string ApplicantFullName
        {
               
        get
            {
                return PepoleBusiness.Find(ApplicationPersonID).FullName;
            }
        }
        public string StatusText
        {
            get
            {

                switch (ApplicationStatus)
                {
                    case enApplicationStatus.New:
                        return "New";
                    case enApplicationStatus.Cancelled:
                        return "Cancelled";
                    case enApplicationStatus.Completed:
                        return "Completed";
                    default:
                        return "Unknown";
                }
            }

        }
        public DateTime ApplicationDate { set; get; }
        public int ApplicationTypeID { set; get; }
        public clsApplicationTypeBusiness ApplicationTypeInfo;
        public enApplicationStatus ApplicationStatus { set; get; }
        public PepoleBusiness PersonInfo { set; get; }
        public DateTime LastStatusDate { set; get; }
        public float PaidFees { set; get; }
        public string Address { set; get; }
        public string Phone { set; get; }
        public string Email { set; get; }
        public int NationalityCountryID { set; get; }
        public string ImagePath { set; get; }
        public int CreateByUserID { set; get; }
        public clsUsersBusiness CreateUserInfo;

        public ApplicationsBusiness()
        {
            this.ApplicationID = -1;
            this.ApplicationDate =DateTime.Now;
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationStatus = enApplicationStatus.New;
            this.LastStatusDate = DateTime.Now;
            this.ApplicationPersonID = -1;
            this.PaidFees =0.0f;
            this.CreateByUserID = -1;
              Mode = enMode.AddNew;

        }
        public ApplicationsBusiness(int ApplicationID, DateTime ApplicationDate, int ApplicationTypeID, enApplicationStatus ApplicationStatus
            , DateTime LastStatusDate, int ApplicationPersonID, float PaidFees, int CreateByUserID)
        {
            this.ApplicationID = ApplicationID;
            this.ApplicationDate = ApplicationDate;
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationStatus = ApplicationStatus;
            this.LastStatusDate = LastStatusDate;
            this.ApplicationPersonID = ApplicationPersonID;
            this.ApplicationTypeInfo = clsApplicationTypeBusiness.Find(ApplicationTypeID);
            this.PaidFees = PaidFees;
            this.CreateByUserID = CreateByUserID;
            this.PersonInfo = PepoleBusiness.Find(ApplicationPersonID);
            this.CreateUserInfo = clsUsersBusiness.FindByUserID(CreateByUserID);
            this.Mode = enMode.Update;

        }

        public static ApplicationsBusiness FindBaseApplication(int ApplicationID)
        {
            int ApplicantPersonID = -1;
            DateTime ApplicationDate = DateTime.Now; int ApplicationTypeID = -1;
            short ApplicationStatus = 1; DateTime LastStatusDate = DateTime.Now;
            float PaidFees = 0; int CreatedByUserID = -1;

            bool IsFound = ApplicationsDataAccess.GetApplicationInfoByID
                                (
                                    ApplicationID, ref ApplicantPersonID,
                                    ref ApplicationDate, ref ApplicationTypeID,
                                    ref  ApplicationStatus, ref LastStatusDate,
                                    ref PaidFees, ref CreatedByUserID
                                );

            if (IsFound)
                //we return new object of that person with the right data
                return new ApplicationsBusiness(ApplicationID, 
                                     ApplicationDate, ApplicationTypeID,
                                    (enApplicationStatus)ApplicationStatus, LastStatusDate, ApplicantPersonID,
                                    PaidFees, CreatedByUserID);
            else
                return null;
        }

        private bool _AddNewPerson()
        {
            this.ApplicationID = ApplicationsDataAccess.AddNewApplication( this.ApplicationPersonID, this.ApplicationDate, this.ApplicationTypeID,(short) this.ApplicationStatus
            , this.LastStatusDate, this.PaidFees, this.CreateByUserID);

            return ApplicationID != -1;
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
            return ApplicationsDataAccess.UpdateApplictaion(this.ApplicationID,this.ApplicationPersonID, this.ApplicationDate, this.ApplicationTypeID,(short) this.ApplicationStatus
            , this.LastStatusDate, this.PaidFees, this.CreateByUserID);
        }
        public bool IsPersonExist(int ApplicationID)
        {
            return ApplicationsDataAccess.IsApplicationExist(ApplicationID);
        }
       
        public DataTable GetAllPeople()
        {
            return ApplicationsDataAccess.GetAllApplications();
        }

        public bool Cancel()
        {
            return ApplicationsDataAccess.UpdateStatus(ApplicationID, 2);
        }
        public bool SetComplate()
        {
            return ApplicationsDataAccess.UpdateStatus(ApplicationID, 3);
        }
        public bool Delete()
        {
            return ApplicationsDataAccess.DeleteApplication(ApplicationID);
        }
        public static bool IsApplicationExist(int ApplicationID)
        {
            return ApplicationsDataAccess.IsApplicationExist(ApplicationID);
        }
        public static bool DoesPersonHaveActiveApplication(int PersonID, int ApplicationID)
        {
            return ApplicationsDataAccess.DoesPersonIDHaveApplicationID(PersonID, ApplicationID);
        }
        public static int GetActiveApplicationIDForLicenseClass(int PersonID, int ApplicationID,int LicenseClassID)
        {
            return ApplicationsDataAccess.GetActiveApplicationIDForLicenseClass(PersonID, ApplicationID,LicenseClassID);
        }
    }
}
