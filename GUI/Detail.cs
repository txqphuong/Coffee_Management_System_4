using MongoDB.Bson;
using MongoDB.Driver;
using SharpCompress.Common;
using System;
using System.Collections.Generic;

using Microsoft.Office.Interop.Word;
using System.Runtime.InteropServices;

using System.Linq;


using System.Windows.Forms;
using DocumentFormat.OpenXml.Wordprocessing;


namespace WindowsFormsApp4
{
    public partial class Detail : Form
    {
        private List<Chitiet> detaillist; // Declare it here

        private MongoClient client;
        private IMongoDatabase database;
        private IMongoCollection<BsonDocument> collection;
        public Detail(string tennv)
        {
            InitializeComponent();
            this.tennv.Text = tennv;
            string connectionString = "mongodb://localhost:27017";
            client = new MongoClient(connectionString);
            database = client.GetDatabase("Quanlycafe");
            collection = database.GetCollection<BsonDocument>("quanlydonhang");
        }
        private string customerName; // Property to store the customer's name
        private string ngaylap;

        void LoadData(string id)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(id));
            var result = collection.Find(filter).FirstOrDefault();

            if (result != null && result.Contains("detail"))
            {
                if (result.Contains("khachhang") && result["khachhang"].IsBsonDocument)
                {
                    var khachhang = result["khachhang"].AsBsonDocument;
                    if (khachhang.Contains("tenkh"))
                    {
                        customerName = khachhang["tenkh"].AsString;
                    }
                    if (khachhang.Contains("ngaylap"))
                    {
                        ngaylap = khachhang["ngaylap"].AsString;
                    }
                }

                var detailArray = result["detail"].AsBsonArray;
                detaillist = new List<Chitiet>(); // Initialize the class-level detaillist

                foreach (var detail in detailArray)
                {
                    var chitiet = new Chitiet
                    {
                        tenmon = detail["tenmon"].AsString,
                        dongia = detail["dongia"].AsInt32,
                        soluong = detail["soluong"].AsInt32,
                    };
                    detaillist.Add(chitiet);
                }
                dataGridView1.DataSource = detaillist;
            }
        }

        private void Detail_Load(object sender, EventArgs e)
        {
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadData(textBox1.Text);
        }
        private string filePath; // Declare the filePath variable at the class level

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void btn_inhoadon_Click(object sender, EventArgs e)
        {
            filePath = "D:\\NoSQL\\WindowsFormsApp4\\document.docx";
            ExportToWord();

        }
        private void ExportToWord()
        {

            var wordApp = new Microsoft.Office.Interop.Word.Application();
            var doc = wordApp.Documents.Add();
            var heading = doc.Paragraphs.Add();
            var headingRange = heading.Range;

            headingRange.Text = "Hóa Đơn";

            headingRange.Font.Size = 24; // Kích thước font 24
            headingRange.Font.Bold = 1; // Font đậm
            headingRange.Font.Name = "Arial"; // Tên font
            headingRange.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
            headingRange.InsertParagraphAfter();

            heading.Range.Text = "Nhân Viên bán hàng: " + tennv.Text + "\nKhách Hàng: " + customerName;


            heading.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
            heading.Range.InsertParagraphAfter();
            var table = doc.Tables.Add(heading.Range, detaillist.Count + 1, 4); // Thêm một cột để tính tổng tiền
            table.Borders.Enable = 1;

            table.Cell(1, 1).Range.Text = "Item";
            table.Cell(1, 2).Range.Text = "Price";
            table.Cell(1, 3).Range.Text = "Quantity";
            table.Cell(1, 4).Range.Text = "Total";
            for (int i = 0; i < detaillist.Count; i++)
            {
                table.Cell(i + 2, 1).Range.Text = detaillist[i].tenmon;
                table.Cell(i + 2, 2).Range.Text = detaillist[i].dongia.ToString();
                table.Cell(i + 2, 3).Range.Text = detaillist[i].soluong.ToString();

                // Tính tổng tiền và đặt giá trị vào cột Total
                int totalPrice = detaillist[i].dongia * detaillist[i].soluong;
                table.Cell(i + 2, 4).Range.Text = totalPrice.ToString() + "\n" + "\n";

            }
            heading.Range.InsertParagraphAfter(); // Thêm một dòng trống
            heading.Range.InsertParagraphAfter(); // Thêm một dòng trống

            int grandTotal = detaillist.Sum(item => item.dongia * item.soluong);

            heading.Range.Text = "Tổng cộng tiền phải trả:" + grandTotal.ToString();

            doc.SaveAs2(filePath);
            doc.Close();
            Marshal.ReleaseComObject(doc);

            wordApp.Quit();
            Marshal.ReleaseComObject(wordApp);

            // Inform the user that the export is complete
            MessageBox.Show("In hóa đơn thành công.", "Đã xuất", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

    }
}