using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebApp_RoleClaims_DotNet.DAL;

namespace WebApp_RoleClaims_DotNet.Controllers
{
    public class TasksController : Controller
    {
        //
        // The Client ID is used by the application to uniquely identify itself to Azure AD.
        // The App Key is a credential used by the application to authenticate to Azure AD.
        // The Tenant is the name of the Azure AD tenant in which this application is registered.
        // The AAD Instance is the instance of Azure, for example public Azure or Azure China.
        // The Authority is the sign-in URL of the tenant.
        //
        private static string aadInstance = ConfigurationManager.AppSettings["ida:AADInstance"];
        private static string tenant = ConfigurationManager.AppSettings["ida:Tenant"];
        private static string clientId = ConfigurationManager.AppSettings["ida:ClientId"];
        private static string appKey = ConfigurationManager.AppSettings["ida:AppKey"];

        static string authority = String.Format(CultureInfo.InvariantCulture, aadInstance, tenant);

        //
        // To authenticate to the To Do list service, the client needs to know the service's App ID URI.
        // To contact the To Do list service we need it's URL as well.
        //
        private static string todoListResourceId = ConfigurationManager.AppSettings["todo:TodoListResourceId"];
        private static string todoListBaseAddress = ConfigurationManager.AppSettings["todo:TodoListBaseAddress"];

        private static HttpClient httpClient = new HttpClient();
        private static AuthenticationContext authContext = null;
        private static ClientCredential clientCredential = null;

        /// <summary>
        /// Lists Out the Tasks stored in the database.  RBAC to editing tasks is controlled by 
        /// the View and other controller actions.  Requires the user has at least one
        /// of the application roles to view tasks.
        /// </summary>
        /// <returns>The Tasks Page.</returns>
        [HttpGet]
        [Authorize(Roles = "Admin, Observer, Writer, Approver")]
        public ActionResult Index()
        {
            var bearerAccessToken = CreateBearerAccessToken();
            
            if (string.IsNullOrWhiteSpace(bearerAccessToken))
                return View();

            bool isSuccessResponseCode;
            var jsonText = SendRequestGetResponse("api/ToGoList", "GET", null, bearerAccessToken, out isSuccessResponseCode);

            var listOfTasks = new List<Models.Task>();
            JArray jArray = JArray.Parse(jsonText);
            foreach (var item in jArray)
            {
                listOfTasks.Add(new Models.Task
                {
                    TaskID = (int)item["ID"],
                    TaskText = (string)item["Description"],
                    Status = "Not Started"
                });
            }
            
            ViewBag.Message = "Tasks";
            if (isSuccessResponseCode == true)
                ViewData["tasks"] = listOfTasks;
            else
                ViewData["tasks"] = TasksDbHelper.GetAllTasks();
            return View();
        }

        /// <summary>
        /// Add a new task to the database or Update the Status of an Existing Task.  Requires that
        /// the user has a application role of Admin, Writer, or Approver, and only allows certain actions based
        /// on which role(s) the user has been granted.
        /// </summary>
        /// <param name="formCollection">The user input including task name and status.</param>
        /// <returns>A Redirect to the Tasks Page.</returns>
        [HttpPost]
        [Authorize(Roles = "Admin, Writer, Approver")]
        public ActionResult TaskSubmit(FormCollection formCollection)
        {
            var response = string.Empty;
            var bearerAccessToken = CreateBearerAccessToken();
            
            if (string.IsNullOrWhiteSpace(bearerAccessToken))
                return RedirectToAction("Index", "Tasks");

            if (User.IsInRole("Admin") || User.IsInRole("Writer"))
            {
                // Add A New task to Tasks.xml
                if (formCollection["newTask"] != null && formCollection["newTask"].Length != 0)
                {
                    bool isSuccessResponseCode;
                    response = SendRequestGetResponse("api/ToGoList", "POST", new StringContent(@"{""Description"": """ + formCollection["newTask"] + @"""}", Encoding.UTF8, "application/json"), bearerAccessToken, out isSuccessResponseCode);
                    //TasksDbHelper.AddTask(formCollection["newTask"]);
                }
            }

            if (User.IsInRole("Admin") || User.IsInRole("Approver"))
            {
                // Change status of existing task
                foreach (string key in formCollection.Keys)
                {
                    if (key != "newtask" && key != "delete")
                    {
                        bool isSuccessResponseCode;
                        response = SendRequestGetResponse("api/ToGoList", "PUT", new StringContent(@"{""ID"": """ + Convert.ToInt32(key) + @""", ""Description"": """ + formCollection[key] + @"""}", Encoding.UTF8, "application/json"), bearerAccessToken, out isSuccessResponseCode);
                        //TasksDbHelper.UpdateTask(Convert.ToInt32(key), formCollection[key]);
                    }
                }
            }

            if (User.IsInRole("Admin"))
            {
                // Delete a Task
                foreach (string key in formCollection.Keys)
                {
                    if (key == "delete" && formCollection[key] != null && formCollection[key].Length > 0)
                    {
                        string[] toDelete = formCollection[key].Split(',');
                        foreach (string id in toDelete)
                        {
                            bool isSuccessResponseCode;
                            response = SendRequestGetResponse("api/ToGoList/" + id, "DELETE", null, bearerAccessToken, out isSuccessResponseCode);
                            //TasksDbHelper.DeleteTask(Convert.ToInt32(id));
                        }
                    }
                }
            }
            return RedirectToAction("Index", "Tasks");
        }

        public string CreateBearerAccessToken()
        {
            #region CREATE_BEARER_ACCESS_TOKEN
            authContext = new AuthenticationContext(authority);
            clientCredential = new ClientCredential(clientId, appKey);

            //
            // Get an access token from Azure AD using client credentials.
            // If the attempt to get a token fails because the server is unavailable, retry twice after 3 seconds each.
            //
            AuthenticationResult result = null;
            int retryCount = 0;
            bool retry = false;

            do
            {
                retry = false;
                try
                {
                    // ADAL includes an in memory cache, so this call will only send a message to the server if the cached token is expired.
                    result = authContext.AcquireTokenAsync(todoListResourceId, clientCredential).Result;
                }
                catch (AdalException ex)
                {
                    Console.WriteLine(
                        String.Format("An error occurred while acquiring a token\nTime: {0}\nError: {1}\nRetry: {2}\n",
                        DateTime.Now.ToString(),
                        ex.ToString(),
                        retry.ToString()));
                }

            } while ((retry == true) && (retryCount < 3));

            if (result == null)
            {
                Console.WriteLine("Canceling attempt to contact To Do list service.\n");
                return "Canceling attempt to contact To Do list service.\n";
            }
            #endregion

            return result.AccessToken;
        }

        public string SendRequestGetResponse(string relativeUrlForHttpResource, string httpVerbAsString, HttpContent httpContent, string bearerAccessToken, out bool isSuccessResponseCode)
        {
            // Add the access token to the authorization header of the request.
            var request = new HttpRequestMessage(new HttpMethod(httpVerbAsString), todoListBaseAddress + relativeUrlForHttpResource);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", bearerAccessToken);
            if (httpContent != null) request.Content = httpContent;
            var response = httpClient.SendAsync(request).Result;
            isSuccessResponseCode = response.IsSuccessStatusCode;

            return response.Content.ReadAsStringAsync().Result;
        }
    }
}