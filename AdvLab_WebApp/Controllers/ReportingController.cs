using AdvLab_WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Net;
using AdvLab_WebApp.Models.Reception;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AdvLab_WebApp.Controllers
{
    public class ReportingController : Controller
    {
        private readonly SampleEfContext _context;
        private readonly IWebHostEnvironment _webHost;
        public ReportingController(SampleEfContext context, IWebHostEnvironment webHost)
        {
            _context = context;
            _webHost = webHost;
        } 
        public async Task<IActionResult> Index()
        {
            ViewBag.statusList = await Getstatus();

            var apiUrl = "https://localhost:7121/API/Reportings/All";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string token = HttpContext.Session.GetString("AccessToken");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseData = await response.Content.ReadAsStringAsync();
                        var ReportingViewModel = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ReportingViewModel>>(responseData);
                        return View(ReportingViewModel);
                    }
                    else
                    {
                        var errorMessage = await response.Content.ReadAsStringAsync();
                        ViewBag.ErrorMessage = errorMessage;
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = $"An error occurred: {ex.Message}";
                }
            }
            return View();
        }
        public async Task<IActionResult> SelectedIndex(string mrno, string labno, string daterange, string patientname, string description, string clientname, string status, string contact)
        {
            ViewBag.statusList = await Getstatus();

            var apiUrl = "https://localhost:7121/API/Reportings/Selected";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string token = HttpContext.Session.GetString("AccessToken");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseData = await response.Content.ReadAsStringAsync();
                        var ReportingViewModel = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ReportingViewModel>>(responseData);
                        return View(ReportingViewModel);
                    }
                    else
                    {
                        var errorMessage = await response.Content.ReadAsStringAsync();
                        ViewBag.ErrorMessage = errorMessage;
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = $"An error occurred: {ex.Message}";
                }
            }
            return View();
        }
        private async Task<List<SelectListItem>> Getstatus()
        {
            var apiUrl = "https://localhost:7121/API/Reportings/GetAllStatus";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string token = HttpContext.Session.GetString("AccessToken");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        var status = await response.Content.ReadFromJsonAsync<List<Models.Reporting.GetParamater>>();
                        if (status != null)
                        {
                            var selectList = status.Select(r => new SelectListItem { Value = r.SOR.ToString(), Text = r.DStatus }).ToList();
                            selectList.Insert(0, new SelectListItem { Value = "Please Select", Text = "Please Select" });
                            return selectList;
                        }
                    }
                    else
                    {
                    }
                }
                catch (Exception ex)
                {
                }

                return new List<SelectListItem>();
            }
        }
        [HttpGet]
        public async Task<IActionResult> ClientsearchingEvent(string searchTerm)
        {

            var apiUrl = "https://localhost:7121/API/Reportings/GetClient?prefix=" + searchTerm + "&ClientV=" + HttpContext.Session.GetString("UserClientV");

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string token = HttpContext.Session.GetString("AccessToken");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        var Client = await response.Content.ReadFromJsonAsync<List<Models.Reporting.GetParamater>>();
                        if (Client != null && Client.Any())
                        {
                            var ClientResults = Client.Select(ac => new { ClientName = ac.CName});
                            return Json(ClientResults);
                        }
                    }
                    else
                    {
                    }
                }
                catch (Exception ex)
                {
                }

                return Json(new List<object>());
            }
        }
        [HttpGet]
        public async Task<IActionResult> DescriptionsearchingEvent(string searchTerm)
        {

            var apiUrl = "https://localhost:7121/API/Reportings/GetDescription?prefix=" + searchTerm + "&CID=" + 1;

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string token = HttpContext.Session.GetString("AccessToken");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        var ClientDesc = await response.Content.ReadFromJsonAsync<List<Models.Reporting.GetParamater>>();
                        if (ClientDesc != null && ClientDesc.Any())
                        {
                            var DescResults = ClientDesc.Select(ac => new {DescName = ac.DescName });
                            return Json(DescResults);
                        }
                    }
                    else
                    {
                    }
                }
                catch (Exception ex)
                {
                }

                return Json(new List<object>());
            }
        }
    }
}
