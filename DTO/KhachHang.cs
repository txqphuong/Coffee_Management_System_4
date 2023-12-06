using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp4
{
    public class KhachHang
    {
        public KhachHang(string name, string phone)
        {
            this.name = name;
            this.SDT = phone;
        }
        public KhachHang() { }


        public string name { get; set; }

        public string SDT { get; set; }
    }
}
