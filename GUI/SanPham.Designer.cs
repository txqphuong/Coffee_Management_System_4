
namespace WindowsFormsApp4
{
    partial class SanPham
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_timkiem = new System.Windows.Forms.Button();
            this.txt_timkiem = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txt_showpath = new System.Windows.Forms.TextBox();
            this.txt_gia = new System.Windows.Forms.TextBox();
            this.txt_tenmon = new System.Windows.Forms.TextBox();
            this.txt_mahang = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.btn_sua = new System.Windows.Forms.Button();
            this.btn_them = new System.Windows.Forms.Button();
            this.btn_xoa = new System.Windows.Forms.Button();
            this.btn_Browse = new System.Windows.Forms.Button();
            this.pic_Anh = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Anh)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(1, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(892, 65);
            this.panel1.TabIndex = 15;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(354, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(186, 39);
            this.label2.TabIndex = 7;
            this.label2.Text = "Sản Phẩm";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.panel2.Controls.Add(this.label1);
            this.panel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(897, 69);
            this.panel2.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(344, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(194, 50);
            this.label1.TabIndex = 0;
            this.label1.Text = "Sản Phẩm";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label3.Location = new System.Drawing.Point(363, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(186, 39);
            this.label3.TabIndex = 7;
            this.label3.Text = "Sản Phẩm";
            // 
            // btn_timkiem
            // 
            this.btn_timkiem.BackgroundImage = global::WindowsFormsApp4.Properties.Resources.search;
            this.btn_timkiem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_timkiem.Font = new System.Drawing.Font("Times New Roman", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btn_timkiem.Location = new System.Drawing.Point(792, 97);
            this.btn_timkiem.Margin = new System.Windows.Forms.Padding(2);
            this.btn_timkiem.Name = "btn_timkiem";
            this.btn_timkiem.Size = new System.Drawing.Size(40, 40);
            this.btn_timkiem.TabIndex = 59;
            this.btn_timkiem.UseVisualStyleBackColor = true;
            this.btn_timkiem.Click += new System.EventHandler(this.btn_timkiem_Click);
            // 
            // txt_timkiem
            // 
            this.txt_timkiem.Location = new System.Drawing.Point(512, 97);
            this.txt_timkiem.Margin = new System.Windows.Forms.Padding(2);
            this.txt_timkiem.Multiline = true;
            this.txt_timkiem.Name = "txt_timkiem";
            this.txt_timkiem.Size = new System.Drawing.Size(275, 39);
            this.txt_timkiem.TabIndex = 46;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.panel3.Controls.Add(this.label3);
            this.panel3.Location = new System.Drawing.Point(1, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(892, 69);
            this.panel3.TabIndex = 55;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_Browse);
            this.groupBox1.Controls.Add(this.pic_Anh);
            this.groupBox1.Font = new System.Drawing.Font("Times New Roman", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.groupBox1.Location = new System.Drawing.Point(471, 281);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(391, 195);
            this.groupBox1.TabIndex = 54;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Ảnh";
            // 
            // txt_showpath
            // 
            this.txt_showpath.Location = new System.Drawing.Point(861, 518);
            this.txt_showpath.Margin = new System.Windows.Forms.Padding(2);
            this.txt_showpath.Name = "txt_showpath";
            this.txt_showpath.Size = new System.Drawing.Size(1, 20);
            this.txt_showpath.TabIndex = 45;
            // 
            // txt_gia
            // 
            this.txt_gia.Location = new System.Drawing.Point(562, 241);
            this.txt_gia.Margin = new System.Windows.Forms.Padding(2);
            this.txt_gia.Name = "txt_gia";
            this.txt_gia.Size = new System.Drawing.Size(270, 20);
            this.txt_gia.TabIndex = 53;
            // 
            // txt_tenmon
            // 
            this.txt_tenmon.Location = new System.Drawing.Point(562, 192);
            this.txt_tenmon.Margin = new System.Windows.Forms.Padding(2);
            this.txt_tenmon.Name = "txt_tenmon";
            this.txt_tenmon.Size = new System.Drawing.Size(270, 20);
            this.txt_tenmon.TabIndex = 52;
            // 
            // txt_mahang
            // 
            this.txt_mahang.Location = new System.Drawing.Point(562, 147);
            this.txt_mahang.Margin = new System.Windows.Forms.Padding(2);
            this.txt_mahang.Name = "txt_mahang";
            this.txt_mahang.Size = new System.Drawing.Size(270, 20);
            this.txt_mahang.TabIndex = 51;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label4.Location = new System.Drawing.Point(471, 238);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 20);
            this.label4.TabIndex = 50;
            this.label4.Text = "Giá";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label5.Location = new System.Drawing.Point(471, 192);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 20);
            this.label5.TabIndex = 49;
            this.label5.Text = "Tên Món";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label6.Location = new System.Drawing.Point(471, 145);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 20);
            this.label6.TabIndex = 48;
            this.label6.Text = "Mã Hàng";
            // 
            // dataGridView2
            // 
            this.dataGridView2.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(8, 97);
            this.dataGridView2.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersWidth = 51;
            this.dataGridView2.RowTemplate.Height = 24;
            this.dataGridView2.Size = new System.Drawing.Size(442, 440);
            this.dataGridView2.TabIndex = 47;
            this.dataGridView2.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellClick);
            this.dataGridView2.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellContentClick);
            // 
            // btn_sua
            // 
            this.btn_sua.BackgroundImage = global::WindowsFormsApp4.Properties.Resources.update;
            this.btn_sua.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_sua.Font = new System.Drawing.Font("Times New Roman", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btn_sua.Location = new System.Drawing.Point(749, 488);
            this.btn_sua.Margin = new System.Windows.Forms.Padding(2);
            this.btn_sua.Name = "btn_sua";
            this.btn_sua.Size = new System.Drawing.Size(50, 50);
            this.btn_sua.TabIndex = 58;
            this.btn_sua.UseVisualStyleBackColor = true;
            this.btn_sua.Click += new System.EventHandler(this.btn_sua_Click);
            // 
            // btn_them
            // 
            this.btn_them.BackgroundImage = global::WindowsFormsApp4.Properties.Resources.add;
            this.btn_them.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_them.Font = new System.Drawing.Font("Times New Roman", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btn_them.Location = new System.Drawing.Point(581, 488);
            this.btn_them.Margin = new System.Windows.Forms.Padding(2);
            this.btn_them.Name = "btn_them";
            this.btn_them.Size = new System.Drawing.Size(50, 50);
            this.btn_them.TabIndex = 56;
            this.btn_them.UseVisualStyleBackColor = true;
            this.btn_them.Click += new System.EventHandler(this.btn_them_Click);
            // 
            // btn_xoa
            // 
            this.btn_xoa.BackgroundImage = global::WindowsFormsApp4.Properties.Resources.delete;
            this.btn_xoa.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_xoa.Font = new System.Drawing.Font("Times New Roman", 13.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.btn_xoa.Location = new System.Drawing.Point(665, 488);
            this.btn_xoa.Margin = new System.Windows.Forms.Padding(2);
            this.btn_xoa.Name = "btn_xoa";
            this.btn_xoa.Size = new System.Drawing.Size(50, 50);
            this.btn_xoa.TabIndex = 57;
            this.btn_xoa.UseVisualStyleBackColor = true;
            this.btn_xoa.Click += new System.EventHandler(this.btn_xoa_Click);
            // 
            // btn_Browse
            // 
            this.btn_Browse.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Browse.Location = new System.Drawing.Point(41, 81);
            this.btn_Browse.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Browse.Name = "btn_Browse";
            this.btn_Browse.Size = new System.Drawing.Size(81, 34);
            this.btn_Browse.TabIndex = 1;
            this.btn_Browse.Text = "Browse";
            this.btn_Browse.UseVisualStyleBackColor = true;
            this.btn_Browse.Click += new System.EventHandler(this.btn_Browse_Click);
            // 
            // pic_Anh
            // 
            this.pic_Anh.Location = new System.Drawing.Point(201, 25);
            this.pic_Anh.Margin = new System.Windows.Forms.Padding(2);
            this.pic_Anh.Name = "pic_Anh";
            this.pic_Anh.Size = new System.Drawing.Size(160, 160);
            this.pic_Anh.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pic_Anh.TabIndex = 0;
            this.pic_Anh.TabStop = false;
            // 
            // SanPham
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(895, 559);
            this.Controls.Add(this.btn_sua);
            this.Controls.Add(this.btn_them);
            this.Controls.Add(this.btn_timkiem);
            this.Controls.Add(this.txt_timkiem);
            this.Controls.Add(this.btn_xoa);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txt_showpath);
            this.Controls.Add(this.txt_gia);
            this.Controls.Add(this.txt_tenmon);
            this.Controls.Add(this.txt_mahang);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "SanPham";
            this.Text = "SanPham";
            this.Load += new System.EventHandler(this.SanPham_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Anh)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_sua;
        private System.Windows.Forms.Button btn_them;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_timkiem;
        private System.Windows.Forms.TextBox txt_timkiem;
        private System.Windows.Forms.Button btn_xoa;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btn_Browse;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox pic_Anh;
        private System.Windows.Forms.TextBox txt_showpath;
        private System.Windows.Forms.TextBox txt_gia;
        private System.Windows.Forms.TextBox txt_tenmon;
        private System.Windows.Forms.TextBox txt_mahang;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView dataGridView2;
    }
}