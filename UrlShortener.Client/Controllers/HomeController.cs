using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using UrlShortener.Client.DTOs;
using UrlShortener.Client.ViewModels;

namespace UrlShortener.Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly string _baseUrl = "https://localhost:7234/api/Url";

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

        [HttpGet("{urlId}")]
        public async Task<IActionResult> Index(string urlId, UrlDto urlDto)
        {
            // Get model
            var client = new HttpClient();
            var response = await client.GetAsync($"{_baseUrl}/GetByString?urlId={urlId}");
            
            if (response.IsSuccessStatusCode)
                urlDto = JsonConvert.DeserializeObject<UrlDto>(await response.Content.ReadAsStringAsync());

            if (urlDto is null)
                return NotFound();

            // Save original link
            //Uri redirectLink = urlDto.UrlLong;

            // Increment counter
            urlDto.Counter++;
            string json = JsonConvert.SerializeObject(urlDto);
            var putResponse = await client.PutAsync($"{_baseUrl}/?id={urlDto.Id}",
                new StringContent(json, Encoding.UTF8, "application/json"));

            if (!putResponse.IsSuccessStatusCode)
            {
                return BadRequest();
            }

            // Redirect to original link
            return Redirect(urlDto.UrlLong);
        }
    }
}
