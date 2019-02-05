using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Mvc;
using VsoReleaseTool.Models;

namespace VsoReleaseTool.Controllers
{
    public class ReleasesController : Controller
    {
        // GET: Releases
        public ActionResult List(int releaseDefinitionId)
        {
            var jsonResponse = GetJsonDataFromVso("https://microsoftit.vsrm.visualstudio.com/defaultcollection/OneITVSO/_apis/release/releases?api-version=3.0-preview.1&definitionId=" + releaseDefinitionId + "&$expand=environments&$top=2&queryOrder=descending");
            var jResponseObject = JObject.Parse(jsonResponse);
            var jArray = (JArray)jResponseObject["value"];
            var _listModel = new List<ReleasesModel>();
            foreach (var jObject in jArray)
            {
                var model = new ReleasesModel()
                {
                    id = (int)jObject["id"],
                    createdBy = (string)jObject["createdBy"]["displayName"] + "(" + (string)jObject["createdBy"]["uniqueName"] + ")",
                    createdOn = (DateTime)jObject["createdOn"],
                    modifiedBy = (string)jObject["modifiedBy"]["displayName"] + "(" + (string)jObject["modifiedBy"]["uniqueName"] + ")",
                    modifiedOn = (DateTime)jObject["createdOn"],
                    name = (string)jObject["name"],
                    selfViewLink = (string)jObject["_links"]["self"]["href"],
                    webViewLink = (string)jObject["_links"]["web"]["href"]
                };

                _listModel.Add(model);
            }
            return View(_listModel);
        }

        // GET: Releases/Details/5
        public ActionResult Details(int id)
        {
            var jsonResponse = GetJsonDataFromVso("https://microsoftit.vsrm.visualstudio.com/defaultcollection/OneITVSO/_apis/release/releases/" + id + "?api-version=3.0-preview.1&$expand=environments");
            var jObject = JObject.Parse(jsonResponse);
            var model = new ReleasesModel()
            {
                id = (int)jObject["id"],
                createdBy = (string)jObject["createdBy"]["displayName"] + "(" + (string)jObject["createdBy"]["uniqueName"] + ")",
                createdOn = (DateTime)jObject["createdOn"],
                modifiedBy = (string)jObject["modifiedBy"]["displayName"] + "(" + (string)jObject["modifiedBy"]["uniqueName"] + ")",
                modifiedOn = (DateTime)jObject["createdOn"],
                name = (string)jObject["name"],
                selfViewLink = (string)jObject["_links"]["self"]["href"],
                webViewLink = (string)jObject["_links"]["web"]["href"]
            };

            return View(model);
        }

        // GET: Releases/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Releases/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Releases/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Releases/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Releases/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Releases/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public string GetJsonDataFromVso(string httpsUrl)
        {
            try
            {
                var personalaccesstoken = "eifmwfiuysk75kshcixbib6oce3w5huajghgdj6ulnwzesubqazq";
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}", "", personalaccesstoken))));

                    using (HttpResponseMessage response = client.GetAsync(httpsUrl).Result)
                    {
                        response.EnsureSuccessStatusCode();
                        string responseBody = response.Content.ReadAsStringAsync().Result;
                        return responseBody;
                    }
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
