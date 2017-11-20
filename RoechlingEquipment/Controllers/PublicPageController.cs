using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RoechlingEquipment.Controllers
{
    public class PublicPageController: Controller
    {
        public ActionResult Error404()
        {
            return View();
        }
    }
}