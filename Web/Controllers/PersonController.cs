using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DAL;
using Web.Models;
using DAL.Repository;

namespace Web.Controllers
{
    public class PersonController : Controller
    {
        //private WebContext db = new WebContext();
        private GenericUnitOfWork uow = null;

        public PersonController()
        {
            uow = new GenericUnitOfWork();
        }

        public PersonController(GenericUnitOfWork _uow)
        {
            this.uow = _uow;
        }




        // GET: /Person/
        public ActionResult Index()
        {
            //return View(db.People.ToList());
            return View(uow.Repository<Person>().GetAll().ToList());
        }

        // GET: /Person/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Person person = db.People.Find(id);
            Person person = uow.Repository<Person>().Get(p => p.Id == id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // GET: /Person/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Person/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,ContactNumber,Address")] Person person)
        {
            if (ModelState.IsValid)
            {
                //db.People.Add(person);
                //db.SaveChanges();

                uow.Repository<Person>().Add(person);
                uow.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(person);
        }

        // GET: /Person/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Person person = db.People.Find(id);
            Person person = uow.Repository<Person>().Get(c => c.Id == id);

            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // POST: /Person/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,ContactNumber,Address")] Person person)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(person).State = EntityState.Modified;
                //db.SaveChanges();

                uow.Repository<Person>().Attach(person);
                uow.SaveChanges();


                return RedirectToAction("Index");
            }
            return View(person);
        }

        // GET: /Person/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Person person = db.People.Find(id);
            Person person = uow.Repository<Person>().Get(c => c.Id == id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // POST: /Person/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Person person = db.People.Find(id);
            //db.People.Remove(person);
            //db.SaveChanges();

            Person person = uow.Repository<Person>().Get(c => c.Id == id);
            uow.Repository<Person>().Delete(person);
            uow.SaveChanges();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
                uow.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
