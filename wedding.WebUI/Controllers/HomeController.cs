using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using wedding.Contracts.Repositories;
using wedding.DAL.Data;
using wedding.DAL.Repositories;
using wedding.Model;
using wedding.WebUI.Models;

namespace wedding.WebUI.Controllers
{
    public class HomeController : Controller
    {
        IRepositoryBase<Wedding> wedding;
        IRepositoryBase<Food> food;
        IRepositoryBase<Accommodation> accommodation;

        public HomeController(IRepositoryBase<Wedding> wedding, IRepositoryBase<Food> food, IRepositoryBase<Accommodation> accommodation)
        {
            this.wedding = wedding;
            this.food = food;
            this.accommodation = accommodation;
        }
        public ActionResult Index()
        {
            
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}