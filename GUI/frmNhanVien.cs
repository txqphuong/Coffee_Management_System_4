using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MongoDB.Driver;
using MongoDB.Bson;
using WindowsFormsApp4.DTO;

namespace WindowsFormsApp4
{
    public partial class frmNhanVien : Form
    {
        private MongoClient client;
        private IMongoDatabase database;
        private IMongoCollection<BsonDocument> collection;
        public frmNhanVien()
        {
            InitializeComponent();
            string connectionString = "mongodb://localhost:27017";
            client = new MongoClient(connectionString);
            database = client.GetDatabase("Quanlycafe");
            collection = database.GetCollection<BsonDocument>("quanlyquan");

        }

        void LoadData()
        {
            var filter = Builders<BsonDocument>.Filter.Empty;
            var result = collection.Find(filter).FirstOrDefault();

            if (result != null && result.Contains("nhanvien"))
            {
                var nvarrray = result["nhanvien"].AsBsonArray;
                var nvlist = new List<NhanVien>();

                foreach (var nv in nvarrray)
                {
                    var nvobj = new NhanVien
                    {
                        id = nv["_id"].AsString,
                        ten = nv["ten"].AsString,
                        chucVu = nv["chucvu"].AsString,
                        diaChi = nv["diachi"].AsString,
                        sdt = nv["sdt"].AsString,
                    };
                    nvlist.Add(nvobj);
                }
                dataGridView1.RowTemplate.Height = 150;
                dataGridView1.DefaultCellStyle.Padding = new Padding(0, 10, 0, 10);

                dataGridView1.DataSource = nvlist;
                DataGridViewTextBoxColumn soluongColumn = new DataGridViewTextBoxColumn();

            }
        }

        private void frmNhanVien_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                // Lấy tên nhân viên từ TextBox nhập liệu
                string tenNhanVienCanTim = txtTimKiem.Text;

                // Tạo filter để tìm kiếm nhân viên dựa trên tên nhân viên
                var filter = Builders<BsonDocument>.Filter.Eq("nhanvien.ten", tenNhanVienCanTim);

                // Tạo projection để lọc kết quả
                var projection = Builders<BsonDocument>.Projection.ElemMatch("nhanvien", Builders<BsonDocument>.Filter.Eq("ten", tenNhanVienCanTim));

                // Thực hiện tìm kiếm trong collection "quanlynhanvien" với projection
                var searchResults = collection.Find(filter).Project(projection).ToList();

                var nhanVienList = new List<NhanVien>();

                foreach (var doc in searchResults)
                {
                    foreach (var nhanVienDoc in doc["nhanvien"].AsBsonArray)
                    {
                        // Tạo đối tượng NhanVien từ dữ liệu trong MongoDB
                        var nhanVien = new NhanVien
                        {
                            id = nhanVienDoc["_id"].AsString,
                            ten = nhanVienDoc["ten"].AsString,
                            chucVu = nhanVienDoc["chucvu"].AsString,
                            sdt = nhanVienDoc["sdt"].AsString,
                            diaChi = nhanVienDoc["diachi"].AsString
                        };

                        nhanVienList.Add(nhanVien);
                    }
                }

