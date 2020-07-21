using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Boat_Rent.Models;
using System.IO;
using CRUDinMVC.Models;

namespace Boat_Rent.Controllers
{
    public class BoatsController : Controller
    {
      

        // GET: Boats
        public ActionResult Index()
        {
            BoatDBHandle dbhandle = new BoatDBHandle();
            ModelState.Clear();
            return View(dbhandle.GetBoat());
        }

     

        // GET: Boats/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Boats/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Boat boat)
        {
            try
            {
                if (ModelState.IsValid)
                {
                //appending the name with id  
                                                               // store the file inside ~/project folder(Img)  
                    HttpPostedFileBase file = boat.Postedfile;
                    boat.Image = Path.GetFileName(file.FileName);
                    string name = Path.GetFileNameWithoutExtension(boat.Image); //getting file name without extension  
                    var ext = Path.GetExtension(file.FileName);
                    string myfile = name + ext;
                    var path = Path.Combine(Server.MapPath("~/Content/Images"), myfile);
                    file.SaveAs(path);
                    BoatDBHandle sdb = new BoatDBHandle();
                    
                    if (sdb.AddBoat(boat))
                    {
                        ViewBag.Message = "Boat Details Added Successfully";
                        ModelState.Clear();
                    }
                }
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return View("Index");
            }
        }

        // GET: Boats/Edit/5
        public ActionResult Buy(int? id)
        {
            BoatDBHandle sdb = new BoatDBHandle();
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Buy(int id,Customer cust)
        {
            try
            {
                BoatDBHandle sdb = new BoatDBHandle();
                sdb.RentedBoat(cust);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Return(int? id)
        {
            BoatDBHandle sdb = new BoatDBHandle();
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Return(int id, Customer cust)
        {
            try
            {
                BoatDBHandle sdb = new BoatDBHandle();
                List<Customer> customer=sdb.ReturnBoat(cust);
                foreach (Customer k in customer)
                {
                    return View(k);
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: Boats/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                BoatDBHandle sdb = new BoatDBHandle();
                if (sdb.DeleteBoat(id))
                {
                    ViewBag.AlertMsg = "Boat Deleted Successfully";
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
       // POST: Boats/Delete/5
       
       
    }
}
