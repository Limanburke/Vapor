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

        // GET: PriceHistories/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var priceHistory = await _context.PriceHistories
        //                             .Include(p => p.Game)
        //                             .FirstOrDefaultAsync(m => m.Id == id);

        //    if (priceHistory == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(priceHistory);
        //}

        // GET: PriceHistories/Create
        //public IActionResult Create()
        //{
        //    ViewData["GameId"] = new SelectList(_context.Games, "Id", "Title");
        //    return View();
        //}

        // POST: PriceHistories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("GameId,OldPrice,NewPrice,ChangedData,Id")] PriceHistory priceHistory)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(priceHistory);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }

        //    ViewData["GameId"] = new SelectList(_context.Games, "Id", "Title", priceHistory.GameId);

        //    return View(priceHistory);
        //}

        // GET: PriceHistories/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var priceHistory = await _context.PriceHistories.FindAsync(id);

        //    if (priceHistory == null)
        //    {
        //        return NotFound();
        //    }

        //    ViewData["GameId"] = new SelectList(_context.Games, "Id", "Title", priceHistory.GameId);

        //    return View(priceHistory);
        //}

        // POST: PriceHistories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("GameId,OldPrice,NewPrice,ChangedData,Id")] PriceHistory priceHistory)
        //{
        //    if (id != priceHistory.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(priceHistory);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!PriceHistoryExists(priceHistory.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }

        //    ViewData["GameId"] = new SelectList(_context.Games, "Id", "Title", priceHistory.GameId);

        //    return View(priceHistory);
        //}

        // GET: PriceHistories/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var priceHistory = await _context.PriceHistories
        //                             .Include(p => p.Game)
        //                             .FirstOrDefaultAsync(m => m.Id == id);

        //    if (priceHistory == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(priceHistory);
        //}

        // POST: PriceHistories/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var priceHistory = await _context.PriceHistories.FindAsync(id);

        //    if (priceHistory != null)
        //    {
        //        _context.PriceHistories.Remove(priceHistory);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool PriceHistoryExists(int id)
        {
            return _context.PriceHistories.Any(e => e.Id == id);
        }
    }
}
