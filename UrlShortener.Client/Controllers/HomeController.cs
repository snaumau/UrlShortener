using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UrlShortener.Client.DTOs;
using UrlShortener.Client.ViewModels;

namespace UrlShortener.Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly string _baseUrl = "https://localhost:7234/api/url/";

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var homeViewModel = new HomeViewModel();
            var client = new HttpClient();
            var response = await client.GetAsync(_baseUrl);
            if (response.IsSuccessStatusCode)
            {
                var urls = JsonConvert.DeserializeObject<List<UrlDto>>(await response.Content.ReadAsStringAsync());
                homeViewModel.Urls = urls.ToList()
                    .OrderByDescending(u => u.CreatedAt);
                return View(homeViewModel);
            }

            return View();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Index(int id)
        {
            // Get model
            var client = new HttpClient();
            var response = await client.GetAsync($"{_baseUrl}/{id}");
            if (response.IsSuccessStatusCode)
            {
                var url = JsonConvert.DeserializeObject<UrlDto>(await response.Content.ReadAsStringAsync());
                var responseUri = $"{client.BaseAddress}";
            }

            return Redirect("");
        }
    }
}
