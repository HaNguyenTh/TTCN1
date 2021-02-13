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
    public partial class FrmKH : Form
    {
        public FrmKH()
        {
            InitializeComponent();
        }

        private void FrmKH_Load(object sender, EventArgs e)
        {
            Hienthi_Luoi();
        }

        private void Hienthi_Luoi()
        {
            string sql;
            DataTable tblKH;
            sql = "SELECT MaKH, TenKH, SDTKH, DiaChi FROM tblKhachHang";
            tblKH = ThucThiSql.DocBang(sql);
            dataGridView1.DataSource = tblKH;
            //dataGridView1.Columns[0].HeaderText = "Mã khách hàng";
            //dataGridView1.Columns[1].HeaderText = "Tên khách hàng";
            //dataGridView1.Columns[2].HeaderText = "SDT khách hàng";
            //dataGridView1.Columns[3].HeaderText = "Địa chỉ";
            //dataGridView1.Columns[0].Width = 200;
            //dataGridView1.Columns[1].Width = 200;
            //dataGridView1.Columns[2].Width = 200;
            //dataGridView1.Columns[3].Width = 200;
            //dataGridView1.AllowUserToAddRows = false;
            //dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;

        }

        private void bntThen_Click(object sender, EventArgs e)
        {
            //txtMaKH.Text = "";
            txtTenKH.Text = "";
            txtSDTKH.Text = "";
            txtDiaChi.Text = "";
            //txtMaKH.Enabled = true;
            txtMaKH.Text = ThucThiSql.CreateKey("KH");
        }

        private void bntLuu_Click(object sender, EventArgs e)
        {
            string sql = @"INSERT INTO tblKhachHang (MaKH, TenKH, SDTKH, DiaChi) VALUES('" + txtMaKH.Text + "','" + txtTenKH.Text + "', '" + txtSDTKH.Text + "', '" + txtDiaChi.Text + "')";
            ThucThiSql.CapNhatDuLieu(sql);
            Hienthi_Luoi();
        }

        private void bntXoa_Click(object sender, EventArgs e)
        {
            string sql;
            string ma = dataGridView1.CurrentRow.Cells["MaKH"].Value.ToString();
            if (MessageBox.Show("Bạn có muốn xóa không", "Thông Báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                sql = "DELETE FROM tblKhachHang WHERE MaKH = N'" +
                    ma + "'";

                ThucThiSql.CapNhatDuLieu(sql);
                Hienthi_Luoi();
            }
        }

        private void bntDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bntSua_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow.Cells["MaKH"].Value.ToString() == "")
            {
                MessageBox.Show("Không có dữ liệu");
                return;
            }
            string sql = @"UPDATE tblKhachHang SET MaKH='" + txtMaKH.Text + "', TenKH='" + txtTenKH.Text + "', SDTKH='" + txtSDTKH.Text + "', DiaChi='" + txtDiaChi.Text + "'" +
               "WHERE (MaKH ='" + txtMaKH.Text + "')";
            ThucThiSql.CapNhatDuLieu(sql);
            Hienthi_Luoi();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaKH.Text = dataGridView1.CurrentRow.Cells["MaKH"].Value.ToString();
            txtTenKH.Text = dataGridView1.CurrentRow.Cells["TenKH"].Value.ToString();
            txtSDTKH.Text = dataGridView1.CurrentRow.Cells["SDTKH"].Value.ToString();
            txtDiaChi.Text = dataGridView1.CurrentRow.Cells["DiaChi"].Value.ToString();
        }

        private void txtSDTKH_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Không thể nhập chữ vào textbook này
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txtMaKH_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Bạn không được sửa Mã khách hàng!!!");
            txtTenKH.Focus();
        }
    }
}
