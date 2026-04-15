using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
    [Authorize]
    public class ReviewsController : Controller
    {
        private readonly VaporContext _context;
        private readonly UserManager<User> _userManager;

        public ReviewsController(VaporContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Reviews
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Index()
        {
            var vaporContext = _context.Reviews.Include(r => r.Game).Include(r => r.User);
            return View(await vaporContext.ToListAsync());
        }

        // GET: Reviews/Details/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.Reviews
                               .Include(r => r.Game)
                               .Include(r => r.User)
                               .FirstOrDefaultAsync(m => m.Id == id);
            if (review == null)
            {
                return NotFound();
            }

            return View(review);
        }

        // GET: Reviews/Create
        public async Task<IActionResult> Create(int? gameId)
        {
            if (gameId != null)
            {
                var review = await _context.Games.FirstOrDefaultAsync(r => r.Id == gameId);
                ViewBag.GameId = review?.Id;
                ViewBag.GameTitle = review?.Title;
            }
            else
            {
                return NotFound();
            }

            var userId = int.Parse(_userManager.GetUserId(User)!);
            ViewBag.UserId = userId;

            return View();
        }

        // POST: Reviews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GameId,Content,Rating,Id")] Review review)
        {
            bool gameExists = await _context.Games.AnyAsync(x => x.Id == review.GameId);
            if (!gameExists)
            {
                return NotFound();
            }

            ModelState.Remove("Game");
            ModelState.Remove("User");

            var userId = int.Parse(_userManager.GetUserId(User)!);
            review.UserId = userId;
            review.CreatedDate = DateTime.UtcNow;

            bool reviewExists = _context.Reviews.Any(r => r.GameId == review.GameId && r.UserId == review.UserId);
            if (reviewExists)
            {
                ModelState.AddModelError("", "Ви вже залишили відгук до цієї гри.");
            }

            if (ModelState.IsValid)
            {
                _context.Add(review);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Games", new { id = review.GameId });
            }

            return View(review);
        }

        // GET: Reviews/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.Reviews
                   .Include(r => r.Game)
                   .Include(r => r.User)
                   .FirstOrDefaultAsync(m => m.Id == id);
            if (review == null)
            {
                return NotFound();
            }

            var userId = int.Parse(_userManager.GetUserId(User)!);
            if (review.UserId != userId)
            {
                return Forbid();
            }

            return View(review);
        }

        // POST: Reviews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GameId,Content,Rating,Id")] Review review)
        {
            bool gameExists = await _context.Games.AnyAsync(x => x.Id == review.GameId);
            if (!gameExists)
            {
                return NotFound();
            }

            var existingReview = await _context.Reviews.FindAsync(review.Id);
            if (existingReview == null)
            {
                return NotFound();
            }

            ModelState.Remove("Game");
            ModelState.Remove("User");

            var userId = int.Parse(_userManager.GetUserId(User)!);
            if (existingReview!.UserId != userId)
            {
                return Forbid();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    existingReview.Content = review.Content;
                    existingReview.Rating = review.Rating;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReviewExists(review.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "Games", new { id = review.GameId });
            }
            return View(review);
        }

        // GET: Reviews/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var review = await _context.Reviews
                               .Include(r => r.Game)
                               .Include(r => r.User)
                               .FirstOrDefaultAsync(m => m.Id == id);
            if (review == null)
            {
                return NotFound();
            }

            var userId = int.Parse(_userManager.GetUserId(User)!);
            if (review.UserId != userId && !User.IsInRole("admin"))
            {
                return Forbid();
            }

            return View(review);
        }

        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }

            var userId = int.Parse(_userManager.GetUserId(User)!);
            if (review.UserId != userId && !User.IsInRole("admin"))
            {
                return Forbid();
            }

            if (review == null)
            {
                return NotFound();
            }

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Games", new { id = review.GameId });
        }

        private bool ReviewExists(int id)
        {
            return _context.Reviews.Any(e => e.Id == id);
        }
    }
}