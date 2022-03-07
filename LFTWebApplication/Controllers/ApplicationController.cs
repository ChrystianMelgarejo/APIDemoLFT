using Newtonsoft.Json;
using SharedLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace LFTWebApplication.Controllers
{
    public class ApplicationController : Controller
    {
        Uri apiBaseAddrees = new Uri("http://localhost:63729/api");
        HttpClient client;

        public ApplicationController()
        {
            client = new HttpClient();
            client.BaseAddress = apiBaseAddrees;
        }

        // GET: Application
        public ActionResult Index()
        {
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/application").Result;
            List<Application> applications = new List<Application>();
            
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                applications = JsonConvert.DeserializeObject<List<Application>>(data);
            }

            return View(applications);
        }

        #region Search Methods        

        [HttpGet]
        public ActionResult Search()
        {               
            return View();
        }

        [HttpPost]
        public ActionResult Search(Search searchCriteria)
        {
            List<Application> matchedApplications = new List<Application>();

            string data = JsonConvert.SerializeObject(searchCriteria);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/SearchApplications", content).Result;

            if (response.IsSuccessStatusCode)
            {
                string resultData = response.Content.ReadAsStringAsync().Result;
                matchedApplications = JsonConvert.DeserializeObject<List<Application>>(resultData);
            }

            return View("Index", matchedApplications);
        }

        #endregion Search Methods

        #region Create Methods

        [HttpGet]
        public ActionResult Create()
        {
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/CreateApplication").Result;
            Application newApplication = new Application();
            
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                newApplication = JsonConvert.DeserializeObject<Application>(data);
            }
            
            return View("Edit", newApplication);
        }

        [HttpPost]
        public ActionResult Create(Application applicationToUpdate)
        {
            string data = JsonConvert.SerializeObject(applicationToUpdate);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/EditApplication", content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Application");
            }

            return View();
        }

        #endregion Create Methods

        #region Edit Methods

        [HttpGet]
        public ActionResult Edit(int applicationId)
        {
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/GetApplication?applicationId=" + applicationId.ToString()).Result;
            Application application = new Application();

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                application = JsonConvert.DeserializeObject<Application>(data);
            }

            return View(application);
        }

        [HttpPost]
        public ActionResult Edit(Application applicationToUpdate)
        {
            string data = JsonConvert.SerializeObject(applicationToUpdate);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/EditApplication", content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Application");
            }

            return View();
        }

        #endregion

        #region Delete Methods

        [HttpGet]
        public ActionResult Delete(int applicationId)
        {
            HttpResponseMessage response = client.DeleteAsync(client.BaseAddress + "/DeleteApplication?applicationId=" + applicationId.ToString()).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Application");
            }

            return View();
        }

        #endregion Delete Methods
    }
}