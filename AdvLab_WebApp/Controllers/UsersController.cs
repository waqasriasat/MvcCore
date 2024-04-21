using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AdvLab_WebApp.Models;
using Newtonsoft.Json;
using System.Data;
using System.Net.Http.Headers;
using System.Net;
using System.Text;
using System.Net.Http;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Authorization;
using AdvLab_WebApp.Models.Users;


namespace AdvLab_WebApp.Controllers
{
    public class UsersController : Controller
    {
        private readonly SampleEfContext _context;
        public UsersController(SampleEfContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                var apiUrl = "https://localhost:7121/API/Users/All";

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
                            var users = JsonConvert.DeserializeObject<List<User>>(responseData);
                            return View(users);
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
            else
            {
                var apiUrl = $"https://localhost:7121/API/Users/Select/{id}";
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
                            var user = JsonConvert.DeserializeObject<User>(responseData);
                            var userList = new List<User> { user }; // Create a list containing the single user
                            return View(userList); // Pass the list to the view
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
        }
       
     
        public async Task<IActionResult> Create()
        {
            ViewBag.RoleList = await GetRoles();
            ViewBag.StatusList = await GetSatus();
            ViewBag.ClientVList = await GetClientV();
            ViewBag.CnlList = await GetCnl();
            ViewBag.LocationList = await GetLocation();
            ViewBag.DepartList = await GetDepart();
            ViewBag.SubDepartList = await GetSubDepart();
            ViewBag.PlacementList = await GetPlacement();
            ViewBag.DesignationList = await GetDesignation();
            var user = new AdvLab_WebApp.Models.User();
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> Create(User newUser)
        {
            
            newUser.LoginStatus = "null";
            newUser.LoginStatusIp = "null";
            newUser.LoginStatusComp = "null";
            newUser.Nsend = "null";
            newUser.Uid = 0;
            newUser.Idloc = "null";
            newUser.CompMac = "null";
            newUser.PayGenerate = "null";
            newUser.Token = "null";
            if (ModelState.IsValid)
            {
                var apiUrl = "https://localhost:7121/API/Users/Create";

                using (HttpClient client = new HttpClient())
                {
                    try
                    {
                        string token = HttpContext.Session.GetString("AccessToken");
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                        string jsonData = JsonConvert.SerializeObject(newUser);
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
                    catch (Exception ex)
                    {
                        ViewBag.ErrorMessage = $"An error occurred: {ex.Message}";
                    }
                }
            }
            return View(newUser);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.RoleList = await GetRoles();
            ViewBag.StatusList = await GetSatus();
            ViewBag.ClientVList = await GetClientV();
            ViewBag.CnlList = await GetCnl();
            ViewBag.LocationList = await GetLocation();
            ViewBag.DepartList = await GetDepart();
            ViewBag.SubDepartList = await GetSubDepart();
            ViewBag.PlacementList = await GetPlacement();
            ViewBag.DesignationList = await GetDesignation();
            if (id == null)
            {
                return NotFound();
            }

            var apiUrl = $"https://localhost:7121/API/Users/Select/{id}";

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
                        var user = JsonConvert.DeserializeObject<User>(responseData);
                        return View(user);
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
        public async Task<IActionResult> Edit(int EmpId, User editedUser)
        {
            if (EmpId != editedUser.EmpId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                //int selectedRoleId = editedUser.RoleId;
                //editedUser.RoleId = selectedRoleId;
                var apiUrl = $"https://localhost:7121/API/Users/Update/{EmpId}";

                using (HttpClient client = new HttpClient())
                {
                    try
                    {
                        string token = HttpContext.Session.GetString("AccessToken");
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                        string jsonData = JsonConvert.SerializeObject(editedUser);
                        HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                        HttpResponseMessage response = await client.PutAsync(apiUrl, content);

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
            }
            return View(editedUser);
        }
        public async Task<IActionResult> Details(int? id)
        {
            ViewBag.RoleList = await GetRoles();
            ViewBag.StatusList = await GetSatus();
            ViewBag.ClientVList = await GetClientV();
            ViewBag.CnlList = await GetCnl();
            ViewBag.LocationList = await GetLocation();
            ViewBag.DepartList = await GetDepart();
            ViewBag.SubDepartList = await GetSubDepart();
            ViewBag.PlacementList = await GetPlacement();
            ViewBag.DesignationList = await GetDesignation();
            if (id == null)
            {
                return NotFound();
            }

            var apiUrl = $"https://localhost:7121/API/Users/Select/{id}";

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
                        var user = JsonConvert.DeserializeObject<User>(responseData);
                        return View(user);
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
        
        public async Task<IActionResult> Delete(int? id)
        {
            ViewBag.RoleList = await GetRoles();
            ViewBag.StatusList = await GetSatus();
            ViewBag.ClientVList = await GetClientV();
            ViewBag.CnlList = await GetCnl();
            ViewBag.LocationList = await GetLocation();
            ViewBag.DepartList = await GetDepart();
            ViewBag.SubDepartList = await GetSubDepart();
            ViewBag.PlacementList = await GetPlacement();
            ViewBag.DesignationList = await GetDesignation();
            if (id == null)
            {
                return NotFound();
            }

            var apiUrl = $"https://localhost:7121/API/Users/Select/{id}";

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
                        var user = JsonConvert.DeserializeObject<User>(responseData);
                        return View(user);
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
            var apiUrl = $"https://localhost:7121/API/Users/Delete/{id}";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string token = HttpContext.Session.GetString("AccessToken");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    // Send a DELETE request to delete the user
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

            // If an error occurred, return to the delete view
            return View(await _context.Users.FindAsync(id));
        }
        private bool UserExists(int id)
        {
            return true;
        }
        private async Task<List<SelectListItem>> GetClientV()
        {
            var apiUrl = "https://localhost:7121/API/Users/GetAllLocation";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string token = HttpContext.Session.GetString("AccessToken");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        var locations = await response.Content.ReadFromJsonAsync<List<AddLocation>>();
                        if (locations != null)
                        {
                            // Now 'locations' is a List of AddLocation objects.
                            // You can use it as needed.
                            // For example, if you want to convert it to a List of SelectListItem:

                            var selectList = locations
                                .Select(l => new SelectListItem { Value = l.LocCate, Text = l.LocCate })
                                .ToList();
                            selectList.Insert(0, new SelectListItem { Value = "Please Select", Text = "Please Select" });

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
        private async Task<List<SelectListItem>> GetCnl()
        {
            var apiUrl = "https://localhost:7121/API/Users/GetAllCNL";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string token = HttpContext.Session.GetString("AccessToken");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        var roles = await response.Content.ReadFromJsonAsync<List<AddConnLab>>();
                        if (roles != null)
                        {
                            // Now 'roles' is a List of AddConnLab objects.
                            // You can use it as needed.
                            // For example, if you want to convert it to a List of SelectListItem:

                            var selectList = roles
                                .Select(r => new SelectListItem { Value = r.LocCate, Text = r.LocCate })
                                .ToList();

                            selectList.Insert(0, new SelectListItem { Value = "Please Select", Text = "Please Select" });

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
        private async Task<List<SelectListItem>> GetLocation()
        {
            var apiUrl = "https://localhost:7121/API/Users/GetAllLocation";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string token = HttpContext.Session.GetString("AccessToken");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        var locations = await response.Content.ReadFromJsonAsync<List<AddLocation>>();
                        if (locations != null)
                        {
                            // Now 'locations' is a List of AddLocation objects.
                            // You can use it as needed.
                            // For example, if you want to convert it to a List of SelectListItem:

                            var selectList = locations
                                .Select(l => new SelectListItem { Value = l.LocCate, Text = l.LocCate })
                                .ToList();

                            selectList.Insert(0, new SelectListItem { Value = "Please Select", Text = "Please Select" });

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
        private async Task<List<SelectListItem>> GetDepart()
        {
            var apiUrl = "https://localhost:7121/API/Users/GetAllDepart";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string token = HttpContext.Session.GetString("AccessToken");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        var departments = await response.Content.ReadFromJsonAsync<List<AddDepart>>();
                        if (departments != null)
                        {
                            // Now 'departments' is a List of AddDepart objects.
                            // You can use it as needed.
                            // For example, if you want to convert it to a List of SelectListItem:

                            var selectList = departments
                                .Select(d => new SelectListItem { Value = d.Depart, Text = d.Depart })
                                .ToList();

                            selectList.Insert(0, new SelectListItem { Value = "Please Select", Text = "Please Select" });

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
        private async Task<List<SelectListItem>> GetSubDepart()
        {
            var apiUrl = "https://localhost:7121/API/Users/GetAllSubDepart";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string token = HttpContext.Session.GetString("AccessToken");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        var subDepartments = await response.Content.ReadFromJsonAsync<List<AddSubDepart>>();
                        if (subDepartments != null)
                        {
                            // Now 'subDepartments' is a List of AddSubDepart objects.
                            // You can use it as needed.
                            // For example, if you want to convert it to a List of SelectListItem:

                            var selectList = subDepartments
                                .Select(sd => new SelectListItem { Value = sd.SubDepart, Text = sd.SubDepart })
                                .ToList();

                            selectList.Insert(0, new SelectListItem { Value = "Please Select", Text = "Please Select" });

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
        public async Task<List<SelectListItem>> GetSubDepartByDepartment(string departValue)
        {
            var apiUrl = "https://localhost:7121/API/Users/GetAllSubDepartbyDepart?depart="+departValue;

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string token = HttpContext.Session.GetString("AccessToken");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        var subDepartments = await response.Content.ReadFromJsonAsync<List<AddSubDepart>>();
                        if (subDepartments != null)
                        {
                            var selectList = subDepartments
                                .Select(sd => new SelectListItem { Value = sd.SubDepart, Text = sd.SubDepart })
                                .ToList();
                            ViewBag.SubDepartList = selectList;
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
        public async Task<IActionResult> GetPlacementAndDepartBySubDepart(string subDepartValue)
        {
            var apiUrl = "https://localhost:7121/API/Users/GetAllPlacementandDepartbySubDepart?subDepartValue=" + subDepartValue;

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string token = HttpContext.Session.GetString("AccessToken");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        var subDepartments = await response.Content.ReadFromJsonAsync<List<AddSubDepart>>();
                        if (subDepartments != null && subDepartments.Any())
                        {
                            var firstSubDepart = subDepartments.First();
                            return Json(new { placement = firstSubDepart.Location, depart = firstSubDepart.Depart });
                        }
                    }
                    else
                    {
                    }
                }
                catch (Exception ex)
                {
                }

                return Json(new { placement = string.Empty, depart = string.Empty });
            }
        }
        [HttpPost]
        public async Task<IActionResult> GetCNLByLocation(string LocationValue)
        {
            var apiUrl = "https://localhost:7121/API/Users/GetCNLByLocation?LocationValue=" + LocationValue;

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string token = HttpContext.Session.GetString("AccessToken");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        var CrystalReportPaths = await response.Content.ReadFromJsonAsync<List<CrystalReportPath>>();
                        if (CrystalReportPaths != null && CrystalReportPaths.Any())
                        {
                            var firstCrystal = CrystalReportPaths.First();
                            return Json(new { Cnl = firstCrystal.CNL });
                        }
                    }
                    else
                    {
                    }
                }
                catch (Exception ex)
                {
                }

                return Json(new { Cnl = string.Empty});
            }
        }
        private async Task<List<SelectListItem>> GetPlacement()
        {
            var apiUrl = "https://localhost:7121/API/Users/GetAllPlacement"; // Assuming the API endpoint returns data related to AddPlacement

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string token = HttpContext.Session.GetString("AccessToken");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        var roles = await response.Content.ReadFromJsonAsync<List<AddPlacement>>();
                        if (roles != null)
                        {
                            // Now 'roles' is a List of AddPlacement objects.
                            // You can use it as needed.
                            // For example, if you want to convert it to a List of SelectListItem:

                            var selectList = roles
                                .Select(ap => new SelectListItem { Value = ap.PlaceCode, Text = ap.PlaceCode }) // Assuming 'RoleName' property in AddPlacement is used for Text
                                .ToList();

                            selectList.Insert(0, new SelectListItem { Value = "Please Select", Text = "Please Select" });

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
        private async Task<List<SelectListItem>> GetDesignation()
        {
            var apiUrl = "https://localhost:7121/API/Users/GetAllDesignation";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string token = HttpContext.Session.GetString("AccessToken");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        var designations = await response.Content.ReadFromJsonAsync<List<AddDesignation>>();
                        if (designations != null)
                        {
                            // Now 'designations' is a List of AddDesignation objects.
                            // You can use it as needed.
                            // For example, if you want to convert it to a List of SelectListItem:

                            var selectList = designations
                                .Select(d => new SelectListItem { Value = d.Designation, Text = d.Designation }) // Assuming 'Designation' property in AddDesignation is used for Text
                                .ToList();

                            selectList.Insert(0, new SelectListItem { Value = "Please Select", Text = "Please Select" });

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
        private async Task<List<SelectListItem>> GetSatus()
        {
            var statusList = new List<SelectListItem>
            {
                new SelectListItem { Value = "Yes", Text = "Yes" },
                new SelectListItem { Value = "No", Text = "No" }
            };

            return statusList;
        }
        private async Task<List<SelectListItem>> GetRoles()
        {
            var apiUrl = "https://localhost:7121/API/Users/GetAllRole";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string token = HttpContext.Session.GetString("AccessToken");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        var roles = await response.Content.ReadFromJsonAsync<List<Role>>();
                        if (roles != null)
                        {
                            // Convert Role objects to SelectListItem
                            var selectList = roles.Select(r => new SelectListItem { Value = r.RoleID.ToString(), Text = r.RoleName }).ToList();
                            selectList.Insert(0, new SelectListItem { Value = "Please Select", Text = "Please Select" });
                            return selectList;
                        }
                        //var roles = await response.Content.ReadFromJsonAsync<List<SelectListItem>>();
                        //if (roles != null)
                        //{
                        //    roles.Insert(0, new SelectListItem { Value = "0", Text = "Select Role" });
                        //    return roles;
                        //}
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
        public async Task<IActionResult> usersearchingEvent(string searchTerm)
        {
           
            var apiUrl = "https://localhost:7121/API/Users/GetUsers?prefix=" + searchTerm;

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string token = HttpContext.Session.GetString("AccessToken");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        var users = await response.Content.ReadFromJsonAsync<List<User>>();
                        if (users != null && users.Any())
                        {
                            var userResults = users.Select(user => new { UserID = user.EmpId, UserName = user.Uname, Location = user.Location });
                            return Json(userResults);
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
      
        public async Task<IActionResult> ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePassword model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.NewPassword != model.ConfirmNewPassword)
            {
                TempData["ErrorMessage"] = "The new password and confirmation password do not match.";
                return View(model);
            }

            if (string.IsNullOrEmpty(model.NewPassword))
            {
                TempData["ErrorMessage"] = "New password cannot be empty.";
                return View(model);
            }

            int? empId = HttpContext.Session.GetInt32("UserID");

            if (!empId.HasValue)
            {
                TempData["ErrorMessage"] = "Wrong CurrentPassword";
            }

            try
            {
                var user = new ChangePassword
                {
                    EmpId = empId.Value,
                    CurrentPassword = model.CurrentPassword,
                    ConfirmNewPassword = model.ConfirmNewPassword
                };

                var apiUrl = $"https://localhost:7121/API/Users/ChangePassword/{empId}";

                using (HttpClient client = new HttpClient())
                {
                    try
                    {
                        string token = HttpContext.Session.GetString("AccessToken");
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                        string jsonData = JsonConvert.SerializeObject(user);
                        HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                        HttpResponseMessage response = await client.PutAsync(apiUrl, content);

                        if (response.IsSuccessStatusCode)
                        {
                            TempData["SuccessMessage"] = "Sucessfully Update";
                            return RedirectToAction(nameof(ChangePassword));
                        }
                        else
                        {
                            var errorMessage = await response.Content.ReadAsStringAsync();
                            TempData["ErrorMessage"] = errorMessage;
                        }
                    }
                    catch (Exception ex)
                    {
                        TempData["ErrorMessage"] = $"An error occurred: {ex.Message}";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred: {ex.Message}";
            }

            return View(model);
        }
        public async Task<IActionResult> ChangeLocation()
        {
            ViewBag.LocationList = await GetLocation();
            var ChangeLocation = new AdvLab_WebApp.Models.Users.ChangeLocation();
            return View(ChangeLocation);
            //return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeLocation(ChangeLocation model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            int? empId = HttpContext.Session.GetInt32("UserID");

            try
            {
                var user = new ChangeLocation
                {
                    EmpId = empId.Value,
                    Location = model.Location,
                };

                var apiUrl = $"https://localhost:7121/API/Users/ChangeLocation/{empId}";

                using (HttpClient client = new HttpClient())
                {
                    try
                    {
                        string token = HttpContext.Session.GetString("AccessToken");
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                        string jsonData = JsonConvert.SerializeObject(user);
                        HttpContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                        HttpResponseMessage response = await client.PutAsync(apiUrl, content);

                        if (response.IsSuccessStatusCode)
                        {
                            TempData["SuccessMessage"] = "Sucessfully Update";
                            return RedirectToAction(nameof(ChangeLocation));
                        }
                        else
                        {
                            var errorMessage = await response.Content.ReadAsStringAsync();
                            TempData["ErrorMessage"] = errorMessage;
                        }
                    }
                    catch (Exception ex)
                    {
                        TempData["ErrorMessage"] = $"An error occurred: {ex.Message}";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred: {ex.Message}";
            }

            return View(model);
        }
    }
}
