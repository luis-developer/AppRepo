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
using System.Text.RegularExpressions;

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

                    if (FileIsValid(client))
                    {
                        InsertClientIfNotExists(client.ClientId, client);
                    }
                    else
                    {
                        return View();
                    }

                }

                return RedirectToAction("ViewClients");

            }
        }

        public bool FileIsValid(ClientModel client)
        {
            if (string.IsNullOrEmpty(client.ClientId))
            {
                return false;
            }
            if (!string.IsNullOrEmpty(client.EmailAddress))
            {
                string emailRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                                         @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                                            @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
                Regex re = new Regex(emailRegex);
                if (!re.IsMatch(client.EmailAddress))
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            
            return true;
        }

        public void InsertClientIfNotExists(string id, ClientModel client)
        {
                if (SqlDataAccess.CountData(id) is false)
                {
                    //klienti nuk ekziston -> shtohet ne sistem
                    AddClient(client);
                }
        }
    }
}