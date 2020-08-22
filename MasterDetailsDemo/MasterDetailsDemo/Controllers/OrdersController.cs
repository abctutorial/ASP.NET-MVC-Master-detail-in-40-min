using MasterDetailsDemo.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MasterDetailsDemo.Controllers
{
    public class OrdersController : Controller
    {
        OnlineShopEntities db = new OnlineShopEntities();

        public ActionResult Index()
        {
            //if (Session["AdminLogin"].ToString() != "")
            //{
                List<Customer> OrderAndCustomerList = db.Customers.OrderByDescending(s=>s.OrderDate).ToList();
            return View(OrderAndCustomerList);
            //}

            //return RedirectToAction("Login", "AdminPanel");
        }


        public ActionResult SaveOrder(string name, String address, Order[] order)
        {
            if (Session["AdminLogin"].ToString() != "")
            {
                string result = "Error! Order Is Not Complete!";
            if (name != null && address != null && order != null)
            {
                var cutomerId = Guid.NewGuid();
                Customer model = new Customer();
                model.CustomerId = cutomerId;
                model.Name = name;
                model.Address = address;
                model.OrderDate = DateTime.Now;
                db.Customers.Add(model);

                foreach (var item in order)
                {
                    var orderId = Guid.NewGuid();
                    Order O = new Order();
                    O.OrderId = orderId;
                    O.ProductName = item.ProductName;
                    O.Quantity = item.Quantity;
                    O.Price = item.Price;
                    O.Amount = item.Amount;
                    O.CustomerId = cutomerId;
                    db.Orders.Add(O);
                }
                db.SaveChanges();
                result = "Success! Order Is Complete!";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
            }

            return RedirectToAction("Login", "AdminPanel");
        }



        public ActionResult EditOrder( Guid? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerId=new SelectList(db.Customers, "CustomerId", "Name", order.CustomerId);
            return View(order);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditOrder([Bind(Include = "OrderId,ProductName,Quantity,Price,Amount,CustomerId")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "Name", order.CustomerId);
            return View(order);
        }



        public ActionResult EditCustomer(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Customer cus = db.Customers.Find(id);
            if (cus == null)
            {
                return HttpNotFound();
            }
           
            return View(cus);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCustomer([Bind(Include = "CustomerId,Name,Address,OrderDate")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
         
            return View(customer);
        }


    }
}