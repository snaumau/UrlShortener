using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UrlShortener.Client.Services;
using UrlShortener.Models.Entities;
using UrlShortener.Models.Interfaces;

namespace UrlShortener.Client.Controllers
{
    public class UrlController : Controller
    {
        private readonly IUrlRepository _urlRepository;

        public UrlController(IUrlRepository urlRepository)
        {
            _urlRepository = urlRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_urlRepository.GetAllUrl
                .OrderByDescending(u => u.Id));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Url url)
        {
            if (url.UrlLong is null)
            {
                return View(url);
            }

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

            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var url = _urlRepository.GetUrl(id);
            if (url == null)
            {
                return NotFound();
            }
            return View(url);
        }

        [HttpPost]
        public IActionResult Edit(int id, Url url)
        {
            if (id != url.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View(url);
            }

            try
            {
                _urlRepository.PutUrl(url);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                ModelState.AddModelError(string.Empty,
                    $@"Unable to save the record. {ex.Message}");
                return View(url);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $@"Unable to save the record. {ex.Message}");
                return View(url);
            }

            return RedirectToAction(nameof(Index));
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
            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }

        [ActionName("Redirect")]
        public ActionResult RedirectToUrl(int? id)
        {
            // Get model
            var url = _urlRepository.GetUrl(id);
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            // Get default url
            string returnUrl = url.UrlLong;

            // Increment counter
            url.Counter++;
            try
            {
                _urlRepository.PutUrl(url);
            }
            catch (Exception)
            {

                throw;
            }

            // Redirect to default url
            return Redirect(returnUrl);
        }
    }
}
