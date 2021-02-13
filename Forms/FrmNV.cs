using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TTCN1_QuanLyBanHangMayStore.Forms
{
    public partial class FrmNV : Form
    {
        public FrmNV()
        {
            InitializeComponent();
        }

        private void FrmNV_Load(object sender, EventArgs e)
        {
            Hienthi_Luoi();

        }

        private void Hienthi_Luoi()
        {
            string sql;
            DataTable tblNV;
            sql = "SELECT MaNV, TenNV, SDTNV FROM tblNhanVien";
            tblNV = ThucThiSql.DocBang(sql);
            dataGridView1.DataSource = tblNV;
            dataGridView1.Columns[0].HeaderText = "Mã nhân viên";
            dataGridView1.Columns[1].HeaderText = "Tên nhân viên";
            dataGridView1.Columns[2].HeaderText = "SDT nhân viên";
            dataGridView1.Columns[0].Width = 200;
            dataGridView1.Columns[1].Width = 200;
            dataGridView1.Columns[2].Width = 200;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;

        }
        private void bntThem_Click(object sender, EventArgs e)
        {
           // txtMaNV.Text = "";
            txtTenNV.Text = "";
            txtSDTNV.Text = "";
            txtMaNV.Text = ThucThiSql.CreateKey("NV");
            // txtMaNV.Enabled = true;
        }

        private void bntLuu_Click(object sender, EventArgs e)
        {
            string sql = @"INSERT INTO tblNhanVien (MaNV, TenNV, SDTNV) VALUES('" + txtMaNV.Text + "','" + txtTenNV.Text + "', '" + txtSDTNV.Text + "')";
            ThucThiSql.CapNhatDuLieu(sql);
            Hienthi_Luoi();
        }

        private void bntSua_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow.Cells["MaNV"].Value.ToString() == "")
            {
                MessageBox.Show("Không có dữ liệu");
                return;
            }
            string sql = @"UPDATE tblNhanVien SET MaNV='" + txtMaNV.Text + "', TenNV='" + txtTenNV.Text + "', SDTNV='" + txtSDTNV.Text + "'" +
               "WHERE (MaNV ='" + txtMaNV.Text + "')";
            ThucThiSql.CapNhatDuLieu(sql);
            Hienthi_Luoi();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaNV.Text = dataGridView1.CurrentRow.Cells["MaNV"].Value.ToString();
            txtTenNV.Text = dataGridView1.CurrentRow.Cells["TenNV"].Value.ToString();
            txtSDTNV.Text = dataGridView1.CurrentRow.Cells["SDTNV"].Value.ToString();
        }

        private void bntXoa_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount == 0)
            {
                MessageBox.Show("Bạn không có dữ liệu để xóa");
                return;
            }

            if (dataGridView1.CurrentRow.Cells["MaNV"].Value.ToString() == "")
            {
                MessageBox.Show("Không có dữ liệu");
                return;
            }
            string sql;
            string ma = dataGridView1.CurrentRow.Cells["MaNV"].Value.ToString();
            if (MessageBox.Show("Bạn có muốn xóa không", "Thông Báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                sql = "DELETE FROM tblNhanVien WHERE MaNV = N'" +
                    ma + "'";

                ThucThiSql.CapNhatDuLieu(sql);
                Hienthi_Luoi();
            }

        }

        private void bntDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmNV_Activated_1(object sender, EventArgs e)
        {
            Hienthi_Luoi();
        }

        private void txtSDTNV_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Chỉ được nhập số, không được nhập chữ
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txtMaNV_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("Bạn không thể sửa Mã nhân viên!!!");
            txtTenNV.Focus();
        }
    }
}

