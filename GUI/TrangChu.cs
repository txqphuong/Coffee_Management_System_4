using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using WindowsFormsApp4;

namespace WindowsFormsApp4
{
    public partial class TrangChu : Form {
     
        public TrangChu(string textFromForm1)
        {
            InitializeComponent();
            label1.Text = textFromForm1;
            Detail dt = new Detail(label1.Text);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }

        private void TrangChu_Load(object sender, EventArgs e)
        {

        }
        private Form conhientai;

       void OpenChildForm(Form childForm)
        {
            if (conhientai != null)
            {
                conhientai.Close(); 
            }

            conhientai = childForm;
            conhientai.TopLevel = false;
            conhientai.FormBorderStyle = FormBorderStyle.None;
            conhientai.Dock = DockStyle.Fill;

            panelChildForm.Controls.Add(conhientai);
            panelChildForm.Tag = conhientai;

            conhientai.BringToFront(); 
            conhientai.Show();
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            OpenChildForm(new BanHang(label1.Text));

        }

        private void btnSanPham_Click(object sender, EventArgs e)
        {
            OpenChildForm(new SanPham());
        }

        private void panelSlideMenu_Paint(object sender, PaintEventArgs e)
        {

        }
 
        private void imgUser_Click(object sender, EventArgs e)
        {
         
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenChildForm(new HoaDon(label1.Text));


        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmQuanLyKhachHang());

        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenChildForm(new ImportExport());

        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenChildForm(new BackupRestore());
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmNhanVien());
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void panelChildForm_Paint(object sender, PaintEventArgs e)
        {

        }

        private void TrangChu_MaximizedBoundsChanged(object sender, EventArgs e)
        {

        }

        private void TrangChu_LocationChanged(object sender, EventArgs e)
        {
            //this.Location = new Point(0, 0);

        }
    }
}
