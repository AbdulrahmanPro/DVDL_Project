using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DVDLDataAccess;

namespace DVDLBusiness
{
    public class CountryBusiness
    {

       public int CountryID { set; get; }
       public string CountryName { set; get; }
        CountryBusiness()
        {
            this.CountryID = -1;
            this.CountryName = "";
        }
        public CountryBusiness(int CountryID, string CountryName)
        {
            this.CountryID = CountryID;
            this.CountryName = CountryName;
        }
        public static CountryBusiness Find(int CountryID)
        {
            bool isFound = false;
            string CountryName = "";
            isFound= CountryDataAccess.GetCountryInfoByID(CountryID,ref CountryName);
            if (isFound == true)
            {
                return new CountryBusiness(CountryID, CountryName);
            }
            else
                return null;
           }
        public static CountryBusiness Find(string CountryName)
        {
            bool isFound = false;
            int CountryID =-1;
            isFound = CountryDataAccess.GetCountryInfoByID(ref CountryID, CountryName);
            if (isFound == true)
            {
                return new CountryBusiness(CountryID, CountryName);
            }
            else
                return null;
        }
        static public DataTable GetAllCountries()
        {
            return CountryDataAccess.GetAllCountries();
        }
    }
}
