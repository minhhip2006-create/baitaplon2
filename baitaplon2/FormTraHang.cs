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
    public partial class FormTraHang : Form
    {
        string connStr = @"Data Source=LAPTOP-FSKOS12F;Initial Catalog=QL_BanGiayHSK;Integrated Security=True";

        public string maDon;

        public FormTraHang()
        {
            InitializeComponent();
        }

        private void FormTraHang_Load(object sender, EventArgs e)
        {
            txtMaDon.Text = maDon;
            txtMaDon.Enabled = false;

            txtMaPT.Text = TaoMaPT();
            txtMaPT.Enabled = false;

            dtNgayTra.Value = DateTime.Now;
            txtTongTienHoan.Enabled = false;

            LoadSKU();
            LoadData();

        }

        void LoadSKU()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string sql = "SELECT sMaSKU FROM tblChiTietHoaDon WHERE sMaHD=@ma";
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.SelectCommand.Parameters.AddWithValue("@ma", maDon);

                DataTable dt = new DataTable();
                da.Fill(dt);

                cbMaSKU.DataSource = dt;
                cbMaSKU.DisplayMember = "sMaSKU";
                cbMaSKU.ValueMember = "sMaSKU";
            }
        }
        void LoadData()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string sql = @"SELECT pt.sMaPT, pt.sMaHD, pt.dNgayTra, pt.sLyDo, pt.fTongTienHoan,
                                      ct.sMaSKU, ct.iSoLuongTra, ct.sTinhTrang
                               FROM tblPhieuTra pt
                               LEFT JOIN tblChiTietTra ct 
                               ON pt.sMaPT = ct.sMaPT
                               WHERE pt.sMaHD = @ma";

                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                da.SelectCommand.Parameters.AddWithValue("@ma", maDon);

                DataTable dt = new DataTable();
                da.Fill(dt);

                dataGridView1.DataSource = dt;
            }
        }

        // ================= TẠO MÃ PT =================
        string TaoMaPT()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                string sql = "SELECT TOP 1 sMaPT FROM tblPhieuTra ORDER BY sMaPT DESC";
                SqlCommand cmd = new SqlCommand(sql, conn);

                object kq = cmd.ExecuteScalar();

                if (kq == null) return "PT001";

                string ma = kq.ToString();
                int so = int.Parse(ma.Substring(2)) + 1;

                return "PT" + so.ToString("D3");
            }
        }

        // ================= LẤY ĐƠN GIÁ =================
        double LayDonGia(string maSKU)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                string sql = @"SELECT TOP 1 fDonGiaBan 
                               FROM tblChiTietHoaDon 
                               WHERE sMaSKU=@sku AND sMaHD=@ma";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@sku", maSKU);
                cmd.Parameters.AddWithValue("@ma", maDon);

                object kq = cmd.ExecuteScalar();

                if (kq == null) return 0;

                return Convert.ToDouble(kq);
            }
        }

        // ================= THÊM =================
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (txtMaNV.Text == "" || txtSoLuongTra.Text == "")
            {
                MessageBox.Show("Nhập thiếu dữ liệu!");
                return;
            }

            if (!int.TryParse(txtSoLuongTra.Text, out int sl))
            {
                MessageBox.Show("Số lượng sai!");
                return;
            }

            double donGia = LayDonGia(cbMaSKU.Text);
            double tien = donGia * sl;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                string maPT = txtMaPT.Text;

                // 1. PHIẾU TRẢ
                string sql1 = @"INSERT INTO tblPhieuTra
                               VALUES (@pt,@hd,@nv,@ngay,@lydo,@tien)";

                SqlCommand cmd1 = new SqlCommand(sql1, conn);

                cmd1.Parameters.AddWithValue("@pt", maPT);
                cmd1.Parameters.AddWithValue("@hd", txtMaDon.Text);
                cmd1.Parameters.AddWithValue("@nv", txtMaNV.Text);
                cmd1.Parameters.AddWithValue("@ngay", dtNgayTra.Value);
                cmd1.Parameters.AddWithValue("@lydo", txtLyDo.Text);
                cmd1.Parameters.AddWithValue("@tien", tien);

                cmd1.ExecuteNonQuery();

                // 2. CHI TIẾT
                string sql2 = @"INSERT INTO tblChiTietTra
                               VALUES (@pt,@sku,@sl,@tt)";

                SqlCommand cmd2 = new SqlCommand(sql2, conn);

                cmd2.Parameters.AddWithValue("@pt", maPT);
                cmd2.Parameters.AddWithValue("@sku", cbMaSKU.Text);
                cmd2.Parameters.AddWithValue("@sl", sl);
                cmd2.Parameters.AddWithValue("@tt", txtTinhTrang.Text);

                cmd2.ExecuteNonQuery();
            }

            txtTongTienHoan.Text = tien.ToString();

            MessageBox.Show("Trả hàng thành công!");

            LoadData();
            txtMaPT.Text = TaoMaPT();
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                string maPT = txtMaPT.Text;

                SqlCommand cmd1 = new SqlCommand("DELETE FROM tblChiTietTra WHERE sMaPT=@pt", conn);
                cmd1.Parameters.AddWithValue("@pt", maPT);
                cmd1.ExecuteNonQuery();

                SqlCommand cmd2 = new SqlCommand("DELETE FROM tblPhieuTra WHERE sMaPT=@pt", conn);
                cmd2.Parameters.AddWithValue("@pt", maPT);
                cmd2.ExecuteNonQuery();
            }

            MessageBox.Show("Xóa thành công!");
            LoadData();
        }

        // ================= LÀM MỚI =================
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaPT.Text = TaoMaPT();
            txtSoLuongTra.Clear();
            txtTinhTrang.Clear();
            txtLyDo.Clear();
            txtTongTienHoan.Clear();
        }
    }
}
