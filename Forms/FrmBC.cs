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
    public partial class FrmBC : Form
    {
        public FrmBC()
        {
            InitializeComponent();
        }

        private void FrmBC_Load(object sender, EventArgs e)
        {
            txtTongChi.Enabled = false;
            txtTongThu.Enabled = false;
            txtTongLoiNhuan.Enabled = false;
            button2.Enabled = true;
            dateTimePicker1.Enabled = true;
            dateTimePicker3.Enabled = true;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sql, tn, dn;
            double tc, tt, ttn;
            tn = dateTimePicker3.Value.ToString("MM/dd/yyyy");
            dn = dateTimePicker1.Value.ToString("MM/dd/yyyy");
            if (dateTimePicker1.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập ngày ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dateTimePicker1.Focus();
                return;
            }
            if (dateTimePicker3.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập ngày ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dateTimePicker3.Focus();
                return;
            }
            DataTable tblPNH;
            sql = "Select MaPNH, MaNCC, MaNV, NgayNhap, TongTien From tblPhieuNhapHang Where NgayNhap >= '" + tn + "' and NgayNhap <= '" + dn + "' ";
            tblPNH = ThucThiSql.DocBang(sql);
            dataGridView1.DataSource = tblPNH;
            Hienthi_Luoi();


            DataTable tblHDB;
            sql = "Select MaHD, MaKH, MaNV, NgayLapHD, TongTien from tblHoaDonBan Where NgayLapHD >= '" + tn + "' and NgayLapHD <= '" + dn + "' ";
            tblHDB = ThucThiSql.DocBang(sql);
            dataGridView2.DataSource = tblHDB;
            Hienthi_Luoi1();

            txtTongChi.Text = ThucThiSql.GetFieldValues("Select sum(TongTien) From tblPhieuNhapHang Where NgayNhap >= '" + tn + "' and NgayNhap <= '" + dn + "' ");
            txtTongThu.Text = ThucThiSql.GetFieldValues("Select sum(TongTien) From tblHoaDonBan Where NgayLapHD >= '" + tn + "' and NgayLapHD <= '" + dn + "' ");
            if (txtTongChi.Text == "")
                txtTongChi.Text = "0";
            if (txtTongThu.Text == "")
                txtTongThu.Text = "0";
            tc = Convert.ToDouble(txtTongChi.Text);
            tt = Convert.ToDouble(txtTongThu.Text);
            ttn = tt - tc;
            txtTongLoiNhuan.Text = Convert.ToString(ttn);
        }
        private void Hienthi_Luoi()
        {
            //string sql;
            //DataTable tblPNH;
            //sql = "SELECT * FROM tblPhieuNhaphang";
            //tblPNH = ThucThiSql.DocBang(sql);
            //dataGridView1.DataSource = tblPNH;
            dataGridView1.Columns[0].HeaderText = "Mã phiếu nhập";
            dataGridView1.Columns[1].HeaderText = "Mã nhà cung cấp";
            dataGridView1.Columns[2].HeaderText = "Mã nhân viên";
            dataGridView1.Columns[3].HeaderText = "Ngày nhập";
            dataGridView1.Columns[4].HeaderText = "Tổng tiền";
            dataGridView1.Columns[0].Width = 200;
            dataGridView1.Columns[1].Width = 110;
            dataGridView1.Columns[2].Width = 90;
            dataGridView1.Columns[3].Width = 90;
            dataGridView1.Columns[4].Width = 90;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;

        }

        private void Hienthi_Luoi1()
        {
            //string sql;
            //DataTable tblHDB;
            //sql = "SELECT * FROM tblHoaDonBan";
            //tblHDB = ThucThiSql.DocBang(sql);
            //dataGridView2.DataSource = tblHDB;
            dataGridView2.Columns[0].HeaderText = "Mã hóa đơn";
            dataGridView2.Columns[1].HeaderText = "Mã khách hàng";
            dataGridView2.Columns[2].HeaderText = "Mã nhân viên";
            dataGridView2.Columns[3].HeaderText = "Ngày lập";
            dataGridView2.Columns[4].HeaderText = "Tổng tiền";
            dataGridView2.Columns[0].Width = 200;
            dataGridView2.Columns[1].Width = 110;
            dataGridView2.Columns[2].Width = 90;
            dataGridView2.Columns[3].Width = 90;
            dataGridView2.Columns[4].Width = 90;
            dataGridView2.AllowUserToAddRows = false;
            dataGridView2.EditMode = DataGridViewEditMode.EditProgrammatically;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

}
