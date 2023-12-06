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
    public partial class SanPham : Form
    {
        private MongoClient client;
        private IMongoDatabase database;
        private IMongoCollection<BsonDocument> moncollection;

        public SanPham()
        {
            InitializeComponent();
            string connectionString = "mongodb://localhost:27017";
            client = new MongoClient(connectionString);
            database = client.GetDatabase("Quanlycafe");
            moncollection = database.GetCollection<BsonDocument>("quanlyquan");
        }
        void LoadData()
        {
            var filter = Builders<BsonDocument>.Filter.Empty;
            var result = moncollection.Find(filter).FirstOrDefault();

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
                        anh = image, 
                    };
                    monList.Add(monObject);
                }

                var imageColumn = new DataGridViewImageColumn
                {
                    HeaderText = "Image",
                    DataPropertyName = "anh",
                    ImageLayout = DataGridViewImageCellLayout.Stretch,
                    Width = 100,
                };

                dataGridView2.Columns.Add(imageColumn);


                dataGridView2.RowTemplate.Height = 150;
                dataGridView2.DefaultCellStyle.Padding = new Padding(0, 10, 0, 10);

                dataGridView2.DataSource = monList;
                DataGridViewTextBoxColumn soluongColumn = new DataGridViewTextBoxColumn();
             
            }
        }
        private void SanPham_Load(object sender, EventArgs e)
        {
            LoadData();
            dataGridView2.CellClick += dataGridView2_CellContentClick;

        }

        private void btn_Browse_Click(object sender, EventArgs e)
        {
            DialogResult dr = openFileDialog1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                pic_Anh.Image = Image.FromFile(openFileDialog1.FileName);
                txt_showpath.Text = openFileDialog1.FileName;
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btn_them_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy thông tin món từ các trường nhập liệu
                string maMon = txt_mahang.Text;
                string tenMon = txt_tenmon.Text;
                int gia = int.Parse(txt_gia.Text);
                string anh = txt_showpath.Text.Replace("\\", "\\\\");

                // Tạo một document BSON cho món mới
                var monMoi = new BsonDocument
                {
                    { "_id", maMon },
                    { "tenmon", tenMon },
                    { "gia", gia },
                    { "anh", anh }
                };

                // Tạo filter để xác định tài liệu cần cập nhật, trong trường hợp này là tất cả tài liệu
                var filter = Builders<BsonDocument>.Filter.Empty;

                // Tạo update để thêm món mới vào mảng "mon"
                var update = Builders<BsonDocument>.Update.Push("mon", monMoi);

                // Thực hiện cập nhật tài liệu trong collection "quanlyquan"
                moncollection.UpdateOne(filter, update);

                // Thông báo thành công hoặc xử lý các tác vụ khác sau khi thêm
                MessageBox.Show("Món đã được thêm vào.");

                // Tải lại dữ liệu hoặc làm bất kỳ điều gì bạn cần sau khi thêm
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
        }

        private void btn_xoa_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy _id của món cần xóa
                string maMonCanXoa = txt_mahang.Text; // Đây có thể là mã món hoặc một thông tin duy nhất để xác định món cần xóa

                // Hiển thị hộp thoại xác nhận
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa món này?", "Xác nhận xóa món", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    // Tạo filter để xác định tài liệu cần cập nhật
                    var filter = Builders<BsonDocument>.Filter.Empty;

                    // Tạo update để xóa món có _id tương ứng khỏi mảng "mon"
                    var update = Builders<BsonDocument>.Update.PullFilter("mon", Builders<BsonDocument>.Filter.Eq("_id", maMonCanXoa));

                    // Thực hiện cập nhật tài liệu trong collection "quanlyquan"
                    var updateResult = moncollection.UpdateOne(filter, update);

                    if (updateResult.ModifiedCount > 0)
                    {
                        // Thông báo thành công sau khi xóa
                        MessageBox.Show("Món đã được xóa khỏi.");
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy món để xóa.");
                    }

                    // Tải lại dữ liệu hoặc làm bất kỳ điều gì bạn cần sau khi xóa
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
        }

        private void btn_sua_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy thông tin món cần cập nhật
                string maMonCanSua = txt_mahang.Text;
                string tenMonMoi = txt_tenmon.Text;
                int giaMoi = int.Parse(txt_gia.Text);
                string anhMoi = txt_showpath.Text.Replace("\\", "\\\\");

                // Hiển thị hộp thoại xác nhận
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn cập nhật món này?", "Xác nhận cập nhật món", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    // Tạo filter để xác định tài liệu cần cập nhật
                    var filter = Builders<BsonDocument>.Filter.Empty;

                    // Tạo update để cập nhật thông tin món trong mảng "mon"
                    var update = Builders<BsonDocument>.Update.Set("mon.$[elem].tenmon", tenMonMoi)
                                                               .Set("mon.$[elem].gia", giaMoi)
                                                               .Set("mon.$[elem].anh", anhMoi);

                    // Tạo mảng điều kiện để áp dụng cập nhật cho món có _id tương ứng
                    var arrayFilters = new List<ArrayFilterDefinition>
            {
                new BsonDocumentArrayFilterDefinition<BsonDocument>(new BsonDocument("elem._id", maMonCanSua))
            };

                    // Thực hiện cập nhật tài liệu trong collection "quanlyquan"
                    var updateResult = moncollection.UpdateOne(filter, update, new UpdateOptions { ArrayFilters = arrayFilters });

                    if (updateResult.ModifiedCount > 0)
                    {
                        // Thông báo thành công sau khi cập nhật
                        MessageBox.Show("Món đã được cập nhật thành công.");
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy món để cập nhật.");
                    }

                    // Tải lại dữ liệu hoặc làm bất kỳ điều gì bạn cần sau khi cập nhật

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
        }

        private void btn_timkiem_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy tên món từ TextBox nhập liệu
                string tenMonCanTim = txt_timkiem.Text;

                // Tạo filter để tìm kiếm món dựa trên tên món
                var filter = Builders<BsonDocument>.Filter.Empty
                     & Builders<BsonDocument>.Filter.ElemMatch("mon", Builders<BsonDocument>.Filter.Eq("tenmon", tenMonCanTim));

                // Thực hiện tìm kiếm trong collection "quanlyquan" và chỉ trả về phần tử mảng "mon" thỏa điều kiện
                var projection = Builders<BsonDocument>.Projection.Exclude("_id").Include("mon.$");

                var result = moncollection.Find(filter).Project(projection).FirstOrDefault();

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
                            anh = image,
                        };
                        monList.Add(monObject);
                    }

                    // Hiển thị kết quả tìm kiếm trong DataGridView hoặc ListBox
                    dataGridView2.DataSource = monList;
                }
                else
                {
                    // Hiển thị thông báo nếu không tìm thấy món
                    MessageBox.Show("Không tìm thấy món có tên: " + tenMonCanTim);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0 && e.RowIndex < dataGridView2.Rows.Count)
            {
                // Lấy thông tin từ dòng được chọn
                DataGridViewRow selectedRow = dataGridView2.Rows[e.RowIndex];
                txt_mahang.Text = selectedRow.Cells["_id"].Value.ToString();
                txt_tenmon.Text = selectedRow.Cells["tenmon"].Value.ToString();
                txt_gia.Text = selectedRow.Cells["gia"].Value.ToString();

            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
