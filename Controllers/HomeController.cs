using System;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Text;
using System.Text.Json;
using FinalProjectMVC.Models;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;

namespace FinalProjectMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _httpClient;
      
        public HomeController(ILogger<HomeController> logger,HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }


        public async Task<IActionResult> Login()
        { 
        return View(new Loggin());
        
        }

        [HttpPost]
        public async Task<IActionResult> Login(Loggin log)
        {
            
            var response = await _httpClient.PostAsJsonAsync("http://localhost:5020/Login", log);

           
            TokenMVC? tokenmvc = await response.Content.ReadFromJsonAsync<TokenMVC>();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenmvc.token);




            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Index()
        {

            GeneralVieo generalvieo = await _httpClient.GetFromJsonAsync<GeneralVieo>("http://localhost:5020/GeneralVieo/GetGeneral");

            return View(generalvieo);
        }

        //הצגת כל המטרות 
        [HttpGet]
        public async Task<IActionResult> Targets()
        {
            
            List<Target> target = await _httpClient.GetFromJsonAsync<List<Target>>("http://localhost:5020/targets");
            return View(target);
        }

        [HttpGet]
        public async Task<IActionResult> Missions()
        {

            List<Missionmanganment> mis = await _httpClient.GetFromJsonAsync<List<Missionmanganment>>("http://localhost:5020/Missions");
            return View(mis);
        }

        [HttpGet]
        public async Task<IActionResult> Agents()
        {

            List<ViewAgent>? mis = await _httpClient.GetFromJsonAsync<List<ViewAgent>>("http://localhost:5020/Agents");
            return View(mis);
        }

        [HttpGet]
        public async Task<IActionResult> GetOneMission(int id)
        {
            Missionmanganment? mis = await _httpClient.GetFromJsonAsync<Missionmanganment>($"http://localhost:5020/Missions/getone/{id}");
                

            
                return View(mis);
            
        }

        public async Task<IActionResult> AddToMission(int id)
        {

            var url = $"http://localhost:5020/Missions/{id}";
            //var content = new StringContent(id.ToString(), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsJsonAsync(url, new { id});

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Missions));
            }
            else
            {
                
                var error = await response.Content.ReadAsStringAsync();
                
                ViewBag.ErrorMessage = error;
                return View("Error"); 
            }
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
