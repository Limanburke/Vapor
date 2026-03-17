using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VaporDomain.Model;
using VaporInfrastructure;

namespace VaporInfrastructure.Controllers
{
    public class GamesController : Controller
    {
        private readonly VaporContext _context;

        public GamesController(VaporContext context)
        {
            _context = context;
        }

        // GET: Games
        public async Task<IActionResult> Index(int? id, string? name)
        {
            if (id == null)
            {
                var allGames = _context.Games
                               .Include(g => g.Publisher)
                               .Include(g => g.Genres)
                               .Include(g => g.Reviews);

                return View(await allGames.ToListAsync());
            }

            ViewBag.PublisherId = id;
            ViewBag.PublisherName = name;

            var gamesByPublisher = _context.Games
                                   .Where(g => g.PublisherId == id)
                                   .Include(g => g.Publisher)
                                   .Include(g => g.Genres)
                                   .Include(g => g.Reviews);

            return View(await gamesByPublisher.ToListAsync());
        }

        // GET: Games/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            int currentUserId = 1; //placeholder

            var game = await _context.Games
                .Include(g => g.Publisher)
                .Include(g => g.Genres)
                .Include(g => g.PriceHistories)
                .Include(g => g.OrderItems)
                .Include(g => g.Reviews)
                    .ThenInclude(r => r.User)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (game == null)
            {
                return NotFound();
            }

            ViewBag.hasOrderItems = await _context.OrderItems.AnyAsync(
                                           o => o.GameId == id &&
                                           o.Order.UserId == currentUserId &&
                                           o.Order.StatusId != 3);

            return View(game);
        }

        // GET: Games/Create
        public IActionResult Create(int? publisherId)
        {
            if (publisherId != null)
            {
                var publisher = _context.Publishers.FirstOrDefault(p => p.Id == publisherId);

                ViewBag.PublisherName = publisher?.Name;
                ViewBag.PublisherId = publisherId;
                ViewBag.IsContextual = true;
            }
            else
            {
                ViewData["PublisherId"] = new SelectList(_context.Publishers, "Id", "Name");
                ViewBag.IsContextual = false;
            }

            ViewBag.Genres = new SelectList(_context.Genres, "Id", "Name");
            return View();
        }

        // POST: Games/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PublisherId,Title,IsAvailable,Description,Price,ReleasedDate,Id")] Game game, int[] selectedGenres)
        {
            var publisher = _context.Publishers.FirstOrDefault(p => p.Id == game.PublisherId);

            if (publisher != null)
            {
                game.Publisher = publisher;
            }
                
            if (selectedGenres != null && selectedGenres.Length > 0)
            {
                var genres = _context.Genres.Where(g => selectedGenres.Contains(g.Id)).ToList();
                game.Genres = genres;
            }

            ModelState.Clear();
            TryValidateModel(game);

            if (_context.Games.Any(g => g.Title.ToLower() == game.Title.ToLower()))
            {
                ModelState.AddModelError("Title", "Гра з такою назвою вже існує!");
            }

            if (ModelState.IsValid)
            {
                _context.Add(game);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Games");
            }

            ViewData["PublisherId"] = new SelectList(_context.Publishers, "Id", "Name", game.PublisherId);
            ViewBag.Genres = new SelectList(_context.Genres, "Id", "Name");

            return View(game);
        }

        // GET: Games/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Games
                            .Include(g => g.Genres)
                            .FirstOrDefaultAsync(m => m.Id == id);

            if (game == null)
            {
                return NotFound();
            }

            var selectedGenresIds = game.Genres.Select(g => g.Id).ToArray();

            ViewData["PublisherId"] = new SelectList(_context.Publishers, "Id", "Name", game.PublisherId);
            ViewBag.Genres = new MultiSelectList(_context.Genres, "Id", "Name", selectedGenresIds);

            return View(game);
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PublisherId,Title,IsAvailable,Description,Price,ReleasedDate,Id")] Game game, int[] Genres)
        {
            if (id != game.Id)
            {
                return NotFound();
            }

            var gameToUpdate = await _context.Games
                                     .Include(g => g.Genres)
                                     .FirstOrDefaultAsync(g => g.Id == id);

            if (gameToUpdate == null)
            {
                return NotFound();
            }

            var publisher = _context.Publishers.FirstOrDefault(p => p.Id == game.PublisherId);

            if (publisher != null) 
            { 
                game.Publisher = publisher;
            }

            ModelState.Clear();
            TryValidateModel(game);

            if (_context.Games.Any(g => g.Title.ToLower() == game.Title.ToLower() && g.Id != game.Id))
            {
                ModelState.AddModelError("Title", "Інша гра з такою назвою вже існує!");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if(game.Price != gameToUpdate.Price)
                    {
                        var priceHistory = new PriceHistory
                        {
                            GameId = game.Id,
                            OldPrice = gameToUpdate.Price,
                            NewPrice = game.Price,
                            ChangedData = DateOnly.FromDateTime(DateTime.Now)
                        };
                        _context.PriceHistories.Add(priceHistory);
                    }

                    gameToUpdate.Title = game.Title;
                    gameToUpdate.Price = game.Price;
                    gameToUpdate.Description = game.Description;
                    gameToUpdate.IsAvailable = game.IsAvailable;
                    gameToUpdate.ReleasedDate = game.ReleasedDate;
                    gameToUpdate.PublisherId = game.PublisherId;

                    gameToUpdate.Genres.Clear();
                    if (Genres != null && Genres.Length > 0)
                    {
                        var newGenres = _context.Genres.Where(g => Genres.Contains(g.Id)).ToList();
                        foreach (var genre in newGenres)
                        {
                            gameToUpdate.Genres.Add(genre);
                        }
                    }

                    //_context.Update(game);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameExists(game.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["PublisherId"] = new SelectList(_context.Publishers, "Id", "Name", game.PublisherId);
            ViewBag.Genres = new MultiSelectList(_context.Genres, "Id", "Name", Genres);

            return View(game);
        }

        // GET: Games/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Games
                             .Include(g => g.Publisher)
                             .Include(g => g.Genres)
                             .FirstOrDefaultAsync(m => m.Id == id);

            if (game == null)
            {
                return NotFound();
            }

            ViewBag.GameTitle = game?.Title;

            return View(game);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var game = await _context.Games
                             .Include(g => g.Genres)
                             .FirstOrDefaultAsync(m => m.Id == id);

            if (game != null)
            {

                try
                {
                    game.Genres.Clear();
                    _context.Games.Remove(game);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException)
                {
                    var gameWithRelations = await _context.Games
                                                  .Include(g => g.Publisher)
                                                  .Include(g => g.Genres)
                                                  .FirstOrDefaultAsync(m => m.Id == id);

                    ViewBag.ErrorMessage = "Неможливо видалити цю гру, оскільки до неї прив'язані коментарі.";

                    ViewBag.GameTitle = gameWithRelations?.Title;

                    return View("Delete", gameWithRelations);
                }
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GameExists(int id)
        {
            return _context.Games.Any(e => e.Id == id);
        }
    }
}
