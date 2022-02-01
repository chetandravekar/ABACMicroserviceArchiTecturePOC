using FrontEndMVCApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FrontEndMVCApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {


        //static readonly string baseUrl = "http://localhost:54784";
        static readonly string baseUrl = "http://localhost:5011";
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }


        public async Task<ActionResult> Products()
        {
            try
            {
                string idToken = await HttpContext.GetTokenAsync("id_token");
                HttpClient client = new HttpClient
                {
                    BaseAddress = new Uri(baseUrl)
                };

                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + idToken);
                HttpResponseMessage response = await client.GetAsync($"/Products");

                if (response.IsSuccessStatusCode)
                {
                    var products = await DeserializeResponseContent(response);
                    return View(products);
                }
                else
                {
                    // If the call failed with access denied, show the user an error indicating they might need to sign-in again.
                    if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        return new RedirectResult("/Home/Error?message=" + response.ReasonPhrase + " You might need to sign in again.");
                    }
                    if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                    {
                        return new RedirectResult("/Home/Error?message=" + response.ReasonPhrase + " Access denied.");
                    }
                }

                return new RedirectResult("/Error?message=An Error Occurred Reading Orders List: " + response.StatusCode);
            }
            catch (Exception ex)
            {
                return new RedirectResult("/Error?message=An Error Occurred Reading Orders List: " + ex.Message);
            }
        }

        public IActionResult Customers()
        {
            return View();
        }


        static async Task<List<ProductResult>> DeserializeResponseContent(HttpResponseMessage response)
        {
            var productResult = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<ProductResult>>(productResult);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(string message)
        {
            //string message = Request.QueryString["message"].ToString();
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Message = message });
        }


    }
}
