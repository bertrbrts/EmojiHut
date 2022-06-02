using EmojiHut.Extensions;
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
        private readonly IEmoji _emoji;

        public HomeController(ILogger<HomeController> logger, IHomeViewModel homeViewModel, IEmoji emoji)
        {
            _logger = logger;
            _homeViewModel = homeViewModel;
            _emoji = emoji;
        }

        public IActionResult IndexAsync()
        {
            return View(_homeViewModel);
        }

        public async Task<IActionResult> CategoryAsync(string group)
        {
            List<Emoji>? allEmoji = await _emoji.GetAllAsync();
            _homeViewModel.Emoji = !string.IsNullOrEmpty(group) ? allEmoji.Where(e => e.Group?.Split('-')[0].ToTitleCase() == group).ToList() : allEmoji;
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => 
            View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}