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
    public partial class FrmSP : Form
    {
        public FrmSP()
        {
            InitializeComponent();
        }

        private void FrmSP_Load(object sender, EventArgs e)
        {
            Hienthi_Luoi();
            cboSize.Items.Add("S");
            cboSize.Items.Add("M");
            cboSize.Items.Add("L");
            cboSize.Items.Add("FreeSize");
            var item1 = this.cboSize.GetItemText(this.cboSize.SelectedItem);

        }

        private void Hienthi_Luoi()
        {
            string sql;
            DataTable tblSP;
            sql = "SELECT MaSP, TenSP, SoLuongSP, DongiaN, MaLSP, DongiaB, Size, Color FROM tblSanPham";
            tblSP = ThucThiSql.DocBang(sql);
            dataGridView1.DataSource = tblSP;

        }

        private void FrmSP_Activated(object sender, EventArgs e)
        {
            Hienthi_Luoi();
        }

        private void bntThem_Click(object sender, EventArgs e)
        {
            txtMaSP.Text = ThucThiSql.CreateKey("SP");
            // txtMaSP.Text = "";
            txtTenSP.Text = "";
            txtSoLuong.Text = "";
            txtDonGiaN.Text = "";
            cboMaLSP.Text = "";
            cboSize.Text = "";
            txtColor.Text = "";
            txtDonGiaB.Text = "";
            txtTenLSP.Text = "";
        }

        private void cboMaLSP_DropDown(object sender, EventArgs e)
        {
            cboMaLSP.DataSource = ThucThiSql.DocBang("SELECT MaLSP FROM tblLoaisanpham");
            cboMaLSP.ValueMember = "MaLSP";
            cboMaLSP.SelectedIndex = -1;

        }

        private void cboMaLSP_TextChanged(object sender, EventArgs e)
        {
            string sql;
            if (cboMaLSP.Text == "")
            {
                txtTenLSP.Text = "";
                return;
            }
            sql = "SELECT TenLSP FROM tblLoaisanpham WHERE MaLSP =N'" + cboMaLSP.Text + "'";
            DataTable table = ThucThiSql.DocBang(sql);
            if (table.Rows.Count > 0)
            {
                txtTenLSP.Text = table.Rows[0][0].ToString();
            }

        }

        private void bntLuu_Click(object sender, EventArgs e)
        {
            string sql = @"INSERT INTO tblSanPham (MaSP, TenSP, SoLuongSP, DongiaN, MaLSP, DonGiaB, Size, Color) VALUES('" + txtMaSP.Text + "','" + txtTenSP.Text + "','" + txtSoLuong.Text + "','" + txtDonGiaN.Text + "','" + cboMaLSP.Text + "','" + txtDonGiaB.Text + "','" + cboSize.Text + "','" + txtColor.Text + "')";
            ThucThiSql.CapNhatDuLieu(sql);
            Hienthi_Luoi();
        }

        private void bntSua_Click(object sender, EventArgs e)
        {
            string sql = @"UPDATE tblSanPham SET MaSP='" + txtMaSP.Text + "', TenSP='" + txtTenSP.Text + "', SoLuongSP='" + txtSoLuong.Text + "', DonGiaN='" + txtDonGiaN.Text + "',MaLSP='" + cboMaLSP.Text + "',DonGiaB='" + txtDonGiaB.Text + "',Size='" + cboSize.Text + "',Color='" + txtColor.Text  +"'" +
               "WHERE (MaSP ='" + txtMaSP.Text + "')";
            ThucThiSql.CapNhatDuLieu(sql);
            Hienthi_Luoi();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaSP.Text = dataGridView1.CurrentRow.Cells["MaSP"].Value.ToString();
            txtTenSP.Text = dataGridView1.CurrentRow.Cells["Ten"].Value.ToString();
            txtSoLuong.Text = dataGridView1.CurrentRow.Cells["SoLuongSP"].Value.ToString();
            txtDonGiaN.Text = dataGridView1.CurrentRow.Cells["DonGiaN"].Value.ToString();
            txtDonGiaB.Text = dataGridView1.CurrentRow.Cells["GiaB"].Value.ToString();
            cboMaLSP.Text = dataGridView1.CurrentRow.Cells["MaLSP"].Value.ToString();
            txtColor.Text = dataGridView1.CurrentRow.Cells["Color"].Value.ToString();
            cboSize.Text = dataGridView1.CurrentRow.Cells["Size"].Value.ToString();
        }

        private void bntXoa_Click(object sender, EventArgs e)
        {
            string sql;
            string ma = dataGridView1.CurrentRow.Cells["MaSP"].Value.ToString();
            if (MessageBox.Show("Bạn có muốn xóa không", "Thông Báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                sql = "DELETE FROM tblSanPham WHERE MaSP = N'" +
                    ma + "'";

                ThucThiSql.CapNhatDuLieu(sql);
                Hienthi_Luoi();
            }
        }

        private void bntDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtMaSP_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("Bạn không thể sửa Mã sản phẩm!!!");
            txtTenSP.Focus();
        }
    }
}