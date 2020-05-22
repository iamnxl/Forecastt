using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Helpers;
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

        public ActionResult Chart()
        {
            var db = new Model1();
            ArrayList xValue = new ArrayList();
            ArrayList yValue = new ArrayList();
            DateTime temp = DateTime.Now.AddDays(-1);
            DateTime temp1 = DateTime.Now.AddDays(1);
            var results = db.Histories.ToList().Where(x=> DateTime.Now.Subtract(x.Time).TotalMinutes <= 120).ToList();
            results.ForEach(item => xValue.Add(item.Time.TimeOfDay.ToString()));
            results.ForEach(item => yValue.Add(item.Capacity));
            new Chart(width: 1200, height: 600, theme: ChartTheme.Green)
                .AddTitle("Chart History")
                .AddSeries("Default", chartType: "Line", xValue: xValue, yValues: yValue)
                .Write("bmp");

            return null;
        }

        private bool MyFunction(DateTime s)
        {
            DateTime d = DateTime.Now;
            return (s.Date == d.Date);
        }

        public ActionResult UploadImage()
        {
            return View();
        }

        public string ProcessUpload(HttpPostedFileBase file)
        {
            file.SaveAs(Server.MapPath("~/Content/images/" + file.FileName));
            return "~Content/images/" + file.FileName;
        }
    }
}
