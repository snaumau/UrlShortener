using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using UrlShortener.Client.DTOs;
using UrlShortener.Client.Services;

namespace UrlShortener.Client.Controllers
{
    public class UrlController : Controller
    {
        private readonly string _baseUrl = "https://localhost:7234/api/url";

        [HttpGet]
        public async Task<IActionResult> GetUrls()
        {
            var client = new HttpClient();
            var response = await client.GetAsync(_baseUrl);
            if (response.IsSuccessStatusCode)
            {
                var urls = JsonConvert.DeserializeObject<List<UrlDto>>(await response.Content.ReadAsStringAsync());

                return View(urls);
            }
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUrlDto createUrlDto)
        {
            createUrlDto.UrlShort = UrlShortenerService.Encode(createUrlDto.UrlLong);

            using (var client = new HttpClient())
            {
                string json = JsonConvert.SerializeObject(createUrlDto);
                var response = await client.PostAsync(
                    _baseUrl,
                    new StringContent(json, Encoding.UTF8, "application/json"));
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToRoute(new { controller = "Home", action = "Index" });
                }
            }

            return View(createUrlDto);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var url = await GetUrlRecord(id.Value);

            if (url == null)
            {
                return NotFound();
            }
            return View(url);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditUrlDto editUrlDto)
        {
            if (id != editUrlDto.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(editUrlDto);
            }

            var client = new HttpClient();
            string json = JsonConvert.SerializeObject(editUrlDto);
            var response = await client.PutAsync($"{_baseUrl}/?id={editUrlDto.Id}",
                new StringContent(json, Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                return RedirectToRoute(new { controller = "Url", action = "GetUrls" });
            }

            return View(editUrlDto);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(
                HttpMethod.Delete,
                $"{_baseUrl}?id={id}");
            await client.SendAsync(request);

            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }

        private async Task<UrlDto?> GetUrlRecord(int id)
        {
            var client = new HttpClient();
            var response = await client.GetAsync($"{_baseUrl}/{id}");
            if (response.IsSuccessStatusCode)
            {
                var url = JsonConvert.DeserializeObject<UrlDto>(await response.Content.ReadAsStringAsync());
                return url;
            }

            return null;
        }
    }
}
