using SianalR.MSSQL.Monitor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SianalR.MSSQL.Monitor.Controllers
{
    public class ValuesController : Controller
    {
        DataRepository repository = new DataRepository();

        public JsonResult getData()
        {
            return Json(repository.GetData(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}
