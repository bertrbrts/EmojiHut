using EmojiHut.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace EmojiHut.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEmoji _emoji;

        public HomeController(ILogger<HomeController> logger, IEmoji emoji)
        {
            _logger = logger;
            _emoji = emoji;
        }

        public async Task<IActionResult> IndexAsync()
        {
            List<Emoji>? allEmoji;
            try
            {
                allEmoji = await _emoji.GetAllAsync();
            }
            catch (Exception je)
            {
                _logger.LogError(je, "failed to get data");
                allEmoji = await _emoji.GetFallbackDataAsync();
            }
            return View(allEmoji);
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