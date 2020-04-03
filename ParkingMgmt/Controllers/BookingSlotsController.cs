using System;
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

        // GET: BookingSlots
        public ActionResult Index()
        {

            return View(db.BookingSlots.ToList());
        }
        [HttpGet]
        public ActionResult availableslots()
        {

            string VT = Request.QueryString["VehicleType"]; ;
            if(VT==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else if(VT=="Large")
            {
          
             //var mm = db.BookingSlots.Where(x => x.VehicleType == VT && x.VehicleNumber==null).ToList();
             return View(db.BookingSlots.Where(x => x.VehicleType == VT && x.VehicleNumber == null).ToList());
            }
            else if(VT=="Medium")
            {
                return View(db.BookingSlots.Where(x => x.VehicleType == VT ||x.VehicleType=="Large"&& x.VehicleNumber == null).ToList());
            }
            else
            {
                return View(db.BookingSlots.Where(x=>x.VehicleNumber==null).ToList());
            }
           
     
            return View();
         
        }

        public ActionResult SlotAvaiabilty()
        {
            ParkingSlotEntities db = new ParkingSlotEntities();
            ViewBag.DropDown = db.BookingSlots.Select(u => u.VehicleType).Distinct().ToList();
            return View(db.BookingSlots.ToList());
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
            db.BookingSlots.Remove(bookingSlot);
            db.SaveChanges();
            return RedirectToAction("Index");
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
