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
    public partial class FrmTKHDBH : Form
    {
        public FrmTKHDBH()
        {
            InitializeComponent();
        }

        private void FrmTKHDBH_Load(object sender, EventArgs e)
        {
            Hienthi_Luoi();
        }

        private void Hienthi_Luoi()
        {
            string sql;
            DataTable tblHDBH;
            sql = "SELECT a.MaHD, a.MaNV, a.MaKH, b.MaSP, a.NgayLapHD, b.SoLuong, b.DonGiaB, b.Giamgia, a.TongTien FROM tblHoaDonBan AS a, tblChiTietHDB AS b WHERE a.MaHD = b.MaHD";
            tblHDBH = ThucThiSql.DocBang(sql);
            dataGridView1.DataSource = tblHDBH;
            dataGridView1.Columns[0].HeaderText = "Mã hóa đơn";
            dataGridView1.Columns[1].HeaderText = "Mã nhân viên";
            dataGridView1.Columns[2].HeaderText = "Mã khách hàng";
            dataGridView1.Columns[3].HeaderText = "Mã sản phẩm";
            dataGridView1.Columns[4].HeaderText = "Ngày lập";
            dataGridView1.Columns[5].HeaderText = "Số lượng";
            dataGridView1.Columns[6].HeaderText = "Đơn giá bán";
            dataGridView1.Columns[7].HeaderText = "Giảm giá %";
            dataGridView1.Columns[8].HeaderText = "Tổng Tiền";
            dataGridView1.Columns[0].Width = 150;
            dataGridView1.Columns[1].Width = 150;
            dataGridView1.Columns[2].Width = 150;
            dataGridView1.Columns[3].Width = 150;
            dataGridView1.Columns[4].Width = 150;
            dataGridView1.Columns[5].Width = 150;
            dataGridView1.Columns[6].Width = 150;
            dataGridView1.Columns[7].Width = 150;
            dataGridView1.Columns[8].Width = 150;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;

        }

        private void bntTimKiem_Click(object sender, EventArgs e)
        {
            DataTable TKHDBH;
            string sql;
            if (radioButton1.Checked == true)
            {
                if (txtNhap.Text == "")
                {
                    MessageBox.Show("Hãy nhập một điều kiện để tìm kiếm!");
                    return;
                }
                else
                {
                    sql = "SELECT a.MaHD, a.MaNV, a.MaKH, b.MaSP, a.NgayLapHD, b.SoLuong, b.DonGiaB, b.Giamgia, a.TongTien FROM tblHoaDonBan AS a, tblChiTietHDB AS b WHERE a.MaHD = b.MaHD AND a.MaHD like N'%" + txtNhap.Text + "%'";
                    TKHDBH = ThucThiSql.DocBang(sql);
                    if (TKHDBH.Rows.Count == 0)
                    {
                        MessageBox.Show("Hóa đơn không tồn tại!");
                    }
                    else
                    {
                        dataGridView1.DataSource = TKHDBH;
                    }
                }
            }
            if (radioButton2.Checked == true)
            {
                if (txtNhap.Text == "")
                {
                    MessageBox.Show("Hãy nhập một điều kiện để tìm kiếm!");
                    return;
                }
                else
                {
                    sql = "SELECT a.MaHD, a.MaNV, a.MaKH, b.MaSP, a.NgayLapHD, b.SoLuong, b.DonGiaB, b.Giamgia, a.TongTien FROM tblHoaDonBan AS a, tblChiTietHDB AS b WHERE a.MaHD = b.MaHD AND a.MaKH like N'%" + txtNhap.Text + "%'";
                    TKHDBH = ThucThiSql.DocBang(sql);
                    if (TKHDBH.Rows.Count == 0)
                    {
                        MessageBox.Show("Hóa đơn không tồn tại!");
                    }
                    else
                    {
                        dataGridView1.DataSource = TKHDBH;
                    }
                }
            }

            if (radioButton3.Checked == true)
            {
                if (txtNhap.Text == "")
                {
                    MessageBox.Show("Hãy nhập một điều kiện để tìm kiếm!");
                    return;
                }
                else
                {
                    sql = "SELECT a.MaHD, a.MaNV, a.MaKH, b.MaSP, a.NgayLapHD, b.SoLuong, b.DonGiaB, b.Giamgia, a.TongTien FROM tblHoaDonBan AS a, tblChiTietHDB AS b WHERE a.MaHD = b.MaHD AND a.MaNV like N'%" + txtNhap.Text + "%''";
                    TKHDBH = ThucThiSql.DocBang(sql);
                    if (TKHDBH.Rows.Count == 0)
                    {
                        MessageBox.Show("Hóa đơn không tồn tại!");
                    }
                    else
                    {
                        dataGridView1.DataSource = TKHDBH;
                    }
                }
            }
        }

        private void Đóng_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
