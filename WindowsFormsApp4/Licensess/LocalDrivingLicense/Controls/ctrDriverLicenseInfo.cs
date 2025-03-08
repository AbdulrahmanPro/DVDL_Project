using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using DVDLBusiness;
using WindowsFormsApp4.GlobalClasses;

namespace WindowsFormsApp4.Licensess.LocalDrivingLicense.Controls
{
    public partial class ctrDriverLicenseInfo : UserControl
    {

        private int _LicenseID = -1;
        private clsLicenses _LicenseInfo;
        public ctrDriverLicenseInfo()
        {
            InitializeComponent();
        }
        public int LicenseID 
        {
            get { return _LicenseID; }
        }
        public clsLicenses SelectedLicenseInfo 
        {
            get { return _LicenseInfo; }
        }


        private void guna2GradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        private void LoadImagePerson()
        {
            if (_LicenseInfo.DriverInfo.PersonInfo.Gendor == 0)
                pbPersonImage.Image = Properties.Resources.Male_512;
            else
                pbPersonImage.Image = Properties.Resources.Female_512;
            string ImagePath = _LicenseInfo.DriverInfo.PersonInfo.ImagePath;
            if (ImagePath != "")
            {
                if (File.Exists(ImagePath))
                {
                    pbPersonImage.Load();
                }
                else
                {
                    MessageBox.Show("Could Not Find This Image: =" + ImagePath, "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            
        }
        public void LoadInfo(int LicenseID)
        {
            _LicenseID = LicenseID;
            _LicenseInfo = clsLicenses.Find(_LicenseID);
            if (_LicenseInfo == null)
            {
                MessageBox.Show("Could Not Found License ID =" + _LicenseID.ToString(), "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _LicenseID = -1;
                return;
            }
            lblClass.Text = _LicenseInfo.LicenseClassInfo.ClassName;
            lblLicenseID.Text = _LicenseInfo.LicenseID.ToString();
            lblNationalNo.Text = _LicenseInfo.DriverInfo.PersonInfo.NationalNo;
                        lblFullName.Text = _LicenseInfo.DriverInfo.PersonInfo.FullName;

            lblGendor.Text = _LicenseInfo.DriverInfo.PersonInfo.Gendor==0?"Male":"Female";
            lblIssueDate.Text = clsFormat.DateToShort( _LicenseInfo.IssueDate);
            lblIssueReason.Text = _LicenseInfo.IssueReasonText;
            lblNotes.Text = _LicenseInfo.Notes == "" ? "No Notes" : _LicenseInfo.Notes;
            lblIsActive.Text = _LicenseInfo.IsActive ? "Yes" : "No";
            lblDateOfBirth.Text = clsFormat.DateToShort(_LicenseInfo.DriverInfo.PersonInfo.DateOfBirth);
            LblDriverID.Text = _LicenseInfo.DriverID.ToString();
            lblExpirationDate.Text = clsFormat.DateToShort(_LicenseInfo.ExpirationDate);
            lblIsDetained.Text = _LicenseInfo.IsDetained ? "Yes" : "NO";
            
        }
    }
}
