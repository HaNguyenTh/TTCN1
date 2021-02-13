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
    public partial class FrmTKPNH : Form
    {
        public FrmTKPNH()
        {
            InitializeComponent();
        }

        private void FrmTKPNH_Load(object sender, EventArgs e)
        {
            Hienthi_Luoi();
        }

        private void Hienthi_Luoi()
        {
            string sql;
            DataTable tblPhieuNH;
            sql = "SELECT a.MaPNH, a.MaNV, a.MaNCC, b.MaSP, a.NgayNhap, b.SoLuong, b.DonGiaN, a.TongTien FROM tblPhieuNhapHang AS a, tblChiTietPNH AS b WHERE a.MaPNH = b.MaPNH";
            tblPhieuNH = ThucThiSql.DocBang(sql);
            dataGridView1.DataSource = tblPhieuNH;
            dataGridView1.Columns[0].HeaderText = "Mã hóa đơn";
            dataGridView1.Columns[1].HeaderText = "Mã nhân viên";
            dataGridView1.Columns[2].HeaderText = "Mã nhà cung cấp";
            dataGridView1.Columns[3].HeaderText = "Mã sản phẩm";
            dataGridView1.Columns[4].HeaderText = "Ngày nhập";
            dataGridView1.Columns[5].HeaderText = "Số lượng";
            dataGridView1.Columns[6].HeaderText = "Đơn giá nhập";
            dataGridView1.Columns[7].HeaderText = "Tổng Tiền";
            dataGridView1.Columns[0].Width = 150;
            dataGridView1.Columns[1].Width = 150;
            dataGridView1.Columns[2].Width = 150;
            dataGridView1.Columns[3].Width = 150;
            dataGridView1.Columns[4].Width = 150;
            dataGridView1.Columns[5].Width = 150;
            dataGridView1.Columns[6].Width = 150;
            dataGridView1.Columns[7].Width = 150;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable TKPNH;
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
                    sql = "SELECT a.MaPNH, a.MaNV, a.MaNCC, b.MaSP, a.NgayNhap, b.SoLuong, b.DonGiaN, a.TongTien FROM tblPhieuNhapHang AS a, tblChiTietPNH AS b WHERE a.MaPNH = b.MaPNH AND a.MaPNH like N'%" + txtNhap.Text + "%'";
                    TKPNH = ThucThiSql.DocBang(sql);
                    if (TKPNH.Rows.Count == 0)
                    {
                        MessageBox.Show("Phiếu nhập hàng không tồn tại!");
                    }
                    else
                    {
                        dataGridView1.DataSource = TKPNH;
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
                    sql = "SELECT a.MaPNH, a.MaNV, a.MaNCC, b.MaSP, a.NgayNhap, b.SoLuong, b.DonGiaN, a.TongTien FROM tblPhieuNhapHang AS a, tblChiTietPNH AS b WHERE a.MaPNH = b.MaPNH AND a.MaNCC like N'%" + txtNhap.Text + "%'";
                    TKPNH = ThucThiSql.DocBang(sql);
                    if (TKPNH.Rows.Count == 0)
                    {
                        MessageBox.Show("Phiếu nhập hàng không tồn tại!");
                    }
                    else
                    {
                        dataGridView1.DataSource = TKPNH;
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
                    sql = "SELECT a.MaPNH, a.MaNV, a.MaNCC, b.MaSP, a.NgayNhap, b.SoLuong, b.DonGiaN, a.TongTien FROM tblPhieuNhapHang AS a, tblChiTietPNH AS b WHERE a.MaPNH = b.MaPNH AND a.MaNV like N'%" + txtNhap.Text + "%'";
                    TKPNH = ThucThiSql.DocBang(sql);
                    if (TKPNH.Rows.Count == 0)
                    {
                        MessageBox.Show("Phiếu nhập hàng không tồn tại!");
                    }
                    else
                    {
                        dataGridView1.DataSource = TKPNH;
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
