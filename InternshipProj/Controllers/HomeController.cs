using InternshipProj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

//added

using DataLibrary.DataAccess;
using DataLibrary.BusinessLogic;
using System.IO;

namespace InternshipProj.Controllers
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

        

        public ActionResult ViewClients()
        {
            ViewBag.Message = "Shiko klientet qe ndodhen ne sistem";

            var data = ClientProcessor.LoadClients();
            List<ClientModel> clients = new List<ClientModel>();
            foreach (var row in data)
            {
                clients.Add(new ClientModel
                {
                    ClientId = row.ClientId,
                    FirstName = row.FirstName,
                    LastName = row.LastName,
                    EmailAddress = row.EmailAddress,
                    Birthday = row.Birthday
                });
            }
            
            return View(clients);
        }
        public ActionResult AddClient()
        {
            ViewBag.Message = "Add a new Client in the system";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]    //shton sigurine e te dhenave qe ndodhen ne forme
        public ActionResult AddClient(ClientModel cl)
        {
            if(ModelState.IsValid)
            {
                int recordsCreated = ClientProcessor.CreateClient(cl.ClientId, 
                    cl.FirstName, cl.LastName, cl.EmailAddress, cl.Birthday);
                return RedirectToAction("ViewClients");
            }

            return View();
        }

        public ActionResult ReadTxtFile()
        {
            string line;
            string tab = "\t";

            using (StreamReader file = new StreamReader(Server.MapPath(@"~/App_Data/file1.txt")))
            {

                var client = new ClientModel();
                while ((line = file.ReadLine()) != null)
                {
                    string[] fields = line.Split(tab.ToCharArray());


                    client.ClientId = fields[0];
                    client.FirstName = fields[1];
                    client.LastName = fields[2];
                    client.EmailAddress = fields[3];
                    client.Birthday = Convert.ToDateTime(fields[4]);

                    InsertClientIfNotExists(client.ClientId, client);
                }

                return RedirectToAction("ViewClients");
            }
        }

        public void InsertClientIfNotExists(string id, ClientModel client)
        {
            if (SqlDataAccess.CountData(id) is false)
            {
                //klienti nuk ekziston
                AddClient(client);
            }
        }
    }
}