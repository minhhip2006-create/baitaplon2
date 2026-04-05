using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace baitaplon2
{
    public partial class FormGiaoHang : Form
    {
        SqlConnection conn;
        string connStr = @"Data Source=LAPTOP-FSKOS12F;Initial Catalog=QL_BanGiayHSK;Integrated Security=True";

        public string maDon;

        public FormGiaoHang()
        {
            InitializeComponent();
        }

        // ================= LOAD =================
        private void FormGiaoHang_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(connStr);

            txtMaDon.Text = maDon;
            txtMaDon.Enabled = false;

            dtNgayGiao.Value = DateTime.Now;

            LoadGiaoHang();
        }

        // ================= LOAD DỮ LIỆU =================
        void LoadGiaoHang()
        {
            SqlDataAdapter da = new SqlDataAdapter(
                "SELECT * FROM tblGiaoHang WHERE sMaHD = @ma", conn);

            da.SelectCommand.Parameters.AddWithValue("@ma", maDon);

            DataTable dt = new DataTable();
            da.Fill(dt);

            dataGridView1.DataSource = dt;
        }

        // ================= TẠO MÃ VẬN ĐƠN =================
        string TaoMaVanDon()
        {
            conn.Open();

            string sql = "SELECT TOP 1 sMaVanDon FROM tblGiaoHang ORDER BY sMaVanDon DESC";
            SqlCommand cmd = new SqlCommand(sql, conn);

            object kq = cmd.ExecuteScalar();

            conn.Close();

            if (kq == null)
                return "VD001";

            string ma = kq.ToString();
            int so = int.Parse(ma.Substring(2)) + 1;

            return "VD" + so.ToString("D3");
        }

        // ================= NÚT GIAO HÀNG =================
        private void btnGiao_Click(object sender, EventArgs e)
        {
            conn.Open();

            string sql = @"INSERT INTO tblGiaoHang
                           (sMaVanDon, sMaHD, sDonViVanChuyen, fPhiShip, sDiaChiGiao, dNgayGiao, iTrangThai)
                           VALUES (@vd,@hd,@dv,@phi,@dc,@ngay,@tt)";

            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@vd", TaoMaVanDon());
            cmd.Parameters.AddWithValue("@hd", txtMaDon.Text);
            cmd.Parameters.AddWithValue("@dv", txtDonViVC.Text);
            cmd.Parameters.AddWithValue("@phi", float.Parse(txtPhiShip.Text));
            cmd.Parameters.AddWithValue("@dc", txtDiaChi.Text);
            cmd.Parameters.AddWithValue("@ngay", dtNgayGiao.Value);
            cmd.Parameters.AddWithValue("@tt", 1); // 1 = đã giao

            cmd.ExecuteNonQuery();

            // cập nhật trạng thái hóa đơn
            string sql2 = "UPDATE tblHoaDon SET fTongTien = fTongTien WHERE sMaHD = @ma";
            SqlCommand cmd2 = new SqlCommand(sql2, conn);
            cmd2.Parameters.AddWithValue("@ma", txtMaDon.Text);
            cmd2.ExecuteNonQuery();

            conn.Close();

            MessageBox.Show("Giao hàng thành công!");
            LoadGiaoHang();
        }

        // ================= CLICK GRID =================
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.RowIndex;
            if (i >= 0)
            {
                txtDonViVC.Text = dataGridView1.Rows[i].Cells["sDonViVanChuyen"].Value.ToString();
                txtPhiShip.Text = dataGridView1.Rows[i].Cells["fPhiShip"].Value.ToString();
                txtDiaChi.Text = dataGridView1.Rows[i].Cells["sDiaChiGiao"].Value.ToString();
                dtNgayGiao.Value = Convert.ToDateTime(dataGridView1.Rows[i].Cells["dNgayGiao"].Value);
            }
        }
    }
}