using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuanLyNongSan.Models;
namespace QuanLyNongSan.Models.Dao
{
    public class NongSanDao
    {
        NongSanVN db = null;
        public NongSanDao()
        {
            db = new NongSanVN();
        }
        public NongSan ViewDetail(long id)
        {
            return db.NongSans.Find(id);
        }
    }
}