using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UsWebServices.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using UsWebServices.Data;
using Newtonsoft.Json;

namespace UsWebServices.Controllers
{
    public class CustomersController : Controller
    {
        /*    public IActionResult Index()
            {
                return View();
            }
        */
        public async Task<ActionResult> Index(String searchString)
        {
            List<Customer> CustomerInfo = new List<Customer>();
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(ConstantsUrl.REST_SERVICE_URL);
                client.DefaultRequestHeaders.Clear();
                // Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string path = "customer";

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
                        CustomerInfo = JsonConvert.DeserializeObject<List<Customer>>(EmResponse);
                    }
                }
                catch
                {
                    ViewData["Error"] = ConstantsUrl.ERROR_MESSAGE;
                    return RedirectToAction(nameof(Index));
                }
                return View(model: CustomerInfo);
            }
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Customer customer)
        {
            //var CustomerInfo = new Customer();
            using (var client = new HttpClient())
            {
                // Passing service base url
                client.BaseAddress = new Uri(ConstantsUrl.REST_SERVICE_URL);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    // Sending request to find web api REST service resource using HttpClient
                    HttpResponseMessage Res = await client.PostAsJsonAsync("customer", customer);
                    // Checking the response
                    if (!Res.IsSuccessStatusCode)
                    {
                        ViewData["Error"] = ConstantsUrl.ERROR_MESSAGE;
                    }
                }
                catch
                {
                    ViewData["Error"] = ConstantsUrl.ERROR_MESSAGE;
                }
                // rerturn the customer list to view
                return RedirectToAction(nameof(Index));
                //return RedirectToAction("Create", "Orders", 1);
            }
        }
    }
}