                if (nhanVienList.Count > 0)
                {
                    // Hiển thị kết quả tìm kiếm trong DataGridView hoặc ListBox
                    dataGridView1.DataSource = nhanVienList;
                }
                else
                {
                    // Hiển thị thông báo nếu không tìm thấy nhân viên
                    MessageBox.Show("Không tìm thấy nhân viên có tên: " + tenNhanVienCanTim);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            // Truy xuất giá trị từ các TextBox
            string ma = txtMa.Text;
            string ten = txtTen.Text;
            string chucVu = txtChucVu.Text;
            string sdt = txtSDT.Text;
            string diaChi = txtDiaChi.Text;

            // Tạo một đối tượng BsonDocument cho tài liệu nhân viên
            BsonDocument nhanVienDocument = new BsonDocument
            {
                { "_id", ma },
                { "ten", ten },
                { "chucvu", chucVu },
                { "sdt", sdt },
                { "diachi", diaChi }
            };

            // Tạo một câu lệnh update để thêm tài liệu nhân viên vào mảng nhanvien
            var filter = Builders<BsonDocument>.Filter.Empty;
            var update = Builders<BsonDocument>.Update.Push("nhanvien", nhanVienDocument);

            // Thực hiện câu lệnh update
            collection.UpdateOne(filter, update);

            // Hiển thị thông báo khi dữ liệu được chèn thành công
            MessageBox.Show("Nhân viên đã được chèn thành công!");

            // Xóa các giá trị nhập trong các TextBox sau khi chèn
            txtMa.Clear();
            txtTen.Clear();
            txtChucVu.Clear();
            txtSDT.Clear();
            txtDiaChi.Clear();
            LoadData();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                string ma = txtMa.Text;
                // Hiển thị hộp thoại xác nhận
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa nhân viên này?", "Xác nhận xóa nhân viên", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    // Tạo filter để xác định tài liệu cần cập nhật
                    var filter = Builders<BsonDocument>.Filter.Empty;

                    // Tạo update để xóa món có _id tương ứng khỏi mảng nhanvien
                    var update = Builders<BsonDocument>.Update.PullFilter("nhanvien", Builders<BsonDocument>.Filter.Eq("_id", ma));

                    // Thực hiện cập nhật tài liệu trong collection "quanlyquan"
                    var updateResult = collection.UpdateOne(filter, update);

                    if (updateResult.ModifiedCount > 0)
                    {
                        // Thông báo thành công sau khi xóa
                        MessageBox.Show("Nhân viên đã được xóa khỏi.");
                        // Xóa các giá trị nhập trong các TextBox sau khi cập nhật
                        txtMa.Clear();
                        txtTen.Clear();
                        txtChucVu.Clear();
                        txtSDT.Clear();
                        txtDiaChi.Clear();
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy nhân viên để xóa.");
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

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                // Truy xuất giá trị mới từ các TextBox
                string ma = txtMa.Text;
                string ten = txtTen.Text;
                string chucVu = txtChucVu.Text;
                string sdt = txtSDT.Text;
                string diaChi = txtDiaChi.Text;

                // Hiển thị hộp thoại xác nhận
                DialogResult rs = MessageBox.Show("Bạn có chắc chắn muốn cập nhật nhân viên này?", "Xác nhận cập nhật nhân viên", MessageBoxButtons.YesNo);

                if (rs == DialogResult.Yes)
                {
                    // Xác định điều kiện để xác định bản ghi cần sửa đổi (dựa trên "_id")
                    var filter = Builders<BsonDocument>.Filter.Empty;

                    // Tạo một đối tượng BsonDocument mới chứa thông tin cập nhật
                    var update = Builders<BsonDocument>.Update
                        .Set("nhanvien.$[elem].ten", ten)
                        .Set("nhanvien.$[elem].chucvu", chucVu)
                        .Set("nhanvien.$[elem].sdt", sdt)
                        .Set("nhanvien.$[elem].diachi", diaChi);


                    // Tạo mảng điều kiện để áp dụng cập nhật cho món có _id tương ứng
                    var arrayFilters = new List<ArrayFilterDefinition>
            {
                new BsonDocumentArrayFilterDefinition<BsonDocument>(new BsonDocument("elem._id", ma))
            };

                    // Thực hiện cập nhật tài liệu trong collection "quanlyquan"
                    var updateResult = collection.UpdateOne(filter, update, new UpdateOptions { ArrayFilters = arrayFilters });

                    if (updateResult.ModifiedCount > 0)
                    {
                        // Hiển thị thông báo khi dữ liệu được cập nhật thành công
                        MessageBox.Show("Thông tin nhân viên đã được cập nhật thành công!");

                        // Xóa các giá trị nhập trong các TextBox sau khi cập nhật
                        txtMa.Clear();
                        txtTen.Clear();
                        txtChucVu.Clear();
                        txtSDT.Clear();
                        txtDiaChi.Clear();
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy nhân viên có mã " + ma);
                    }
                    LoadData();
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                // Truy xuất các giá trị của dòng được chọn và hiển thị lên các TextBox
                txtMa.Text = row.Cells["id"].Value.ToString();
                txtTen.Text = row.Cells["ten"].Value.ToString();
                txtChucVu.Text = row.Cells["chucvu"].Value.ToString();
                txtSDT.Text = row.Cells["sdt"].Value.ToString();
                txtDiaChi.Text = row.Cells["diachi"].Value.ToString();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                // Truy xuất các giá trị của dòng được chọn và hiển thị lên các TextBox
                txtMa.Text = row.Cells["id"].Value.ToString();
                txtTen.Text = row.Cells["ten"].Value.ToString();
                txtChucVu.Text = row.Cells["chucvu"].Value.ToString();
                txtSDT.Text = row.Cells["sdt"].Value.ToString();
                txtDiaChi.Text = row.Cells["diachi"].Value.ToString();
            }
        }
    }
}
