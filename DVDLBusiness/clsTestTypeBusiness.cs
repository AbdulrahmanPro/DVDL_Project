using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVDLDataAccess;
using System.Data;
namespace DVDLBusiness
{
   public class clsTestTypeBusiness
    {
        enum enMode { AddNew=1,Update=2 };
        enMode Mode;
        public  enum enTestType { VisionTest = 1, WrittenTest = 2, StreetTest = 3 };


       public  clsTestTypeBusiness.enTestType ID { set; get; }
       public string TestTypeTitle { set; get; }
       public string TestTypeDescripation { set; get; }
       public float TestTypeFees { set; get; }

        private clsTestTypeBusiness()
        {

            this.ID = enTestType.VisionTest;
            this.TestTypeTitle = "";
            this.TestTypeDescripation = "";
            this.TestTypeFees = 0;
            Mode = enMode.AddNew;
        }
       
        private clsTestTypeBusiness(clsTestTypeBusiness.enTestType ID, string TestTypeTitle, string TestTypeDescripation, float TestTypeFees)
        {                                  
                                      
            this.ID = clsTestTypeBusiness.enTestType.VisionTest;             
            this.TestTypeTitle = TestTypeTitle;
            this.TestTypeDescripation = TestTypeDescripation;
            this.TestTypeFees = TestTypeFees;

            Mode = enMode.Update;
        }

        private bool _AddNewTestType()
        {
            this.ID = (clsTestTypeBusiness.enTestType) clsTestType.AddNewTestType(this.TestTypeTitle, this.TestTypeDescripation, this.TestTypeFees);

            return (this.TestTypeTitle != "");
        }
        private bool UpdateTestType() {

            return clsTestType.UpdateTestType((int)this.ID, this.TestTypeTitle, this.TestTypeDescripation, this.TestTypeFees);
        }
        public bool Save()
        {

            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTestType())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    if (UpdateTestType())
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
        public static clsTestTypeBusiness Find(clsTestTypeBusiness.enTestType ID)
        {
            string TestTypeTitle = "";
            string TestTypeDescripation = "";
            float TestTypeFees = 0;
            if (clsTestType.GetTestTypeByID((int)ID, ref TestTypeTitle, ref TestTypeDescripation, ref TestTypeFees))

            {
                return new clsTestTypeBusiness(ID, TestTypeTitle, TestTypeDescripation, TestTypeFees);

            }
            else
            {
                return null;
            }
            
        }
        public static DataTable GetAllTestType()
        {
            return clsTestType.GetAllTestTypes();
        }
    }
}
