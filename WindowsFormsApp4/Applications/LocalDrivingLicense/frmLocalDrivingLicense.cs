using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVDLBusiness;
using WindowsFormsApp4.GlobalClasses;
namespace WindowsFormsApp4.Applications.LocalDrivingLicense
{
    public partial class frmLocalDrivingLicense : Form
    {
        enum enMode { AddNew = 1, Update = 2 }
        enMode Mode;
        private int _LocalDrivingLicenseID=-1;
        private int _SelectedPersonID = -1;

        private clsLocalDrivingLicenseBusiness _LocalDrivingLicenseInfo;
        public frmLocalDrivingLicense()
        {
            InitializeComponent();
            Mode = enMode.AddNew;
        }
        public frmLocalDrivingLicense(int LocalDrivingLicenseID)
        {
            InitializeComponent();
            _LocalDrivingLicenseID = LocalDrivingLicenseID;
            Mode = enMode.Update;

        }
        private void _FillComoboBoxWithLicenseClass()
        {
            DataTable dt = clsClassLicenseBusiness.GetAllLicenseClasses();
            foreach(DataRow row in dt.Rows)
            {
                cbLicenseClass.Items.Add(row["ClassName"]);
            }
        }
        private void _RestToDefualtValue()
        {
            _FillComoboBoxWithLicenseClass();
            if (Mode == enMode.AddNew)
            {
                this.Text = "Add New Local Driving License";
                lblTitle.Text = "Add New Local Driving License";
               _LocalDrivingLicenseInfo = new clsLocalDrivingLicenseBusiness();
                personInfoWithFilters1.FilterFocus();

                cbLicenseClass.SelectedIndex = 2;
                lblFees.Text = clsApplicationTypeBusiness.Find((int)ApplicationsBusiness.enApplicationType.NewDrivingLicense).ApplicationTypeFees.ToString();
                lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
                lblApplicationDate.Text = DateTime.Now.ToShortDateString();
            }
            else
            {
                this.Text = "Update Local Driving License";
                lblTitle.Text = "Update Local Driving License";
                btnSave.Enabled = true;

            }
        }
        private void _LoadData()
        {
            personInfoWithFilters1.FilterEnable = false;
            _LocalDrivingLicenseInfo = clsLocalDrivingLicenseBusiness.FindByLocalDrivingLicenseApplication(_LocalDrivingLicenseID);

            if (_LocalDrivingLicenseInfo == null)
            {
                MessageBox.Show("No Appliction With ID:" + _LocalDrivingLicenseID, "No Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
             }
            personInfoWithFilters1.LoadpersonInfo(_LocalDrivingLicenseInfo.ApplicationPersonID);
            lblApplicationID.Text = _LocalDrivingLicenseInfo.LocalDrivingLicenseApplicationID.ToString();
            lblApplicationDate.Text = clsFormat.DateToShort(_LocalDrivingLicenseInfo.ApplicationDate);
            cbLicenseClass.SelectedIndex = cbLicenseClass.FindString(clsClassLicenseBusiness.Find(_LocalDrivingLicenseInfo.LicenseClassID).ClassName);
            lblFees.Text = _LocalDrivingLicenseInfo.PaidFees.ToString();
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;

        
        }
        private void frmLocalDrivingLicense_Load(object sender, EventArgs e)
        {
            _RestToDefualtValue();
            if (Mode == enMode.Update)
            {
                _LoadData();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds Is Not Valide! ,Put Moude over The  Red Icon","not Valide", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int ClassLicenseID = clsClassLicenseBusiness.Find(cbLicenseClass.Text).LicenseClassID;
            int ActiveApplicationID = ApplicationsBusiness.GetActiveApplicationIDForLicenseClass(_LocalDrivingLicenseInfo.ApplicationPersonID, _LocalDrivingLicenseInfo.ApplicationID, ClassLicenseID);

            if (ActiveApplicationID != -1)
            {
                MessageBox.Show("choose Another Application Class,Because The Person Has an Active application Class with ID"+ActiveApplicationID,"Already Exist", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbLicenseClass.Focus();
                return;

            }
            if (clsLicenses.IsLicenseExistByPersonID(personInfoWithFilters1.PersonID, ClassLicenseID))
            {
                MessageBox.Show("Person Already have a License with The Same Applied driving " + ActiveApplicationID, "Already Exist", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _LocalDrivingLicenseInfo.ApplicationPersonID = personInfoWithFilters1.PersonID;
            _LocalDrivingLicenseInfo.ApplicationDate = DateTime.Now;
            _LocalDrivingLicenseInfo.ApplicationTypeID = 1;
            _LocalDrivingLicenseInfo.ApplicationStatus = ApplicationsBusiness.enApplicationStatus.New;
            _LocalDrivingLicenseInfo.LastStatusDate = DateTime.Now;
            _LocalDrivingLicenseInfo.PaidFees = Convert.ToSingle(lblFees.Text);
            _LocalDrivingLicenseInfo.CreateByUserID = clsGlobal.CurrentUser.UserID;
            _LocalDrivingLicenseInfo.LicenseClassID = ClassLicenseID;

            if (_LocalDrivingLicenseInfo.Save())
            {
                lblApplicationID.Text = _LocalDrivingLicenseInfo.LocalDrivingLicenseApplicationID.ToString();
                Mode = enMode.Update;
                lblTitle.Text = "Update Local Driving License Application";

                MessageBox.Show("The Data Saved Successfully ","Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("Error: The Data is Not Saved Successfully ", "not Saved", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void personInfoWithFilters1_OnPersonSelected(int obj)
        {
            _SelectedPersonID = obj;
        }

        private void frmLocalDrivingLicense_Activated(object sender, EventArgs e)
        {
            personInfoWithFilters1.FilterFocus();
        }

        private void guna2ImageButton3_Click(object sender, EventArgs e)
        {
            if (Mode == enMode.Update)
            {
                btnSave.Enabled = true;
                tpControl.SelectedTab = tpControl.TabPages["tpApplicationInfo"];
                return;
            }
            if(personInfoWithFilters1.PersonID!=-1)
            {
                btnSave.Enabled = true;
                tpControl.SelectedTab = tpControl.TabPages["tpApplicationInfo"];
                return;
            
            }
            else

            {
                MessageBox.Show("Please Select a Person", "Select a Person", MessageBoxButtons.OK, MessageBoxIcon.Error);
                personInfoWithFilters1.FilterFocus();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lblCreatedByUser_Click(object sender, EventArgs e)
        {

        }

        private void lblFees_Click(object sender, EventArgs e)
        {

        }
    }
}
