using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVDLDataAccess;
using DVDLBusiness;
using System.Data;

namespace DVDLBusiness
{
    public class clsUsersBusiness
    {
        enum enMode { AddNew=1, Update=2};
        enMode Mode=enMode.AddNew;

        public int UserID { set; get; }
        public int PersonID { set; get; }
        public string UserName { set; get; }
        public string Password { set; get; }
        public bool IsActive { set; get; }
        public PepoleBusiness PersonInfo;

        public clsUsersBusiness()
        {
            this.UserID = -1;
            this.UserName = "";
            this.Password = "";
            this.IsActive = true;
            Mode = enMode.AddNew;

        }

        private clsUsersBusiness(int UserID,int PersonID,string UserName,string Password,bool IsActive)
        {
            this.UserID = UserID;
            this.PersonID = PersonID;
            PersonInfo = PepoleBusiness.Find(PersonID);
            this.UserName = UserName;
            this.Password = Password;
            this.IsActive = IsActive;

            Mode = enMode.Update;
        }

        private bool _AddNewUser()
        {
            this.UserID = clsUsersDataAccess.AddNewUser(this.PersonID, this.UserName, this.Password, this.IsActive);
                return (this.UserID != -1);
        }
        private bool _UpdateUser() 
        {
            return clsUsersDataAccess.UpdateUser(this.UserID, this.PersonID, this.UserName, this.Password, this.IsActive);
        }
        public bool Save()
        {
            switch (Mode) {
                case enMode.AddNew:
                    if (_AddNewUser())
                    {
                        Mode=enMode.Update;
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
            return clsUsersDataAccess.GetAllUsers();
        }
        public static clsUsersBusiness  FindByUserID(int UserID)
        {
            bool isFound = false;
            int PersonID = 0;
            string UserName = "";
            string Password = "";
            bool IsActive = true;
            isFound = clsUsersDataAccess.GetUserByUserID(UserID, ref PersonID, ref UserName
               , ref Password, ref IsActive);
            if (isFound)
            {
                return new clsUsersBusiness(UserID, PersonID, UserName, Password, IsActive);
            }
            else
            {
                return null;
            }
        }
        public static clsUsersBusiness FindByPersonID(int PersonID)
        {
            bool isFound = false;
            int UserID = 0;
            string UserName = "";
            string Password = "";
            bool IsActive = true;
            isFound = clsUsersDataAccess.GetUserByPersonID(ref UserID,  PersonID, ref UserName
               , ref Password, ref IsActive);
            if (isFound)
            {
                return new clsUsersBusiness(UserID, PersonID, UserName, Password, IsActive);
            }
            else
            {
                return null;
            }
        }
        public static clsUsersBusiness FindUserByUserNameAndPassword(string UserName,string Password)
        {
            bool isFound = false;
            int UserID = 0;
            int PersonID = 0;
              bool IsActive = true;
            isFound = clsUsersDataAccess.GetUserByUserNameAndPassword(ref  UserID, ref PersonID, ref UserName
               , ref Password, ref IsActive);
            if (isFound)
            {
                return new clsUsersBusiness(UserID, PersonID, UserName, Password, IsActive);
            }
            else
            {
                return null;
            }
        }

        public static bool IsUserExist(int UserID)
        {
            return clsUsersDataAccess.IsUserExist(UserID);
        }
        public static bool IsUserExistForPersonID(int PersonID)
        {
            return clsUsersDataAccess.IsUserExistForPersonID(PersonID);
        }
        public static bool ChangePassword(int UserID,string NewPassword)
        {
           return clsUsersDataAccess.ChangePassword(UserID, NewPassword);
        }
        public static bool DeleteUser(int UserID)
        {
         return   clsUsersDataAccess.DeleteUser(UserID);
        }
    }
}
