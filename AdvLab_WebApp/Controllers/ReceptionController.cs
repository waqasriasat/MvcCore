

using AdvLab_WebApp.Models;
using AdvLab_WebApp.Models.Reception;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Xml.Linq;

namespace AdvLab_WebApp.Controllers
{
    public class ReceptionController : Controller
    {
        private readonly SampleEfContext _context;
        private readonly IWebHostEnvironment _webHost;
        public ReceptionController(SampleEfContext context, IWebHostEnvironment webHost)
        {
            _context = context;
            _webHost = webHost;
        }
        public async Task<IActionResult> Index()
        {
            var apiUrl = "https://localhost:7121/API/Receptions/All";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string token = HttpContext.Session.GetString("AccessToken");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                            var PatRegViewModelWithLabNo = await response.Content.ReadFromJsonAsync<List<PatRegViewModel>>();
                            return View(PatRegViewModelWithLabNo);
                    }
                    else
                    {
                    }
                }
                catch (Exception ex)
                {
                }

                return View(new List<object>());
            }
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            try
            {
                ViewBag.InitialList = await GetInitialList();
                ViewBag.RelationList = await GetRelationList();
                ViewBag.AgeTypeList = await GetAgeTypeList();
                ViewBag.GenderList = await GetGenderList();

                PatReg PatRegs = new PatReg();
                DescCashier DescCashiers = new DescCashier();
                DescCashiers.DescLabs.Add(new DescLab() { LabNo = 1 });

                var model = new RecepViewModel
                {
                    PatReg = PatRegs,
                    DescCashier = DescCashiers
                };

                return View(model);
            }
            catch (Exception ex)
            {
                // Handle the exception
                return View("Error", ex); // You might have a custom error view
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateViewModel model)
        {
            if (HttpContext.Session.GetInt32("UserID") != null)
            {
                int userId;
                if (int.TryParse(HttpContext.Session.GetInt32("UserID").ToString(), out userId))
                {
                    model.DescCashier.DescLabs.RemoveAll(e => e.Charges == 0);
                    string uniqueFileName = GetUploadedFileName(model.PatReg);
                    model.PatReg.PhotoUrl = uniqueFileName;
                    model.PatReg.RegDate = DateTime.Now;
                    model.PatReg.LastUpdate = DateTime.Now;
                    model.PatReg.UId = userId;
                    model.PatReg.Idloc = HttpContext.Session.GetString("UserLocation");
                    model.DescCashier.UId = userId;
                    model.DescCashier.CpName = HttpContext.Session.GetString("UserLocation");
                    model.DescCashier.Idloc = HttpContext.Session.GetString("UserLocation");
                    model.DescCashier.RegDate = DateTime.Now;
                    model.DescCashier.CpNo = model.DescCashier.CpName + "-" + await GetLocationNo(model.DescCashier.CpName);

                    for (int i = 0; i < model.DescCashier.DescLabs.Count; i++)
                    {
                        model.DescCashier.DescLabs[i].RepDate = await ReportingDate(model.DescCashier.DescLabs[i].DescID);
                        model.DescCashier.DescLabs[i].StatusDate = DateTime.Now;
                     
                    }
                    {
                        var apiUrl = "https://localhost:7121/API/Receptions/Create";

                        using (HttpClient client = new HttpClient())
                        {
                            
                            string token = HttpContext.Session.GetString("AccessToken");
                            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            string jsonData = JsonConvert.SerializeObject(model);
                            HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                            HttpResponseMessage response = await client.PostAsync(apiUrl, content);
                            if (response.IsSuccessStatusCode)
                            {
                                return RedirectToAction("Index");
                            }
                            else
                            {
                                var errorMessage = await response.Content.ReadAsStringAsync();
                                ViewBag.ErrorMessage = errorMessage;
                            }
                        }
                    }
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.InitialList = await GetInitialList();
            ViewBag.RelationList = await GetRelationList();
            ViewBag.AgeTypeList = await GetAgeTypeList();
            ViewBag.GenderList = await GetGenderList();

            var apiUrl = $"https://localhost:7121/API/Receptions/Select/{id}";

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
                        var model = JsonConvert.DeserializeObject<RecepViewModel>(responseData);
                        return View(model);
                    }
                    else if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        return NotFound();
                    }
                    else
                    {
                        var errorMessage = await response.Content.ReadAsStringAsync();
                        ViewBag.ErrorMessage = errorMessage;
                        return View();
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = $"An error occurred: {ex.Message}";
                    return View();
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, CreateViewModel model)
        {
            if (HttpContext.Session.GetInt32("UserID") != null)
            {
                int userId;
                if (int.TryParse(HttpContext.Session.GetInt32("UserID").ToString(), out userId))
                {
                    model.DescCashier.DescLabs.RemoveAll(e => e.Charges == 0);
                    string uniqueFileName = GetUploadedFileName(model.PatReg);
                    model.PatReg.PhotoUrl = uniqueFileName;
                    for (int i = 0; i < model.DescCashier.DescLabs.Count; i++)
                    {
                        model.DescCashier.DescLabs[i].RepDate = await ReportingDate(model.DescCashier.DescLabs[i].DescID);
                        model.DescCashier.DescLabs[i].StatusDate = DateTime.Now;

                    }
                    {
                        var apiUrl = $"https://localhost:7121/API/Receptions/Update/{id}";

                        using (HttpClient client = new HttpClient())
                        {

                            string token = HttpContext.Session.GetString("AccessToken");
                            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            string jsonData = JsonConvert.SerializeObject(model);
                            HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                            HttpResponseMessage response = await client.PutAsync(apiUrl, content);
                            if (response.IsSuccessStatusCode)
                            {
                                return RedirectToAction("Index");
                            }
                            else
                            {
                                var errorMessage = await response.Content.ReadAsStringAsync();
                                ViewBag.ErrorMessage = errorMessage;
                            }
                        }
                    }
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.InitialList = await GetInitialList();
            ViewBag.RelationList = await GetRelationList();
            ViewBag.AgeTypeList = await GetAgeTypeList();
            ViewBag.GenderList = await GetGenderList();

            var apiUrl = $"https://localhost:7121/API/Receptions/Select/{id}";

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
                        var model = JsonConvert.DeserializeObject<RecepViewModel>(responseData);
                        return View(model);
                    }
                    else if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        return NotFound();
                    }
                    else
                    {
                        var errorMessage = await response.Content.ReadAsStringAsync();
                        ViewBag.ErrorMessage = errorMessage;
                        return View();
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = $"An error occurred: {ex.Message}";
                    return View();
                }
            }
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.InitialList = await GetInitialList();
            ViewBag.RelationList = await GetRelationList();
            ViewBag.AgeTypeList = await GetAgeTypeList();
            ViewBag.GenderList = await GetGenderList();

            var apiUrl = $"https://localhost:7121/API/Receptions/Select/{id}";

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
                        var model = JsonConvert.DeserializeObject<RecepViewModel>(responseData);
                        return View(model);
                    }
                    else if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        return NotFound();
                    }
                    else
                    {
                        var errorMessage = await response.Content.ReadAsStringAsync();
                        ViewBag.ErrorMessage = errorMessage;
                        return View();
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = $"An error occurred: {ex.Message}";
                    return View();
                }
            }
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var apiUrl = $"https://localhost:7121/API/Receptions/Delete/{id}";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string token = HttpContext.Session.GetString("AccessToken");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    HttpResponseMessage response = await client.DeleteAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
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
            return View(await _context.Users.FindAsync(id));
        }
        private string GetUploadedFileName(PatReg patRegs)
        {
            string uniqueFileName = null;

            if (patRegs.ProfilePhoto != null)
            {
                string uploadsFolder = Path.Combine(_webHost.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + patRegs.ProfilePhoto.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    patRegs.ProfilePhoto.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
        [HttpPost]
        private async Task<List<SelectListItem>> GetInitialList()
        {
            var apiUrl = "https://localhost:7121/API/Receptions/GetInitialList";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string token = HttpContext.Session.GetString("AccessToken");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        var Initials = await response.Content.ReadFromJsonAsync<List<PatReg_Shortkey>>();
                        if (Initials != null)
                        {

                            var selectList = Initials
                                .Select(l => new SelectListItem { Value = l.Initial, Text = l.Initial })
                                .ToList();

                            selectList.Insert(0, new SelectListItem { Value = "", Text = "" });

                            return selectList;
                        }
                    }
                    else
                    {
                        // Handle non-success response
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions
                }

                return new List<SelectListItem>();
            }
        }
        [HttpPost]
        private async Task<List<SelectListItem>> GetRelationList()
        {
            var apiUrl = "https://localhost:7121/API/Receptions/GetRelationList";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string token = HttpContext.Session.GetString("AccessToken");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        var Relations = await response.Content.ReadFromJsonAsync<List<PatReg_Shortkey>>();
                        if (Relations != null)
                        {

                            var selectList = Relations
                                .Select(l => new SelectListItem { Value = l.Relation, Text = l.Relation })
                                .ToList();
                            var distinctRelations = selectList
                                .Select(item => new SelectListItem { Value = item.Value, Text = item.Text })
                                .Distinct(new SelectListItemComparer())
                                .ToList();



                            distinctRelations.Insert(0, new SelectListItem { Value = "", Text = "" });

                            return distinctRelations;
                        }
                    }
                    else
                    {
                        // Handle non-success response
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions
                }

                return new List<SelectListItem>();
            }
        }
        [HttpPost]
        private async Task<List<SelectListItem>> GetAgeTypeList()
        {
            var apiUrl = "https://localhost:7121/API/Receptions/GetAgeTypeList";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string token = HttpContext.Session.GetString("AccessToken");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        var AgeTypes = await response.Content.ReadFromJsonAsync<List<PatReg_Shortkey>>();
                        if (AgeTypes != null)
                        {

                            var selectList = AgeTypes
                                .Select(l => new SelectListItem { Value = l.Years, Text = l.Years })
                                .ToList();
                            var distinctAgeTypes = selectList
                                .Select(item => new SelectListItem { Value = item.Value, Text = item.Text })
                                .Distinct(new SelectListItemComparer())
                                .ToList();

                            distinctAgeTypes.Insert(0, new SelectListItem { Value = "", Text = "" });

                            return distinctAgeTypes;
                        }
                    }
                    else
                    {
                        // Handle non-success response
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions
                }

                return new List<SelectListItem>();
            }
        }
        [HttpPost]
        private async Task<List<SelectListItem>> GetGenderList()
        {
            var apiUrl = "https://localhost:7121/API/Receptions/GetGenderList";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string token = HttpContext.Session.GetString("AccessToken");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        var Genders = await response.Content.ReadFromJsonAsync<List<PatReg_Shortkey>>();
                        if (Genders != null)
                        {

                            var selectList = Genders
                                .Select(l => new SelectListItem { Value = l.Gender, Text = l.Gender })
                                .ToList();
                            var distinctGenders = selectList
                                .Select(item => new SelectListItem { Value = item.Value, Text = item.Text })
                                .Distinct(new SelectListItemComparer())
                                .ToList();

                            distinctGenders.Insert(0, new SelectListItem { Value = "", Text = "" });

                            return distinctGenders;
                        }
                    }
                    else
                    {
                        // Handle non-success response
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions
                }

                return new List<SelectListItem>();
            }
        }
        [HttpPost]
        public async Task<IActionResult> GetDefaultNameKeys(string InitialValue)
        {
            var apiUrl = "https://localhost:7121/API/Receptions/GetDefaultNameKeys?InitialValue=" + InitialValue;

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string token = HttpContext.Session.GetString("AccessToken");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        var InitialValues = await response.Content.ReadFromJsonAsync<List<PatReg_Shortkey>>();
                        if (InitialValues != null && InitialValues.Any())
                        {
                            var firstInitialValues = InitialValues.First();
                            return Json(new
                            {
                                Initial = firstInitialValues.Initial,
                                Relation = firstInitialValues.Relation,
                                AgeType = firstInitialValues.Years,
                                Gender = firstInitialValues.Gender
                            });
                        }
                    }
                    else
                    {
                    }
                }
                catch (Exception ex)
                {
                }

                return Json(new
                {
                    Initial = string.Empty,
                    Relation = string.Empty,
                    AgeType = string.Empty,
                    Gender = string.Empty
                });
            }
        }
        [HttpGet]
        public async Task<IActionResult> ClientsearchingEvent(string searchTerm)
        {

            var apiUrl = "https://localhost:7121/API/Receptions/GetClient?prefix=" + searchTerm + "&ClientV=" + HttpContext.Session.GetString("UserClientV");

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string token = HttpContext.Session.GetString("AccessToken");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        var Client = await response.Content.ReadFromJsonAsync<List<AddClient>>();
                        if (Client != null && Client.Any())
                        {
                            var DiscountAllow = await GetDiscountAllow(HttpContext.Session.GetInt32("UserID") ?? 0);
                            var BalanceAllow = await GetBalanceAllow(HttpContext.Session.GetInt32("UserID") ?? 0);
                            var ClientResults = Client.Select(ac => new { ClientID = ac.CID, ClientName = ac.CName, Location = ac.Location, PerA = ac.PerA, discountPerAllow= DiscountAllow, balancePerAllow= BalanceAllow });
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
        public async Task<IActionResult> DescriptionsearchingEvent(string searchTerm, int clientID)
        {

            var apiUrl = "https://localhost:7121/API/Receptions/GetDescription?prefix=" + searchTerm + "&CID=" + clientID;

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string token = HttpContext.Session.GetString("AccessToken");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        var ClientDesc = await response.Content.ReadFromJsonAsync<List<GetDescription>>();
                        if (ClientDesc != null && ClientDesc.Any())
                        {
                            var DescResults = ClientDesc.Select(ac => new { DescID = ac.DescID, Price = ac.Price, DescName = ac.DescName });
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
        [HttpGet]
        public async Task<IActionResult> ClientIDzsearchingEvent(int searchTerm)
        {

            var apiUrl = "https://localhost:7121/API/Receptions/GetClientIdz?prefix=" + searchTerm;

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string token = HttpContext.Session.GetString("AccessToken");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        var Client = await response.Content.ReadFromJsonAsync<List<AddClient>>();
                        if (Client != null && Client.Any())
                        {
                            var DiscountAllow = await GetDiscountAllow(HttpContext.Session.GetInt32("UserID") ?? 0);
                            var BalanceAllow = await GetBalanceAllow(HttpContext.Session.GetInt32("UserID") ?? 0);
                            var ClientResults = Client.Select(ac => new { ClientID = ac.CID, ClientName = ac.CName, Location = ac.Location, PerA = ac.PerA, discountPerAllow = DiscountAllow, balancePerAllow = BalanceAllow });
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
        public async Task<IActionResult> DescriptionIDzsearchingEvent(int searchTerm)
        {

            var apiUrl = "https://localhost:7121/API/Receptions/GetDescriptionByID?prefix=" + searchTerm;

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string token = HttpContext.Session.GetString("AccessToken");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        var ClientDesc = await response.Content.ReadFromJsonAsync<List<GetDescription>>();
                        if (ClientDesc != null && ClientDesc.Any())
                        {
                            var DescResults = ClientDesc.Select(ac => new { DescID = ac.DescID, DescName = ac.DescName });
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
        [HttpGet]
        private async Task<string> GetLocationNo(string? cpName)
        {
            try
            {
                string apiUrl = $"https://localhost:7121/API/Receptions/UpdateLocSnoWithGet?location={cpName}";
                using (var client = new HttpClient())
                {
                    string token = HttpContext.Session.GetString("AccessToken");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        string locationNo = await response.Content.ReadAsStringAsync();
                        return locationNo;
                    }
                    else
                    {
                        return "Error"; // or throw new Exception("API call failed");
                    }
                }
            }
            catch (Exception ex)
            {
                return "Error"; // or log ex.Message;
            }
        }
        [HttpGet]
        private async Task<DateTime> ReportingDate(int descID)
        {
            try
            {
                string apiUrl = $"https://localhost:7121/API/Receptions/ReportingDateGet?descID={descID}";
                string token = HttpContext.Session.GetString("AccessToken");

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        String RepDate = await response.Content.ReadAsStringAsync();
                        if (int.TryParse(RepDate, out int reportingDay))
                        {
                            DateTime currentDateTime = DateTime.Now.AddDays(reportingDay);
                            return currentDateTime;
                        }
                        else
                        {
                            throw new FormatException("The response content could not be parsed to an integer.");
                        }
                    }
                    else
                    {
                        throw new HttpRequestException($"API call failed with status code: {response.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred during the API call: {ex.Message}", ex);
            }
        }
        [HttpGet]
        private async Task<int> GetDiscountAllow(int UserID)
        {
            try
            {
                string apiUrl = $"https://localhost:7121/API/Receptions/GetDiscountAllow?userID={UserID}";
                string token = HttpContext.Session.GetString("AccessToken");

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        if (int.TryParse(content, out int DiscountAllow))
                        {
                            return DiscountAllow;
                        }
                        else
                        {
                            throw new InvalidOperationException("Failed to parse DiscountAllow value.");
                        }
                    }
                    else
                    {
                        throw new HttpRequestException($"API call failed with status code: {response.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred during the API call: {ex.Message}", ex);
            }
        }
        [HttpGet]
        private async Task<int> GetBalanceAllow(int UserID)
        {
            try
            {
                string apiUrl = $"https://localhost:7121/API/Receptions/GetBalanceAllow?userID={UserID}";
                string token = HttpContext.Session.GetString("AccessToken");

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        string RepDate = await response.Content.ReadAsStringAsync();
                        if (int.TryParse(RepDate, out int balanceAllow))
                        {
                            return balanceAllow;
                        }
                        else
                        {
                            throw new FormatException("The response content could not be parsed to an integer.");
                        }
                    }
                    else
                    {
                        throw new HttpRequestException($"API call failed with status code: {response.StatusCode}");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred during the API call: {ex.Message}", ex);
            }
        }



    }
}
