using htmltest;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using v1news.Models;

namespace v1news.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly INewsServices _newsServices;

        public HomeController(ILogger<HomeController> logger,
            INewsServices newsServices)
        {
            _newsServices = newsServices;
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<News> news = _newsServices.GetNews();
            return View(news);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> Refresh()
        {
            News n = _newsServices.Getone();
            DateTime regularDate = n.Date.HasValue ? n.Date.Value : default(DateTime);
            List<News> ns = await Repository.Resrtuct(regularDate);
            foreach (var item in ns)
            {
                _newsServices.AddNews(item);
            }
            ns = _newsServices.GetNews();
            return View("index", ns);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}