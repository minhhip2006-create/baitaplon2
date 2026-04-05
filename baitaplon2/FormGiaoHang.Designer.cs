namespace baitaplon2
{
    partial class FormGiaoHang
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dtNgayGiao = new System.Windows.Forms.DateTimePicker();
            this.txtDiaChi = new System.Windows.Forms.TextBox();
            this.txtMaDon = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.grGiaoHang = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.txtDonViVC = new System.Windows.Forms.TextBox();
            this.txtPhiShip = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dtNgayGiao
            // 
            this.dtNgayGiao.Location = new System.Drawing.Point(99, 159);
            this.dtNgayGiao.Name = "dtNgayGiao";
            this.dtNgayGiao.Size = new System.Drawing.Size(216, 22);
            this.dtNgayGiao.TabIndex = 23;
            // 
            // txtDiaChi
            // 
            this.txtDiaChi.Location = new System.Drawing.Point(99, 196);
            this.txtDiaChi.Name = "txtDiaChi";
            this.txtDiaChi.Size = new System.Drawing.Size(267, 22);
            this.txtDiaChi.TabIndex = 22;
            // 
            // txtMaDon
            // 
            this.txtMaDon.Location = new System.Drawing.Point(81, 108);
            this.txtMaDon.Name = "txtMaDon";
            this.txtMaDon.Size = new System.Drawing.Size(188, 22);
            this.txtMaDon.TabIndex = 21;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 165);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 16);
            this.label4.TabIndex = 20;
            this.label4.Text = "Ngày giao ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 202);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 16);
            this.label3.TabIndex = 19;
            this.label3.Text = "Địa chỉ ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 165);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 16);
            this.label2.TabIndex = 18;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 114);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 16);
            this.label1.TabIndex = 17;
            this.label1.Text = "Mã đơn ";
            // 
            // grGiaoHang
            // 
            this.grGiaoHang.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.grGiaoHang.Location = new System.Drawing.Point(12, 35);
            this.grGiaoHang.Name = "grGiaoHang";
            this.grGiaoHang.Size = new System.Drawing.Size(336, 45);
            this.grGiaoHang.TabIndex = 16;
            this.grGiaoHang.TabStop = false;
            this.grGiaoHang.Text = "                               Thông tin giao hàng";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(27, 300);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(374, 147);
            this.dataGridView1.TabIndex = 24;
            // 
            // txtDonViVC
            // 
            this.txtDonViVC.Location = new System.Drawing.Point(99, 238);
            this.txtDonViVC.Name = "txtDonViVC";
            this.txtDonViVC.Size = new System.Drawing.Size(228, 22);
            this.txtDonViVC.TabIndex = 25;
            // 
            // txtPhiShip
            // 
            this.txtPhiShip.Location = new System.Drawing.Point(99, 272);
            this.txtPhiShip.Name = "txtPhiShip";
            this.txtPhiShip.Size = new System.Drawing.Size(267, 22);
            this.txtPhiShip.TabIndex = 26;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(25, 244);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 16);
            this.label5.TabIndex = 27;
            this.label5.Text = "Đơn vvc";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(20, 275);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 16);
            this.label6.TabIndex = 28;
            this.label6.Text = "Phí Ship";
            // 
            // FormGiaoHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtPhiShip);
            this.Controls.Add(this.txtDonViVC);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.dtNgayGiao);
            this.Controls.Add(this.txtDiaChi);
            this.Controls.Add(this.txtMaDon);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.grGiaoHang);
            this.Name = "FormGiaoHang";
            this.Text = "FormGiaoHang";
            this.Load += new System.EventHandler(this.FormGiaoHang_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtNgayGiao;
        private System.Windows.Forms.TextBox txtDiaChi;
        private System.Windows.Forms.TextBox txtMaDon;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox grGiaoHang;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox txtDonViVC;
        private System.Windows.Forms.TextBox txtPhiShip;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}