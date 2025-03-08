using WindowsFormsApp4.Properties;
using WindowsFormsApp4.PeopleForms;
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
using DVDLBusiness;
using WindowsFormsApp4.GlobalClasses;

namespace WindowsFormsApp4.PeopleForms.Controls
{
    public partial class PersonInfoWithFilters : UserControl
    {
        public event Action<int> OnPersonSelected;

        protected virtual void PersonSelected(int PersonID)
        {
            Action<int> handler = OnPersonSelected;
            if (handler != null)
            {
                handler(PersonID);
            }
        }
        public PersonInfoWithFilters()
        {
            InitializeComponent();
        }
        private bool _ShowAddPerson = true;
        public bool ShowAddPerson {

            get
            {
                return _ShowAddPerson;
            }
            set
            {
                _ShowAddPerson = value;
                btnAddPerson.Visible = _ShowAddPerson;
            }
        }

        private bool _FilterEnable = true;
        public bool FilterEnable
        {
            get
            {
                return _FilterEnable;
            }
            set
            {
                _FilterEnable = value;
                gbFilter.Enabled = _FilterEnable;
            }
        }

        private int _PersinID = -1;
        public int PersonID {
        get { return personInfo1.PersnID; }
        }


        public PepoleBusiness SelectPersonInfo{
            get
            {
                return personInfo1.SelectPersonInfo;
            }
          }

       
        public void LoadpersonInfo(int PersonID)
        {
            cbFilter.SelectedIndex = 0;
            txtFilterValue.Text = PersonID.ToString();
            FindNew();

        }
        public void LoadpersonInfo(string NationalNo)
        {
            cbFilter.SelectedIndex = 1;
            txtFilterValue.Text = NationalNo;
            FindNew();

        }
        private void FindNew()
        {
            switch (cbFilter.Text)
            {
                case "Person ID":

                    personInfo1.LoadPersonInfo(int.Parse(txtFilterValue.Text));
                    break;


                case "National No":

                    personInfo1.LoadPersonInfo(txtFilterValue.Text);
                    break;

                default:
                    break;          
            }

            if (OnPersonSelected != null && FilterEnable)
                OnPersonSelected(personInfo1.PersnID);
           
        }



        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            frmAddUpdatePersons frm1 = new frmAddUpdatePersons();
            frm1.DataBack += DataBackEvent;
            frm1.ShowDialog();
        }
        private void DataBackEvent(object sender,int Person)
        {
            cbFilter.SelectedIndex = 1;
            txtFilterValue.Text = PersonID.ToString();
            personInfo1.LoadPersonInfo(PersonID);
        }


        public void FilterFocus()
        {
            txtFilterValue.Focus();
        }

        private  void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ((char)13))
            {
                btnFind.PerformClick();
            }
            if(cbFilter.Text=="Person ID")
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }

        private void guna2GradientPanel1_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFilterValue.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFilterValue, "This Field is required!");
            }
            else
            {
                errorProvider1.SetError(txtFilterValue, null);
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the Error", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            FindNew();
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Text = "";
            txtFilterValue.Focus();

        }

        private void guna2GradientPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2GradientPanel1_Paint_1(object sender, PaintEventArgs e)
        {

        }
    }
    
}
