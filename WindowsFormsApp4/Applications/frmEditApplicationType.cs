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

namespace WindowsFormsApp4.Applications
{
    public partial class frmEditApplicationType : Form
    {
        private int _ApplicationTypeID;
        private clsApplicationTypeBusiness ApplicationInfo;
        public frmEditApplicationType(int ApplicationTypeID)
        {
            InitializeComponent();
            _ApplicationTypeID = ApplicationTypeID;
        }

        private void frmEditApplicationType_Load(object sender, EventArgs e)
        {
            lblApplicationTypeID.Text = _ApplicationTypeID.ToString();
            ApplicationInfo = clsApplicationTypeBusiness.Find(_ApplicationTypeID);

            if (ApplicationInfo != null)
            {
                txtTitle.Text = ApplicationInfo.ApplicationTypeTitle;
                txtFees.Text = ApplicationInfo.ApplicationTypeFees.ToString();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show("Some Fileds is not Valide!,put Mouse over The Red Icon", "Is Not Valide", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ApplicationInfo.ApplicationTypeTitle = txtTitle.Text.Trim();
            ApplicationInfo.ApplicationTypeFees = Convert.ToSingle(txtFees.Text.Trim());
            if (ApplicationInfo.Save())
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
            if (string.IsNullOrEmpty(txtTitle.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtTitle,"Title Can not be empty");
            }
            else
            {
                errorProvider1.SetError(txtTitle, null);
            }
        }

        private void txtFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFees.Text))
            {

                e.Cancel = true;
                errorProvider1.SetError(txtFees, "Fees Can not be empty");
                return;
            }
            else
            {
                errorProvider1.SetError(txtFees, null);
            }
            if (!clsValidation.IsNumber(txtFees.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFees, "Fees Can not be string");
                return;
            }
            else
            {
                errorProvider1.SetError(txtFees, null);
            }
        }
    }
}
