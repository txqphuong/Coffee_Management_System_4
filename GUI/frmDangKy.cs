using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp4.GUI
{
    public partial class frmDangKy : Form
    {
        private MongoClient client;
        private IMongoDatabase database;
        private IMongoCollection<BsonDocument> collection;
        public frmDangKy()
        {
            InitializeComponent();
            string connectionString = "mongodb://localhost:27017";
            client = new MongoClient(connectionString);
            database = client.GetDatabase("Quanlycafe");
            collection = database.GetCollection<BsonDocument>("quanlyquan");
        }
        public bool KiemTraTenDangNhapTonTai(string tendn)
        {

            var filter = Builders<BsonDocument>.Filter.Eq("nguoidung.tendn", tendn);

            // Thực hiện truy vấn để kiểm tra xem tài khoản đã tồn tại hay chưa
            var existingAccount = collection.Find(filter).FirstOrDefault();

            // Nếu tài khoản đã tồn tại, trả về true
            // Ngược lại, trả về false
            return existingAccount != null;
        }
        private void btndangky_Click(object sender, EventArgs e)
        {
            
            try
            {
                string username = txtuser.Text;
                string pass = txtpass.Text;
                string[] chdn = {txtch1.Text, txtch2.Text};
                bool tkDaTonTai = KiemTraTenDangNhapTonTai(username);
                if(tkDaTonTai)
                {
                    MessageBox.Show("Tên đăng nhập đã tồn tại");
                    txtuser.Text = txtpass.Text = txtch1.Text = txtch2.Text = "";
                    return;
                }

                var tkmoi = new BsonDocument
                {
                    { "tendn", username },
                    { "pass", pass },
                    { "chdn", new BsonArray(chdn) }
                 };

                var filter = Builders<BsonDocument>.Filter.Empty;

                var update = Builders<BsonDocument>.Update.Push("nguoidung", tkmoi);

                collection.UpdateOne(filter, update);

                MessageBox.Show("Đăng kí thành công!!!");

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
        }

    }
}
