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
    public class OrdersController : Controller
    {
        private readonly VaporContext _context;

        public OrdersController(VaporContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var vaporContext = _context.Orders.Include(o => o.Status).Include(o => o.User);
            return View(await vaporContext.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Status)
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToCart(int? gameId)
        {
            int CurrentUserId = 1; // placeholder

            var game = await _context.Games.FirstOrDefaultAsync(g => g.Id == gameId);
            if (game == null) return NotFound();
            var order = await _context.Orders.Include(o => o.OrderItems).FirstOrDefaultAsync(o => o.UserId == CurrentUserId && o.StatusId == 1);
            if (order == null)
            {

                order = new Order
                {
                    UserId = CurrentUserId,
                    StatusId = 1,
                    CreatedDate = DateOnly.FromDateTime(DateTime.Now),
                };

                _context.Orders.Add(order);
                await _context.SaveChangesAsync();
            }

            var existingItems = order.OrderItems ?? new List<OrderItem>();

            if (!(existingItems.Any(o => o.GameId == gameId)))
            {
                var orderItem = new OrderItem
                {
                    GameId = game.Id,
                    OrderId = order.Id,
                    Price = game.Price,
                };
                _context.OrderItems.Add(orderItem);
                await _context.SaveChangesAsync();
            }


            return RedirectToAction("Details", "Games", new { id = gameId });
        }

        // GET: Orders/Create
        //public IActionResult Create()
        //{
        //    ViewData["StatusId"] = new SelectList(_context.Statuses, "Id", "Name");
        //    ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email");
        //    return View();
        //}

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("UserId,StatusId,CreatedDate,Id")] Order order)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(order);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["StatusId"] = new SelectList(_context.Statuses, "Id", "Name", order.StatusId);
        //    ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", order.UserId);
        //    return View(order);
        //}

        // GET: Orders/Edit/5

        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var order = await _context.Orders.FindAsync(id);
        //    if (order == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["StatusId"] = new SelectList(_context.Statuses, "Id", "Name", order.StatusId);
        //    ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", order.UserId);
        //    return View(order);
        //}

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("UserId,StatusId,CreatedDate,Id")] Order order)
        //{
        //    if (id != order.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(order);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!OrderExists(order.Id))
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
        //    ViewData["StatusId"] = new SelectList(_context.Statuses, "Id", "Name", order.StatusId);
        //    ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", order.UserId);
        //    return View(order);
        //}

        // GET: Orders/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var order = await _context.Orders
        //        .Include(o => o.Status)
        //        .Include(o => o.User)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (order == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(order);
        //}

        // POST: Orders/Delete/5

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var order = await _context.Orders.FindAsync(id);
        //    if (order != null)
        //    {
        //        _context.Orders.Remove(order);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
