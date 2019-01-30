using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            get();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Index(Input input)
        {
            if (!ModelState.IsValid)
            {
                return View(input);
            }

            ViewBag.Message = input.message;
            return View(input);
        }

        public void get()
        {
            try
            {
                var personalaccesstoken = "eifmwfiuysk75kshcixbib6oce3w5huajghgdj6ulnwzesubqazq";
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(
                        new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                        Convert.ToBase64String(
                            System.Text.ASCIIEncoding.ASCII.GetBytes(
                                string.Format("{0}:{1}", "", personalaccesstoken))));

                    using (HttpResponseMessage response = client.GetAsync(
                                "https://microsoftit.vsrm.visualstudio.com/DefaultCollection/OneITVSO/_apis/release/definitions/2239?$expand=environments&api-version=3.0-preview.1").Result)
                    {
                        response.EnsureSuccessStatusCode();
                        string responseBody = response.Content.ReadAsStringAsync().Result;
                        Console.WriteLine(responseBody);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }

    public class Input
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string message { get; set; }
    }
}