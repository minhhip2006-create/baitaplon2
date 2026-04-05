using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace baitaplon2
{
    public partial class Form1 : Form
    {

        SqlConnection conn;
        string connStr = @"Data Source=LAPTOP-FSKOS12F;Initial Catalog=QL_BanGiayHSK;Integrated Security=True";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(connStr);

            LoadDonHang();
            LoadKhachHang();

            txtMaDon.Text = TaoMaDon();
            dtNgayLap.Value = DateTime.Now;

            // trạng thái
            cbTrangThai.Items.Clear();
            cbTrangThai.Items.Add("Chờ xử lý");
            cbTrangThai.Items.Add("Đã giao");
            cbTrangThai.Items.Add("Trả hàng");
            cbTrangThai.SelectedIndex = 0;

            txtTongTien.Enabled = false; // không cho nhập
        }

        void LoadDonHang()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM tblHoaDon", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        void LoadKhachHang()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT sMaKH FROM tblKhachHang", conn);
            DataTable dt = new DataTable();
            da.Fill(dt);

            cbMaKH.DataSource = dt;
            cbMaKH.DisplayMember = "sMaKH";
            cbMaKH.ValueMember = "sMaKH";
        }

        string TaoMaDon()
        {
            conn.Open();

            string sql = "SELECT TOP 1 sMaHD FROM tblHoaDon ORDER BY sMaHD DESC";
            SqlCommand cmd = new SqlCommand(sql, conn);
            object kq = cmd.ExecuteScalar();

            conn.Close();

            if (kq == null)
                return "HD001";

            string ma = kq.ToString();
            int so = int.Parse(ma.Substring(2)) + 1;

            return "HD" + so.ToString("D3");
        }

        double TinhTongTien(string maDon)
        {
            conn.Open();

            string sql = "SELECT SUM(iSoLuongBan * fDonGiaBan) FROM tblChiTietHoaDon WHERE sMaHD=@ma";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@ma", maDon);

            object kq = cmd.ExecuteScalar();

            conn.Close();

            if (kq == DBNull.Value || kq == null)
                return 0;

            return Convert.ToDouble(kq);
        }

        bool KiemTra()
        {
            if (txtMaDon.Text == "")
            {
                MessageBox.Show("Chưa có mã đơn!");
                return false;
            }

            if (cbMaKH.Text == "")
            {
                MessageBox.Show("Chọn khách hàng!");
                return false;
            }

            if (cbTrangThai.Text == "")
            {
                MessageBox.Show("Chọn trạng thái!");
                return false;
            }

            return true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!KiemTra()) return;

            conn.Open();

            double tong = TinhTongTien(txtMaDon.Text);

            string sql = @"INSERT INTO tblHoaDon 
                   (sMaHD, sMaKH, dNgayBan, fTongTien) 
                   VALUES (@ma,@makh,@ngay,@tong)";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@ma", txtMaDon.Text);
            cmd.Parameters.AddWithValue("@makh", cbMaKH.Text);
            cmd.Parameters.AddWithValue("@ngay", dtNgayLap.Value);
            cmd.Parameters.AddWithValue("@tong", tong);

            cmd.ExecuteNonQuery();
            conn.Close();

            MessageBox.Show("Thêm thành công!");
            LoadDonHang();
            txtMaDon.Text = TaoMaDon();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (!KiemTra()) return;

            conn.Open();

            double tong = TinhTongTien(txtMaDon.Text);

            string sql = @"UPDATE tblHoaDon 
                   SET sMaKH=@makh, dNgayBan=@ngay, fTongTien=@tong
                   WHERE sMaHD=@ma";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@ma", txtMaDon.Text);
            cmd.Parameters.AddWithValue("@makh", cbMaKH.Text);
            cmd.Parameters.AddWithValue("@ngay", dtNgayLap.Value);
            cmd.Parameters.AddWithValue("@tong", tong);

            cmd.ExecuteNonQuery();
            conn.Close();

            MessageBox.Show("Sửa thành công!");
            LoadDonHang();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult r = MessageBox.Show("Bạn có chắc muốn xóa?", "Xác nhận", MessageBoxButtons.YesNo);
            if (r == DialogResult.No) return;

            conn.Open();

            // XÓA CHI TIẾT TRƯỚC
            string sql1 = "DELETE FROM tblChiTietHoaDon WHERE sMaHD=@ma";
            SqlCommand cmd1 = new SqlCommand(sql1, conn);
            cmd1.Parameters.AddWithValue("@ma", txtMaDon.Text);
            cmd1.ExecuteNonQuery();

            // XÓA HÓA ĐƠN
            string sql2 = "DELETE FROM tblHoaDon WHERE sMaHD=@ma";
            SqlCommand cmd2 = new SqlCommand(sql2, conn);
            cmd2.Parameters.AddWithValue("@ma", txtMaDon.Text);
            cmd2.ExecuteNonQuery();

            conn.Close();

            MessageBox.Show("Xóa thành công!");
            LoadDonHang();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaDon.Text = TaoMaDon();
            cbTrangThai.SelectedIndex = 0;
            dtNgayLap.Value = DateTime.Now;
            txtTongTien.Clear();
        }

        private void btnGiaoHang_Click(object sender, EventArgs e)
        {
            if (txtMaDon.Text == "")
            {
                MessageBox.Show("Chọn đơn trước!");
                return;
            }

            FormGiaoHang f = new FormGiaoHang();
            f.maDon = txtMaDon.Text;
            f.ShowDialog();

        }

        private void btnTraHang_Click(object sender, EventArgs e)
        {
            if (txtMaDon.Text == "")
            {
                MessageBox.Show("Chọn đơn trước!");
                return;
            }

            FormTraHang f = new FormTraHang();
            f.maDon = txtMaDon.Text;

            f.ShowDialog();

            LoadDonHang();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            txtMaDon.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            cbMaKH.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            dtNgayLap.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[2].Value);
            txtTongTien.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            cbTrangThai.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
        }
    }
}
