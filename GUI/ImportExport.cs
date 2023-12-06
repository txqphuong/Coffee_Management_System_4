using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WindowsFormsApp4
{
    public partial class ImportExport : Form
    {
        private MongoClient client;
        private IMongoDatabase database;
        private IMongoCollection<BsonDocument> c1;
        private IMongoCollection<BsonDocument> c2;
        public ImportExport()
        {
            InitializeComponent();
            string connectionString = "mongodb://localhost:27017";
            client = new MongoClient(connectionString);
            database = client.GetDatabase("Quanlycafe");
            c1 = database.GetCollection<BsonDocument>("quanlyquan");
            c2 = database.GetCollection<BsonDocument>("quanlydonhang");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                // Tạo SaveFileDialog để cho phép người dùng chọn đường dẫn xuất file
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "JSON Files (*.json)|*.json";
                saveFileDialog.Title = "Export to JSON";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string jsonFilePath = saveFileDialog.FileName;

                    // Xác định bộ sưu tập cần xuất
                    IMongoCollection<BsonDocument> collectionToExport;
                    string successMessage;
                    if (quanlyquan.Checked)
                    {
                        collectionToExport = c1;
                        successMessage = "Dữ liệu đã được xuất từ quanlyquan sang JSON thành công!";
                    }
                    else
                    {
                        collectionToExport = c2;
                        successMessage = "Dữ liệu đã được xuất từ quanlydonhang sang JSON thành công!";
                    }

                    // Lấy dữ liệu từ MongoDB
                    var filter = Builders<BsonDocument>.Filter.Empty;
                    var cursor = collectionToExport.Find(filter).ToCursor();

                    // Tạo StreamWriter để ghi dữ liệu vào tệp JSON
                    using (StreamWriter writer = new StreamWriter(jsonFilePath))
                    {
                        // Bắt đầu mảng JSON
                        writer.WriteLine("[");

                        // Chuyển đổi mỗi tài liệu sang JSON và ghi vào tệp, cách nhau bằng dấu phẩy
                        bool firstDocument = true;
                        foreach (var document in cursor.ToEnumerable())
                        {
                            if (!firstDocument)
                                writer.WriteLine(",");

                            var json = document.ToJson();
                            writer.Write(json);

                            firstDocument = false;
                        }

                        // Kết thúc mảng JSON
                        writer.WriteLine("]");
                    }

                    MessageBox.Show(successMessage);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xuất dữ liệu: {ex.Message}");
            }

        }

        private void ImportExport_Load(object sender, EventArgs e)
        {

        }

        private void import_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "JSON Files (*.json)|*.json";
                openFileDialog.Title = "Import from JSON";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string jsonFilePath = openFileDialog.FileName;
                    string jsonContent = File.ReadAllText(jsonFilePath).Trim();

                    // Đảm bảo nội dung JSON không trống
                    if (string.IsNullOrEmpty(jsonContent))
                    {
                        throw new Exception("Tệp JSON trống.");
                    }

                    IMongoCollection<BsonDocument> collectionToImport;
                    if (radioButton1.Checked)
                    {
                        collectionToImport = c1;
                    }
                    else if (radioButton2.Checked)
                    {
                        collectionToImport = c2;
                    }
                    else
                    {
                        throw new Exception("Không có bộ sưu tập nào được chọn để nhập.");
                    }

                    // Phân tích nội dung JSON
                    if (jsonContent.StartsWith("[") && jsonContent.EndsWith("]")) // it's an array
                    {
                        var documents = BsonSerializer.Deserialize<List<BsonDocument>>(jsonContent);
                        collectionToImport.InsertMany(documents);
                    }
                    else // đây là một tài liệu đơn lẻ
                    {
                        var document = BsonDocument.Parse(jsonContent);
                        collectionToImport.InsertOne(document);
                    }

                    MessageBox.Show("Dữ liệu đã được nhập từ JSON thành công!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi nhập dữ liệu: {ex.Message}");
            }
        }
    }
}
