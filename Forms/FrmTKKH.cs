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
    public partial class FrmTKKH : Form
    {
        public FrmTKKH()
        {
            InitializeComponent();
        }

        private void FrmTKKH_Load(object sender, EventArgs e)
        {
            Hienthi_Luoi();
        }
        private void Hienthi_Luoi()
        {
            string sql;
            DataTable tblKH;
            sql = "SELECT * FROM tblKhachHang";
            tblKH = ThucThiSql.DocBang(sql);
            dataGridView1.DataSource = tblKH;

        }

        private void bntTimKiem_Click(object sender, EventArgs e)
        {
            DataTable TKKH;
            string sql;
            if ((txtMaKH.Text == "") && (txtTenKH.Text == "") && (txtSDTKH.Text == ""))
            {
                MessageBox.Show("Hãy nhập một điều kiện để tìm kiếm!");
                return;
            }
            sql = "SELECT * FROM tblKhachHang WHERE 1=1";
            if (txtMaKH.Text != "")
                sql = sql + " AND MaKH like N'%" + txtMaKH.Text + "%'";
            if (txtTenKH.Text != "")
                sql = sql + " AND TenKH like N'%" + txtTenKH.Text + "%'";
            if (txtSDTKH.Text != "")
                sql = sql + " AND SDTKH like N'%" + txtSDTKH.Text + "%'";
            TKKH = ThucThiSql.DocBang(sql);
            if (TKKH.Rows.Count == 0)
            {
                MessageBox.Show("Khách hàng này chưa có trên hệ thống!");
            }
            else
            {
                dataGridView1.DataSource = TKKH;
            }
        }

        private void bntDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
