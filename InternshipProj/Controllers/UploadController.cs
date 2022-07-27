using DataLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static InternshipProj.Controllers.HomeController;

namespace InternshipProj.Controllers
{
    public class UploadController : Controller
    {
        // This action renders the form
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            string fileName=" ";
            if (file != null && file.ContentLength > 0)
            {
                 fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/App_Data"), "file1.txt");
                file.SaveAs(path);
            }
            return this.RedirectToAction("ReadTxtFile", "Home");
        }

        
    }
}