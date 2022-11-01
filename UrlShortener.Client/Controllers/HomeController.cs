using Microsoft.AspNetCore.Mvc;
using UrlShortener.Client.ViewModels;
using UrlShortener.Models.Interfaces;

namespace UrlShortener.Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUrlRepository _urlRepository;

        public HomeController(IUrlRepository urlRepository)
        {
            _urlRepository = urlRepository;
        }

        public IActionResult Index()
        {
            var homeViewModel = new HomeViewModel
            {
                Urls = _urlRepository.GetAllUrl
            };

            return View(homeViewModel);
        }
    }
}
