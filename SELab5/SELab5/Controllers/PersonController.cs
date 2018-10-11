using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SELab5.Models;
using Microsoft.AspNet.Identity;

namespace SELab5.Controllers
{
    public class PersonController : Controller
    {
        
        PhoneBookDbEntities _db;

        public PersonController()
        {
            _db = new PhoneBookDbEntities();
           

        }
        // GET: Person
        public ActionResult Index()
        {
            string login = User.Identity.GetUserId();

            List<Person> personList = new List<Person>();
              personList=_db.People.Where(person => person.AddedBy == login).ToList();
            return View(personList);
        }

        // GET: Person/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        [Authorize]
        // GET: Person/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Person/Create
        [HttpPost]
        public ActionResult Create(Person getPerson)
        {
            try
            {
                string login = User.Identity.GetUserId();

                // TODO: Add insert logic here
                Person StorePerson = new Person();
                StorePerson.FirstName = getPerson.FirstName;
                StorePerson.MiddleName = getPerson.MiddleName;
                StorePerson.LastName = getPerson.LastName;
                StorePerson.DateOfBirth = getPerson.DateOfBirth;
                StorePerson.FaceBookAccountId = getPerson.FaceBookAccountId;
                StorePerson.TwitterId = getPerson.TwitterId;
                StorePerson.LinkedInId = getPerson.LinkedInId;
                StorePerson.HomeAddress = getPerson.HomeAddress;
                StorePerson.AddedOn = DateTime.Now;
                StorePerson.UpdateOn = DateTime.Now;
                StorePerson.AddedBy = login;
                StorePerson.EmailId = getPerson.EmailId;
                StorePerson.HomeCity = getPerson.HomeCity;

                _db.People.Add(StorePerson);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Person/Edit/5
        public ActionResult Edit(int id)
        {
            Person editPerson = _db.People.Find(id);
            return View(editPerson);
        }

        // POST: Person/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Person getPerson)
        {
            try
            {
                Person updatedPerson = _db.People.Find(id);
                updatedPerson.FirstName = getPerson.FirstName;
                updatedPerson.MiddleName = getPerson.MiddleName;
                updatedPerson.LastName = getPerson.LastName;
                updatedPerson.DateOfBirth = getPerson.DateOfBirth;
                updatedPerson.HomeAddress = getPerson.HomeAddress;
                updatedPerson.HomeCity = getPerson.HomeCity;
                updatedPerson.FaceBookAccountId = getPerson.FaceBookAccountId;
                updatedPerson.TwitterId = getPerson.TwitterId;
                updatedPerson.EmailId = getPerson.EmailId;
                updatedPerson.UpdateOn = DateTime.Now;
                _db.SaveChanges();

                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Person/Delete/5
        public ActionResult Delete(int id)
        {
            Person showTo = _db.People.Find(id);
            return View(showTo);
        }

        // POST: Person/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Person getperson)
        {
            try
            {
                // TODO: Add delete logic here
             Person deletePerson = _db.People.Find(id);
                List<Contact> deletePersonContacts = _db.Contacts.Where(contact => contact.PersonId == id).ToList();
                _db.Contacts.RemoveRange(deletePersonContacts);
                _db.SaveChanges();
                _db.People.Remove(deletePerson);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Contact(int id)
        {
            return View();
        }

        // POST: Person/Delete/5
        [HttpPost]
        public ActionResult Contact(int id, Contact personContact)
        {
          
            try
            {
                // TODO: Add delete logic here
                if (ModelState.IsValid)
                {
                    Contact addContact = new Contact();
                    addContact.ContactNumber = personContact.ContactNumber;
                    addContact.Type = personContact.Type;
                    addContact.PersonId = id;
                    _db.Contacts.Add(addContact);
                    _db.SaveChanges();
                }
                    return RedirectToAction("Index");
                
            }
            catch
            {
                return View();
            }
        }
    }
}
