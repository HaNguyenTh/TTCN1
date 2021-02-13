using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace TTCN1_QuanLyBanHangMayStore.Forms
{
    public partial class FrmHDBH : Form
    {
        public FrmHDBH()
        {
            InitializeComponent();
        }

        private void FrmHDBH_Load(object sender, EventArgs e)
        {
            bntThem.Enabled = true;
            bntLuu.Enabled = false;
            bntHuy.Enabled = false;
            txtMaHD.ReadOnly = true;
            txtTenNV.ReadOnly = true;
            txtTenKH.ReadOnly = true;
            txtDiaChi.ReadOnly = true;
            txtSDT.ReadOnly = true;
            txtMaSP.ReadOnly = true;
            txtThanhTien.ReadOnly = true;
            txtTongTien.ReadOnly = true;
            txtGiamGia.Text = "0";
            txtTongTien.Text = "0";
        }

        private void cboMaNV_DropDown(object sender, EventArgs e)
        {
            //Lấy mã nhân viên từ bảng nhân nhiên cho combobox MaNV
            cboMaNV.DataSource = ThucThiSql.DocBang("SELECT MaNV FROM tblNhanVien");
            cboMaNV.ValueMember = "MaNV";
            cboMaNV.SelectedIndex = -1;

        }

        private void cboMaKH_DropDown(object sender, EventArgs e)
        {
            //Lấy mã nhân viên từ bảng khách hàng cho combobox MaKH
            cboMaKH.DataSource = ThucThiSql.DocBang("SELECT MaKH FROM tblKhachHang");
            cboMaKH.ValueMember = "MaKH";
            cboMaKH.SelectedIndex = -1;

        }
        private void cboMaNV_TextChanged(object sender, EventArgs e)
        {
            // khi kích hoạt mã nv thì tên nhân viên tự động hiện ra
            string sql;
            if (cboMaNV.Text == "")
            {
                txtTenNV.Text = "";
                return;
            }
            sql = "SELECT TenNV FROM tblNhanVien WHERE MaNV =N'" + cboMaNV.Text + "'";
            DataTable table = ThucThiSql.DocBang(sql);
            if (table.Rows.Count > 0)
            {
                txtTenNV.Text = table.Rows[0][0].ToString();
            }

        }

        private void cboMaKH_TextChanged(object sender, EventArgs e)
        {
            string sql;
            if (cboMaKH.Text == "")
            {
                txtTenKH.Text = "";
                txtSDT.Text = "";
                txtDiaChi.Text = "";
                return;
            }
            sql = "SELECT TenKH,SDTKH,DiaChi FROM tblKhachHang WHERE MaKH =N'" + cboMaKH.Text + "'";
            DataTable table = ThucThiSql.DocBang(sql);
            if (table.Rows.Count > 0)
            {
                txtTenKH.Text = table.Rows[0][0].ToString();
                txtSDT.Text = table.Rows[0][1].ToString();
                txtDiaChi.Text = table.Rows[0][2].ToString();
            }

        }

        private void ResetValues()
        {
            txtMaHD.Text = "";
            dateTimePicker1.Text = DateTime.Now.ToShortDateString();
            cboMaKH.Text = "";
            txtTongTien.Text = "0";
            cboTenSP.Text = "";
            txtSoLuong.Text = "";
            txtGiaBan.Text = "";
            txtGiamGia.Text = "0";
            txtThanhTien.Text = "0";
        }

        private void bntLuu_Click(object sender, EventArgs e)
        {
            string sql;
            if (dateTimePicker1.Text.Length == 0)
            {
                MessageBox.Show("Bạn phải nhập ngày bán", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dateTimePicker1.Focus();
                return;
            }
            if (cboMaNV.Text.Length == 0)
            {
                MessageBox.Show("Bạn phải nhập nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboMaNV.Focus();
                return;
            }
            if (cboMaKH.Text.Length == 0)
            {
                MessageBox.Show("Bạn phải nhập khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboMaKH.Focus();
                return;
            }
            sql = "INSERT INTO tblHoaDonBan(MaHD, NgayLapHD,MaNV, MaKH, TongTien) VALUES(N'" +
                txtMaHD.Text.Trim() + "',N'" + dateTimePicker1.Value.ToShortDateString() + "',N'" +
                cboMaNV.Text + "',N'" + cboMaKH.Text + "'," + txtTongTien.Text + ")";
            ThucThiSql.CapNhatDuLieu(sql);

            //Lưu thông tin các sản phẩm

            if (cboTenSP.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã sản phẩm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboTenSP.Focus();
                return;
            }
            if ((txtSoLuong.Text.Trim().Length == 0) || (txtSoLuong.Text == "0"))
            {
                MessageBox.Show("Bạn phải nhập số lượng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoLuong.Text = "";
                txtSoLuong.Focus();
                return;
            }
            if (txtGiamGia.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập giảm giá", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGiamGia.Text = "";
                txtGiamGia.Focus();
                return;
            }

            sql = "SELECT MaSP FROM tblChiTietHDB WHERE MaSP=N'" + cboTenSP.Text +
                "' AND MaHD = N'" + txtMaHD.Text.Trim() + "'";
            if (ThucThiSql.DocBang(sql).Rows.Count > 0)
            {
                MessageBox.Show("Mã sản phẩm này đã có, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ResetValuesSP();
                cboTenSP.Focus();
                return;
            }

            // Kiểm tra xem số lượng hàng trong kho còn đủ để cung cấp không?
            double sl = Convert.ToDouble(ThucThiSql.GetFieldValues("SELECT SoLuongSP FROM tblSanPham WHERE TenSP = N'" + cboTenSP.SelectedValue + "'"));
            if (Convert.ToDouble(txtSoLuong.Text) > sl)
            {
                MessageBox.Show("Số lượng mặt hàng này chỉ còn " + sl, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSoLuong.Text = "";
                txtSoLuong.Focus();
                return;
            }

            sql = "INSERT INTO tblChiTietHDB(MaHD, MaSP, SoLuong, DonGiaB, GiamGia, ThanhTien) VALUES(N'" +
                txtMaHD.Text.Trim() + "',N'" + txtMaSP.Text.ToString() + "'," + txtSoLuong.Text +
                "," + txtGiaBan.Text + "," + txtGiamGia.Text + "," + txtThanhTien.Text + ")";
            ThucThiSql.CapNhatDuLieu(sql);
            Hienthi_Luoi();


            //cập nhật số lượng mới vào bảng sản phẩm
            sl = Convert.ToDouble(ThucThiSql.DocBang("SELECT SoLuongSP FROM tblSanPham WHERE MaSP = N'" +
               txtMaSP.Text + "'").Rows[0][0].ToString());
            double slmoi = sl - Convert.ToDouble(txtSoLuong.Text);
            sql = "UPDATE tblSanPham SET SoLuongSP =" + slmoi + "WHERE MaSP = N'" + txtMaSP.Text + "'";
            ThucThiSql.CapNhatDuLieu(sql);
            //txtTongTien.Text = tongmoi.ToString();



            //cập nhật tổng tiền mới
            double tong = Convert.ToDouble(ThucThiSql.DocBang("SELECT TongTien FROM tblHoaDonBan WHERE MaHD = N'" +
                txtMaHD.Text + "'").Rows[0][0].ToString());
            double tongmoi = tong + Convert.ToDouble(txtThanhTien.Text);
            sql = "UPDATE tblHoaDonBan SET TongTien =" + tongmoi + "WHERE MaHD = N'" + txtMaHD.Text + "'";
            ThucThiSql.CapNhatDuLieu(sql);
            txtTongTien.Text = tongmoi.ToString();


            ResetValuesSP();
            bntHuy.Enabled = true;
            //bntIn.Enabled = true;


        }

        private void bntThem_Click(object sender, EventArgs e)
        {
            bntHuy.Enabled = false;
            bntLuu.Enabled = true;
            dataGridView1.DataSource = null;
            ResetValues();
            txtMaHD.Text = ThucThiSql.CreateKey("HDBH");

        }

        private void txtSoLuong_TextChanged(object sender, EventArgs e)
        {
            // khi thay đổi số lượng đơn giá giảm giá thì thành tiền tự động cập nhật
            double tt, sl, dg, gg;
            if (txtSoLuong.Text == "")
                sl = 0;
            else
                sl = Convert.ToDouble(txtSoLuong.Text);
            if (txtGiaBan.Text == "")
                dg = 0;
            else
                dg = Convert.ToDouble(txtGiaBan.Text);
            if (txtGiamGia.Text == "")
                gg = 0;
            else
                gg = Convert.ToDouble(txtGiamGia.Text);
            tt = sl * dg - sl * dg * gg / 100;
            txtThanhTien.Text = tt.ToString();

        }

        private void txtGiamGia_TextChanged(object sender, EventArgs e)
        {
            double tt, sl, dg, gg;
            if (txtSoLuong.Text == "")
                sl = 0;
            else
                sl = Convert.ToDouble(txtSoLuong.Text);
            if (txtGiaBan.Text == "")
                dg = 0;
            else
                dg = Convert.ToDouble(txtGiaBan.Text);
            if (txtGiamGia.Text == "")
                gg = 0;
            else
                gg = Convert.ToDouble(txtGiamGia.Text);
            tt = sl * dg - sl * dg * gg / 100;
            txtThanhTien.Text = tt.ToString();

        }

        private void ResetValuesSP()
        {
            cboTenSP.Text = "";
            txtSoLuong.Text = "";
            txtGiamGia.Text = "0";
        }

        private void Hienthi_Luoi()
        {
            string sql;
            sql = "SELECT MaSP, SoLuong, DonGiaB, GiamGia, ThanhTien FROM tblChiTietHDB WHERE MaHD = N'" +
                txtMaHD.Text + "'";
            dataGridView1.DataSource = ThucThiSql.DocBang(sql);
            dataGridView1.Columns[0].HeaderText = "Mã hàng";
            dataGridView1.Columns[1].HeaderText = "Số lượng";
            dataGridView1.Columns[2].HeaderText = "Đơn giá bán";
            dataGridView1.Columns[3].HeaderText = "Giảm giá";
            dataGridView1.Columns[4].HeaderText = "Thành tiền";
            dataGridView1.Columns[0].Width = 150;
            dataGridView1.Columns[1].Width = 150;
            dataGridView1.Columns[2].Width = 150;
            dataGridView1.Columns[3].Width = 150;
            dataGridView1.Columns[4].Width = 150;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void bntDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM tblChiTietHDB WHERE MaHD = N'" + txtMaHD.Text + "'";
            if (ThucThiSql.DocBang(sql).Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if ((MessageBox.Show("Bạn có chắc chắn muốn xóa không", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
            {
                //lấy thông tin của dòng dữ liệu muốn xóa
                string maspxoa = dataGridView1.CurrentRow.Cells["MaSP"].Value.ToString();
                double slxoa = Convert.ToDouble(dataGridView1.CurrentRow.Cells["SoLuong"].Value.ToString());
                double thanhtienxoa = Convert.ToDouble(dataGridView1.CurrentRow.Cells["ThanhTien"].Value.ToString());
                //xóa hàng trong bảng chi tiết
                sql = "DELETE tblChiTietHDB WHERE MaHD=N'" + txtMaHD.Text +
                    "'AND MaSP=N'" + maspxoa + "'";
                ThucThiSql.CapNhatDuLieu(sql);
                Hienthi_Luoi();
                // cập nhật lại số lượng hàng
                DelUpdateHang(maspxoa, slxoa);
                // cập nhật lại tổng tiền cho HDN
                DelUpdateTongtien(txtMaHD.Text, thanhtienxoa);
            }

        }
        private void DelUpdateHang(string maspxoa, double slxoa)
        {
            //xóa sản phẩm đã chọn trong hóa đơn
            double sl = Convert.ToDouble(ThucThiSql.DocBang("SELECT SoLuongSP FROM tblSanPham WHERE MaSP=N'" +
                maspxoa + "'").Rows[0][0].ToString());
            double slmoi = sl - slxoa;
            string sql = "UPDATE tblSanPham SET SoLuongSP=" + slmoi + " WHERE MaSP=N'" + maspxoa + "'";
            ThucThiSql.CapNhatDuLieu(sql);

        }

        private void DelUpdateTongtien(string mahoadonxoa, double thanhtienxoa)
        {
            //cập nhật lại tổng tiền sau khi xóa sản phẩm
            double tong = Convert.ToDouble(ThucThiSql.DocBang("SELECT TongTien FROM tblHoaDonBan WHERE MaHD = N'" +
                mahoadonxoa + "'").Rows[0][0].ToString());
            double tongmoi = tong - thanhtienxoa;
            string sql = "UPDATE tblHoaDonBan SET TongTien =" + tongmoi + "WHERE MaHD =N'" + mahoadonxoa + "'";
            ThucThiSql.CapNhatDuLieu(sql);
            txtTongTien.Text = tongmoi.ToString();
        }

        private void bntHuy_Click(object sender, EventArgs e)
        {
            //Xóa hóa đơn đã tạo
            if (MessageBox.Show("Bạn có chắc muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string sql = "SELECT MaSP, SoLuong, DonGiaB FROM tblChiTietHDB WHERE MaHD = N'" + txtMaHD.Text + "'";
                DataTable tbl = ThucThiSql.DocBang(sql);
                sql = "DELETE tblHoaDonBan WHERE MaHD= N'" + txtMaHD.Text + "'";
                ThucThiSql.CapNhatDuLieu(sql);
                ResetValues();
                Hienthi_Luoi();
                // cập nhật lại số lượng hàng cho từng mặt hàng bị xóa
                for (int i = 0; i < tbl.Rows.Count; i++)
                    DelUpdateHang(tbl.Rows[i][0].ToString(), Convert.ToDouble(tbl.Rows[i][1]));

                bntHuy.Enabled = false;

            }

        }

        private void cboTenSP_DropDown(object sender, EventArgs e)
        {
            //Lấy tên sản phẩm từ bảng sản phẩm cho combobox TenSP
            cboTenSP.DataSource = ThucThiSql.DocBang("SELECT TenSP FROM tblSanPham");
            cboTenSP.ValueMember = "TenSP";
            cboTenSP.SelectedIndex = -1;
        }

        private void cboTenSP_TextChanged(object sender, EventArgs e)
        {
            string sql;
            if (cboTenSP.Text == "")
            {
                txtMaSP.Text = "";
                txtGiaBan.Text = "";
                return;
            }
            sql = "SELECT MaSP,DonGiaB FROM tblSanPham WHERE TenSP =N'" + cboTenSP.Text + "'";
            DataTable table = ThucThiSql.DocBang(sql);
            if (table.Rows.Count > 0)
            {
                txtMaSP.Text = table.Rows[0][0].ToString();
                txtGiaBan.Text = table.Rows[0][1].ToString();
            }
        }


        //private void cboMaSP_TextChanged(object sender, EventArgs e)
        //{
        //        string sql;
        //        if (cboMaSP.Text == "")
        //        {
        //            txtTenSP.Text = "";
        //            txtGiaBan.Text = "";
        //            return;
        //        }
        //        sql = "SELECT TenSP,DonGiaB FROM tblSanPham WHERE MaSP =N'" + cboMaSP.Text + "'";
        //        DataTable table = ThucThiSql.DocBang(sql);
        //        if (table.Rows.Count > 0)
        //        {
        //            txtTenSP.Text = table.Rows[0][0].ToString();
        //            txtGiaBan.Text = table.Rows[0][1].ToString();
        //        }

        //    }

        //private void cboMaSP_DropDown(object sender, EventArgs e)
        //{
        //    //Lấy mã sản phẩm từ bảng sản phẩm cho combobox MaSP
        //    cboMaSP.DataSource = ThucThiSql.DocBang("SELECT MaSP FROM tblSanPham");
        //    cboMaSP.ValueMember = "MaSP";
        //    cboMaSP.SelectedIndex = -1;
        //}





    }
}