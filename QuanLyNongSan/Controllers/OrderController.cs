using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using QuanLyNongSan.Common;
using QuanLyNongSan.Models;
using QuanLyNongSan.Models.Dao;

namespace QuanLyNongSan.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        private const string OrderSession = "OrderSession";
        // GET: Cart
        public ActionResult Index()
        {
            var cart = Session[OrderSession];
            var list = new List<GioHang>();
            if (cart != null)
            {
                list = (List<GioHang>)cart;
            }
            return View(list);
        }

        public JsonResult DeleteAll()
        {
            Session[OrderSession] = null;
            return Json(new
            {
                status = true
            });
        }

        public JsonResult Delete(long id)
        {
            var sessionCart = (List<GioHang>)Session[OrderSession];
            sessionCart.RemoveAll(x => x.nongsan.ID == id);
            Session[OrderSession] = sessionCart;
            return Json(new
            {
                status = true
            });
        }
        public JsonResult Update(string cartModel)
        {
            var jsonCart = new JavaScriptSerializer().Deserialize<List<GioHang>>(cartModel);
            var sessionCart = (List<GioHang>)Session[OrderSession];

            foreach (var item in sessionCart)
            {
                var jsonItem = jsonCart.SingleOrDefault(x => x.nongsan.ID == item.nongsan.ID);
                if (jsonItem != null)
                {
                    item.Quantity = jsonItem.Quantity;
                }
            }
            Session[OrderSession] = sessionCart;
            return Json(new
            {
                status = true
            });
        }
        public ActionResult AddItem(long productId, int quantity)
        {
            var product = new NongSanDao().ViewDetail(productId);
            var cart = Session[OrderSession];
            if (cart != null)
            {
                var list = (List<GioHang>)cart;
                if (list.Exists(x => x.nongsan.ID == productId))
                {

                    foreach (var item in list)
                    {
                        if (item.nongsan.ID == productId)
                        {
                            item.Quantity += quantity;
                        }
                    }
                }
                else
                {
                    //tạo mới đối tượng cart item
                    var item = new GioHang();
                    item.nongsan = product;
                    item.Quantity = quantity;
                    list.Add(item);
                }
                //Gán vào session
                Session[OrderSession] = list;
            }
            else
            {
                //tạo mới đối tượng cart item
                var item = new GioHang();
                item.nongsan = product;
                item.Quantity = quantity;
                var list = new List<GioHang>();
                list.Add(item);
                //Gán vào session
                Session[OrderSession] = list;
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Payment()
        {
            var cart = Session[OrderSession];
            var list = new List<GioHang>();
            if (cart != null)
            {
                list = (List<GioHang>)cart;
            }
            return View(list);
        }

        [HttpPost]
        public ActionResult Payment(string shipName, string mobile, string address, string email)
        {
            var order = new Order();
            order.CreatedDate = DateTime.Now;
            order.ShipAddress = address;
            order.ShipMobile = mobile;
            order.ShipName = shipName;
            order.ShipEmail = email;

            try
            {
                var id = new OrderDao().Insert(order);
                var cart = (List<GioHang>)Session[OrderSession];
                var detailDao = new OrderDetailDao();
                decimal total = 0;
                foreach (var item in cart)
                {
                    var orderDetail = new OrderDetail();
                    orderDetail.ProductID = item.nongsan.ID;
                    orderDetail.OrderID = (int)id;
                    orderDetail.Price = item.nongsan.Price;
                    orderDetail.Quantity = item.Quantity;
                    detailDao.Insert(orderDetail);

                    total += (item.nongsan.Price.GetValueOrDefault(0) * item.Quantity);
                }
                string content = System.IO.File.ReadAllText(Server.MapPath("~/assets/client/template/neworder.html"));

                content = content.Replace("{{CustomerName}}", shipName);
                content = content.Replace("{{Phone}}", mobile);
                content = content.Replace("{{Email}}", email);
                content = content.Replace("{{Address}}", address);
                content = content.Replace("{{Total}}", total.ToString("N0"));
                var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();

                new MailHelper().SendMail(email, "Đơn hàng mới từ OnlineShop", content);
                new MailHelper().SendMail(toEmail, "Đơn hàng mới từ OnlineShop", content);
            }
            catch (Exception ex)
            {
                //ghi log
                return Redirect("/loi-thanh-toan");
            }
            return Redirect("/hoan-thanh");
        }

        public ActionResult Success()
        {
            return View();
        }
    }
}