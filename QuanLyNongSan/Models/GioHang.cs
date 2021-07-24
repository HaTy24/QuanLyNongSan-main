using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuanLyNongSan.Models;

namespace QuanLyNongSan.Models
{
    public class GioHang
    {
        public NongSan nongsan { set; get; }
        public int Quantity { set; get; }
        public int ThanhTien
        {
            get
            {
                return (int)(Quantity * nongsan.Price);
            }
        }
    }
}