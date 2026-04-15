using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VaporDomain.Model;
using VaporInfrastructure;

namespace VaporInfrastructure.Controllers
{
    [Authorize(Roles = "admin")]
    public class PriceHistoriesController : Controller
    {
        private readonly VaporContext _context;

        public PriceHistoriesController(VaporContext context)
        {
            _context = context;
        }

        // GET: PriceHistories
        public async Task<IActionResult> Index()
        {
            var vaporContext = _context.PriceHistories.Include(p => p.Game);
            return View(await vaporContext.ToListAsync());
        }
        private bool PriceHistoryExists(int id)
        {
            return _context.PriceHistories.Any(e => e.Id == id);
        }
    }
}
