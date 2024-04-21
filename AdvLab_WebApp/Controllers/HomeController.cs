using AdvLab_WebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;

namespace AdvLab_WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly SampleEfContext context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HomeController(SampleEfContext context, IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            if (HttpContext.Session.GetInt32("UserID") != null)
            {
                return RedirectToAction("Dashboard");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(User users)
        {
            var apiUrl = "https://localhost:7121/API/Users/login"; // Replace with your actual API URL
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string jsonData = JsonConvert.SerializeObject(users);
                    HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponseString = await response.Content.ReadAsStringAsync();
                        var apiResponse = JsonConvert.DeserializeAnonymousType(apiResponseString, new { Token = "", empID = "", uName = "", cnl = "", clientV = "", depart = "",
                            subDepart = "", placement = "", designation = "", roleID = "", location = "" });
                        HttpContext.Session.SetInt32("UserID", users.EmpId);
                        HttpContext.Session.SetString("UserName", apiResponse.uName);
                        HttpContext.Session.SetString("UserCNL", apiResponse.cnl);
                        HttpContext.Session.SetString("UserClientV", apiResponse.clientV);
                        HttpContext.Session.SetString("UserDepart", apiResponse.depart);
                        HttpContext.Session.SetString("UserSubDepart", apiResponse.subDepart);
                        HttpContext.Session.SetString("UserPlacement", apiResponse.placement);
                        HttpContext.Session.SetString("UserDesignation", apiResponse.designation);
                        HttpContext.Session.SetString("UserRoleID", apiResponse.roleID);
                        HttpContext.Session.SetString("UserLocation", apiResponse.location);
                   
                        HttpContext.Session.SetString("AccessToken", apiResponse.Token);
                        await GetAccessRightByID(users.EmpId);
                        return RedirectToAction("Dashboard");
                    }
                    else
                    {
                        string errorMessage = await response.Content.ReadAsStringAsync();
                        TempData["ErrorMessage"]= errorMessage;
                        //ViewBag.Message = errorMessage;
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "An error occurred: " + ex.Message;
                    return View();
                }
            }

            
            return View();
  
        }
        [HttpGet]
        private async Task<IActionResult> GetAccessRightByID(int empId)
        {
            var apiUrl = $"https://localhost:7121/API/AccessRights/{empId}";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string token = HttpContext.Session.GetString("AccessToken");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        //var responseData = await response.Content.ReadAsAsync<List<AccessRight>>();
                        var responseData = await response.Content.ReadFromJsonAsync<List<AccessRight>>();
                        // Convert the data to a DataTable
                        DataTable dataTable = ConvertToDataTable(responseData);

                        // Store DataTable in the session
                        HttpContext.Session.SetString("AccessRightData", JsonConvert.SerializeObject(dataTable));
                    }
                    else
                    {
                        // Handle the error
                        var errorMessage = await response.Content.ReadAsStringAsync();
                        ViewBag.ErrorMessage = errorMessage;
                    }
                }
                catch (Exception ex)
                {
                    // Log or handle the exception
                    ViewBag.ErrorMessage = $"An error occurred: {ex.Message}";
                }
            }

            return View();
        }

        private DataTable ConvertToDataTable(List<AccessRight> data)
        {
            //DataTable table = new DataTable();

            //// Create columns
            //foreach (var prop in typeof(AccessRight).GetProperties())
            //{
            //    table.Columns.Add(prop.Name, prop.PropertyType);
            //}

            //// Add rows
            //foreach (var item in data)
            //{
            //    DataRow row = table.NewRow();
            //    foreach (var prop in typeof(AccessRight).GetProperties())
            //    {
            //        row[prop.Name] = prop.GetValue(item);
            //    }
            //    table.Rows.Add(row);
            //}

            //return table;
            DataTable table = new DataTable();

            // Assuming your AccessRight class has properties like ID, SOR, EmpID, Assigning
            table.Columns.Add("ID", typeof(int));
            table.Columns.Add("SOR", typeof(int));
            table.Columns.Add("EmpID", typeof(int));
            table.Columns.Add("Assigning", typeof(string));

            // Add rows
            foreach (var item in data)
            {
                DataRow row = table.NewRow();
                row["ID"] = item.ID;
                row["SOR"] = item.SOR;
                row["EmpID"] = item.EmpID;
                row["Assigning"] = item.Assigning;
                table.Rows.Add(row);
            }

            return table;
        }

        public IActionResult Dashboard()
        {
            if(HttpContext.Session.GetInt32("UserID") != null)
            {
                ViewBag.MySession = HttpContext.Session.GetInt32("UserID").ToString();
            }
            else
            {
                return RedirectToAction("Login");
            }
            return View();
        }
        public IActionResult Logout()
        {
            if (HttpContext.Session.GetInt32("UserID") != null)
            {
                HttpContext.Session.Remove("UserID");
                return RedirectToAction("Login");
            }
            else
            {
                return RedirectToAction("Login");
            }
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
