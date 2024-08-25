using System.Diagnostics;
using FinalProjectMVC.Models;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPut]
        public async Task<IActionResult> AddToMission(int id)
        {

            await _httpClient.PutAsJsonAsync("http://localhost:5020/Missions",id);
            return RedirectToAction(nameof(Missions));
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
