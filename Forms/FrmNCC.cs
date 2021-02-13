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
    public partial class FrmNCC : Form
    {
        public FrmNCC()
        {
            InitializeComponent();
        }

        private void FrmNCC_Load(object sender, EventArgs e)
        {
            Hienthi_Luoi();
        }

        private void Hienthi_Luoi()
        {
            string sql;
            DataTable tblNCC;
            sql = "SELECT MaNCC, TenNCC, SDTNCC, DiaChi FROM tblNhaCungCap";
            tblNCC = ThucThiSql.DocBang(sql);
            dataGridView1.DataSource = tblNCC;
            dataGridView1.Columns[0].HeaderText = "Mã nhà cung cấp";
            dataGridView1.Columns[1].HeaderText = "Tên nhà cung cấp";
            dataGridView1.Columns[2].HeaderText = "SDT nhà cung cấp";
            dataGridView1.Columns[3].HeaderText = "Địa chỉ";
            dataGridView1.Columns[0].Width = 200;
            dataGridView1.Columns[1].Width = 200;
            dataGridView1.Columns[2].Width = 200;
            dataGridView1.Columns[3].Width = 200;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;

        }

        private void bntThem_Click(object sender, EventArgs e)
        {
            txtMaNCC.Text = ThucThiSql.CreateKey("NCC");
            txtTenNCC.Text = "";
            txtSDTNCC.Text = "";
            txtDiaChi.Text = "";
            //txtMaNCC.Enabled = true;
        }

        private void bntLuu_Click(object sender, EventArgs e)
        {
            string sql = @"INSERT INTO tblNhaCungCap (MaNCC, TenNCC, SDTNCC, DiaChi) VALUES('" + txtMaNCC.Text + "','" + txtTenNCC.Text + "', '" + txtSDTNCC.Text + "', '" + txtDiaChi.Text + "')";
            ThucThiSql.CapNhatDuLieu(sql);
            Hienthi_Luoi();
        }

        private void bntSua_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow.Cells["MaNCC"].Value.ToString() == "")
            {
                MessageBox.Show("Không có dữ liệu");
                return;
            }
            string sql = @"UPDATE tblNhaCungCap SET MaNCC='" + txtMaNCC.Text + "', TenNCC='" + txtTenNCC.Text + "', SDTNCC='" + txtSDTNCC.Text + "', DiaChi='" + txtDiaChi.Text + "'" +
               "WHERE (MaNCC ='" + txtMaNCC.Text + "')";
            ThucThiSql.CapNhatDuLieu(sql);
            Hienthi_Luoi();
        }

        private void bntXoa_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount == 0)
            {
                MessageBox.Show("Bạn không có dữ liệu để xóa");
                return;
            }

            if (dataGridView1.CurrentRow.Cells["MaNCC"].Value.ToString() == "")
            {
                MessageBox.Show("Không có dữ liệu");
                return;
            }
            string sql;
            string ma = dataGridView1.CurrentRow.Cells["MaNCC"].Value.ToString();
            if (MessageBox.Show("Bạn có muốn xóa không", "Thông Báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                sql = "DELETE FROM tblNhaCungCap WHERE MaNCC = N'" +
                    ma + "'";

                ThucThiSql.CapNhatDuLieu(sql);
                Hienthi_Luoi();
            }

        }

        private void bntDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaNCC.Text = dataGridView1.CurrentRow.Cells["MaNCC"].Value.ToString();
            txtTenNCC.Text = dataGridView1.CurrentRow.Cells["Ten"].Value.ToString();
            txtSDTNCC.Text = dataGridView1.CurrentRow.Cells["SDT"].Value.ToString();
            txtDiaChi.Text = dataGridView1.CurrentRow.Cells["DiaChi"].Value.ToString();
        }

        private void FrmNCC_Activated(object sender, EventArgs e)
        {
            Hienthi_Luoi();
        }

        private void txtSDTNCC_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Không thể nhập chữ vào textbook này
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txtMaNCC_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Bạn không được sửa Mã nhà cung cấp!!!");
            txtTenNCC.Focus();
        }
    }
}
