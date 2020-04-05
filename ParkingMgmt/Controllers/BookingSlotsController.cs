﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ParkingSlotDataLib;
using ParkingMgmt.Models;

namespace ParkingMgmt.Controllers
{
    public class BookingSlotsController : Controller
    {
        private ParkingSlotEntities db = new ParkingSlotEntities();
        private History his= new History();
        // GET: BookingSlots
        public ActionResult AllSlots()
        { 
            return View(db.BookingSlots.ToList());
        }


        public ActionResult SlotAvaiabilty()
        {
            // ParkingSlotEntities db = new ParkingSlotEntities();
            ViewBag.DropDown = db.BookingSlots.Select(u => u.VehicleType).Distinct().ToList();
            return View();
        }

        [HttpGet]
        public ActionResult availableslots()
        {

            string VT = Request.QueryString["VehicleType"]; ;
            if (VT =="")
            {
                ViewBag.error = "Please select Vehicle Type";
                return View("");
            }
            else if (VT == "Large")
            {
                var count = db.BookingSlots.Where(x => x.VehicleType == VT && x.VehicleNumber == null).ToList().Count();
                if (count == 0)
                {
                    ViewBag.error = "Parking Slots Not Avilable";
                }
                else
                {
                    return View(db.BookingSlots.Where(x => x.VehicleType == VT && x.VehicleNumber == null).ToList());
                }
             
            }
            else if(VT=="Medium")
            {
                var count = db.BookingSlots.Where(x => x.VehicleType == VT || x.VehicleType == "Large" && x.VehicleNumber == null).ToList().Count();
                if(count==0)
                {
                    ViewBag.error = "Parking Slots Not Avilable";
                }
                else
                {
                    return View(db.BookingSlots.Where(x => x.VehicleType == VT || x.VehicleType == "Large" && x.VehicleNumber == null).ToList());
                }
                
            }
            else if(VT == "Small")
            {
                var count = db.BookingSlots.Where(x => x.VehicleNumber == null).ToList().Count();
                if(count==0)
                {
                    ViewBag.error = "Parking Slots Not Avilable";
                }
                else
                {
                    return View(db.BookingSlots.Where(x => x.VehicleNumber == null).ToList());
                }
                
            }

            return View();
         
        }
        public ActionResult BookSlot()
        {
            return View();
        }
        [HttpPost]
        public ActionResult BookSlot([Bind(Include = "VehicleNumber,AllocatedDate,AllocatedTime,VehicleType")] BookingSlot bs)
        {
            if(bs.VehicleType!=null&&bs.VehicleNumber!=null)
            {

                if (bs.VehicleType == "Large")
                {

                    var count = db.BookingSlots.Where(x => x.VehicleType == "Large" && x.VehicleNumber == null).ToList().Count();
                    if (count != 0)
                    {
                        var EditedObj = db
                              .BookingSlots
                              .Where(x => x.VehicleType == "Large" && x.VehicleNumber == null)
                              .First();

                        EditedObj.VehicleNumber = bs.VehicleNumber;
                        EditedObj.AllocatedDate = DateTime.Now;
                        EditedObj.AllocatedTime = DateTime.Now.TimeOfDay;
                        db.SaveChanges();
                    }
                }
                else if (bs.VehicleType == "Medium")
                {
                    var EditedObj = db
                             .BookingSlots
                             .Where(x => x.VehicleType == "Medium" || x.VehicleType == "Medium" && x.VehicleNumber == null)
                             .First();
                    EditedObj.VehicleNumber = bs.VehicleNumber;
                    EditedObj.AllocatedDate = DateTime.Now;
                    EditedObj.AllocatedTime = DateTime.Now.TimeOfDay;
                    db.SaveChanges();
                }
                else if (bs.VehicleType == "Small")
                {
                    var EditedObj = db
                             .BookingSlots
                             .Where(x => x.VehicleNumber == null)
                             .First();
                    EditedObj.VehicleNumber = bs.VehicleNumber;
                    EditedObj.AllocatedDate = DateTime.Now;
                    EditedObj.AllocatedTime = DateTime.Now.TimeOfDay;
                    db.SaveChanges();
                }

            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View();
        }

        public ActionResult UnallocateParkingSlot()
        {
            return View(db.BookingSlots.Where(x=>x.VehicleNumber!=null).ToList());
        }


        // GET: BookingSlots/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookingSlot bookingSlot = db.BookingSlots.Find(id);
            if (bookingSlot == null)
            {
                return HttpNotFound();
            }
            return View(bookingSlot);
        }

        // GET: BookingSlots/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BookingSlots/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,VehicleNumber,AllocatedDate,AllocatedTime,VehicleType")] BookingSlot bookingSlot)
        {
            if (ModelState.IsValid)
            {
                
                db.BookingSlots.Add(bookingSlot);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bookingSlot);
        }






        /// <summary>
        /// //////////////
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>



        // GET: BookingSlots/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookingSlot bookingSlot = db.BookingSlots.Find(id);
            if (bookingSlot == null)
            {
                return HttpNotFound();
            }
            return View(bookingSlot);
        }

        // POST: BookingSlots/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,VehicleNumber,AllocatedDate,AllocatedTime,VehicleType")] BookingSlot bookingSlot)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bookingSlot).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bookingSlot);
        }

        // GET: BookingSlots/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookingSlot bookingSlot = db.BookingSlots.Find(id);
            if (bookingSlot == null)
            {
                return HttpNotFound();
            }
            return View(bookingSlot);
        }

        // POST: BookingSlots/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BookingSlot bookingSlot = db.BookingSlots.Find(id);
            his.AllocatedDate = bookingSlot.AllocatedDate.Value;
            his.AllocatedTime = bookingSlot.AllocatedTime.Value;
            his.VehicleNo = bookingSlot.VehicleNumber;
            his.SlotNo = bookingSlot.SlotNo.Value;
            his.UnAllocatedDate = DateTime.Now;
            his.UnAllocatedTime = DateTime.Now.TimeOfDay;
          
            bookingSlot.VehicleNumber = null;
            bookingSlot.AllocatedDate = null;
            bookingSlot.AllocatedTime = null;

            db.History.Add(his);
            db.SaveChanges();
            return RedirectToAction("AllSlots");
          
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
