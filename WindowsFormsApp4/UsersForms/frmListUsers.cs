using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using DVDLBusiness;

namespace WindowsFormsApp4.UsersForms
{
    public partial class frmListUsers : Form
    {
        private static DataTable _dtAllUser;
        public frmListUsers()
        {
            InitializeComponent();
        }

        private void guna2GradientPanel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void frmListUsers_Load(object sender, EventArgs e)
        {
            _dtAllUser = clsUsersBusiness.GetAllUsers();
            dgvUsers.DataSource = _dtAllUser;


            cmbFilter.SelectedIndex = 0;
            cmbIsActive.Visible = false;
            txtFilterBy.Visible = false;

            lblRecordCount.Text = dgvUsers.Rows.Count.ToString();
            if (dgvUsers.Rows.Count > 0)
            {
                dgvUsers.Columns[0].HeaderText = "User ID";
                dgvUsers.Columns[0].Width = 110;

                dgvUsers.Columns[1].HeaderText = "Person ID";
                dgvUsers.Columns[1].Width = 120;

                dgvUsers.Columns[2].HeaderText = "Full Name";
                dgvUsers.Columns[2].Width = 350;


                dgvUsers.Columns[3].HeaderText = "User Name";
                dgvUsers.Columns[3].Width = 120;

                dgvUsers.Columns[4].HeaderText = "Is Active";
                dgvUsers.Columns[4].Width = 120;

            }
        }

        private void guna2ImageButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddUpdateUsers();
            frm.ShowDialog();
            frmListUsers_Load(null, null);
        }

        private void cmbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmbFilter.Text=="Is Active")
            {
                txtFilterBy.Visible = false;
                cmbIsActive.Visible = true;
                cmbIsActive.Focus();
                cmbIsActive.SelectedIndex = 0;
            }
            else
            {
                txtFilterBy.Visible = (cmbFilter.Text!="None");
                cmbIsActive.Visible = false;
                txtFilterBy.Focus();
                txtFilterBy.Text = "";

            }
        }

        private void txtFilterBy_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            switch (cmbFilter.Text) {

                case "User ID":
                    FilterColumn = "UserID"; break;
                case "Person ID":
                    FilterColumn = "PersonID"; break;
                case "User Name":
                    FilterColumn = "UserName"; break;
                case "Full Name":
                    FilterColumn = "FullName"; break;
                default:
                    FilterColumn = "None";
                    break;

             }
            if(txtFilterBy.Text.Trim() =="" || cmbFilter.Text == "None")
            {
                _dtAllUser.DefaultView.RowFilter = "";
                lblRecordCount.Text = dgvUsers.Rows.Count.ToString();
            }
            else if (FilterColumn != "FullName" && FilterColumn != "UserName")
            {
                _dtAllUser.DefaultView.RowFilter = string.Format("[{0}]={1}", FilterColumn, txtFilterBy.Text.Trim());
            }
            else
            {
                _dtAllUser.DefaultView.RowFilter = string.Format("[{0}] Like '{1}%'", FilterColumn, txtFilterBy.Text.Trim());

            }
            lblRecordCount.Text = dgvUsers.Rows.Count.ToString();
            
        }

        private void cmbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {

            string FilterColumn = "IsActive";
            string FilterValue = cmbIsActive.Text;
            switch (cmbIsActive.Text) {
                case "All":break;
                
                case "Yes":
                    FilterValue = "1";break;

                case "No":
                    FilterValue = "0";break;
                
            }
            if (FilterValue == "All")
            {
                _dtAllUser.DefaultView.RowFilter = "";
            }
            else
            {
                _dtAllUser.DefaultView.RowFilter = string.Format("[{0}]={1}", FilterColumn, FilterValue);
            }
        }

        private void cnmShowUserInfo_Click(object sender, EventArgs e)
        {
            frmUserInfo frm = new frmUserInfo((int)dgvUsers.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void dgvUsers_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            frmUserInfo frm = new frmUserInfo((int)dgvUsers.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void cnMenu_Opening(object sender, CancelEventArgs e)
        {
            cnMenu.Show();
        }

        private void cnmAddNewUser_Click(object sender, EventArgs e)
        {
            frmAddUpdateUsers frm = new frmAddUpdateUsers();
            frm.ShowDialog();
            frmListUsers_Load(null, null);

        }

        private void cnmUpdateUser_Click(object sender, EventArgs e)
        {
            frmAddUpdateUsers frm = new frmAddUpdateUsers((int)dgvUsers.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            frmListUsers_Load(null, null);


        }

        private void cnmChangePassword_Click(object sender, EventArgs e)
        {
            frmChangePassword frm = new frmChangePassword((int)dgvUsers.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

        }

        private void cnmDeleteUser_Click(object sender, EventArgs e)
        {
            if (clsUsersBusiness.DeleteUser((int)dgvUsers.CurrentRow.Cells[0].Value))
            {
                MessageBox.Show("The user Deleted SuccessFully", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            }
            else
            {
                MessageBox.Show("The user is not Deleted SuccessFully", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            frmListUsers_Load(null, null);


        }

        
    }
}
