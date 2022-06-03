using EmojiHut.Extensions;
using EmojiHut.Models;
using EmojiHut.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EmojiHut_Net48.Controllers
{
    public class HomeController : Controller
    {
        private readonly HomeViewModel _homeViewModel;
        private Emoji _emoji;

        public HomeController()        
        {
            _emoji = new Emoji();
            _homeViewModel = new HomeViewModel(_emoji);
        }

        public ActionResult Index()        
        {
            HomeViewModel viewModel = TempData["hmvw"] as HomeViewModel;
            return View(viewModel ?? _homeViewModel);
        }

        [HttpGet]
        public async Task<ActionResult> Category(string group)
        {
            List<Emoji> allEmoji = await _emoji.GetAllAsync();
            _homeViewModel.Emoji = !string.IsNullOrEmpty(group) ? allEmoji.Where(e => e.Group?.Split('-')[0].ToTitleCase() == group).ToList() : allEmoji;

            TempData["hmvw"] = _homeViewModel;
            return RedirectToAction("Index");
        }
    }
}