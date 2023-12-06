using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;


namespace WindowsFormsApp4
{
    public partial class BanHang : Form
    {

        private MongoClient client;
        private IMongoDatabase database;
        private IMongoCollection<BsonDocument> collection;
        public BanHang(String tennv)
        {
            InitializeComponent();
            string connectionString = "mongodb://localhost:27017";
            client = new MongoClient(connectionString);
            database = client.GetDatabase("Quanlycafe");
            collection = database.GetCollection<BsonDocument>("quanlyquan");
            label5.Text = tennv;
            dataGridView2.ColumnCount = 2;
            dataGridView2.Columns[0].Name = "tenmon";
            dataGridView2.Columns[1].Name = "gia";
        }

        private void LoadData()
        {
            var filter = Builders<BsonDocument>.Filter.Empty;
            var result = collection.Find(filter).FirstOrDefault();

            if (result != null && result.Contains("mon"))
            {
                var monArray = result["mon"].AsBsonArray;
                var monList = new List<MenuItem>();

                foreach (var mon in monArray)
                {
                    var imagePath = mon["anh"].AsString;
                    var image = Image.FromFile(imagePath);

                    var monObject = new MenuItem
                    {
                        _id = mon["_id"].AsString,
                        tenmon = mon["tenmon"].AsString,
                        gia = mon["gia"].AsInt32,
                        anh = image, // Store the loaded image
                    };
                    monList.Add(monObject);
                }

                var imageColumn = new DataGridViewImageColumn
                {
                    HeaderText = "Image",
                    DataPropertyName = "anh",
                    ImageLayout = DataGridViewImageCellLayout.Stretch,
                    Width = 120,

                };

                dataGridView1.Columns.Add(imageColumn);


                dataGridView1.RowTemplate.Height = 150;
                dataGridView1.DefaultCellStyle.Padding = new Padding(0, 10, 0, 10);

                dataGridView1.DataSource = monList;
                DataGridViewTextBoxColumn soluongColumn = new DataGridViewTextBoxColumn();
                soluongColumn.Name = "soluong";
                soluongColumn.HeaderText = "Số Lượng";

                dataGridView2.Columns.Add(soluongColumn);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtkhachhang.Text.Length == 0 || txtsdt.Text.Length == 0)
                {
                    MessageBox.Show(" Thiếu thông tin ");
                }
                else
                {
                    int thanhTien = Convert.ToInt32(lblthanhtien.Text);
                    DateTime ngayLap = DateTime.Now;

                    string newHoadonId = "hd" + GenerateRandomString(3);

                    string connectionString = "mongodb://localhost:27017";
                    var client = new MongoClient(connectionString);
                    var database = client.GetDatabase("Quanlycafe");
                    var collection = database.GetCollection<BsonDocument>("quanlydonhang");

                    var detailArray = new BsonArray();

                    foreach (DataGridViewRow row in dataGridView2.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            string tenMon = row.Cells["tenmon"].Value.ToString();
                            int gia = Convert.ToInt32(row.Cells["gia"].Value);
                            int soLuong = Convert.ToInt32(row.Cells["soluong"].Value);

                            var monDocument = new BsonDocument
                    {
                        { "tenmon", tenMon },
                        { "soluong", soLuong },
                        { "dongia", gia }
                    };

                            detailArray.Add(monDocument);
                        }
                    }

                    string tenKhachHang = txtkhachhang.Text;
                    string soDienThoai = txtsdt.Text;

                    var newHoadon = new BsonDocument
            {
                { "_id", ObjectId.GenerateNewId() },
                { "ngaylap", ngayLap },
                { "khachhang", new BsonDocument
                    {
                        { "tenkh", tenKhachHang },
                        { "sdt", soDienThoai }
                    }
                },
                { "detail", detailArray },
                { "tennv", label5.Text },
                { "thanhtien", thanhTien }
            };

                    collection.InsertOne(newHoadon);

                    MessageBox.Show("Thanh toán thành công!");
                    dataGridView2.Rows.Clear(); // Clear dataGridView2
                    txtkhachhang.Clear();
                    txtsdt.Clear();
                }
            }
            catch
            {
                MessageBox.Show("  Vui lòng nhập đủ thông tin ");
            }
        }

        private string GenerateRandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dataGridView1.Rows.Count > 0)
            {
                string tenMon = dataGridView1.Rows[e.RowIndex].Cells["tenmon"].Value.ToString();
                int giaTien = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["gia"].Value);

                bool monDaCo = false;
                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    if (row.Cells["tenmon"].Value != null && row.Cells["tenmon"].Value.ToString() == tenMon)
                    {
                        int soLuong = Convert.ToInt32(row.Cells["soluong"].Value) + 1;
                        row.Cells["soluong"].Value = soLuong;
                        monDaCo = true;
                        break;
                    }
                }

                if (!monDaCo)
                {
                    dataGridView2.Rows.Add(tenMon, giaTien, 1);
                }

                int tongTien = 0;

                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    if (row.Cells["gia"].Value != null && int.TryParse(row.Cells["gia"].Value.ToString(), out giaTien) &&
                        row.Cells["soluong"].Value != null && int.TryParse(row.Cells["soluong"].Value.ToString(), out int soLuong))
                    {
                        tongTien += giaTien * soLuong;
                    }
                }

                lblthanhtien.Text = tongTien.ToString();
            }
        }


        private void label8_ControlRemoved(object sender, ControlEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtkhachhang.Text.Length == 0 || txtsdt.Text.Length == 0)
                {
                    MessageBox.Show(" thiếu");
                }
                else
                {
                    MessageBox.Show(" đủ rồi");
                }
            }
            catch
            {
                MessageBox.Show(" đủ rồi");

            }
        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dataGridView2.Rows.Count)
            {
                dataGridView2.Rows.RemoveAt(e.RowIndex);

            }
            int giaTien = Convert.ToInt32(dataGridView2.Rows[e.RowIndex].Cells["gia"].Value);
            int tongTien = 0;

            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (row.Cells["gia"].Value != null && int.TryParse(row.Cells["gia"].Value.ToString(), out giaTien) &&
                    row.Cells["soluong"].Value != null && int.TryParse(row.Cells["soluong"].Value.ToString(), out int soLuong))
                {
                    tongTien += giaTien * soLuong;
                }
            }

            lblthanhtien.Text = tongTien.ToString();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
