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

        // This action handles the form POST and the upload
        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)
        {
            string fileName=" ";
            // Verify that the user selected a file
            if (file != null && file.ContentLength > 0)
            {
                // extract only the filename
                 fileName = Path.GetFileName(file.FileName);
                // store the file inside ~/App_Data/uploads folder
                var path = Path.Combine(Server.MapPath("~/App_Data"), "file1.txt");
                file.SaveAs(path);
            }
            // redirect back to the index action to show the form once again
            ViewBag.Message = "Uploaded successfuly!";
            return this.RedirectToAction("ReadTxtFile", "Home");
        }

        
    }
}