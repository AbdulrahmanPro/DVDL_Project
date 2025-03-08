using DVDLDataAccess;
using System;
using System.Data;
using System.Security.Policy;

namespace DVDLBusiness
{
    public class PepoleBusiness
    {

        public enum enMode { AddNew = 1, Update = 2 };
        public enMode Mode = enMode.AddNew;
        public int PersonID { set; get; }
        public string NationalNo { set; get; }
        public string FirstName { set; get; }
        public string SecondName { set; get; }
        public string ThirdName { set; get; }
        public string LastName { set; get; }
        public DateTime DateOfBirth { set; get; }
        public short Gendor { set; get; }
        public string Address { set; get; }
        public string Phone { set; get; }
        public string Email { set; get; }
        public int NationalityCountryID { set; get; }
        public string ImagePath { set; get; }
        public string FullName
        {
            get { return FirstName + " " + SecondName + " " + ThirdName + " " + LastName; }

        }
        public CountryBusiness CountryInfo;
        public PepoleBusiness()
        {
            this.PersonID = -1;
            this.FirstName = "";
            this.SecondName = "";
            this.ThirdName = "";
            this.LastName = "";
            this.NationalNo = "";
            this.DateOfBirth = DateTime.Now;
            this.Gendor = -1;
            this.Address = "";
            this.Phone = "";
            this.Email = "";
            this.NationalityCountryID = -1;
            this.ImagePath = "";
            Mode = enMode.AddNew;

        }
        private PepoleBusiness(int PersonID, string FirstName, string SecondName, string ThirdName
            , string LastName, string NationalNo, DateTime DateOfBirth, short Gendor, string Address
            , string Phone, string Email, int NationalityCountryID, string ImagePath)
        {
            this.PersonID = PersonID;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.ThirdName = ThirdName;
            this.LastName = LastName;
            this.NationalNo = NationalNo;
            this.DateOfBirth = DateOfBirth;
            this.Gendor = Gendor;
            this.Address = Address;
            this.Phone = Phone;
            this.Email = Email;
            this.NationalityCountryID = NationalityCountryID;
            this.CountryInfo = CountryBusiness.Find(NationalityCountryID);
            this.ImagePath = ImagePath;
            this.Mode = enMode.Update;

        }
        public  static PepoleBusiness Find(int ID)
        {

            string FirstName = "", SecondName = "", ThirdName = "", LastName = "", NationalNo = "", Email = "", Phone = "", Address = "", ImagePath = "";
            DateTime DateOfBirth = DateTime.Now;
            int NationalityCountryID = -1;
            short Gendor = 0;
            bool isFound = PeploeDataAccess.GetPersonInfoByID(ID, ref FirstName, ref SecondName, ref ThirdName
            , ref LastName, ref NationalNo, ref DateOfBirth, ref Gendor, ref Address
            , ref Phone, ref Email, ref NationalityCountryID, ref ImagePath);

            if (isFound)
             {
                return new PepoleBusiness(ID, FirstName, SecondName, ThirdName
            , LastName, NationalNo, DateOfBirth, Gendor, Address
            , Phone, Email, NationalityCountryID, ImagePath);
            }
            else
                return null;



        }
        public static PepoleBusiness Find(string NationalNo)
        {

            string FirstName = "", SecondName = "", ThirdName = "", LastName = "", Email = "", Phone = "", Address = "", ImagePath = "";
            DateTime DateOfBirth = DateTime.Now;
            int PersonID = -1, NationalityCountryID = -1;
            short Gendor = 0;
            bool isFound = PeploeDataAccess.GetPersonInfoByNationalNo(NationalNo, ref PersonID, ref FirstName, ref SecondName, ref ThirdName
            , ref LastName, ref DateOfBirth, ref Gendor, ref Address
            , ref Phone, ref Email, ref NationalityCountryID, ref ImagePath);

            if (isFound)
            {
                return new PepoleBusiness(PersonID, FirstName, SecondName, ThirdName
            , LastName, NationalNo, DateOfBirth, Gendor, Address
            , Phone, Email, NationalityCountryID, ImagePath);
            }
            else
                return null;



        }

        private bool _AddNewPerson()
        {
            this.PersonID = PeploeDataAccess.AddNewPerson(this.FirstName, this.SecondName, this.ThirdName,
                this.LastName, this.NationalNo, this.DateOfBirth, this.Gendor, this.Address,
                this.Phone, this.Email, this.NationalityCountryID, this.ImagePath);

            return PersonID != -1;
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
            return PeploeDataAccess.UpdatePerson(this.PersonID, this.FirstName, this.SecondName, this.ThirdName,
                 this.LastName, this.NationalNo, this.DateOfBirth, this.Gendor, this.Address,
                 this.Phone, this.Email, this.NationalityCountryID, this.ImagePath);
        }
        public static bool IsPersonExist(int PersonID)
        {
            return PeploeDataAccess.IsPersonExist(PersonID);
        }
        public bool IsPersonExist(string NationalNo)
        {
            return PeploeDataAccess.IsPersonExist(NationalNo);
        }
        public static DataTable GetAllPeople()
        {
            return PeploeDataAccess.GetAllPeople();
        }
        public static bool DeletePerson(int PersonID)
        {
            return PeploeDataAccess.DeletePerson(PersonID);
        }
    }
}
