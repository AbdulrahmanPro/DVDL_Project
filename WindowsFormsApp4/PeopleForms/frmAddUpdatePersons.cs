using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVDLBusiness;
using System.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using System.Runtime.ConstrainedExecution;
using WindowsFormsApp4.GlobalClasses;

namespace WindowsFormsApp4
{
    public partial class frmAddUpdatePersons : Form
    {

        public delegate void DataBackEventHandler(object sender, int PersonID);
        public event DataBackEventHandler DataBack;
        private enum enMode { AddNew = 0, Update = 1 };
        private enum enGendor { Male = 0, Female = 1 };
        private int _PersonID;
        enMode _Mode;
        PepoleBusiness _Person;
        public frmAddUpdatePersons()
        {
            InitializeComponent();
            _Mode = enMode.AddNew;

        }
        private void _FillCountryinComobBox()
        {
            DataTable dt = CountryBusiness.GetAllCountries();
            foreach (DataRow row in dt.Rows)
            {
                cmbCountry.Items.Add(row["CountryName"]);
            }

        }
        public frmAddUpdatePersons(int PersonID)
        {
            InitializeComponent();
            _Mode = enMode.Update;
            _PersonID = PersonID;
        }

        private void _ResetToDefualtValues()
        {
            _FillCountryinComobBox();

            if (_Mode == enMode.AddNew)
            {
                lblTitle.Text = "Add New Person";
                _Person = new PepoleBusiness();
            }
            else
            {
                lblTitle.Text = "Update Person";
            }
            if (rdMale.Checked)
            {

                pbImage.Image = Properties.Resources.Male_512;
            }
            else
            {
                pbImage.Image = Properties.Resources.Female_512;
            }
            btnRemoveImage.Visible = (pbImage.ImageLocation != null);

            dtpDateOfBirth.MaxDate = DateTime.Now.AddYears(-18);
            dtpDateOfBirth.Value = dtpDateOfBirth.MaxDate;

            dtpDateOfBirth.MinDate = DateTime.Now.AddYears(-100);

            cmbCountry.SelectedIndex = cmbCountry.FindString("Yemen");
            txtFirstName.Text = "";
            txtSecondName.Text = "";
            txtThirdName.Text = "";
            txtLastName.Text = "";
            txtNaltionalNo.Text = "";
            txtPhome.Text = "";
            txtEmail.Text = "";
            txtAddress.Text = "";
            rdMale.Checked = true;
        }
        private void personCard1_Load(object sender, EventArgs e)
        {

        }

