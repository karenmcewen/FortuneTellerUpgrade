using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FortuneTellerUpgrade.Models;

namespace FortuneTellerUpgrade.Controllers
{
    public class CustomersController : Controller
    {
        private FortuneTellerUpgradeContext db = new FortuneTellerUpgradeContext();

        // GET: Customers
        public ActionResult Index()
        {
            var customers = db.Customers.Include(c => c.Color).Include(c => c.Month);
            return View(customers.ToList());
        }

        //------------------------------------------------------------
        // GET: Customers/Details/5

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }

            //This is where the fortune will be!
            //+++++++++++++++++++++++++++++++++++++++++++
            ////RETIREMENT AGE
            int retireYears = 40; //default definition for variable used in the if statement
            if (customer.Age % 2 == 0) //userAge is even
            {
                retireYears = 33;
            }
            else //userAge is odd
            {
                retireYears = 3;
            }
            ViewBag.NumberYearsToRetirement = retireYears;

            //BIRTH MONTH
            //ERROR - because I allowed nulls in the initial database, it input as int? which 
            //cannot be implicitly converted to an int, causing an error.
            //ALSO change to viewbags for view

            //int retireMoolah = 0;

            //int birthMonth = customer.MonthID;

            //if (birthMonth >= 1 && birthMonth <= 4)
            //{
            //    retireMoolah = 1000;
            //}
            //else if (birthMonth >= 5 && birthMonth <= 8)
            //{
            //    retireMoolah = 500000000;
            //}
            //else 
            //{
            //    retireMoolah = 250000;
            //}


            //FAVORITE COLOR - need to convert to int numbers (not nullable)
            //and change to viewbags for view
            //switch (favColor.ToLower())
            //{
            //    case "red":
            //        modeTransport = "ferrari";
            //        break;

            //    case "orange":
            //        modeTransport = "scooter";
            //        break;

            //    case "yellow":
            //        modeTransport = "hot air balloon";
            //        break;

            //    case "green":
            //        modeTransport = "horse";
            //        break;

            //    case "blue":
            //        modeTransport = "sailboat";
            //        break;

            //    case "indigo":
            //        modeTransport = "spaceship";
            //        break;

            //    case "violet":
            //        modeTransport = "chauffeured limosine";
            //        break;

            //    default:
            //        Console.WriteLine("\nWhether you are a poor speller, or you didn't follow the directions, \nthe fates are not amused.");
            //        modeTransport = "motorized wheelchair with a broken down battery";
            //        break;


            //NUMBER OF SIBLINGS
            //if (numSibs < 0)
            //{
            //    Console.WriteLine("\nHa, ha, where did you bury the bodies?");
            //    vacationHome = "antarctica";
            //}
            //else
            //{
            //    switch (numSibs)
            //    {
            //        case 0:
            //            vacationHome = "Costa Rica";
            //            break;

            //        case 1:
            //            vacationHome = "Put-in-Bay";
            //            break;
            //        case 2:
            //            vacationHome = "London";
            //            break;
            //        case 3:
            //            vacationHome = "Arizona";
            //            break;
            //        default:
            //            vacationHome = "the Zoo";
            //            break;
                        //+++++++++++++++++++++++++++++++++++++++++++
                        //This is at the end of all the logic to return to the view!
                        return View(customer);
        }
        //------------------------------------------------------------
        // GET: Customers/Create
        public ActionResult Create()
        {
            ViewBag.ColorID = new SelectList(db.Colors, "ColorID", "Color1");
            ViewBag.MonthID = new SelectList(db.Months, "MonthID", "Month1");
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerID,FirstName,LastName,Age,ColorID,MonthID,NumSibs")] customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ColorID = new SelectList(db.Colors, "ColorID", "Color1", customer.ColorID);
            ViewBag.MonthID = new SelectList(db.Months, "MonthID", "Month1", customer.MonthID);
            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            ViewBag.ColorID = new SelectList(db.Colors, "ColorID", "Color1", customer.ColorID);
            ViewBag.MonthID = new SelectList(db.Months, "MonthID", "Month1", customer.MonthID);
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerID,FirstName,LastName,Age,ColorID,MonthID,NumSibs")] customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ColorID = new SelectList(db.Colors, "ColorID", "Color1", customer.ColorID);
            ViewBag.MonthID = new SelectList(db.Months, "MonthID", "Month1", customer.MonthID);
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
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
