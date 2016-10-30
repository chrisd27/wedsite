using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using wedding.Contracts.Repositories;
using wedding.Model;
using wedding.Services;
using wedding.WebUI.Models;

namespace wedding.WebUI.Controllers
{
    public class WedsiteController : Controller
    {
        IRepositoryBase<Wedding> wedding;
        IRepositoryBase<Guest> guest;
        IRepositoryBase<Food> food;
        IRepositoryBase<Accommodation> accommodation;
        IRepositoryBase<Invitation> invitation;
        IRepositoryBase<LoginDetails> login;

        public WedsiteController(IRepositoryBase<Wedding> wedding, IRepositoryBase<Guest> guest, IRepositoryBase<Food> food, IRepositoryBase<Accommodation> accommodation, IRepositoryBase<Invitation> invitation, IRepositoryBase<LoginDetails> login)
        {
            this.wedding = wedding;
            this.guest = guest;
            this.food = food;
            this.accommodation = accommodation;
            this.invitation = invitation;
            this.login = login;
        }

        public ActionResult Index()
        {
            var model = wedding.GetAll();
            ViewData["WeddingId"] = model.First().WeddingId;
            string cookieName = "Chris and Jess Wedding";
            ViewData["loggedIn"] = false;
            var cookieValue = "false";

            if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains(cookieName))
            {
                var cookie = this.ControllerContext.HttpContext.Request.Cookies[cookieName].Value.ToString();
                int index = cookie.IndexOf("&");
                if (index > 0)
                    cookieValue = cookie.Substring(0, index);
                ViewData["loggedIn"] = cookieValue;

                string usernameText = "username=";
                int usernameIndexStart = cookie.IndexOf(usernameText) + usernameText.Length;
                int usernameIndexEnd = cookie.IndexOf("&nick");
                int length = usernameIndexEnd - usernameIndexStart;

                TempData["username"] = cookie.Substring(usernameIndexStart, length);
                var username = TempData["username"];

                // delete cookie if username no longer aligns to a guest (i.e. guest has been deleted)
                var guestModel = guest.Where(c => c.username == username.ToString());
                if(guestModel.Count() < 1)
                {
                    if (Request.Cookies["Chris and Jess Wedding"] != null)
                    {
                        Response.Cookies["Chris and Jess Wedding"].Expires = DateTime.Now.AddDays(-1);
                        ViewData["loggedIn"] = false;
                    }
                }

            }

            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginDetails login)
        {
            var email = login.email;
            var inviteUsername = login.username;
            IEnumerable<Guest> loginGuests = Enumerable.Empty<Guest>();
            if ((email != null && inviteUsername != null) || (email == null && inviteUsername != null))
            {
                loginGuests = guest.Where(c => c.username == login.username);

            }
            else if(email != null && inviteUsername == null)
            {
                loginGuests = guest.Where(c => c.emailAddress == login.email);
            }

            string jsonString = "";
            var changes = false;
            if (loginGuests != null && loginGuests.Any())
            {
                if (loginGuests.First().emailAddress == null && login.email != null)
                {
                    // Update all guests with the new email address entered in the form.
                    // loop through them one by one, updating the email
                    foreach (var indGuest in loginGuests)
                    {
                        Guest updateGuest = guest.GetById(indGuest.GuestId);
                        updateGuest.emailAddress = login.email;
                        guest.Update(updateGuest);
                        indGuest.emailAddress = login.email;
                        
                    }

                    changes = true;
                }

                if (changes)
                {
                    guest.Commit();
                }

                // When email address is updated, add cookie
                string cookieName = "Chris and Jess Wedding";
                //string domain = "localhost"; // DEV
                string domain = "chrisandjess.wedding"; // Prod
                string username = loginGuests.First().username;
                string nickname = loginGuests.Count() > 1 ? loginGuests.First().lastName : loginGuests.First().firstName;
                int weddingID = loginGuests.First().WeddingId;
                Cookie cookie = new Cookie();
                cookie.createCookie(this.ControllerContext.HttpContext, cookieName, domain, username, nickname, weddingID);



                jsonString = JsonConvert.SerializeObject(loginGuests);

            }
            else
            {
                UsernameFailure fail = new UsernameFailure();
                fail.status = 0;
                fail.message = "The username is incorrect";
                jsonString = JsonConvert.SerializeObject(fail);
            }

            return Json(jsonString);
        }

        public ActionResult Login()
        {
            return PartialView();
        }

        public ActionResult Wedding(int weddingId)
        {
            var model = wedding.GetById(weddingId);

            return PartialView(model);
        }

        public JsonResult GetData(int weddingId)
        {
            var data = wedding.GetById(weddingId);
            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet};
        }

        public ActionResult Accommodation(int weddingId, string inviteCode)
        {
            var model = accommodation.GetAll();

            return PartialView(model);
        }

        public ActionResult RSVP(int weddingId)
        {
            var username = TempData["username"];
            var model = guest.Where(c => c.username == username.ToString());

            return PartialView(model);
        }

        [HttpPost]
        public ActionResult RSVP(Guest guestRSVP)
        {
            string jsonString = "";
            // update RSVP list
            Guest guestFromDB = guest.GetById(guestRSVP.GuestId);
            guestFromDB.RSVP = guestRSVP.RSVP;
            guestFromDB.RSVPTimestamp = DateTime.Now;
            if (guestFromDB.RSVPTimestampInitial.Equals(null))
            {
                guestFromDB.RSVPTimestampInitial = DateTime.Now;
            }
            guest.Update(guestFromDB);
            guest.Commit();

            guestRSVP.firstName = guestFromDB.firstName;
            guestRSVP.lastName = guestFromDB.lastName;
                
            jsonString = JsonConvert.SerializeObject(guestRSVP);
            // return JSON
            return Json(jsonString);
        }

        public ActionResult Food(int weddingId)
        {
            var model = food.GetAll();
            return PartialView();
        }

        public ActionResult deleteCookie()
        {
            var jsonString = "false";
            if (Request.Cookies["Chris and Jess Wedding"] != null)
            {
                Response.Cookies["Chris and Jess Wedding"].Expires = DateTime.Now.AddDays(-1);
                Response.Cookies["Chris and Jess Wedding"].Domain = "chrisandjess.wedding";
                jsonString = "true";
            }

            return Json(jsonString);
        }

        
    }
}