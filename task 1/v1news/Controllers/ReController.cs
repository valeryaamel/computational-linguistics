using htmltest;
using Microsoft.AspNetCore.Mvc;

namespace v1news.Controllers
{
    public class ReController : Controller
    {
        private readonly INewsServices _newsServices;

        public ReController(INewsServices newsServices)
        {
            _newsServices = newsServices;
        }

        public async Task<IActionResult> IndexAsync()
        {
            News n = _newsServices.Getone();
            DateTime regularDate = n.Date.HasValue ? n.Date.Value : default(DateTime);
            regularDate.AddHours(2);
            List<News> ns = await Repository.Resrtuct(regularDate);
            foreach (var item in ns)
            {
                _newsServices.AddNews(item);
            }
            ns = _newsServices.GetNews();
            return View(ns);
        }

        public async Task<IActionResult> Refresh()
        {
            News n = _newsServices.Getone();
            DateTime regularDate = n.Date.HasValue ? n.Date.Value : default(DateTime);
            regularDate.AddHours(2);
            List<News> ns = await Repository.Resrtuct(regularDate);
            foreach (var item in ns)
            {
                _newsServices.AddNews(item);
            }
            ns = _newsServices.GetNews();
            return View(ns);
        }
    }
}
