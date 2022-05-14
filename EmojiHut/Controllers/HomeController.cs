using EmojiHut.Models;
using EmojiHut.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EmojiHut.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHomeViewModel _homeViewModel;

        public HomeController(ILogger<HomeController> logger, IHomeViewModel homeViewModel)
        {
            _logger = logger;
            _homeViewModel = homeViewModel;
        }

        public IActionResult IndexAsync()
        {
            return View(_homeViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}