using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
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
        public ActionResult ForecastGHI()
        {
            Model1 db = new Model1();
            List<Forecast> list = db.Forecasts.Where(s => s.Time >= DateTime.Now).ToList();
            double? sum = 0;
            foreach(Forecast item in list)
            {
                if (item.Capacity.HasValue)
                {
                    sum += item.Capacity;
                }
            }
            ViewBag.Total = sum;
            return View(list);
        }
    }
}