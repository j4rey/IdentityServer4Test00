using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVCClient.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json.Linq;
using System.Net.Http;

namespace MVCClient.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }
        
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        //public async Task Logout()
        //{
        //    await HttpContext.SignOutAsync("Cookies");
        //    await HttpContext.SignOutAsync("oidc");
        //}

        public IActionResult Logout()
        {
            return new SignOutResult(new[] { "Cookies", "oidc" });
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> CallApiUsingUserAccessToken()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var client = new System.Net.Http.HttpClient();
            client.SetBearerToken(accessToken);
            //client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
            var content = await client.GetStringAsync("http://localhost:5001/api/values");

            ViewBag.Json = JArray.Parse(content).ToString();
            return View("json");
        }
    }
}
