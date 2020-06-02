using Newtonsoft.Json;
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
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [CustomAuthorize(1)]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [AllowAnonymous]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult ForecastGHI()
        {
            if (Session["login"] == null)
            {
                return RedirectToAction("Index", "Login", new { error = "You have to login first" });
            }
            else
            {
                ViewBag.login = Session["login"];
            }
            Model1 db = new Model1();
            List<Forecast> list = db.Forecasts.Where(s => s.Time >= DateTime.Now).ToList();
            double? sum = 0;
            foreach(Forecast item in list)
            {
                if (item.Capactiy.HasValue)
                {
                    sum += item.Capactiy;
                }
            }
            ViewBag.Total = sum;
            return View(list);
        }

        public ActionResult test()
        {
            var db = new Model1();
            int count= db.Histories.Count();
            var tesst = db.Histories.OrderBy(x => x.Id).Skip(count - 100).Take(100).Select(s => new { Date = s.Time, Value = s.Capactiy }).ToList();
            var result = tesst.Select(s => new { Date = s.Date.ToString("yyyy-MM-ddTHH:mm:ss"), Value = s.Value }).ToList();

            return Json(result);
        }
        
        public ActionResult test2()
        {
            DateTime start = DateTime.Today.AddMinutes(-10);
            DateTime end = DateTime.Today.AddDays(1);
            DateTime now = DateTime.Now;
            UseStatic rnd = new UseStatic();
            var db = new Model1();
            var cstk = db.Histories.Max(x => x.Capactiy);
            var table = (from f in db.Forecasts
                         join h in db.Histories on f.Time equals h.Time
                         select new
                         {
                             Forecast = f,
                             History = h
                         })
                         .ToList().Where(x => x.Forecast.Time >= start).ToList();
            //var result = table.Select(s => new { Date = s.Forecast.Time.ToString("yyyy-MM-ddTHH:mm:ss"), hValue = s.History.Capacity, fValue = s.History.Capacity + rnd.Next(-10, 10), hCost = String.Format("{0:0.##}", (s.History.Capacity - s.Forecast.Capacity)/cstk), cstk = String.Format("{0:0.##}",cstk), gValue=s.History.Ghi, gFValue = s.History.Ghi + rnd.Next(-100, 100) }).ToList();
            var result1 = table.Select(s => new { hisDate = s.History.Time.ToString("yyyy-MM-ddTHH:mm:ss"), hValue = s.History.Capactiy, hCost = String.Format("{0:0.##}", (s.History.Capactiy - s.Forecast.Capactiy) / cstk), cstk = String.Format("{0:0.##}", cstk), gValue = s.History.Ghi }).ToList();
            var foreTable = db.Forecasts.Where(x => x.Time <= end&&x.Time>=now).ToList();

            var result2 = foreTable.Select(s => new { foreDate = s.Time.ToString("yyyy-MM-ddTHH:mm:ss"), fValue = rnd.ranMethod(), gFValue = rnd.ranMethodGhi()}).ToList();
            var data = Json(new { history = result1, forecast = result2 }).Data;
            return Json(new {history=result1, forecast=result2 });
        }
        class UseStatic
        {
            public static double ranNum=40;
            public static double ranGhi = 600;
            // Use class-level Random.
            // ... When this method is called many times, it still has good Randoms.
            public double ranMethod()
            {
                Random ran = new Random();
                ranNum += ran.NextDouble() * ran.Next(-5,5);
                while (ranNum > 80 || ranNum < 0)
                {
                    ranNum += ran.NextDouble() * ran.Next(-5, 5);
                }
                return ranNum;
            }
            public double ranMethodGhi()
            {
                Random ran = new Random();
                ranGhi += ran.NextDouble() * ran.Next(-100, 100);
                while (ranGhi > 1200 || ranGhi < 0)
                {
                    ranGhi += ran.NextDouble() * ran.Next(-100, 100);
                }
                return ranGhi;
            }
        }
        public ActionResult Chart()
        {
            var db = new Model1();
            ArrayList xValue = new ArrayList();
            ArrayList yValue = new ArrayList();
            DateTime temp = DateTime.Now.AddDays(-1);
            DateTime temp1 = DateTime.Now.AddDays(1);
            var results = db.Histories.ToList();
            results.ForEach(item => xValue.Add(item.Time.TimeOfDay.ToString()));
            results.ForEach(item => yValue.Add(item.Capactiy));
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
