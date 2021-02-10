using ContactApp.Models;
using ContactApp.Models.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;

namespace ContactApp.Controllers
{
    public class HomeController : Controller
    {
        AppDBEntities db = new AppDBEntities();
        public ActionResult Index()
        {
            
            var list = db.People.ToList();
            return View(list);
        }

        public ActionResult PersonDetails(Guid id)
        {
            PersonContact personContact = new PersonContact();
            var person = db.People.Find(id);
            personContact.PersonID = (Guid)id;
            personContact.Ad = person.Ad;
            personContact.Soyad = person.Soyad;
            personContact.Firma = person.Firma;
            personContact.PersonPhone = new List<string>();
            personContact.PersonMail = new List<string>();
            personContact.PersonLocation = new List<string>();
            var phone = from item in db.Phone
                        where item.PersonID.ToString().Contains(id.ToString())
                        select item.Phone1;
            foreach (var item in phone)
            {
                personContact.PersonPhone.Add(item);
            }
            var mail = from item in db.Mail
                        where item.PersonID.ToString().Contains(id.ToString())
                        select item.Mail1;
            foreach (var item in mail)
            {
                personContact.PersonMail.Add(item);
            }
            var location = from item in db.Location
                        where item.PersonID.ToString().Contains(id.ToString())
                        select item.Location1;
            foreach (var item in location)
            {
                personContact.PersonLocation.Add(item);
            }
            return View("PersonDetails", personContact);
        }
        public ActionResult DeleteNumber()
        {
            var phone = Request.QueryString["phone"].ToString();
            var phoneID = db.Phone.FirstOrDefault(x => x.Phone1 == phone);
            db.Phone.Remove(phoneID);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DeleteMail()
        {
            var mail = Request.QueryString["mail"].ToString();
            var mailID = db.Mail.FirstOrDefault(x => x.Mail1 == mail);
            db.Mail.Remove(mailID);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DeleteLocation()
        {
            var loc = Request.QueryString["location"].ToString();
            var locID = db.Location.FirstOrDefault(x => x.Location1 == loc);
            db.Location.Remove(locID);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        
        public ActionResult Add(PersonContact personContact)
        {
            string telefon = personContact.Phone;
            if (telefon != "" && telefon != null)
            {
                Phone phone = new Phone();
                phone.PersonID = (Guid)personContact.PersonID;
                phone.Phone1 = personContact.Phone;
                db.Phone.Add(phone);
                db.SaveChanges();
            }
            if (personContact.Mail != "" && personContact.Mail != null)
            {
                Mail mail = new Mail();
                mail.PersonID = (Guid)personContact.PersonID;
                mail.Mail1 = personContact.Mail;
                db.Mail.Add(mail);
                db.SaveChanges();
            }
            if (personContact.Location != "" && personContact.Location != null)
            {
                Location loc = new Location();
                loc.PersonID = (Guid)personContact.PersonID;
                loc.Location1 = personContact.Location;
                db.Location.Add(loc);
                db.SaveChanges();
            }
            return Redirect("/Home/PersonDetails/" + personContact.PersonID.ToString());
        }
        public ActionResult AddPerson()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddPerson(PersonContact personContact)
        {
            People people = new People();
            string newpeopleID = Guid.NewGuid().ToString();
            people.ID = new Guid(newpeopleID);
            people.Ad = personContact.Ad;
            people.Soyad = personContact.Soyad;
            people.Firma = personContact.Firma;
            db.People.Add(people);
            db.SaveChanges();
            return Redirect("PersonDetails/" + newpeopleID);
        }

        public ActionResult PersonDelete()
        {
            Guid personid = new Guid(Request.QueryString["id"].ToString());
            var phonelist = from item in db.Phone
                            where item.PersonID.ToString().Contains(personid.ToString())
                            select item.PhoneID;
            foreach (var item in phonelist)
            {
                Phone phone = db.Phone.FirstOrDefault(x => x.PhoneID == item);
                db.Phone.Remove(phone);
            }
            var maillist = from item in db.Mail
                            where item.PersonID.ToString().Contains(personid.ToString())
                            select item.MailID;
            foreach (var item in maillist)
            {
                Mail mail = db.Mail.FirstOrDefault(x => x.MailID == item);
                db.Mail.Remove(mail);
            }
            var loclist = from item in db.Location
                            where item.PersonID.ToString().Contains(personid.ToString())
                            select item.LocationID;
            foreach (var item in loclist)
            {
                Location location = db.Location.FirstOrDefault(x => x.LocationID == item);
                db.Location.Remove(location);
            }
            People people = db.People.FirstOrDefault(x => x.ID == personid);
            db.People.Remove(people);

            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Report()
        {
            ViewBag.locationList = db.Location.Select(x => x.Location1).Distinct();
            return View();
        }
        public ActionResult NumberOfPeople()
        {
            string il = Request.QueryString["il"].ToString();
            int count = (from x in db.Location where x.Location1 == il 
                         select x).Count();
            ViewBag.loc = il;
            ViewBag.sayi = count.ToString();
            return View();
        }
        
    }
}