using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UrlShortener.Client.Services;
using UrlShortener.Models.Entities;
using UrlShortener.Models.Interfaces;

namespace UrlShortener.Client.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUrlRepository _urlRepository;

        public AdminController(IUrlRepository urlRepository)
        {
            _urlRepository = urlRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_urlRepository.GetAllUrl);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Url url)
        {
            Url dbEntry = new Url
            {
                UrlLong = url.UrlLong,
                UrlShort = UrlShortenerService.Encode(url.UrlLong),
                CreatedAt = DateTime.Now,
                Counter = 0,
            };
            

            if (dbEntry.UrlShort is null)
            {
                return View(url);
            }

            try
            {
                _urlRepository.PostUrl(dbEntry);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $@"Unable to create record: {ex.Message}");
                return View(url);
            }

            return View(dbEntry);
        }

        [HttpPost]
        public IActionResult Delete(Url url)
        {
            try
            {
                _urlRepository.DeleteUrl(url);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                ModelState.AddModelError(string.Empty,
                    $@"Unable to delete record. Another user updated the record. {ex.Message}");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $@"Unable to create record: {ex.Message}");
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
