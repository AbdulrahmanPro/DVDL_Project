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
using System.Windows.Forms.VisualStyles;

namespace WindowsFormsApp4.PeopleForms
{
    public partial class frmListPeople : Form
    {
        private static DataTable _dtAllPeople = PepoleBusiness.GetAllPeople();
        private DataTable _dtPeople = _dtAllPeople.DefaultView.ToTable(false, "PersonID", "NationalNo", "FirstName", "SecondName",
                            "ThirdName", "LastName", "DateOfBirth", "GendorCaption", "Phone", "Email",
                       "CountryName");
        public frmListPeople()
        {
            InitializeComponent();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddUpdatePersons();
            frm.ShowDialog();
        }

        private void _RefreshPeoplList()
        {
            _dtAllPeople = PepoleBusiness.GetAllPeople();
            dgvPeopleList.DataSource =_dtAllPeople.DefaultView.ToTable(false, "PersonID", "NationalNo", "FirstName", "SecondName",
                            "ThirdName", "LastName", "DateOfBirth", "Gendor", "Phone", "Email",
                       "CountryName");
            dgvPeopleList.DataSource = _dtPeople;
            lblRecordCount.Text = dgvPeopleList.Rows.Count.ToString();


        }
        private void frmListPeople_Load(object sender, EventArgs e)
        {

            dgvPeopleList.DataSource = _dtPeople;
            cmFilterBy.SelectedIndex = 0;
            lblRecordCount.Text = dgvPeopleList.Rows.Count.ToString();

            if (dgvPeopleList.Rows.Count > 0)
            {
                dgvPeopleList.Columns[0].HeaderText = "Person ID";
                dgvPeopleList.Columns[0].Width = 120;


                dgvPeopleList.Columns[1].HeaderText = "National No.";
                dgvPeopleList.Columns[1].Width = 120;


                dgvPeopleList.Columns[2].HeaderText = "First Name";
                dgvPeopleList.Columns[2].Width = 120;

                dgvPeopleList.Columns[3].HeaderText = "Second Name";
                dgvPeopleList.Columns[3].Width = 120;

                dgvPeopleList.Columns[4].HeaderText = "Third Name";
                dgvPeopleList.Columns[4].Width = 120;

                dgvPeopleList.Columns[5].HeaderText = "Last Name";
                dgvPeopleList.Columns[5].Width = 120;

                dgvPeopleList.Columns[6].HeaderText = "Date Of Birth";
                dgvPeopleList.Columns[6].Width = 140;

                dgvPeopleList.Columns[7].HeaderText = "Gendor";
                dgvPeopleList.Columns[7].Width = 120;

                dgvPeopleList.Columns[8].HeaderText = "Phone";
                dgvPeopleList.Columns[8].Width = 120;

                dgvPeopleList.Columns[9].HeaderText = "Email";
                dgvPeopleList.Columns[9].Width = 140;


                dgvPeopleList.Columns[10].HeaderText = "Country Name";
                dgvPeopleList.Columns[10].Width = 140;



            }



        }

        private void button1_Click(object sender, EventArgs e)
        {

            Form frm = new FindPerson();
            frm.ShowDialog();
        }

        private void stmShowPersonInfo_Click(object sender, EventArgs e)
        {
            int PersonID = (int)dgvPeopleList.CurrentRow.Cells[0].Value;
            Form frm = new ShowPersonInfo(PersonID);
            frm.ShowDialog();
        }

     

        private void stmEditPerson_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddUpdatePersons((int)dgvPeopleList.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _RefreshPeoplList();
        }

        private void stmSendSMS_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Will Be Come Soon!");
        }

        private void stmSendEmail_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Will Be Come Soon!");

        }

        private void guna2Button1_Click_1(object sender, EventArgs e)
        {

            Form frm = new frmAddUpdatePersons();
            frm.ShowDialog();
            _RefreshPeoplList();
        }

        private void cmFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterBy.Visible = (cmFilterBy.Text != "None");
            if (txtFilterBy.Visible)
            {
                txtFilterBy.Text = "";
                txtFilterBy.Focus();
            }
        }

        private void txtFilterBy_TextChanged(object sender, EventArgs e)
        {

            string FilterColumn = "";
            switch (cmFilterBy.Text)
            {
                case "Person ID":
                    FilterColumn = "PersonID"; break;
                case "National No.":
                    FilterColumn = "NationalNo"; break;
                case "First Name":
                    FilterColumn = "FirstName"; break;
                case "Second Name":
                    FilterColumn = "SecondName"; break;
                case "Third Name":
                    FilterColumn = "ThirdName"; break;
                case "Last Name":
                    FilterColumn = "LastName"; break;
                case "Nationality":
                    FilterColumn = "CountryName"; break;
                case "Gendor":
                    FilterColumn = "Gendor"; break;
                case "Phone":
                    FilterColumn = "Phone"; break;
                case "Email":
                    FilterColumn = "Email"; break;
                default:
                    FilterColumn = "None"; break;
            }
            if (txtFilterBy.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtPeople.DefaultView.RowFilter = "";
                lblRecordCount.Text = _dtPeople.Rows.Count.ToString();
                return;
            }
            if (FilterColumn == "PersonID")
            {
                _dtPeople.DefaultView.RowFilter = string.Format("[{0}]={1}", FilterColumn, txtFilterBy.Text.Trim());
            }
            else
            {
                _dtPeople.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterBy.Text.Trim());
            }
            lblRecordCount.Text = _dtPeople.Rows.Count.ToString();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new ShowPersonInfo((int)dgvPeopleList.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddUpdatePersons();
            frm.ShowDialog();
            _RefreshPeoplList();

        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Form frm = new frmAddUpdatePersons((int)dgvPeopleList.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _RefreshPeoplList();
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure you Must To Delete Person [" + dgvPeopleList.CurrentRow.Cells[0].Value + "]", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (PepoleBusiness.DeletePerson((int)dgvPeopleList.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("Person Delete SuccessFully.", "SuccessFul", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    _RefreshPeoplList();

                }
                else
                {
                    MessageBox.Show("Person Delete Failed.", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }

        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Service Will be Come Soon!", "Soon", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void phoneCallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Service Will be Come Soon!", "Soon", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
    }
}