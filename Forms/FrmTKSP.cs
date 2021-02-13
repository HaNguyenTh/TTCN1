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
    public partial class FrmTKSP : Form
    {
        public FrmTKSP()
        {
            InitializeComponent();
        }

        private void FrmTKSP_Load(object sender, EventArgs e)
        {
            Hienthi_Luoi();
        }

        private void Hienthi_Luoi()
        {
            string sql;
            DataTable tblSP;
            sql = "SELECT MaSP, TenSP, SoluongSP, DongiaN, MaLSP, DongiaB, Size, Color FROM tblSanpham";
            tblSP = ThucThiSql.DocBang(sql);
            dataGridView1.DataSource = tblSP;

        }

        private void bntTimKiem_Click(object sender, EventArgs e)
        {
            DataTable TKSP;
            string sql;
            if ((txtMaSP.Text == "") && txtTenSP.Text == "")
            {
                MessageBox.Show("Hãy nhập một điều kiện để tìm kiếm!");
                return;
            }
            sql = "SELECT * FROM tblSanPham WHERE 1=1";
            if (txtMaSP.Text != "")
                sql = sql + " AND MaSP like N'%" + txtMaSP.Text + "%'";
            if (txtTenSP.Text != "")
                sql = sql + " AND TenSP like N'%" + txtTenSP.Text + "%'";
            TKSP = ThucThiSql.DocBang(sql);

            if( TKSP.Rows.Count == 0)
            {
                MessageBox.Show("Sản phẩm này chưa có trong danh mục!");
            }    
            else
            {
                dataGridView1.DataSource = TKSP;
            }    
        }

        private void bntDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
