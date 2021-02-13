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
    public partial class FrmPNH : Form
    {
        public FrmPNH()
        {
            InitializeComponent();
        }

        private void FrmPNH_Load(object sender, EventArgs e)
        {
            bntThem.Enabled = true;
            bntLuu.Enabled = false;
            txtMaPNH.ReadOnly = true;
            txtTenNV.ReadOnly = true;
            txtTenNCC.ReadOnly = true;
            txtDiaChi.ReadOnly = true;
            txtSDT.ReadOnly = true;
            txtTenSP.ReadOnly = true;
            txtThanhTien.ReadOnly = true;
            txtTongTien.ReadOnly = true;
            txtTongTien.Text = "0";
        }

        private void cboMaNV_DropDown(object sender, EventArgs e)
        {
            cboMaNV.DataSource = ThucThiSql.DocBang("SELECT MaNV FROM tblNhanVien");
            cboMaNV.ValueMember = "MaNV";
            cboMaNV.SelectedIndex = -1;
        }

        private void cboMaNCC_DropDown(object sender, EventArgs e)
        {
            cboMaNCC.DataSource = ThucThiSql.DocBang("SELECT MaNCC FROM tblNhaCungCap");
            cboMaNCC.ValueMember = "MaNCC";
            cboMaNCC.SelectedIndex = -1;
        }

        private void cboMaSP_DropDown(object sender, EventArgs e)
        {
            cboMaSP.DataSource = ThucThiSql.DocBang("SELECT MaSP FROM tblSanPham");
            cboMaSP.ValueMember = "MaSP";
            cboMaSP.SelectedIndex = -1;
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

        private void cboMaNCC_TextChanged(object sender, EventArgs e)
        {
            string sql;
            if (cboMaNCC.Text == "")
            {
                txtTenNCC.Text = "";
                txtSDT.Text = "";
                txtDiaChi.Text = "";
                return;
            }
            sql = "SELECT TenNCC,SDTNCC,DiaChi FROM tblNhaCungCap WHERE MaNCC =N'" + cboMaNCC.Text + "'";
            DataTable table = ThucThiSql.DocBang(sql);
            if (table.Rows.Count > 0)
            {
                txtTenNCC.Text = table.Rows[0][0].ToString();
                txtSDT.Text = table.Rows[0][1].ToString();
                txtDiaChi.Text = table.Rows[0][2].ToString();
            }
        }

        private void cboMaSP_TextChanged(object sender, EventArgs e)
        {
            string sql;
            if (cboMaSP.Text == "")
            {
                txtTenSP.Text = "";
                txtGiaNhap.Text = "";
                return;
            }
            sql = "SELECT TenSP,DonGiaN FROM tblSanPham WHERE MaSP =N'" + cboMaSP.Text + "'";
            DataTable table = ThucThiSql.DocBang(sql);
            if (table.Rows.Count > 0)
            {
                txtTenSP.Text = table.Rows[0][0].ToString();
                txtGiaNhap.Text = table.Rows[0][1].ToString();
            }
        }

        private void ResetValues()
        {
            txtMaPNH.Text = "";
            dateTimePicker1.Text = DateTime.Now.ToShortDateString();
            cboMaNCC.Text = "";
            txtTongTien.Text = "0";
            cboMaSP.Text = "";
            txtSoLuong.Text = "";
            txtGiaNhap.Text = "";
            txtThanhTien.Text = "0";
        }

        private void bntThem_Click(object sender, EventArgs e)
        {
            bntLuu.Enabled = true;
            dataGridView1.DataSource = null;
            ResetValues();
            txtMaPNH.Text = ThucThiSql.CreateKey("PNH");
        }

        private void txtSoLuong_TextChanged(object sender, EventArgs e)
        {
            // khi thay đổi số lượng đơn giá giảm giá thì thành tiền tự động cập nhật
            double tt, sl, dg;
            if (txtSoLuong.Text == "")
                sl = 0;
            else
                sl = Convert.ToDouble(txtSoLuong.Text);
            if (txtGiaNhap.Text == "")
                dg = 0;
            else
                dg = Convert.ToDouble(txtGiaNhap.Text);
            tt = sl * dg;
            txtThanhTien.Text = tt.ToString();

        }

        private void txtGiaNhap_TextChanged(object sender, EventArgs e)
        {
            double tt, sl, dg;
            if (txtSoLuong.Text == "")
                sl = 0;
            else
                sl = Convert.ToDouble(txtSoLuong.Text);
            if (txtGiaNhap.Text == "")
                dg = 0;
            else
                dg = Convert.ToDouble(txtGiaNhap.Text);

            tt = sl * dg;
            txtThanhTien.Text = tt.ToString();

        }

        private void bntLuu_Click(object sender, EventArgs e)
        {
            string sql;
            if (dateTimePicker1.Text.Length == 0)
            {
                MessageBox.Show("Bạn phải nhập ngày nhập hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dateTimePicker1.Focus();
                return;
            }
            if (cboMaNV.Text.Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboMaNV.Focus();
                return;
            }
            if (cboMaNCC.Text.Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã nhà cung cấp", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboMaNCC.Focus();
                return;
            }
            sql = "INSERT INTO tblPhieuNhapHang(MaPNH, NgayNhap, MaNV, MaNCC, TongTien) VALUES(N'" +
                txtMaPNH.Text.Trim() + "',N'" + dateTimePicker1.Value.ToShortDateString() + "',N'" +
                cboMaNV.Text + "',N'" + cboMaNCC.Text + "'," + txtTongTien.Text + ")";
            ThucThiSql.CapNhatDuLieu(sql);

            //Lưu thông tin các sản phẩm

            if (cboMaSP.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã sản phẩm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboMaSP.Focus();
                return;
            }
            if ((txtSoLuong.Text.Trim().Length == 0) || (txtSoLuong.Text == "0"))
            {
                MessageBox.Show("Bạn phải nhập số lượng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoLuong.Text = "";
                txtSoLuong.Focus();
                return;
            }

            sql = "SELECT MaSP FROM tblChiTietPNH WHERE MaSP=N'" + cboMaSP.Text +
                "' AND MaPNH = N'" + txtMaPNH.Text.Trim() + "'";
            if (ThucThiSql.DocBang(sql).Rows.Count > 0)
            {
                MessageBox.Show("Mã sản phẩm này đã có, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ResetValuesSP();
                cboMaSP.Focus();
                return;
            }

            sql = "INSERT INTO tblChiTietPNH(MaPNH, MaSP, SoLuong, DonGiaN, ThanhTien) VALUES(N'" +
                txtMaPNH.Text.Trim() + "',N'" + cboMaSP.Text.ToString() + "'," + txtSoLuong.Text +
                "," + txtGiaNhap.Text + "," + txtThanhTien.Text + ")";
            ThucThiSql.CapNhatDuLieu(sql);
            Hienthi_Luoi();


            //cập nhật tổng tiền mới
            double tong = Convert.ToDouble(ThucThiSql.DocBang("SELECT TongTien FROM tblPhieuNhapHang WHERE MaPNH = N'" +
                txtMaPNH.Text + "'").Rows[0][0].ToString());
            double tongmoi = tong + Convert.ToDouble(txtThanhTien.Text);
            sql = "UPDATE tblPhieuNhapHang SET TongTien =" + tongmoi + "WHERE MaPNH = N'" + txtMaPNH.Text + "'";
            ThucThiSql.CapNhatDuLieu(sql);
            txtTongTien.Text = tongmoi.ToString();

            //cập nhật số lượng mới vào bảng
            double sl = Convert.ToDouble(ThucThiSql.DocBang("SELECT SoLuongSP FROM tblSanPham WHERE MaSP = N'" +
               cboMaSP.Text + "'").Rows[0][0].ToString());
            double slmoi = sl + Convert.ToDouble(txtSoLuong.Text);
            sql = "UPDATE tblSanPham SET SoLuongSP =" + slmoi + "WHERE MaSP = N'" + cboMaSP.Text + "'";
            ThucThiSql.CapNhatDuLieu(sql);


            //cập nhật đơn giá nhập mới vào bảng hàng
            double dgn = Convert.ToDouble(ThucThiSql.DocBang("SELECT DonGiaN FROM tblSanPham WHERE MaSP = N'" +
                cboMaSP.Text + "'").Rows[0][0].ToString());
            double dgnmoi = (sl * dgn + Convert.ToDouble(txtSoLuong.Text) * Convert.ToDouble(txtGiaNhap.Text)) / (sl + Convert.ToDouble(txtSoLuong.Text));
            sql = "UPDATE tblSanpham SET DongiaN =" + dgnmoi + "WHERE MaSP = N'" + cboMaSP.Text + "'";
            ThucThiSql.CapNhatDuLieu(sql);


            //cập nhật giá bán mới vào bảng hàng
            double dgb = Convert.ToDouble(ThucThiSql.DocBang("SELECT DonGiaB FROM tblSanPham WHERE MaSP = N'" +
                cboMaSP.Text + "'").Rows[0][0].ToString());
            double dgbmoi = dgnmoi * 2;
            sql = "UPDATE tblSanPham SET DonGiaB =" + dgbmoi + "WHERE MaSP = N'" + cboMaSP.Text + "'";
            ThucThiSql.CapNhatDuLieu(sql);
            txtTongTien.Text = tongmoi.ToString();

            ResetValuesSP();

        }
        private void ResetValuesSP()
        {
            cboMaSP.Text = "";
            txtSoLuong.Text = "";
        }
        private void Hienthi_Luoi()
        {
            string sql;
            sql = "SELECT MaSP, SoLuong, DonGiaN, ThanhTien FROM tblChiTietPNH WHERE MaPNH = N'" +
                txtMaPNH.Text + "'";
            dataGridView1.DataSource = ThucThiSql.DocBang(sql);
            dataGridView1.Columns[0].HeaderText = "Mã sản phẩm";
            dataGridView1.Columns[1].HeaderText = "Số lượng";
            dataGridView1.Columns[2].HeaderText = "Giá nhập";
            dataGridView1.Columns[3].HeaderText = "Thành tiền";
            dataGridView1.Columns[0].Width = 150;
            dataGridView1.Columns[1].Width = 150;
            dataGridView1.Columns[2].Width = 150;
            dataGridView1.Columns[3].Width = 150;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM tblChiTietPNH WHERE MaPNH = N'" + txtMaPNH.Text + "'";
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
                double gianhapxoa = Convert.ToDouble(dataGridView1.CurrentRow.Cells["DonGiaN"].Value.ToString());
                double thanhtienxoa = Convert.ToDouble(dataGridView1.CurrentRow.Cells["ThanhTien"].Value.ToString());
                //xóa hàng trong bảng chi tiết
                sql = "DELETE tblChiTietPNH WHERE MaPNH=N'" + txtMaPNH.Text +
                    "'AND MaSP=N'" + maspxoa + "'";
                ThucThiSql.CapNhatDuLieu(sql);
                Hienthi_Luoi();
                // cập nhật lại số lượng hàng, đơn giá nhập, bán
                DelUpdateSP(maspxoa, slxoa, gianhapxoa);
                // cập nhật lại tổng tiền cho HDN
                DelUpdateTongtien(txtMaPNH.Text, thanhtienxoa);
            }

        }

        private void DelUpdateSP(string maspxoa, double slxoa, double gianhapxoa)
        {
            double sl = Convert.ToDouble(ThucThiSql.DocBang("SELECT SoLuongSP FROM tblSanPham WHERE MaSP=N'" +
                maspxoa + "'").Rows[0][0].ToString());
            double slmoi = sl - slxoa;
            string sql = "UPDATE tblSanPham SET SoLuongSP=" + slmoi + " WHERE MaSP=N'" + maspxoa + "'";
            ThucThiSql.CapNhatDuLieu(sql);
            //cập nhật lại đơn giá nhập vào bang hàng sau khi xóa 1 mặt hàng trong chi tiết HDN
            double dgn = Convert.ToDouble(ThucThiSql.DocBang("SELECT DonGiaN FROM tblSanPham WHERE MaSP=N'" +
                maspxoa + "'").Rows[0][0].ToString());
            double dgnmoi = (sl * dgn) - (slxoa * gianhapxoa) / slxoa;
            string sql1 = "UPDATE tblSanPham SET DonGiaN=" + dgnmoi + " WHERE MaSP=N'" + maspxoa + "'";
            ThucThiSql.CapNhatDuLieu(sql);
            //cập nhật đơn giá bán mới vào bảng hàng
            double dgb = Convert.ToDouble(ThucThiSql.DocBang("SELECT DonGiaB FROM tblSanPham WHERE MaSP=N'" +
                maspxoa + "'").Rows[0][0].ToString());
            double dgbmoi = dgnmoi * 2;
            string sql2 = "UPDATE tblSanPham SET DonGiaB=" + dgbmoi + " WHERE MaSP=N'" + maspxoa + "'";
            ThucThiSql.CapNhatDuLieu(sql);
        }
        private void DelUpdateTongtien(string mapnxoa, double thanhtienxoa)
        {
            double tong = Convert.ToDouble(ThucThiSql.DocBang("SELECT TongTien FROM tblPhieuNhapHang WHERE MaPNH = N'" +
                mapnxoa + "'").Rows[0][0].ToString());
            double tongmoi = tong - thanhtienxoa;
            string sql = "UPDATE tblPhieuNhapHang SET TongTien =" + tongmoi + "WHERE MaPNH =N'" + mapnxoa + "'";
            ThucThiSql.CapNhatDuLieu(sql);
            txtTongTien.Text = tongmoi.ToString();
        }

        private void bntDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
