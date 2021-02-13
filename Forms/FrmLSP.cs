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
    public partial class FrmLSP : Form
    {
        public FrmLSP()
        {
            InitializeComponent();
        }

        private void FrmLSP_Load(object sender, EventArgs e)
        {
            Hienthi_Luoi();
        }

        private void Hienthi_Luoi()
        {
            string sql;
            DataTable tblLSP;
            sql = "SELECT MaLSP, TenLSP FROM tblLoaiSanPham";
            tblLSP = ThucThiSql.DocBang(sql);
            dataGridView1.DataSource = tblLSP;

        }

        private void FrmLSP_Activated(object sender, EventArgs e)
        {
            Hienthi_Luoi();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaLSP.Text = dataGridView1.CurrentRow.Cells["MaLSP"].Value.ToString();
            txtTenLSP.Text = dataGridView1.CurrentRow.Cells["TenLSP"].Value.ToString();
        }

        private void bntThem_Click(object sender, EventArgs e)
        {
            txtMaLSP.Text = ThucThiSql.CreateKey("LSP");
            txtTenLSP.Text = "";
        }

        private void bntLuu_Click(object sender, EventArgs e)
        {
            string sql = @"INSERT INTO tblLoaiSanPham (MaLSP, TenLSP) VALUES('" + txtMaLSP.Text + "','" + txtTenLSP.Text + "')";
            ThucThiSql.CapNhatDuLieu(sql);
            Hienthi_Luoi();
        }

        private void bntSua_Click(object sender, EventArgs e)
        {
           
            string sql = @"UPDATE tblLoaiSanPham SET MaLSP='" + txtMaLSP.Text + "', TenLSP='" + txtTenLSP.Text  + "'" +
               "WHERE (MaLSP ='" + txtMaLSP.Text + "')";
            ThucThiSql.CapNhatDuLieu(sql);
            Hienthi_Luoi();
        }

        private void bntXoa_Click(object sender, EventArgs e)
        {
            string sql;
            string ma = dataGridView1.CurrentRow.Cells["MaLSP"].Value.ToString();
            if (MessageBox.Show("Bạn có muốn xóa không", "Thông Báo", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                sql = "DELETE FROM tblLoaiSanPham WHERE MaLSP = N'" +
                    ma + "'";

                ThucThiSql.CapNhatDuLieu(sql);
                Hienthi_Luoi();
            }
        }

        private void bntDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtMaLSP_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("Bạn không thể sửa Mã Loại sản phẩm!!!");
            txtTenLSP.Focus();
        }
    }
}