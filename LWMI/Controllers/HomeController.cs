using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Amazon.S3;
using Amazon.S3.Model;
using LWMI.Services;

namespace LWMI.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index(string key)
        {
            if (key == null)
            {
                ViewBag.Text = " ";
            }
            else
            {
                ViewBag.Text = S3Service.GetData(key);
                ViewBag.Key = key;
            }
            return View();
        }

        [System.Web.Mvc.HttpPost]
        [ValidateInput(false)]
        public ActionResult post(string key)
        {
            string data = Request.Form.Keys[0] + Request.Form.Get(0);
            S3Service.SetData(key, data);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}
