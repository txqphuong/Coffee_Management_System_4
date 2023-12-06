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
    public partial class HoaDon : Form
    {
        private MongoClient client;
        private IMongoDatabase database;
        private IMongoCollection<BsonDocument> collection;
        string connectionString = "mongodb://localhost:27017";
        public HoaDon(string tennv)
        {
            InitializeComponent();
            client = new MongoClient(connectionString);
            database = client.GetDatabase("Quanlycafe");
            collection = database.GetCollection<BsonDocument>("quanlydonhang");
            label3.Text = tennv;
        }
        private List<DonHang> GetInvoiceData()
        {
            var filter = Builders<BsonDocument>.Filter.Empty;
            var invoices = collection.Find(filter).ToList();

            var invoiceDataList = new List<DonHang>();
            foreach (var invoice in invoices)
            {
                var DonHang = new DonHang
                {
                    name= invoice["khachhang"]["tenkh"].AsString,
                    SDT = invoice["khachhang"]["sdt"].AsString,
                    tennv = invoice["tennv"].AsString,
                  thanhtien = invoice["thanhtien"].AsInt32,
                    ngaylap = invoice["ngaylap"].ToUniversalTime(),
                    Id = invoice["_id"].AsObjectId // Include _id

                };
                invoiceDataList.Add(DonHang);
            }

            return invoiceDataList;
        }

        private void HoaDon_Load(object sender, EventArgs e)
        {
            var invoices = GetInvoiceData();

            // Bind the data to dataGridView1
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.DataSource = invoices;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Detail dt = new Detail(label3.Text);
            dt.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                // Truy xuất các giá trị của dòng được chọn và hiển thị lên các TextBox
                name.Text = row.Cells["name"].Value.ToString();
                sdt.Text = row.Cells["sdt"].Value.ToString();
                id.Text = row.Cells["id"].Value.ToString();
                ngaylap.Text = row.Cells["ngaylap"].Value.ToString();
                tennv.Text = row.Cells["tennv"].Value.ToString();
           thanhtien.Text = row.Cells["thanhtien"].Value.ToString();
            }
        }

        private void btn_them_Click(object sender, EventArgs e)
        {
        }

        private void btn_xoa_Click(object sender, EventArgs e)
        {
            try
            {
                // Check if a row is selected
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    // Get the selected row
                    DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                    // Retrieve the ObjectId of the selected invoice
                    string selectedInvoiceId = Convert.ToString(selectedRow.Cells["Id"].Value); // Make sure the cell name matches the one in your DataGridView

                    // Convert string to ObjectId
                    ObjectId invoiceObjectId = new ObjectId(selectedInvoiceId);

                    // Define the filter for deletion
                    var deleteFilter = Builders<BsonDocument>.Filter.Eq("_id", invoiceObjectId);

                    // Perform the delete operation
                    collection.DeleteOne(deleteFilter);

                    // Show a success message
                    MessageBox.Show("Hóa đơn đã được xóa.");

                    // Refresh the DataGridView
                    var invoices = GetInvoiceData();
                    dataGridView1.DataSource = invoices;
                }
                else
                {
                    MessageBox.Show("No rows selected. Please select a row to delete.");
                }
            }
            catch (Exception ex)
            {
                // Handle any errors that occurred during deletion
                MessageBox.Show("An error occurred while deleting the invoice: " + ex.Message);
            }
        }

        private void btn_sua_Click(object sender, EventArgs e)
        {
            try
            {
                // Check if a row is selected
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    // Get the selected row
                    DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                    // Retrieve the ObjectId of the selected invoice
                    string selectedInvoiceId = Convert.ToString(selectedRow.Cells["Id"].Value); // Ensure the cell name matches your DataGridView

                    // Convert string to ObjectId
                    ObjectId invoiceObjectId = new ObjectId(selectedInvoiceId);

                    // Retrieve updated data from textboxes (assuming the textboxes are defined and contain the updated data)
                    string updatedName = name.Text;
                    string updatedSdt = sdt.Text;
                    string updatedTennv = tennv.Text;
                    int updatedThanhtien = int.Parse(thanhtien.Text); // Add error handling
                    DateTime updatedNgaylap = DateTime.Parse(ngaylap.Text); // Add error handling

                    // Define the filter for the document we're updating
                    var updateFilter = Builders<BsonDocument>.Filter.Eq("_id", invoiceObjectId);

                    // Define the update operations
                    var update = Builders<BsonDocument>.Update
                        .Set("khachhang.tenkh", updatedName)
                        .Set("khachhang.sdt", updatedSdt)
                        .Set("tennv", updatedTennv)
                        .Set("thanhtien", updatedThanhtien)
                        .Set("ngaylap", updatedNgaylap);

                    // Perform the update operation
                    collection.UpdateOne(updateFilter, update);

                    // Show a success message
                    MessageBox.Show("Hóa đơn đã được cập nhật.");

                    // Refresh the DataGridView
                    var invoices = GetInvoiceData();
                    dataGridView1.DataSource = invoices;
                }
                else
                {
                    MessageBox.Show("No rows selected. Please select a row to update.");
                }
            }
            catch (FormatException formatEx)
            {
                // Handle format exceptions
                MessageBox.Show("Error in input format: " + formatEx.Message);
            }
            catch (Exception ex)
            {
                // Handle any other errors
                MessageBox.Show("An error occurred while updating the invoice: " + ex.Message);
            }
        }
    }
}
