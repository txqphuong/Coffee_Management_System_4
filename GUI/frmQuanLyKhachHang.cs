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

namespace WindowsFormsApp4
{
    public partial class frmQuanLyKhachHang : Form
    {
        private MongoClient client;
        private IMongoDatabase database;
        private IMongoCollection<BsonDocument> collection;
        public frmQuanLyKhachHang()
        {
            InitializeComponent();
            client = new MongoClient("mongodb://localhost:27017");
            database = client.GetDatabase("Quanlycafe");
            collection = database.GetCollection<BsonDocument>("quanlydonhang");
            LoadData();
        }
        public void LoadData()
        {
            dgvData.DataSource = getData();
            dgvData.Columns[0].HeaderText = "Họ tên khách hàng";
            dgvData.Columns[1].HeaderText = "Số điện thoại";
            //dgvData.Columns[2].HeaderText = "Tổng tiền đã mua";
            dgvData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            foreach (DataGridViewColumn column in dgvData.Columns)
            {
                //column.HeaderCell.Style.Font = new Font("Segoe UI", 12, FontStyle.Bold);
                column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }

        }
        public List<KhachHang> getData()
        {

            var filter = Builders<BsonDocument>.Filter.Ne("khachhang", BsonNull.Value);
            var projection = Builders<BsonDocument>.Projection.Include("khachhang");

            var bsonResult = collection.Find(filter).Project(projection).ToList();

            List<KhachHang> khachHangList = new List<KhachHang>();

            foreach (var bsonDocument in bsonResult)
            {
                var khachHangDocument = bsonDocument["khachhang"].AsBsonDocument;
                KhachHang khachHang = new KhachHang
                {
                    name = khachHangDocument["tenkh"].AsString,
                    SDT = khachHangDocument["sdt"].AsString
                };
                khachHangList.Add(khachHang);
            }

            return khachHangList;
        }
        public List<KhachHang> TimKiemKhachHang(string tenKhachHang, string soDienThoai)
        {
            var filterBuilder = Builders<BsonDocument>.Filter;
            var projectionBuilder = Builders<BsonDocument>.Projection;

            // Tạo bộ lọc ban đầu, bỏ qua các khách hàng có giá trị "khachhang" là null
            var filter = filterBuilder.Ne("khachhang", BsonNull.Value);

            // Nếu có giá trị tên khách hàng được nhập, thêm nó vào bộ lọc
            if (!string.IsNullOrEmpty(tenKhachHang))
            {
                filter = filter & filterBuilder.Eq("khachhang.tenkh", tenKhachHang);
            }

            // Nếu có giá trị số điện thoại được nhập, thêm nó vào bộ lọc
            if (!string.IsNullOrEmpty(soDienThoai))
            {
                filter = filter & filterBuilder.Eq("khachhang.sdt", soDienThoai);
            }

            // Chọn các trường bạn muốn hiển thị
            var projection = projectionBuilder
                .Include("khachhang.tenkh")
                .Include("khachhang.sdt");

            // Thực hiện tìm kiếm và lấy kết quả
            var bsonResult = collection.Find(filter).Project(projection).ToList();

            List<KhachHang> khachHangList = new List<KhachHang>();

            foreach (var bsonDocument in bsonResult)
            {
                var khachHangDocument = bsonDocument["khachhang"].AsBsonDocument;
                KhachHang khachHang = new KhachHang
                {
                    name = khachHangDocument["tenkh"].AsString,
                    SDT = khachHangDocument["sdt"].AsString
                };
                khachHangList.Add(khachHang);
            }

            return khachHangList;
        }
        private void frmQuanLyKhachHang_Load(object sender, EventArgs e)
        {

        }
        private void btntim_Click(object sender, EventArgs e)
        {
            List<KhachHang> list = TimKiemKhachHang(txtname.Text, txtsdt.Text);
            dgvData.DataSource = list;
        }

        private void dgvData_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            
        }
        public long DemSoLuongHoaDonTheoTenVaSDT(string tenKhachHang, string soDienThoai)
        {
            var filterBuilder = Builders<BsonDocument>.Filter;

            // Tạo một bộ lọc để tìm các hóa đơn của khách hàng có tên và số điện thoại cụ thể
            var filter = filterBuilder.Eq("khachhang.tenkh", tenKhachHang) &
                         filterBuilder.Eq("khachhang.sdt", soDienThoai);

            // Thực hiện tìm kiếm sử dụng bộ lọc và đếm kết quả
            long soLuongHoaDon = collection.CountDocuments(filter);

            return soLuongHoaDon;
        }
        public int TinhTongGiaTriHoaDon(string tenKhachHang, string soDienThoai)
        {
            var filterBuilder = Builders<BsonDocument>.Filter;

            // Tạo một bộ lọc để tìm các hóa đơn của khách hàng có tên và số điện thoại cụ thể
            var filter = filterBuilder.Eq("khachhang.tenkh", tenKhachHang) &
                         filterBuilder.Eq("khachhang.sdt", soDienThoai);

            // Chọn trường "thanhtien" để tính tổng giá trị
            var projection = Builders<BsonDocument>.Projection.Include("thanhtien");

            // Thực hiện tìm kiếm sử dụng bộ lọc và chọn trường "thanhtien"
            var bsonResult = collection.Find(filter).Project(projection).ToList();

            // Tính tổng giá trị hóa đơn
            int tongGiaTriHoaDon = 0;

            foreach (var bsonDocument in bsonResult)
            {
                tongGiaTriHoaDon += bsonDocument["thanhtien"].AsInt32;
            }

            return tongGiaTriHoaDon;
        }
        private void dgvData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvData.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvData.SelectedRows[0];

                // Lấy dữ liệu từ các cột của dòng được chọn và hiển thị lên các TextBox
                txtttten.Text = selectedRow.Cells[0].Value.ToString();
                txtttsdt.Text = selectedRow.Cells[1].Value.ToString();
                txttotal.Text = DemSoLuongHoaDonTheoTenVaSDT(txtttten.Text, txtttsdt.Text).ToString();
                txttongtien.Text = TinhTongGiaTriHoaDon(txtttten.Text, txtttsdt.Text).ToString();
            }
        }
    }
}
