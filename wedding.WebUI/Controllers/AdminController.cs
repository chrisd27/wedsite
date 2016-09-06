using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using wedding.Contracts.Repositories;
using wedding.Model;
using wedding.WebUI.Models;
using wedding.Services;

namespace wedding.WebUI.Controllers
{
    public class AdminController : Controller
    {
        IRepositoryBase<Wedding> wedding;
        IRepositoryBase<Guest> guest;
        IRepositoryBase<Food> food;
        IRepositoryBase<Accommodation> accommodation;

        public AdminController(IRepositoryBase<Wedding> wedding, IRepositoryBase<Guest> guest, IRepositoryBase<Food> food, IRepositoryBase<Accommodation> accommodation)
        {
            this.wedding = wedding;
            this.guest = guest;
            this.food = food;
            this.accommodation = accommodation;
        }

        public ActionResult Index()
        {
            ViewData["admin"] = "false";
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            var password = "J3551ca17";
            if (form["adminPassword"] == password) {
                TempData["admin"] = "true";
                return RedirectToAction("AdminIndex");
            }
            
            return RedirectToAction("Index");
        }

        public ActionResult AdminIndex()
        {
            ViewData["admin"] = TempData["admin"];
            var model = wedding.GetAll();
            return View(model);
        }
        

        public ActionResult GuestList()
        {

            var model = guest.GetAll();

            return View(model);
        }

        public ActionResult CreateGuest(int weddingId)
        {
            ViewData["WeddingId"] = weddingId;

            return View();
            
        }

        [HttpPost]
        public ActionResult CreateGuest(Guest newGuest, List<Dependant> dependant)
        {
            // Create Username
            var lastname = newGuest.lastName;
            Random newNum = new Random();
            string guestUsername = "";
            var allGuests = guest.GetAll();
            var match = false;
           
            do {
                var no = GenerateRandomNo();
                guestUsername = lastname + no.ToString();
                var guestTemp = guest.Where(c => c.username == guestUsername.ToString());
                if (guestTemp.Count() > 0)
                {
                    match = true;
                }
            }
            while (match == true); 


            List<Guest> guests = new List<Guest>();
            newGuest.username = guestUsername;
            guest.Insert(newGuest);
            guest.Commit();

            if (dependant != null)
            {
                var dependantNo = dependant.Count;
                if (dependantNo > 0)
                {
                    for (var i = 0; i < dependantNo; i++)
                    {
                        Guest tempGuest = new Guest();
                        tempGuest = newGuest;
                        tempGuest.firstName = dependant[i].FirstName;
                        tempGuest.lastName = dependant[i].LastName;
                        guest.Insert(tempGuest);
                        guest.Commit();
                    }
                }
            }
            
            return RedirectToAction("DashboardIndex", new { id = newGuest.WeddingId });
        }

        public ActionResult DeleteGuest(int id)
        {
            Guest deletedGuest = guest.GetById(id);
            var wedId = deletedGuest.WeddingId;
            guest.Delete(deletedGuest);
            guest.Commit();

            return RedirectToAction("DashboardIndex", new { id = wedId });
        }

        public ActionResult EditGuest(int id)
        {
            Guest editGuest = guest.GetById(id);

            return View(editGuest);
        }

        [HttpPost]
        public ActionResult EditGuest(Guest updateGuest)
        {
            guest.Update(updateGuest);
            guest.Commit();

            return RedirectToAction("DashboardIndex", new { id = updateGuest.WeddingId });
        }

        public ActionResult CreateWedding()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateWedding(Wedding newWedding)
        {
            wedding.Insert(newWedding);
            wedding.Commit();
            

            return RedirectToAction("AdminIndex");
        }

        public ActionResult EditWedding(int id)
        {
            Wedding editWedding = wedding.GetById(id);

            return View(editWedding);
        }

        [HttpPost]
        public ActionResult EditWedding(Wedding editWedding)
        {
            wedding.Update(editWedding);
            wedding.Commit();

            return RedirectToAction("Index");
        }

        public ActionResult DeleteWedding(int id)
        {
            Wedding deleteWedding = wedding.GetById(id);
            wedding.Delete(deleteWedding);
            wedding.Commit();

            return RedirectToAction("Index");
        }

        public ActionResult WeddingDetails()
        {
            return View();
        }

        public ActionResult CreateAccomm(int weddingId)
        {
            ViewData["WeddingId"] = weddingId;
            return View();
        }

        [HttpPost]
        public ActionResult CreateAccomm(Accommodation accom)
        {
            accommodation.Insert(accom);
            accommodation.Commit();


            return RedirectToAction("DashboardIndex", new { id = accom.WeddingId });
        }

        public ActionResult EditAccomm(int id)
        {
            Accommodation editAccomm = accommodation.GetById(id);
            var i = "test";
            return View(editAccomm);
        }

        [HttpPost]
        public ActionResult EditAccomm(Accommodation accomm)
        {
            accommodation.Update(accomm);
            accommodation.Commit();
            
            return RedirectToAction("DashboardIndex", new { id = accomm.WeddingId });
        }

        public ActionResult DeleteAccomm(int id)
        {
            Accommodation deleteAccomm = accommodation.GetById(id);
            accommodation.Delete(deleteAccomm);
            accommodation.Commit();

            return RedirectToAction("DashboardIndex", new { id = deleteAccomm.WeddingId });
        }

        public ActionResult DashboardIndex(int id)
        {
            var wed = wedding.GetById(id);
            TempData["wedmodel"] = wed;
            DashboardInit dashInit = new DashboardInit();
            TempData["weddingId"] = wed.WeddingId;
            TempData["numGuests"] = dashInit.countGuests(wed.WeddingId, guest.GetAll());
            TempData["numAccom"] = dashInit.countAccom(wed.WeddingId, accommodation.GetAll());
            TempData["numFood"] = dashInit.countFood(wed.WeddingId, food.GetAll());
            return View(TempData["wedmodel"]);
        }

        [ChildActionOnly]
        public ActionResult Dashboard_Guests()
        {
            
            var model = guest.GetAll();
            var guestBool = false;
            if (model.Any())
            {
                guestBool = true;
            }
            ViewData["guests"] = guestBool;
            ViewData["weddingId"] = TempData["weddingId"];
            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult Dashboard_Food()
        {

            var model = food.GetAll();
            var foodBool = false;
            if(model.Any())
            {
                foodBool = true;
            }
            ViewData["food"] = foodBool;

            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult Dashboard_Wedding(int weddingId)
        {
            var model = wedding.GetById(weddingId);

            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult Dashboard_Accom()
        {

            var model = accommodation.GetAll();
            var accom = false;
            if (model.Any())
            {
                accom = true;
            }
            ViewData["accom"] = accom;

            return PartialView(model);
        }

        public int GenerateRandomNo()
        {
            int _min = 1000;
            int _max = 9999;
            Random _rdm = new Random();
            return _rdm.Next(_min, _max);
        }
    }
}