using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using UsWebServices.Data;
using UsWebServices.Models;

namespace UsWebServices.Controllers
{
    public class OrdersController : Controller
    {
        /*    public IActionResult Index()
            {
                return View();
            }
        */
        public async Task<IActionResult> Index(String searchString)
        {
            List<Order> OrderInfo = new List<Order>();
            List<ServiceType> ServiceTyperInfo = new List<ServiceType>();
            IEnumerable<SelectListItem> list = new List<SelectListItem>();


            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(ConstantsUrl.REST_SERVICE_URL);
                client.DefaultRequestHeaders.Clear();
                // Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string pathOrder = "order";
                string pathServiceTypes = "servicetypes";
                if (!String.IsNullOrEmpty(searchString))
                {
                    //
                    pathOrder = pathOrder + "/Search/" + searchString;
                }
                try
                {
                    // Sending request to find web api REST service resource GetAll using HttpClient
                    HttpResponseMessage ResOrder = await client.GetAsync(pathOrder);
                    HttpResponseMessage ResServiceTypes = await client.GetAsync(pathServiceTypes);
                    // Checking response is successful or not which is sent using HttpClient
                    if (ResOrder.IsSuccessStatusCode && ResServiceTypes.IsSuccessStatusCode)
                    {
                        // Storing the response details received from web api
                        var EmResponseOrder = ResOrder.Content.ReadAsStringAsync().Result;
                        var EmResponseServiceTypes = ResServiceTypes.Content.ReadAsStringAsync().Result;

                        // Deserializing the response received from web api and storing into the order list
                        OrderInfo = JsonConvert.DeserializeObject<List<Order>>(EmResponseOrder);
                        ServiceTyperInfo = JsonConvert.DeserializeObject<List<ServiceType>>(EmResponseServiceTypes);

                        //ViewBag.listAuthers = new SelectList(ServiceTyperInfo);

                        IEnumerable<SelectListItem> myCollection = ServiceTyperInfo
                                           .Select(i => new SelectListItem()
                                           {
                                               Text = i.ToString(),
                                               Value = i.ToString()
                                           });
                        ViewBag.listAuthers = new SelectList(myCollection);

                    }
                }
                catch
                {
                    ViewData["Error"] = ConstantsUrl.ERROR_MESSAGE;
                    return RedirectToAction(nameof(Index));
                }

                //Order test = OrderInfo.Find(x => x.GetId() == "xy");
                return View(OrderInfo);
            }

        }

        // GET: Orders/Create
        public IActionResult Create(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            // Start test

            // End test

            var OrderInfo = new Order();
            OrderInfo.CustomerId = id;

            return View(OrderInfo);
        }
    }
}