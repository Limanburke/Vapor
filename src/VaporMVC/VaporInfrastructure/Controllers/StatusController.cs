using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VaporDomain.Model;
using VaporInfrastructure;

namespace VaporInfrastructure.Controllers
{
    public class StatusController : Controller
    {
        private readonly VaporContext _context;

        public StatusController(VaporContext context)
        {
            _context = context;
        }

        // GET: Status
        public async Task<IActionResult> Index()
        {
            return View(await _context.Statuses.ToListAsync());
        }

        // GET: Status/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var status = await _context.Statuses.FirstOrDefaultAsync(m => m.Id == id);

            if (status == null)
            {
                return NotFound();
            }

            return View(status);
        }
    }
}
