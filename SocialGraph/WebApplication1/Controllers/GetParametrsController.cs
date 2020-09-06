using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class GetParametrsController : Controller
    {
       
        public ActionResult GetGraf(GrafMaker g)
        {
            if (Request.IsAjaxRequest())
            {
                    return View("PartialGetGraf", g);
            }
            return View("GetGraf", new GrafMaker());
        }

        public ActionResult grafiki(GrafMaker g)
        {
            if (Request.IsAjaxRequest())
            {
                return View("PartialGetGraf", g);
            }
            return View("grafiki", new GrafMaker());
        }
    }
}