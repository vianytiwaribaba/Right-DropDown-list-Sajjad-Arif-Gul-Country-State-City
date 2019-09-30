using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ALLTypeFormWithDropDown.Controllers
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

        public ActionResult create()
        {
            ImageUploadWithAlltypeFormContext context = new ImageUploadWithAlltypeFormContext();

            ViewBag.Country = new SelectList(context.tbl_countryStateCity.Select(x => x.Cauntry).Distinct().OrderBy(x => x));
            ViewBag.State = new SelectList(new List<string>());
            ViewBag.City = new SelectList(new List<string>());

            return View();
        }

        [HttpPost]
        public ActionResult create(tbl_regis regis)
        {
            ImageUploadWithAlltypeFormContext context = new ImageUploadWithAlltypeFormContext();

            ViewBag.Country = new SelectList(context.tbl_countryStateCity.Select(x => x.Cauntry).Distinct().OrderBy(x => x));
            ViewBag.State = new SelectList(new List<string>());
            ViewBag.City = new SelectList(new List<string>());

            if (!string.IsNullOrEmpty(regis.country))
            {
                // If state not selected, return to view with state list 
                ViewBag.State = new SelectList(context.tbl_countryStateCity.Where(x => x.Cauntry == regis.country).Select(x => x.State).Distinct().OrderBy(state => state));

            }
            if (!string.IsNullOrEmpty(regis.State))
            {
                // If city not selected, return to view with city list
                ViewBag.City = new SelectList(context.tbl_countryStateCity.Where(x => x.State == regis.State).Select(x => x.City).Distinct().OrderBy(city => city));

            }
            if (string.IsNullOrEmpty(regis.country) || string.IsNullOrEmpty(regis.State) || string.IsNullOrEmpty(regis.City))
                return View(regis);
            // make manual validation for example


            context.tbl_regis.Add(regis);

            context.SaveChanges();

            return View();
        }
    }
}