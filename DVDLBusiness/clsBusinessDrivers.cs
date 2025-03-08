using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DVDLDataAccess;

namespace DVDLBusiness
{
    public class clsBusinessDrivers
    {

        enum enMode { AddNew = 1, Update = 2 };
        enMode Mode = enMode.AddNew;

        public int DriverID { set; get; }
        public int PersonID { set; get; }
        public int CreateByUserID { set; get; }
        public DateTime CreateDate { set; get; }

        public PepoleBusiness PersonInfo;

        public clsBusinessDrivers()
        {
            this.DriverID = -1;
            this.PersonID = -1;
            this.CreateByUserID = -1;
            this.CreateDate = DateTime.Now;
            Mode = enMode.AddNew;
        }

        private clsBusinessDrivers(int DriverID, int PersonID, int CreateByUserID, DateTime CreateDate)
        {
            this.DriverID = DriverID;
            this.PersonID = PersonID;
            this.CreateByUserID = CreateByUserID;
            this.CreateDate = CreateDate;
            this.PersonInfo = PepoleBusiness.Find(PersonID);

            Mode = enMode.Update;
        }

        private bool _AddNewUser()
        {
            this.DriverID = clsDriversDataAccess.AddNewDrivers(this.PersonID, this.CreateByUserID);
            return (this.DriverID != -1);
        }
        private bool _UpdateUser()
        {
            return clsDriversDataAccess.UpdateDrivers(this.DriverID, this.PersonID, this.CreateByUserID, this.CreateDate);
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
        public static DataTable GetLicenses(int DriverID)
        {
            return clsLicenses.GetDriverLicenses(DriverID);
        }
        public static DataTable GetAllDrivers()
        {
            return clsDriversDataAccess.GetAllDriverss();
        }
        public static clsBusinessDrivers FindByDriverID(int DriverID)
        {
            bool isFound = false;
            int PersonID = -1;
            int CreateByUserID = -1;
            DateTime CreateDate = DateTime.Now;
            isFound = clsDriversDataAccess.GetDriversByID(DriverID, ref PersonID, ref CreateByUserID, ref CreateDate);
            if (isFound)
            {
                return new clsBusinessDrivers(DriverID,  PersonID, CreateByUserID, CreateDate);
            }
            else
            {
                return null;
            }
        }
        public static clsBusinessDrivers FindByPersonID(int PersonID)
        {
            bool isFound = false;
            int DriverID = -1;
            int CreateByUserID = -1;
            DateTime CreateDate = DateTime.Now;
            isFound = clsDriversDataAccess.GetDriversByPersonID(PersonID, ref DriverID,  ref CreateByUserID, ref CreateDate);

            if (isFound)
            {
                return new clsBusinessDrivers(DriverID, PersonID, CreateByUserID, CreateDate);
            }
            else
            {
                return null;
            }
        }

        public static DataTable GetInternationalLicenses(int DriverID)
        {
            return clsInternalLicensesBusiness.GetDriverInternationalLicenses(DriverID);
        }

    }
}
