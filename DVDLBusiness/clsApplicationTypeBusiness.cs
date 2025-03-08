using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DVDLDataAccess;

namespace DVDLBusiness
{
    public class clsApplicationTypeBusiness
    {
        enum enMode { AddNew=1,Update=2 };
        enMode Mode = enMode.AddNew;
       public int ApplicationTypeID { set; get; }
       public string ApplicationTypeTitle { set; get; }
       public float ApplicationTypeFees { set; get; }
       
        clsApplicationTypeBusiness()
        {
            int ApplicationTypeID = -1;
            string ApplicationTypeTitle = "";
            float ApplicationTypeFees = 0;

            Mode = enMode.AddNew;
        }
        clsApplicationTypeBusiness(int ApplicationTypeID, string ApplicationTypeTitle, float ApplicationTypeFees)
        {
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationTypeTitle = ApplicationTypeTitle;
            this.ApplicationTypeFees = ApplicationTypeFees;

            Mode = enMode.Update;
        }



        private bool _AddNewApplicationType()
        {
            this.ApplicationTypeID = clsApplicationType.AddNewApplicationType(this.ApplicationTypeTitle,   this.ApplicationTypeFees);

            return (this.ApplicationTypeID != -1);
        }
        private bool UpdateApplicationType()
        {

            return clsApplicationType.UpdateApplicationType(this.ApplicationTypeID, this.ApplicationTypeTitle, this.ApplicationTypeFees);
        }
        public bool Save()
        {

            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewApplicationType())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    if (UpdateApplicationType())
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
        public static clsApplicationTypeBusiness Find(int ID)
        {
            string Title = ""; float Fees = 0;

            if (clsApplicationType.GetApplicationtypeByID((int)ID, ref Title, ref Fees))

                return new clsApplicationTypeBusiness(ID, Title, Fees);
            else
                return null;

        }
        public static DataTable GetAllApplicationType()
        {
            return clsApplicationType.GetAllApplicationTypes();
        }
    }
}