        private void _LoadData()
        {
            _Person = PepoleBusiness.Find(_PersonID);
            if (_Person == null)
            {
                MessageBox.Show("The Person With " + _PersonID, " ID Is Not Found :-(", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }
            lblPersonID.Text = _Person.PersonID.ToString();
            txtFirstName.Text = _Person.FirstName;
            txtSecondName.Text = _Person.SecondName;
            txtThirdName.Text = _Person.ThirdName;
            txtLastName.Text = _Person.LastName;

            txtNaltionalNo.Text = _Person.NationalNo;
            txtPhome.Text = _Person.Phone;
            txtEmail.Text = _Person.Email;
            txtAddress.Text = _Person.Address;
            dtpDateOfBirth.Value = _Person.DateOfBirth;
            if (_Person.Gendor == 0)
                rdMale.Checked = true;
            else
                rdFamale.Checked = false;

            cmbCountry.SelectedIndex = cmbCountry.FindString(_Person.CountryInfo.CountryName);

            if (_Person.ImagePath != "")
            {
                pbImage.ImageLocation = _Person.ImagePath;
            }
            btnRemoveImage.Visible = (_Person.ImagePath != "");
            return;
        }

        private void btnSetImage_Click(object sender, EventArgs e)
        {


        }
        private void ValidateEmptyTextBox(object sender, CancelEventArgs e)
        {
            TextBox Temp = ((TextBox)sender);
            if (string.IsNullOrEmpty(Temp.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(Temp, "This Filed Is Required!");
            }
            else
            {
                errorProvider1.SetError(Temp, null);
            }
        }
        private void txtEmailValidating(object sender, CancelEventArgs e)
        {
            if (txtEmail.Text.Trim() == "")
                return;
            if (!clsValidation.EmailValidate(txtEmail.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtEmail, "Invalid Email Address");
            }
            else
            {
                errorProvider1.SetError(txtEmail, null);
            }
        }
        private void txtNationalNoValidating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNaltionalNo.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNaltionalNo, "The National No Is Required");
            }
            else
            {
                errorProvider1.SetError(txtNaltionalNo, null);

            }
            if (txtNaltionalNo.Text.Trim() != _Person.NationalNo && PepoleBusiness.IsPersonExist(_Person.PersonID))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtNaltionalNo, "The National Number Is used by Another Person");

            }
            else
            {
                errorProvider1.SetError(txtNaltionalNo, null);

            }
        }

        private void rdMale_Click(object sender, EventArgs e)
        {
        }

        private void rdFamale_Click(object sender, EventArgs e)
        {
        }
        private bool _HandalPersonImage()
        {
            if (_Person.ImagePath != pbImage.ImageLocation)
            {
                if (_Person.ImagePath != "")
                {
                    try
                    {
                        File.Delete(_Person.ImagePath);
                    }
                    catch (IOException)
                    {

                        MessageBox.Show("The image Is Not loaded");
                    }
                }
            }
            if (pbImage.ImageLocation != null)
            {
                string SourceFile = pbImage.ImageLocation.ToString();
                if (clsUtil.CopyImageToProjectImagesFolder(ref SourceFile)) {
                    pbImage.ImageLocation = SourceFile;
                    return true;
                }
                else
                {
                    MessageBox.Show("Error Copying Image File", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return true;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            }

        private void txtFirstName_TextChanged(object sender, EventArgs e)
        {

        }

        
        private void btnSave_Click_1(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some Filed is Not Valid ,put Mouse Over red Icon To see Error", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!_HandalPersonImage())
                return;
            int NationalCountryID = CountryBusiness.Find(cmbCountry.Text).CountryID;
            _Person.FirstName = txtFirstName.Text.Trim();
            _Person.SecondName = txtSecondName.Text.Trim();
            if (txtThirdName.Text != null)
            {
                _Person.ThirdName = txtThirdName.Text.Trim();
            }
            else
            {
                _Person.ThirdName = "";
            }
            _Person.LastName = txtLastName.Text.Trim();
            _Person.Phone = txtPhome.Text.Trim();
            _Person.Email = txtEmail.Text.Trim();
            _Person.Address = txtAddress.Text.Trim();
            _Person.NationalNo = txtNaltionalNo.Text.Trim();
            _Person.NationalityCountryID = NationalCountryID;
            _Person.DateOfBirth = dtpDateOfBirth.Value;

            if (rdMale.Checked)
            {
                _Person.Gendor = (short)enGendor.Male;
            }
            else
            {
                _Person.Gendor = (short)enGendor.Female;

            }
            if (pbImage.ImageLocation != null)
            {
                _Person.ImagePath = pbImage.ImageLocation;
            }
            else
            {
                _Person.ImagePath = "";
            }

            if (_Person.Save())
            {
                lblPersonID.Text = _Person.PersonID.ToString();
                lblTitle.Text = "Update Person";
                MessageBox.Show("Data is  Saved Successfully", "done Successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("Error:Data is not Saved Successfuly", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            DataBack?.Invoke(this, _Person.PersonID);

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void frmAddUpdatePersons_Load(object sender, EventArgs e)
        {
            _ResetToDefualtValues();
            if (_Mode == enMode.Update)
                _LoadData();
        }

        private void btnSetImage_Click_1(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Image FIles|*.jpg;*.jpeg;*.pen;*.gif;*.bmp;";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string SelectedFilePath = openFileDialog1.FileName;
                pbImage.ImageLocation=SelectedFilePath;
                btnRemoveImage.Visible = true;

            }
        }

        private void rdMale_CheckedChanged(object sender, EventArgs e)
        {
            if (pbImage.ImageLocation == null)
                pbImage.Image = Properties.Resources.Male_512;

        }

        private void rdFamale_CheckedChanged(object sender, EventArgs e)
        {
            if (pbImage.ImageLocation == null)
                pbImage.Image = Properties.Resources.Female_512;
        }

        private void guna2GroupBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2GradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void btnConcel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}