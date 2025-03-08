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

namespace WindowsFormsApp4.Applications.TestType
{
    public partial class frmEditTestType : Form
    {

        clsTestTypeBusiness.enTestType _TestTypeID=clsTestTypeBusiness.enTestType.VisionTest;
        private clsTestTypeBusiness _TestTypeInfo;
        
        public frmEditTestType(clsTestTypeBusiness.enTestType TestTypeID)
        {
            InitializeComponent();
            _TestTypeID = TestTypeID;
        }
        private void frmEditTestType_Load(object sender, EventArgs e)
        {
            _TestTypeInfo = clsTestTypeBusiness.Find(_TestTypeID);
            if (_TestTypeInfo != null)
            {
                lblTestTyep.Text = ((int)_TestTypeID).ToString();
                txtTitle.Text = _TestTypeInfo.TestTypeTitle;
                txtDescription.Text = _TestTypeInfo.TestTypeDescripation;
                txtFees.Text = _TestTypeInfo.TestTypeFees.ToString();
            }
            else
            {
                MessageBox.Show("Can Not Find Test Type With ID =" + _TestTypeID, "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }
        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void guna2ImageButton2_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some Fileds is not Valide!,put Mouse over The Red Icon", "Is Not Valide", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _TestTypeInfo.TestTypeTitle = txtTitle.Text.Trim();
            _TestTypeInfo.TestTypeDescripation = txtTitle.Text.Trim();
            _TestTypeInfo.TestTypeFees = Convert.ToSingle(txtFees.Text.Trim());
            if (_TestTypeInfo.Save())
            {
                MessageBox.Show("Data Saved Successfully", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                MessageBox.Show("Error: Data Is Not Saved Successfuly", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtTitle_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtTitle.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtTitle, "The Title can Not Be Null");
            }
            else
            {
                errorProvider1.SetError(txtTitle, null);
            }
        }

        private void txtDescription_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtDescription.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtDescription, "The Descripition can Not Be Null");
            }
            else
            {
                errorProvider1.SetError(txtDescription, null);
            }
        }

        private void txtFees_Validating(object sender, CancelEventArgs e)
        {

            if (string.IsNullOrEmpty(txtDescription.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtDescription, "The Descripition can Not Be Null");
            }
            else
            {
                errorProvider1.SetError(txtDescription, null);
            }

            if (!clsValidation.IsNumber(txtFees.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFees, "The Fees can Not Be string");
            }
            else
            {
                errorProvider1.SetError(txtFees, null);
            }
        }


    }
}
