using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using VaporInfrastructure.Models;

namespace VaporInfrastructure.Controllers
{
    public class HomeController : Controller
    {
        private readonly VaporContext _context;

        public HomeController(VaporContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string? searchString, int? genreId, int? publisherId, int? year)
        {

            var gamesQuery = _context.Games
                            .Include(g => g.Publisher)
                            .Include(g => g.Genres)
                            .Where(g => g.IsAvailable == true)
                            .AsQueryable();
            if (!string.IsNullOrEmpty(searchString))
            {
                gamesQuery = gamesQuery.Where(g => g.Title.Contains(searchString));
            }

            if (genreId != null)
            {
                gamesQuery = gamesQuery.Where(g => g.Genres.Any(x => x.Id == genreId));
            }

            if (publisherId != null)
            {
                gamesQuery = gamesQuery.Where(g => g.PublisherId == publisherId);
            }

            ViewBag.Genres = _context.Genres.ToList();
            ViewBag.Publishers = _context.Publishers.ToList();

            var years = await _context.Orders
                              .Select(o => o.CreatedDate.Year)
                              .Distinct()
                              .OrderByDescending(y => y)
                              .ToListAsync();

            if (!years.Any()) years.Add(DateTime.Now.Year);
            int selectedYear = year ?? years.First();

            ViewBag.Years = years;
            ViewBag.SelectedYear = selectedYear;

            return View(await gamesQuery.ToListAsync());
        }

        public IActionResult About()
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