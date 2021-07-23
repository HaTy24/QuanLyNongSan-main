using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuanLyNongSan.Models;
namespace QuanLyNongSan.Models.Dao
{
    public class OrderDao
    {
        NongSanVN db = null;
        public OrderDao()
        {
            db = new NongSanVN();
        }
        public long Insert(Order order)
        {
            db.Orders.Add(order);
            db.SaveChanges();
            return order.ID;
        }
    }
}