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

namespace WindowsFormsApp4.Applications
{
    public partial class frmListApplicationType : Form
    {
        private DataTable _dtApplicationTypeInfo;
        public frmListApplicationType()
        {
            InitializeComponent();
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void frmListApplicationType_Load(object sender, EventArgs e)
        {
            _dtApplicationTypeInfo = clsApplicationTypeBusiness.GetAllApplicationType();
            dgvAllApplicationType.DataSource = _dtApplicationTypeInfo;
            lblRecordCount.Text = _dtApplicationTypeInfo.Rows.Count.ToString();
            if (dgvAllApplicationType.Rows.Count > 0)
            {
                dgvAllApplicationType.Columns[0].HeaderText = "ID";
                dgvAllApplicationType.Columns[0].Width =110 ;


                dgvAllApplicationType.Columns[1].HeaderText = "Title";
                dgvAllApplicationType.Columns[1].Width = 400;


                dgvAllApplicationType.Columns[2].HeaderText = "Fees";
                dgvAllApplicationType.Columns[2].Width =100;
            }



        }

        private void editApplicationType_Click(object sender, EventArgs e)
        {
            frmEditApplicationType frm = new frmEditApplicationType((int)dgvAllApplicationType.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            frmListApplicationType_Load(null, null);
        }

        private void cmnApplicationType_Click(object sender, EventArgs e)
        {
            frmEditApplicationType frm = new frmEditApplicationType((int)dgvAllApplicationType.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            frmListApplicationType_Load(null, null);

        }

        private void dgvAllApplicationType_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            frmEditApplicationType frm = new frmEditApplicationType((int)dgvAllApplicationType.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            frmListApplicationType_Load(null, null);

        }
    }
}
