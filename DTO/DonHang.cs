using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp4.DTO;
namespace WindowsFormsApp4
{
    public class DonHang
    {
        public string name { get; set; }

        public string SDT { get; set; }
        public ObjectId Id { get; set; } // Include the _id field
        public DateTime ngaylap { get; set; }

        public string tennv { get; set; }
        public double thanhtien { get; set; }
        public List<Detail> dt { get; set; }

    }
}
