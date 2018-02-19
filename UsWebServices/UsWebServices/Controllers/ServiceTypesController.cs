using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UsWebServices.Models;
using UsWebServices.Data;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace UsWebServices.Controllers
{
    public class ServiceTypesController : Controller
    {
        /*   public IActionResult Index()
           {
               return View();
           }
       */
        public async Task<ActionResult> Index(String searchString)
        {
            //searchString = "1";
            List<ServiceType> ServiceTyperInfo = new List<ServiceType>();
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(ConstantsUrl.REST_SERVICE_URL);
                client.DefaultRequestHeaders.Clear();
                // Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string path = "servicetypes";

                if (!String.IsNullOrEmpty(searchString))
                {
                    path = path + "/" + searchString;
                }
                try
                {
                    // Sending request to find web api REST service resource GetAll using HttpClient
                    HttpResponseMessage Res = await client.GetAsync(path);
                    // Checking response is successful or not which is sent using HttpClient
                    if (Res.IsSuccessStatusCode)
                    {
                        // Storing the response details received from web api
                        var EmResponse = Res.Content.ReadAsStringAsync().Result;

                        // Deserializing the response received from web api and storing into the customer list
                        ServiceTyperInfo = JsonConvert.DeserializeObject<List<ServiceType>>(EmResponse);
                    }
                }
                catch
                {
                    ViewData["Error"] = ConstantsUrl.ERROR_MESSAGE;
                    return RedirectToAction(nameof(Index));
                }
                return View(model: ServiceTyperInfo);
            }
        }

        // GET: ServiceType/Create
        public IActionResult Create()
        {
            return View();
        }
    }
}