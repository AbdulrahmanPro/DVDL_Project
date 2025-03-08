using WindowsFormsApp4.Properties;
using DVDLBusiness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.IO;
using WindowsFormsApp4.PeopleForms;


namespace WindowsFormsApp4.PeopleForms.Controls
{
    public partial class PersonInfo : UserControl
    {
        private PepoleBusiness _Person;
        private int _PersonID = -1;
        public PepoleBusiness SelectPersonInfo{
            get { return _Person; }
        }
        public int PersnID
        {
            get { return _PersonID; }
        }
        public PersonInfo()
        {
            InitializeComponent();
        }

        private void guna2HtmlLabel5_Click(object sender, EventArgs e)
        {

        }
        public void RestToDefualtvalues()
        {
            lblPersonID.Text = "[?????]";
            lblPersonName.Text = "[?????]";
            lblNationalNumber.Text = "[?????]";
            lblDateOfDate.Text = "[?????]";
            pbGendorImage.Image = Properties.Resources.Man_32;
            lblGendor.Text = "[?????]";
            lblPhone.Text = "[?????]";
            lblEmail.Text = "[?????]";
            lblCountry.Text = "[?????]";
            lblAddress.Text = "[?????]";
            pbImage.Image = Properties.Resources.Male_512;
        }
        public void LoadPersonInfo(int PersonID)
        {
            _Person = PepoleBusiness.Find(PersonID);
            if (_Person == null)
            {
                MessageBox.Show("The Person Is Not Found", "No Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                RestToDefualtvalues();
            }
            else
            {
                _FillPersonInfo();
            }
        }
        public void LoadPersonInfo(string NationalNo)
        {
            _Person = PepoleBusiness.Find(NationalNo);
            if (_Person == null)
            {
                RestToDefualtvalues();
            }
            else
            {
                _FillPersonInfo();
            }
        }
        private void _FillPersonInfo()
        {
            lnkEditInfo.Enabled = true;
            _PersonID = _Person.PersonID;
            lblPersonID.Text = _Person.PersonID.ToString();
            lblPersonName.Text = _Person.FullName;
            lblNationalNumber.Text = _Person.NationalNo;
            lblDateOfDate.Text = _Person.DateOfBirth.ToShortDateString();
            lblGendor.Text = _Person.Gendor == 0 ? "Male" : "Female";
            lblPhone.Text = _Person.Phone;
            lblEmail.Text = _Person.Email;
            lblCountry.Text =CountryBusiness.Find(_Person.NationalityCountryID).CountryName;
            lblAddress.Text = _Person.Address;
            _LoadPersonImage();
        }
        private void _LoadPersonImage()
        {
            if (_Person.Gendor == 0)
            {
                pbImage.Image = Properties.Resources.Male_512;
            }
            else
            {
                pbImage.Image = Properties.Resources.Female_512;
            }
            string ImagePath = _Person.ImagePath;
            if (ImagePath != "")
            {
                if (File.Exists(ImagePath))
                {
                    pbImage.ImageLocation = ImagePath;
                }
                else
                {
                    MessageBox.Show("Could not find this image: = " + ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void lnkEditInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddUpdatePersons frm = new frmAddUpdatePersons(_PersonID);
            frm.ShowDialog();
            LoadPersonInfo(_PersonID);
        }

        private void lblEmail_Click(object sender, EventArgs e)
        {

        }

        private void lblGendor_Click(object sender, EventArgs e)
        {

        }

        private void lblDateOfDate_Click(object sender, EventArgs e)
        {

        }

        private void lblNationalNumber_Click(object sender, EventArgs e)
        {

        }

        private void lblPersonID_Click(object sender, EventArgs e)
        {

        }

        private void lblPersonName_Click(object sender, EventArgs e)
        {

        }

        private void lblPhone_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel17_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel10_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel9_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel13_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel8_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel12_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel11_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel7_Click(object sender, EventArgs e)
        {

        }

        private void guna2HtmlLabel6_Click(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox6_Click(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox8_Click(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox7_Click(object sender, EventArgs e)
        {

        }

        private void pbGendorImage_Click(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void guna2GradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lnkEditInfo_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm = new frmAddUpdatePersons(_PersonID);
            frm.ShowDialog();
        }
    }
}
