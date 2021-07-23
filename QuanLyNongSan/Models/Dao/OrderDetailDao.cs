using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuanLyNongSan.Models;
namespace QuanLyNongSan.Models.Dao
{
    public class OrderDetailDao
    {
        NongSanVN db = null;
        public OrderDetailDao()
        {
            db = new NongSanVN();
        }
        public bool Insert(OrderDetail detail)
        {
            try
            {
                db.OrderDetails.Add(detail);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;

            }
        }
    }
}