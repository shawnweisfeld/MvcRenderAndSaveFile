using MvcRenderAndSaveFile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace MvcRenderAndSaveFile.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();   
        }

        public ActionResult CustomerInfo(int id)
        {
            return View(new Customer() 
            { 
                FirstName = string.Format("Customer First Name {0}", id),
                LastName = string.Format("Customer Last Name {0}", id),
                Age = id
            });
        }

        public ActionResult ProcessCustomers()
        {
            Parallel.For(0, 100, x =>
            {
                using (var wc = new WebClient())
                {
                    var filename = string.Format(@"{0}\Customer{1}.html", Server.MapPath("~"), x);
                    var result = wc.DownloadString(string.Format("http://localhost:2989/Home/CustomerInfo/{0}", x));
                    System.IO.File.WriteAllText(filename, result);
                }
            });

            return new ContentResult()
            {
                Content = "Done!"
            };
        }
    }
}
