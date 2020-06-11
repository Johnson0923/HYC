using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HYCWEB.Models;
using HYCWEB.Models.ViewModels;
using HYCWEB.Repositorys;

namespace HYCWEB.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult ac(string TabName)
        {
            return Content("<H1>AAAAAA BBBB</H1>");
        }
        public ActionResult List(vList obj)
        {
            HYCEntities model = new HYCEntities();
            var i2 =model.AS_WORKTIME.Where(x=>x.AS_USERID == obj.EmployeeName && x.AS_DATE.ToString().Substring(0, 6) == obj.StartYYMM).Select(y=>y);
            var i3 = model.AS_WORKTIME.Where(x => x.AS_USERID == obj.EmployeeName && x.AS_DATE.ToString().Substring(0, 6) == obj.StartYYMM).Sum(y=>y.AS_HOURS);
            double result = Convert.ToInt32(i3);
            double Dd;
            double Hh;
            double Mm;
            if (result > 1440)
            {
                Dd = Math.Floor(result / 1440);
                Hh = Math.Floor((result / 60) % 24);
                Mm = Math.Floor(result % 60);
            }
            else
            {
                Dd = 0;
                Hh = Math.Floor(result / 60);
                Mm = result % 60;
            }
            List<vEmployeeWorktime> items = new List<vEmployeeWorktime>();
            vEmployeeWorktime newItem = new vEmployeeWorktime();
            newItem.ASS_SN = 0;
            newItem.AS_USERID = "";
            newItem.AS_DATE = 0;
            newItem.AS_STIME = 0;
            newItem.AS_ETIME = 0;
            newItem.AS_HOURS = 0;
            newItem.SHOWTIME = Convert.ToString(Dd) +'天'+ Convert.ToString(Hh) +'時'+ Convert.ToString(Mm) +'分';
            items.Add(newItem);
            foreach (var item in i2)
            {
                string oShowTime;
                item.AS_HOURS = calHours(item.AS_DATE, item.AS_ETIME, item.AS_STIME, out oShowTime);
                newItem = new vEmployeeWorktime();
                newItem.ASS_SN = item.ASS_SN;
                newItem.AS_USERID = item.AS_USERID;
                newItem.AS_DATE = item.AS_DATE;
                newItem.AS_STIME = item.AS_STIME;
                newItem.AS_ETIME = item.AS_ETIME;
                newItem.AS_HOURS = item.AS_HOURS;
                newItem.SHOWTIME = oShowTime;
                items.Add(newItem);
            }


            return View("Index",items);
        }
        // GET: Home
        public ActionResult Index()
        {
            HYCEntities model = new HYCEntities();
            List<vEmployeeWorktime> items = new List<vEmployeeWorktime>();

            foreach (var item in model.AS_WORKTIME)
            {
                string oShowTime;
                item.AS_HOURS = calHours(item.AS_DATE, item.AS_ETIME, item.AS_STIME,out oShowTime);
                vEmployeeWorktime newItem = new vEmployeeWorktime();
                newItem.ASS_SN = item.ASS_SN;
                newItem.AS_USERID = item.AS_USERID;
                newItem.AS_DATE = item.AS_DATE;
                newItem.AS_STIME = item.AS_STIME;
                newItem.AS_ETIME = item.AS_ETIME;
                newItem.AS_HOURS = item.AS_HOURS;//SAVE DATA
                newItem.SHOWTIME = oShowTime;//VIEW DATA
                items.Add(newItem);
                
            }
            model.SaveChanges();
            return View(items);
        }

        private double? calHours(int iDATE, int iETIME, int iSTIME, out string oShowTime)
        { 
            string sETIME = Convert.ToString(iETIME).PadLeft(4, '0').Substring(0, 2) + ':' + Convert.ToString(iETIME).PadLeft(4, '0').Substring(2, 2);
            string sSTIME = Convert.ToString(iSTIME).PadLeft(4, '0').Substring(0, 2) + ':' + Convert.ToString(iSTIME).PadLeft(4, '0').Substring(2, 2);
            TimeSpan sHours = (Convert.ToDateTime(sETIME) - Convert.ToDateTime(sSTIME));
            double sTotal = sHours.TotalMinutes;

            oShowTime = String.Format("{0}時{1}分", sHours.Hours, sHours.Minutes);
            return sTotal;
        }


        public ActionResult SummaryList(vSummaryList obj)
        {
            CalWorkTime cal = new CalWorkTime();
            //IQueryable<vEmployeeListSummary> model = cal.getEmployeeListSummary<vEmployeeListSummary>(obj.EmployeeName, obj.StartDate, obj.EndDate);
            var model = cal.GetEmployeeListSummary<vEmployeeListSummary>(obj.EmployeeName/*,obj.StartDate,obj.EndDate*/);
            return View("SummaryList",model);
        }
    }
}