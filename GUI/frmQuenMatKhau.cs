using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp4.GUI
{
    public partial class frmQuenMatKhau : Form
    {
        private MongoClient client;
        private IMongoDatabase database;
        private IMongoCollection<BsonDocument> collection;
        public frmQuenMatKhau()
        {
            InitializeComponent();
            string connectionString = "mongodb://localhost:27017";
            client = new MongoClient(connectionString);
            database = client.GetDatabase("Quanlycafe");
            collection = database.GetCollection<BsonDocument>("quanlyquan");
        }

        private void btnkiemtra_Click(object sender, EventArgs e)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("nguoidung.tendn", txtuser.Text);


            var projection = Builders<BsonDocument>.Projection.ElemMatch("nguoidung", Builders<BsonDocument>.Filter.Eq("tendn", txtuser.Text));

            var userAccount = collection.Find(filter).Project(projection).FirstOrDefault();
            if (userAccount == null)
            {
                MessageBox.Show("Tên đăng nhập không tồn tại");
                return;
            }
            var chdnArray = new BsonArray();
            chdnArray.Add(BsonValue.Create(string.Empty));
            chdnArray.Add(BsonValue.Create(string.Empty));
            try
            {
                chdnArray = userAccount["nguoidung"][0]["chdn"].AsBsonArray;

                //// Lấy giá trị đầu tiên và giá trị thứ hai từ mảng "chdn"
                //string value1 = chdnArray[0].AsString;
                //string value2 = chdnArray[1].AsString;
            }
            catch { }
            bool chdn1Matches = chdnArray.Count > 0 && chdnArray[0] == BsonValue.Create(txtch1.Text);
            bool chdn2Matches = chdnArray.Count > 1 && chdnArray[1] == BsonValue.Create(txtch2.Text);
            if (chdn1Matches && chdn2Matches)
            {
                lb3.Text = "Thông tin chính xác, nhập mật khẩu mới!!!";
                grpconfirm.Visible = true;
            }
            else
                lb3.Text = "Thông tin sai";
        }

        private void btnconfirm_Click(object sender, EventArgs e)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("nguoidung.tendn", txtuser.Text);
            var update = Builders<BsonDocument>.Update.Set("nguoidung.$.pass", txtnewpass.Text);
            var result = collection.UpdateOne(filter, update);
            if (result != null)
            {
                MessageBox.Show("Đổi mật khẩu thành công");
                this.Close();
            }
        }

        private void txtuser_TextChanged(object sender, EventArgs e)
        {
            grpconfirm.Visible = false;
        }
    }
}
