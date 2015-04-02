using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LWMI.Services;

namespace LWMI.Controllers
{
    public class BucketController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Keys = S3Service.GetBucketList();
            return View();
        }
    }
}
