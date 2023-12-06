using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using WindowsFormsApp4.GUI;

namespace WindowsFormsApp4
{
    public partial class Login : Form
    {
        private MongoClient client;
        private IMongoDatabase database;
        private IMongoCollection<BsonDocument> collection;
        public Login()
        {
            InitializeComponent();
            string connectionString = "mongodb://localhost:27017";
            client = new MongoClient(connectionString);
            database = client.GetDatabase("Quanlycafe");
            collection = database.GetCollection<BsonDocument>("quanlyquan");
        }

        private void Login_Load(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;

            var filter = Builders<BsonDocument>.Filter.Eq("nguoidung.tendn", username) &
                         Builders<BsonDocument>.Filter.Eq("nguoidung.pass", password);

            var result = collection.Find(filter).FirstOrDefault();

            if (result != null)
            {
                MessageBox.Show("Đăng nhập thành công!");
                BanHang bh = new BanHang(username);
                TrangChu tc = new TrangChu(username);
                tc.Show();
                tc.FormClosed += Tc_FormClosed;
                this.Hide();
            }
            else
            {
                MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu!");
            }

        }

        private void Tc_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Show();
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            frmDangKy frmDangKy = new frmDangKy();
            frmDangKy.Show();
        }

        private void btnQuenMK_Click(object sender, EventArgs e)
        {
            frmQuenMatKhau frmQuenMatKhau = new frmQuenMatKhau();
            frmQuenMatKhau.Show();
        }
    }
    
}
